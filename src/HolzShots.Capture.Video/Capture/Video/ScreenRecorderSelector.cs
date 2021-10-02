using System;
using System.Diagnostics;

namespace HolzShots.Capture.Video
{
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
