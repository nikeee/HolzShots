using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolzShots.Capture.Video
{
    public class FFmpegManager
    {
        const string FFmpegExecutable = "ffmpeg.exe";

        public static bool HasFFmpegInPath()
        {
            var sb = new StringBuilder(Shlwapi.MAX_PATH);
            sb.Append(FFmpegExecutable);
            return Shlwapi.PathFindOnPath(sb, null);
        }
    }
}
