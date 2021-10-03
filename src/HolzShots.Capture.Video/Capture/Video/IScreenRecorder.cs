using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace HolzShots.Capture.Video
{
    public interface IScreenRecorder : IDisposable
    {
        Task<ScreenRecording> Invoke(Rectangle rectangleOnScreenToCapture, string targetFile, HSSettings settingsContext, CancellationToken cancellationToken);
    }

    public record ScreenRecording(
        DateTime StartTime,
        DateTime EndTime,
        Rectangle Bounds,
        bool CursorCaptured,
        int FramesPerSecond,
        VideoCaptureFormat Format,
        string FilePath
    )
    {
        public TimeSpan Duration => EndTime - StartTime;
    }
}
