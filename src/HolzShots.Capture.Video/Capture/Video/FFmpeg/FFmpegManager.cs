using System.IO;
using System.Text;
using System.Diagnostics;

namespace HolzShots.Capture.Video.FFmpeg
{
    public static class FFmpegManager
    {
        const string FFmpegExecutable = "ffmpeg.exe";

        /// <summary> Path where a downloaded version of FFmpeg will be saved. </summary>
        public static string FFmpegAppDataPath { get; } = Path.Combine(IO.HolzShotsPaths.AppDataDirectory, "ffmpeg");

        private static string? GetFFmpegAbsolutePathFromEnvVar()
        {
            var sb = new StringBuilder(Shlwapi.MAX_PATH);
            sb.Append(FFmpegExecutable);
            var res = Shlwapi.PathFindOnPath(sb, null);

            var p = sb.ToString();
            return res && File.Exists(p)
                    ? p
                    : null;
        }


        /// <param name="allowPathEnvVar">If true, will be preferred if present</param>
        public static string? GetAbsoluteFFmpegPath(bool allowPathEnvVar)
        {
            // We may ship with ffmpeg out of the box, so just return that if that is available
            if (File.Exists(FFmpegExecutable))
                return Path.GetFullPath(FFmpegExecutable);

            if (allowPathEnvVar)
            {
                var ffmpegInPath = GetFFmpegAbsolutePathFromEnvVar();
                if (ffmpegInPath != null)
                {
                    Debug.Assert(File.Exists(ffmpegInPath));
                    return ffmpegInPath;
                }
            }

            var downloadedFFmpeg = Path.Combine(FFmpegAppDataPath, FFmpegExecutable);
            return File.Exists(downloadedFFmpeg) ? downloadedFFmpeg : null;
        }
    }
}
