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

                var userAgent = uplInfo.Headers?["User-Agent"] ?? SuggestedUserAgent;
                // uplInfo.Headers?.GetUserAgent(SuggestedUserAgent) ?? SuggestedUserAgent;
                cl.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

                // TODO: Set headers defined in request headers

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

                    var imageUrl = ParseUrlFromResponse(resStr, UploaderInfo.Uploader.ResponseParser);
                    Debug.Assert(!string.IsNullOrWhiteSpace(imageUrl));

                    return new UploadResult(this, imageUrl, DateTime.Now);
                }
            }
        }

        private string ParseUrlFromResponse(string response, Parser parser)
        {
            Debug.Assert(response != null);
            Debug.Assert(parser != null);

            switch (UploaderInfo.Uploader.ResponseParser.Kind.ToUpperInvariant())
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
                        var url = FillUrlTemplate(parser.UrlTemplate, matches.Cast<Match>().ToArray());

                        Debug.Assert(!string.IsNullOrEmpty(url));
                        return url;
                    }
                case "JSON":
                    {
                        JObject obj;
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
                            throw new UploadException($"Server returned error: {fail}");
                        }
                        var imageUrl = parser.UrlTemplate.Replace("$match$", suc.ToString());
                        return imageUrl;
                    }
                default: throw new InvalidDataException();
            }
        }

        // TODO: Move this templating to a separate class
        private static string FillUrlTemplate(string template, IReadOnlyList<Match> matches)
        {
            Debug.Assert(!string.IsNullOrEmpty(template));
            Debug.Assert(matches != null);

            const char PatternToken = '$';

            if (!template.Contains(PatternToken.ToString()))
                return template;

            var sb = new StringBuilder();
            string? currentToken = null;
            for (var i = 0; i < template.Length; ++i)
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
