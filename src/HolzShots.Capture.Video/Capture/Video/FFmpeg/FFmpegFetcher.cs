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

    /// <summary> Ref: https://stackoverflow.com/a/46497896 </summary>
    public static class HttpClientExtensions
    {
        public static async Task DownloadAsync(this HttpClient client, string requestUri, Stream destination, IProgress<TransferProgress> progress = null, CancellationToken cancellationToken = default)
        {
            // Get the http headers first to examine the content length
            using var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            var contentLength = response.Content.Headers.ContentLength;

            using var download = await response.Content.ReadAsStreamAsync(cancellationToken);

            // Ignore progress reporting when no progress reporter was 
            // passed or when the content length is unknown
            if (progress == null || !contentLength.HasValue)
            {
                await download.CopyToAsync(destination, cancellationToken);
                return;
            }

            var totalData = new MemSize(contentLength!.Value);

            // Convert absolute progress (bytes downloaded) into relative progress (0% - 100%)
            var relativeProgress = new Progress<long>(bytesLoaded => progress.Report(new TransferProgress(new MemSize(bytesLoaded), totalData, UploadState.Processing)));
            // Use extension method to report progress while downloading
            await download.CopyToAsync(destination, 81920, relativeProgress, cancellationToken);
            progress.Report(new TransferProgress(totalData, totalData, UploadState.Finished));
        }
    }

    public static class StreamExtensions
    {
        public static async Task CopyToAsync(this Stream source, Stream destination, int bufferSize, IProgress<long> progress = null, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (!source.CanRead)
                throw new ArgumentException("Has to be readable", nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (!destination.CanWrite)
                throw new ArgumentException("Has to be writable", nameof(destination));
            if (bufferSize < 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            var buffer = new byte[bufferSize];
            long totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = await source.ReadAsync(buffer, cancellationToken).ConfigureAwait(false)) != 0)
            {
                await destination.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken).ConfigureAwait(false);
                totalBytesRead += bytesRead;
                progress?.Report(totalBytesRead);
            }
        }
    }
}
