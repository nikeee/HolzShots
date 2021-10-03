using System;
using System.Net.Http;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using HolzShots.Net;
using System.Threading;

namespace HolzShots.Capture.Video.FFmpeg
{
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

        public static async Task LoadAndUnzipToDirectory(string targetDir, string url, IProgress<TransferProgress> progress, CancellationToken cancellationToken)
        {
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);

            using var client = new HttpClient();

            var tempZipFilePath = Path.GetTempFileName();
            try
            {
                using (var diskZipFileStream = File.OpenWrite(tempZipFilePath))
                    await client.DownloadAsync(url, diskZipFileStream, progress, cancellationToken);

                ZipFile.ExtractToDirectory(tempZipFilePath, targetDir, true);
            }
            finally
            {
                File.Delete(tempZipFilePath);
            }
        }
    }

    internal record FFmpegBinaryResponse(
        [property: JsonPropertyName("version")] string Version, // Version Version
        [property: JsonPropertyName("permalink")] string Permalink,
        [property: JsonPropertyName("bin")] Dictionary<string, FFmpegBinEntry> binaries
    );

    internal record FFmpegBinEntry(
        [property: JsonPropertyName("ffmpeg")] string FFmpegUrl,
        [property: JsonPropertyName("ffplay")] string? FFplayUrl,
        [property: JsonPropertyName("ffprobe")] string FFprobeUrl
    );
}
