using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Diagnostics;

namespace HolzShots.Capture.Video
{
    public class FFmpegManager
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
        // TODO: If the result is null, the suer shouold be instructed to set up ffmpeg manually
        public static async Task<string?> GetUrlOfLatestBinary()
        {
            using var client = new HttpClient();
            var res = await client.GetAsync("https://ffbinaries.com/api/v1/version/latest");
            var responseStream = await res.Content.ReadAsStreamAsync();
            var parsedResponse = JsonSerializer.Deserialize<FFmpegBinaryResponse>(responseStream);

            // HS requires a 64 bit windows, so we can hard-code this
            return parsedResponse?.binaries["windows-64"]?.FFmpegUrl;
        }

        public static async Task LoadAndUnzipToDirectory(string targetDir, string url)
        {
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var zipStream = await response.Content.ReadAsStreamAsync();

            var tempZipFilePath = Path.GetTempFileName();
            try
            {
                using (var fileStream = File.OpenWrite(tempZipFilePath))
                    zipStream.CopyTo(fileStream);

                ZipFile.ExtractToDirectory(tempZipFilePath, targetDir);
            }
            finally
            {
                File.Delete(tempZipFilePath);
            }
        }
    }
}
