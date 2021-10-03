using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HolzShots.Capture.Video.FFmpeg
{
    public static class FFmpegManager
    {
        const string FFmpegExecutable = "ffmpeg.exe";

        public static string FFmpegAppDataPath { get; } = Path.Combine(IO.HolzShotsPaths.AppDataDirectory, "ffmpeg");

        // TODO: Ask the user if he wants to use the FFmpeg on the PATH if it is present
        public static string? GetFFmpegAbsolutePathFromEnvVar()
        {
            var sb = new StringBuilder(Shlwapi.MAX_PATH);
            sb.Append(FFmpegExecutable);
            var res = Shlwapi.PathFindOnPath(sb, null);
            return res && File.Exists(sb.ToString()) ? sb.ToString() : null;
        }


        /// <param name="allowPathEnvVar">If true, will be preferred if present</param>
        public static string GetAbsoluteFFmpegPath(bool allowPathEnvVar)
        {
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
            Debug.Assert(File.Exists(downloadedFFmpeg));
            return downloadedFFmpeg;
        }

        public static bool HasDownloadedFFmpeg()
        {
            var downloadedFFmpeg = Path.Combine(FFmpegAppDataPath, FFmpegExecutable);
            return File.Exists(downloadedFFmpeg);
        }
    }
}
