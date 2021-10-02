using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FFMpegCore;
using HolzShots.Capture.Video.FFmpeg;

namespace HolzShots.Capture.Video
{
    public interface IScreenRecorder : IDisposable
    {
        Task<ScreenRecordingResult> Invoke(Rectangle rectangleOnScreenToCapture, string targetFile, CancellationToken cancellationToken, HSSettings settingsContext);
    }

    /// <summary>
    /// This recorder assumes that ffmpeg is in the current PATH and the version that should be used gets the highest priority.
    /// </summary>
    public class WindowsScreenRecorder : IScreenRecorder
    {
        public async Task<ScreenRecordingResult> Invoke(Rectangle rectangleOnScreenToCapture, string targetFile, CancellationToken cancellationToken, HSSettings settingsContext)
        {
            var formats = FFMpeg.GetContainerFormats();

            // var format = formats.FirstOrDefault(e => e.Name == "dshow");
            var format = formats.First(e => e.Name == "gdigrab");

            var ffmpegInstance = FFMpegArguments.FromFileInput(
                "desktop",
                false,
                options => options
                    .ForceFormat(format)
                    .WithFramerate(30)
                    .WithArgument(new OffsetArgument(rectangleOnScreenToCapture.X, 'x'))
                    .WithArgument(new OffsetArgument(rectangleOnScreenToCapture.Y, 'y'))
                    .WithArgument(new VideoSizeArgument(rectangleOnScreenToCapture.Width, rectangleOnScreenToCapture.Height))
                    .WithArgument(new ShowRegionArgument(true))

                    // Turning off cursor capturing is not supported on gdigrab
                    .WithArgument(new DrawMouseArgument(false)) // settingsContext.CaptureCursor
                )
                .OutputToFile(targetFile)
                .CancellableThrough(cancellationToken);

            await ffmpegInstance.ProcessAsynchronously();

            return new ScreenRecordingResult();
        }

        public void Dispose() => throw new NotImplementedException();
    }

    public record ScreenRecordingResult();
}
