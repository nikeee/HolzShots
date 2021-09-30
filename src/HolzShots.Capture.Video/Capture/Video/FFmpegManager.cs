using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HolzShots.Capture.Video
{
    public class FFmpegManager
    {
        const string FFmpegExecutable = "ffmpeg.exe";

        // TODO: Ask the user if he wants to use the FFmpeg on the PATH if it is present
        public static bool HasFFmpegInPath()
        {
            var sb = new StringBuilder(Shlwapi.MAX_PATH);
            sb.Append(FFmpegExecutable);
            return Shlwapi.PathFindOnPath(sb, null);
        }
    }

    record FFmpegBinaryResponse(
        [property: JsonPropertyName("version")] string Version, // Version Version
        [property: JsonPropertyName("permalink")] string Permalink,
        [property: JsonPropertyName("bin")] Dictionary<string, FFmpegBinEntry> binaries
    );

    record FFmpegBinEntry(
        [property: JsonPropertyName("ffmpeg")] string FFmpegUrl,
        [property: JsonPropertyName("ffplay")] string? FFplayUrl,
        [property: JsonPropertyName("ffprobe")] string FFprobeUrl
    );

    public class FFmpegFetcher
    {
        // TODO: Ask the user if he wants to download the binary from that source
        public static async Task<string?> GetUrlOfLatestBinary()
        {
            using var client = new HttpClient();
            var res = await client.GetAsync("https://ffbinaries.com/api/v1/version/latest");
            var responseStream = await res.Content.ReadAsStreamAsync();
            var parsedResponse = JsonSerializer.Deserialize<FFmpegBinaryResponse>(responseStream);
            return parsedResponse?.binaries["windows-64"]?.FFplayUrl;
        }
    }
}
