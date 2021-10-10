using System;
using System.Diagnostics;

namespace HolzShots.Capture.Video
{
    /// <summary> Actually it's some kind of a factory, but I don't like that word. </summary>
    public class ScreenRecorderSelector
    {
        public static IScreenRecorder CreateScreenRecorderForCurrentPlatform(string ffmpegPath) => Environment.OSVersion.Platform switch
        {
            PlatformID.Win32S or
            PlatformID.Win32Windows or
            PlatformID.Win32NT or
            PlatformID.WinCE => new WindowsFFmpegScreenRecorder(ffmpegPath),

            PlatformID.Unix or
            PlatformID.MacOSX or
            PlatformID.Xbox or
            _ => throw new NotSupportedException(),
        };
    }
}
