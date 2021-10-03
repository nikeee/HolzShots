using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using HolzShots.Net;
using System.Threading;

namespace HolzShots.Capture.Video.FFmpeg
{
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
}
