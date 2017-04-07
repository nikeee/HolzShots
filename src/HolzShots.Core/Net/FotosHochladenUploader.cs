using HolzShots.Composition;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HolzShots.Net
{
    [UploadPlugin("Fotos-Hochladen.net", "Niklas Mollenhauer", "1.0.0", "Uploader plugin for Fotos-Hochladen.net", "nikeee@outlook.com", "https://holz.nu", "https://github.com/nikeee/HolzShots")]
    public class FotosHochladenUploader : Uploader
    {
        private const string ServiceName = "Fotos-Hochladen.net";

        public override async Task<UploadResult> InvokeAsync(Stream data, string suggestedFileName, string mimeType, CancellationToken cancellationToken, IProgress<UploadProgress> progress)
        {
            Debug.Assert(data != null);
            Debug.Assert(!string.IsNullOrEmpty(suggestedFileName));
            // Debug.Assert(!string.IsNullOrEmpty(mimeType));
            // Debug.Assert(cancellationToken != null);
            Debug.Assert(progress != null);

            const string destUrl = "http://www.fotos-hochladen.net/";

            using (var progressHandler = new ProgressMessageHandler(new HttpClientHandler()))
            using (var cl = new HttpClient(progressHandler))
            {
                progressHandler.HttpSendProgress += (s, e) => progress.Report(new UploadProgress(e));
                cl.DefaultRequestHeaders.UserAgent.ParseAdd(SuggestedUserAgent);
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(data), "upload", suggestedFileName);
                    content.Add(new StringContent("no-resize"), "resize");
                    content.Add(new StringContent("akzeptiert"), "bedingungen");
                    content.Add(new StringContent("Bild hochladen"), "submit");

                    var res = await cl.PostAsync(destUrl, content, cancellationToken).ConfigureAwait(false);

                    if (!res.IsSuccessStatusCode)
                        throw new UploadException($"The servers of {ServiceName} responded with the error {res.StatusCode}: \"{res.ReasonPhrase}\".");

                    var resStr = await res.Content.ReadAsStringAsync().ConfigureAwait(false);

                    // http://img4.fotos-hochladen.net/uploads/holzshots57e0gxa6bk.png
                    const string urlPattern = @"http://[a-zA-Z0-9]{1,}\.fotos-hochladen\.net/uploads/(.+)\.[a-zA-Z]{1,3}";
                    var matches = Regex.Matches(resStr, urlPattern);

                    Debug.Assert(matches.Count > 0);

                    if (matches.Count == 0)
                        throw new UploadException($"The response of {ServiceName} did not contain any valid image urls.");

                    var resMatch = matches.Cast<Match>().FirstOrDefault(m => !m.Value.Contains("temp"));
                    if (resMatch != null)
                        return new UploadResult(this, resMatch.Value, DateTime.Now);
                    throw new UploadException($"The response of {ServiceName} did not contain any valid image urls.");
                }
            }
        }
    }
}
