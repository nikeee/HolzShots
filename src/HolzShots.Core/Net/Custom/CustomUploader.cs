using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HolzShots.Net.Custom
{
    public class CustomUploader : Uploader
    {
        private const string SupportedSchema = "0.2.0";
        public CustomUploaderSpec UploaderInfo { get; }

        protected CustomUploader(CustomUploaderSpec? customData)
        {
            UploaderInfo = customData ?? throw new ArgumentNullException(nameof(customData));
        }

        public async override Task<UploadResult> InvokeAsync(Stream data, string suggestedFileName, string mimeType, IProgress<UploadProgress> progress, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Debug.Assert(UploaderInfo != null);
            Debug.Assert(UploaderInfo.Uploader != null);
            Debug.Assert(!string.IsNullOrWhiteSpace(suggestedFileName));
            Debug.Assert(!string.IsNullOrWhiteSpace(mimeType));

            // If the stream has a specific length, check if it exceeds the set maximum file size
            var mfs = UploaderInfo.Uploader.MaxFileSize;
            if (mfs.HasValue && mfs.Value > 0 && data.CanSeek)
            {
                var size = data.Length;
                if (size > mfs.Value)
                {
                    var memSize = new MemSize(mfs.Value);
                    throw new UploadException($"File is {memSize} in size, which is larger than the specified limit of {nameof(UploaderInfo.Uploader.MaxFileSize)}.");
                }
            }

            var uplInfo = UploaderInfo.Uploader;

            using (var progressHandler = new ProgressMessageHandler(new HttpClientHandler()))
            using (var cl = new HttpClient(progressHandler))
            {
                progressHandler.HttpSendProgress += (s, e) => progress.Report(new UploadProgress(e));

                // Add the user-agent first, so the user can override it
                cl.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", SuggestedUserAgent);

                if (uplInfo.Headers != null)
                {
                    foreach (var (header, value) in uplInfo.Headers)
                        cl.DefaultRequestHeaders.TryAddWithoutValidation(header, value);
                }

                var responseParser = uplInfo.ResponseParser;
                using (var content = new MultipartFormDataContent())
                {
                    var postParams = uplInfo.PostParams;
                    if (postParams is not null)
                    {
                        foreach (var (name, value) in postParams)
                            content.Add(new StringContent(value), name);
                    }

                    var fname = uplInfo.GetEffectiveFileName(suggestedFileName);

                    Debug.Assert(!string.IsNullOrWhiteSpace(uplInfo.FileFormName));
                    Debug.Assert(!string.IsNullOrWhiteSpace(fname));

                    content.Add(new StreamContent(data), uplInfo.FileFormName, fname);

                    Debug.Assert(!string.IsNullOrWhiteSpace(uplInfo.RequestUrl));

                    var res = await cl.PostAsync(uplInfo.RequestUrl, content, cancellationToken).ConfigureAwait(false);

                    if (!res.IsSuccessStatusCode)
                        throw new UploadException($"The servers of {UploaderInfo.Meta.Name} responded with the error {res.StatusCode}: \"{res.ReasonPhrase}\".");

                    var resStr = await res.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var urlTemplateSpec = responseParser.UrlTemplateSpec;

                    // As the URL template is optional, we just take the entire response if it is not there
                    if (urlTemplateSpec == null)
                        return new UploadResult(this, resStr, DateTime.Now);

                    try
                    {
                        var imageUrl = urlTemplateSpec.Evaluate(resStr);
                        Debug.Assert(!string.IsNullOrWhiteSpace(imageUrl));

                        return new UploadResult(this, imageUrl, DateTime.Now);
                    }
                    catch (UnableToFillTemplateException e)
                    {
                        throw new UploadException(e.Message, e);
                    }
                }
            }
        }

        public static bool TryParse(string value, [MaybeNullWhen(false)] out CustomUploader? result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = null;
                return false;
            }
            var info = JsonConvert.DeserializeObject<CustomUploaderSpec>(value, JsonConfig.JsonSettings);
            return TryLoad(info, out result);
        }

        public static bool TryLoad(CustomUploaderSpec? value, [MaybeNullWhen(false)] out CustomUploader? result)
        {
            try
            {
                result = new CustomUploader(value);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}
