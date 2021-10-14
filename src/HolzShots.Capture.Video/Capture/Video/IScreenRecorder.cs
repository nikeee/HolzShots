using System.Drawing;
using HolzShots.IO.Naming;

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
        string FilePath,
        MemSize FileSize
    )
    {
        public TimeSpan Duration => EndTime - StartTime;
        public FileMetadata GetMetadata() => new(StartTime, FileSize, Bounds.Size);
    }
}
