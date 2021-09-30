using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolzShots.Capture.Video
{
    public interface IScreenRecorder : IDisposable
    {
        Task<ScreenRecordingResult> Invoke(Rectangle rectangleOnScreenToCapture, string targetFile, HSSettings settingsContext);
    }

    public class WindowsScreenRecorder : IScreenRecorder
    {
        public Task<ScreenRecordingResult> Invoke(Rectangle rectangleOnScreenToCapture, string targetFile, HSSettings settingsContext)
        {
            throw new NotImplementedException();
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
