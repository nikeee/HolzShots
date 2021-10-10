using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace HolzShots.Capture.Video.FFmpeg
{
    internal class FFmpegWrapper : IDisposable
    {
#if DEBUG
        private const bool ShowFFmpegWindow = true;
#else
        private const bool ShowFFmpegWindow = false;
#endif

        private readonly string _executablePath;
        private readonly Process _process;
        public FFmpegWrapper(string executablePart)
        {
            _executablePath = executablePart ?? throw new ArgumentNullException(nameof(executablePart));
            _process = new Process();
        }

        public async Task<bool> Start(IFFmpegArguments arguments, CancellationToken cancellationToken)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            var commandLineArgs = arguments.GetArgumentString();
            _process.StartInfo = new ProcessStartInfo(_executablePath, commandLineArgs)
            {
                UseShellExecute = false,
                WorkingDirectory = Environment.CurrentDirectory,
                RedirectStandardError = false,
                RedirectStandardOutput = false,
                RedirectStandardInput = true,
                WindowStyle = ProcessWindowStyle.Minimized,
                CreateNoWindow = !ShowFFmpegWindow,
            };

#if DEBUG
            Debug.WriteLine($"Using ffmpeg: {_executablePath}");
            Debug.WriteLine($"With arguments: {commandLineArgs}");
#endif

            cancellationToken.Register(() => _process.StandardInput.WriteLine("q"));

            _process.Start();

#pragma warning disable CA2016 // Forward the 'CancellationToken' parameter to methods

            // not passing cancellationToken because we want to exit the process by sending "q" to it
            await _process.WaitForExitAsync();

#pragma warning restore CA2016 // Forward the 'CancellationToken' parameter to methods

            return _process.ExitCode == 0;
        }
        public void Dispose() => _process?.Kill(true);
    }

    interface IFFmpegArguments
    {
        string GetArgumentString();
    }

    record FFmpegGdiGrabArguments(
        Rectangle CaptureBounds,
        int FrameRate,
        bool CaptureCursor,
        string? PixelFormat,
        string TargetFile

    ) : IFFmpegArguments
    {
        public string GetArgumentString() => string.Join(" ",
            $"-f gdigrab",
            $"-r {FrameRate}",
            $"-offset_x {CaptureBounds.X}",
            $"-offset_y {CaptureBounds.Y}",
            $"-video_size {CaptureBounds.Width}x{CaptureBounds.Height}",
            $"-show_region 0",
            $"-draw_mouse {(CaptureCursor ? 1 : 0)}",
            $"-i desktop",
            $"-movflags",
            $"+faststart",
            $"-c:v libx264",
            $"{(PixelFormat == null ? string.Empty : "-pix_fmt " + PixelFormat.ToLowerInvariant())}",
            $"\"{TargetFile}\" -y"
        );
    }
}
