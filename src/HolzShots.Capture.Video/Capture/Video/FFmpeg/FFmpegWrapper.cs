using System.Diagnostics;
using System.Drawing;

namespace HolzShots.Capture.Video.FFmpeg;

internal class FFmpegWrapper(string executablePart) : IDisposable
{
#if DEBUG
    private const bool ShowFFmpegWindow = true;
#else
    private const bool ShowFFmpegWindow = false;
#endif

    private readonly string _executablePath = executablePart ?? throw new ArgumentNullException(nameof(executablePart));
    private readonly Process _process = new();

    public async Task<bool> Start(IFFmpegArguments arguments, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(arguments);

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

        _process.Start();

        // Register after starting, as StandardInput is not available before (an already cancelled token invokes the callback immediately)
        using var registration = cancellationToken.Register(() => _process.StandardInput.WriteLine("q"));

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
    VideoCaptureFormat OutputFormat,
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
        GetEncoderArguments(),
        $"\"{TargetFile}\" -y"
    );

    private string GetEncoderArguments() => OutputFormat switch
    {
        VideoCaptureFormat.Mp4 => string.Join(" ",
            $"-movflags",
            $"+faststart",
            $"-c:v libx264",
            GetPixelFormatArgument()
        ),
        // The WebM container only supports VP8, VP9 and AV1 (GH-110)
        // Without the realtime settings, libvpx cannot keep up with the capture
        // Without "-b:v 0", libvpx-vp9 targets a very low default bitrate
        VideoCaptureFormat.Webm => string.Join(" ",
            $"-c:v libvpx-vp9",
            $"-deadline realtime",
            $"-cpu-used 8",
            $"-row-mt 1",
            $"-crf 32",
            $"-b:v 0",
            GetPixelFormatArgument()
        ),
        // The GIF muxer only supports the gif codec, which is ffmpeg's default for .gif (GH-110)
        // We generate a palette per frame, because a global palette would require buffering the entire recording in memory until it ends
        VideoCaptureFormat.Gif => "-vf \"split[a][b];[a]palettegen=stats_mode=single[p];[b][p]paletteuse=new=1:diff_mode=rectangle\"",
        _ => throw new ArgumentException("Unhandled VideoCaptureFormat: " + OutputFormat),
    };

    private string GetPixelFormatArgument() => PixelFormat == null ? string.Empty : "-pix_fmt " + PixelFormat.ToLowerInvariant();
}
