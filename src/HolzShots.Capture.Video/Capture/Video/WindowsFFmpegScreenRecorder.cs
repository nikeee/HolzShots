using System.Drawing;
using System.Linq;
using HolzShots.Capture.Video.FFmpeg;

namespace HolzShots.Capture.Video
{
    /// <summary>
    /// This recorder assumes that ffmpeg is in the current PATH and the version that should be used gets the highest priority.
    /// </summary>
    public class WindowsFFmpegScreenRecorder : IScreenRecorder
    {
        private readonly string _ffmpegPath;
        public WindowsFFmpegScreenRecorder(string ffmpegPath) => _ffmpegPath = ffmpegPath ?? throw new ArgumentNullException(nameof(ffmpegPath));

        public async Task<ScreenRecording> Invoke(Rectangle rectangleOnScreenToCapture, string targetFile, HSSettings settingsContext, CancellationToken cancellationToken)
        {
            // Capture important parameters beforehand, as they may randomly change during recording (this shouldn't happen, but we never know)
            var captureCursor = settingsContext.CaptureCursor;
            var fps = settingsContext.VideoFrameRate;
            var outputFormat = settingsContext.VideoOutputFormat;

            var pixelFormat = settingsContext.VideoPixelFormat;
            if (pixelFormat == null)
            {
                // The user did not force a pixel format.

                // We use "yuv420p" pixel format for firefox compatibility
                // However, yuv420p needs both dimensions to be even. So we reduce the image size by 1 pixel on each dimension if the respective dimension is odd to fix the issue.

                // If we would get an invalid rectangle size by applying this size reduction, we take the original size and prohibit the yuv420p format, so it's "just" broken in firefox.
                pixelFormat = "yuv420p";
                if (!IsRectangleShrinkable(rectangleOnScreenToCapture))
                {
                    // yuv420p not pussible, just use ffmpeg's default
                    pixelFormat = null;
                }
                else
                {
                    rectangleOnScreenToCapture = CreateEvenRectangle(rectangleOnScreenToCapture);
                }
            }
            else if (pixelFormat.Trim().ToLowerInvariant() == "yuv420p") // Are there other formats that may require a fixup?
            {
                // The user explicitly forced this pixel format

                if (!IsRectangleShrinkable(rectangleOnScreenToCapture))
                {
                    // In this case the user wants to record something that's too small and not poxxible to "fix" due to its size. We cannot do that in this format.
                    // We basically have two options:
                    //      1. Abort and tell the user to turn that off
                    //      2. Use FFmpeg's default format silently
                    // These options would both be viable. I rolled a dice and took the second option. This may change in the future.
                    pixelFormat = null;
                }
                else
                {
                    // The user forced this format, but we need to shrink it to a valid size
                    rectangleOnScreenToCapture = CreateEvenRectangle(rectangleOnScreenToCapture);
                }
            }

            var args = new FFmpegGdiGrabArguments(rectangleOnScreenToCapture, fps, captureCursor, pixelFormat, targetFile);

            var startTime = DateTime.Now;

            using var recordedRegionIndicator = new UI.RecordingFrame(rectangleOnScreenToCapture); //(new(7, 1, 300, 400));

            recordedRegionIndicator.Show();
            recordedRegionIndicator.StartIndicating(cancellationToken);

            // When the cancellationToken is cancelled, no exception is thrown; instead, it just returns
            var ffmpeg = new FFmpegWrapper(_ffmpegPath);

            var res = await ffmpeg.Start(args, cancellationToken);

            System.Diagnostics.Debug.WriteLine($"ffmpegInstance res: {res}");

            var endTime = DateTime.Now;

            var fileInfo = new System.IO.FileInfo(targetFile);
            var fileSize = new MemSize(fileInfo.Length);

            return new ScreenRecording(startTime, endTime, rectangleOnScreenToCapture, captureCursor, fps, outputFormat, targetFile, fileSize);
        }

        private static Rectangle CreateEvenRectangle(Rectangle source) => source with
        {
            Width = (source.Width & 1) == 0
                            ? source.Width
                            : (source.Width - 1),
            Height = (source.Height & 1) == 0
                            ? source.Height
                            : (source.Height - 1),
        };

        private static bool IsRectangleShrinkable(Rectangle rectangle) => rectangle.Width >= 3 && rectangle.Height >= 3;

        public void Dispose()
        {
            // TODO: Do we need to dispose something?
        }
    }
}
