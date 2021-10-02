using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Task<ScreenRecordingResult> Invoke(Rectangle rectangleOnScreenToCapture, string targetFile, CancellationToken ct, HSSettings settingsContext);
    }

    public class WindowsScreenRecorder : IScreenRecorder
    {
        public async Task<ScreenRecordingResult> Invoke(Rectangle rectangleOnScreenToCapture, string targetFile, CancellationToken ct, HSSettings settingsContext)
        {
            // TODO: Do this before invocation of the area selector
            // var path = await FFmpegManagerUi.EnsureAvailableFFmpeg(settingsContext);
            // GlobalFFOptions.Configure(options => options.BinaryFolder = FFmpegManager.FFmpegAppDataPath);

            /*
            GlobalFFOptions.Configure(new FFOptions
            {
                BinaryFolder = FFmpegManager.FFmpegAppDataPath,
                // TemporaryFilesFolder = System.IO.Path.GetTempPath(),
            });
            */

            var formats = FFMpeg.GetContainerFormats();

            // var format = formats.FirstOrDefault(e => e.Name == "dshow");
            var format = formats.First(e => e.Name == "gdigrab");

            var ffmpegInstance = FFMpegArguments.FromFileInput("desktop", false, options => options
                                .ForceFormat(format)
                                .WithFramerate(30) // TODO: Make this configurable
                                .WithArgument(new OffsetArgument(rectangleOnScreenToCapture.X, 'x'))
                                .WithArgument(new OffsetArgument(rectangleOnScreenToCapture.Y, 'y'))
                                .WithArgument(new VideoSizeArgument(rectangleOnScreenToCapture.Width, rectangleOnScreenToCapture.Height))
                                .WithArgument(new ShowRegionArgument(true))
                            ).OutputToFile(targetFile);

            ffmpegInstance.CancellableThrough(ct);
            // ffmpegInstance.CancellableThrough(out var lol);

            await ffmpegInstance.ProcessAsynchronously();

            return new ScreenRecordingResult();
        }

        public void Dispose() => throw new NotImplementedException();
    }

    public record ScreenRecordingResult();

    /// <summary> Actually it's some kind of a factory, but I don't like that word. </summary>
    public class ScreenRecorderSelector
    {
        public static IScreenRecorder CreateScreenRecorderForCurrentPlatform()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.Win32NT:
                case PlatformID.WinCE:
                    return new WindowsScreenRecorder();
                case PlatformID.Unix:
                case PlatformID.MacOSX:
                case PlatformID.Xbox:
                default:
                    Debug.Fail($"Unhandled platform: {Environment.OSVersion.Platform}");
                    throw new NotSupportedException();
            }
        }
    }
}
