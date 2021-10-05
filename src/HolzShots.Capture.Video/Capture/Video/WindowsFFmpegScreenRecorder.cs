using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FFMpegCore;
using HolzShots.Capture.Video.FFmpeg;

namespace HolzShots.Capture.Video
{
    /// <summary>
    /// This recorder assumes that ffmpeg is in the current PATH and the version that should be used gets the highest priority.
    /// </summary>
    public class WindowsFFmpegScreenRecorder : IScreenRecorder
    {
        public async Task<ScreenRecording> Invoke(Rectangle rectangleOnScreenToCapture, string targetFile, HSSettings settingsContext, CancellationToken cancellationToken)
        {
            var formats = FFMpeg.GetContainerFormats();

            // Capture important parameters beforehand, as they may randomly change during recording (this shouldn't happen, but we never know)
            var captureCursor = settingsContext.CaptureCursor;
            var fps = settingsContext.VideoFrameRate;
            var outputFormat = settingsContext.VideoOutputFormat;

            var ffmpegFormat = formats.First(e => e.Name == "gdigrab");

            var ffmpegInstance = FFMpegArguments.FromFileInput(
                "desktop",
                false,
                options => options
                    .ForceFormat(ffmpegFormat)
                    .WithFramerate(fps)
                    .WithArgument(new OffsetArgument(rectangleOnScreenToCapture.X, 'x'))
                    .WithArgument(new OffsetArgument(rectangleOnScreenToCapture.Y, 'y'))
                    .WithArgument(new VideoSizeArgument(rectangleOnScreenToCapture.Width, rectangleOnScreenToCapture.Height))
                    .WithArgument(new ShowRegionArgument(true))
                    .WithArgument(new DrawMouseArgument(captureCursor))
                )
                .OutputToFile(targetFile)
                .CancellableThrough(cancellationToken);

            var startTime = DateTime.Now;

            // Somehow, ProcessAsynchronously does not throw a TaskCanceledException when it was cancelled. It just returns.
            var res = await ffmpegInstance.ProcessAsynchronously(false);
            System.Diagnostics.Debug.WriteLine($"ffmpegInstance res: {res}");

            var endTime = DateTime.Now;

            var fileInfo = new System.IO.FileInfo(targetFile);
            var fileSize = new MemSize(fileInfo.Length);

            return new ScreenRecording(startTime, endTime, rectangleOnScreenToCapture, captureCursor, fps, outputFormat, targetFile, fileSize);
        }

        public void Dispose()
        {
            // TODO: Do we need to dispose something?
        }
    }
}
