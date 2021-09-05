using System.Diagnostics;
using System.Net.Http.Handlers;
using System.Text.RegularExpressions;
using HolzShots.Composition;

namespace HolzShots.Net.Custom
{
    [System.Composition.Export(typeof(Uploader))]
    [UploadPlugin("DirectUpload.net", "Niklas Mollenhauer", "1.0.0", "Uploader plugin for DirectUpload.net", "nikeee@outlook.com", "https://holz.nu", "https://github.com/nikeee/HolzShots")]
    public class DirectUploadUploader : Uploader
    {
        private const string ServiceName = "DirectUpload.net";

        public override async Task<UploadResult> InvokeAsync(Stream data, string suggestedFileName, string mimeType, IProgress<UploadProgress> progress, CancellationToken cancellationToken)
        {
            Debug.Assert(data != null);
            Debug.Assert(!string.IsNullOrEmpty(suggestedFileName));
            // Debug.Assert(!string.IsNullOrEmpty(mimeType));
            Debug.Assert(progress != null);

            const string destUrl = "http://www.directupload.net/index.php?mode=upload";

            using (var progressHandler = new ProgressMessageHandler(new HttpClientHandler()))
            using (var cl = new HttpClient(progressHandler))
            {
                progressHandler.HttpSendProgress += (s, e) => progress.Report(new UploadProgress(e));
                cl.DefaultRequestHeaders.UserAgent.ParseAdd(SuggestedUserAgent);
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent("file"), "input");
                    content.Add(new StreamContent(data), "bilddatei", suggestedFileName);

                    var res = await cl.PostAsync(destUrl, content, cancellationToken).ConfigureAwait(false);

                    if (!res.IsSuccessStatusCode)
                        throw new UploadException($"The servers of {ServiceName} responded with the error {res.StatusCode}: \"{res.ReasonPhrase}\".");

                    var resStr = await res.Content.ReadAsStringAsync().ConfigureAwait(false);

                    const string urlPattern = @"https?://[a-zA-Z0-9]*.directupload.net/images/\d{1,}/\w{1,}.[a-zA-Z]{1,3}";
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
