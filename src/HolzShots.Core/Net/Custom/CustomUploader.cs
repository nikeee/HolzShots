using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HolzShots.Net.Custom
{
    public class CustomUploader : Uploader
    {
        private const string SupportedSchema = "0.1.0";
        public CustomUploaderRoot CustomData { get; }

        private CustomUploader(CustomUploaderRoot customData)
        {
            if (customData == null)
                throw new ArgumentNullException(nameof(customData));

            Debug.Assert(customData.Validate(SupportedSchema));
            CustomData = customData;
        }

        public async override Task<UploadResult> InvokeAsync(Stream data, string suggestedFileName, string mimeType, IProgress<UploadProgress> progress, CancellationToken cancellationToken)
        {
            Debug.Assert(CustomData != null);
            Debug.Assert(CustomData.Validate(SupportedSchema));
            Debug.Assert(CustomData.Uploader != null);
            Debug.Assert(data != null);
            Debug.Assert(!string.IsNullOrWhiteSpace(suggestedFileName));
            Debug.Assert(!string.IsNullOrWhiteSpace(mimeType));
            Debug.Assert(cancellationToken != null);

            // If the stream has a specific length, check if it exceeds the set maximum file size
            var mfs = CustomData.Uploader.MaxFileSize;
            if (mfs.HasValue && data.CanSeek)
            {
                var size = data.Length;
                if (size > mfs.Value)
                {
                    var memSize = new MemSize(mfs.Value);
                    throw new UploadException($"File is {memSize} in size, which is larger than the specified limit of {nameof(CustomData.Uploader.MaxFileSize)}.");
                }
            }

            var uplInfo = CustomData.Uploader;

            using (var progressHandler = new ProgressMessageHandler(new HttpClientHandler()))
            using (var cl = new HttpClient(progressHandler))
            {
                progressHandler.HttpSendProgress += (s, e) => progress.Report(new UploadProgress(e));

                var userAgent = uplInfo.Headers?.GetUserAgent(SuggestedUserAgent) ?? SuggestedUserAgent;
                cl.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

                using (var content = new MultipartFormDataContent())
                {
                    foreach (var (name, value) in uplInfo.PostParams)
                        content.Add(new StringContent(value), name);

                    var fname = uplInfo.GetFileName(suggestedFileName);

                    Debug.Assert(!string.IsNullOrWhiteSpace(uplInfo.FileFormName));
                    Debug.Assert(!string.IsNullOrWhiteSpace(fname));

                    content.Add(new StreamContent(data), uplInfo.FileFormName, fname);

                    Debug.Assert(!string.IsNullOrWhiteSpace(uplInfo.RequestUrl));

                    var res = await cl.PostAsync(uplInfo.RequestUrl, content, cancellationToken).ConfigureAwait(false);

                    if (!res.IsSuccessStatusCode)
                        throw new UploadException($"The servers of {CustomData.Info.Name} responded with the error {res.StatusCode}: \"{res.ReasonPhrase}\".");

                    var resStr = await res.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var imageUrl = ParseUrlFromResponse(resStr, CustomData.Uploader.Parser);
                    Debug.Assert(!string.IsNullOrWhiteSpace(imageUrl));

                    return new UploadResult(this, imageUrl, DateTime.Now);
                }
            }
        }

        private string ParseUrlFromResponse(string response, Parser parser)
        {
            Debug.Assert(response != null);
            Debug.Assert(parser != null);

            switch (CustomData.Uploader.Parser.Kind.ToUpperInvariant())
            {
                case "REGEX":
                    {
                        var successPattern = new Regex(parser.Success); // TODO: Regex options?
                        var matches = successPattern.Matches(response);
                        if (matches.Count == 0)
                        {
                            if (parser.Failure == null)
                                throw new UploadException("Response did not contain valid data.");

                            // No m atch found, look for error message
                            var failurePattern = new Regex(parser.Failure); // TODO: Regex options?
                            var failureMatch = failurePattern.Match(response);
                            if (!failureMatch.Success)
                                throw new UploadException("Response did not contain valid data.");
                            throw new UploadException($"Server returned error:\n{failureMatch.Value}");
                        }
                        var url = FillUrlTemplate(parser.Url, matches.Cast<Match>().ToArray());

                        Debug.Assert(!string.IsNullOrEmpty(url));
                        return url;
                    }
                case "JSON":
                    {
                        JObject obj = null;
                        try
                        {
                            obj = JObject.Parse(response);
                        }
                        catch (JsonReaderException ex)
                        {
                            throw new UploadException("Invalid JSON response", ex);
                        }

                        Debug.Assert(obj != null);
                        // Get Success Link-Value
                        var suc = obj.SelectToken(parser.Success);
                        if (suc == null)
                        {
                            if (parser.Failure == null)
                                throw new UploadException("Response did not contain valid data.");

                            // There was none, fetch error message
                            var fail = obj.SelectToken(parser.Failure);
                            if (fail == null) // Check if there is an error message
                                throw new UploadException("Response did not contain valid data.");
                            throw new UploadException($"Server returned error: {fail.ToString()}");
                        }
                        var imageUrl = parser.Url.Replace("$match$", suc.ToString());
                        return imageUrl;
                    }
                default: throw new InvalidDataException();
            }
        }

        private string FillUrlTemplate(string template, IReadOnlyList<Match> matches)
        {
            Debug.Assert(!string.IsNullOrEmpty(template));
            Debug.Assert(matches != null);

            const char PatternToken = '$';

            if (!template.Contains(PatternToken.ToString()))
                return template;

            var sb = new StringBuilder();
            string currentToken = null;
            for (int i = 0; i < template.Length; ++i)
            {
                var c = template[i];

                // Token starts or ends
                if (c == PatternToken)
                {
                    if (currentToken == null)
                    {
                        // Token is started, since currentToken is null
                        currentToken = string.Empty;
                    }
                    else
                    {
                        // Token was now closed. Append evaluated token and reset.

                        var split = currentToken.Split(',');
                        Debug.Assert(split != null);

                        int matchIndex = int.Parse(split[0]);

                        Debug.Assert(matchIndex < matches.Count);

                        var match = matches[matchIndex];
                        Debug.Assert(match != null);
                        if (split.Length == 2)
                        {
                            // use a specific match group if provided
                            var matchGroup = split[1];
                            var group = match.Groups[matchGroup];

                            Debug.Assert(group != null);
                            Debug.Assert(group.Success);
                            sb.Append(group.Value);
                        }
                        else
                        {
                            Debug.Assert(split.Length < 2);
                            // Insert the entire match
                            sb.Append(match.Value);
                        }
                        currentToken = null;
                    }
                }
                else
                {
                    // No token char (no $)
                    // Just append it to the current token or text
                    if (currentToken != null)
                        currentToken += c;
                    else
                        sb.Append(c);
                }
            }

            // Assert that all tokens have been closed
            Debug.Assert(currentToken == null);
            return sb.ToString();
        }

        public static bool TryParse(string value, out CustomUploader result)
        {
            result = null;
            if (string.IsNullOrWhiteSpace(value))
                return false;
            var info = JsonConvert.DeserializeObject<CustomUploaderRoot>(value, JsonConfig.JsonSettings);
            if (info?.Validate(SupportedSchema) == true)
            {
                result = new CustomUploader(info);
                return true;
            }
            return false;
        }
    }
}
