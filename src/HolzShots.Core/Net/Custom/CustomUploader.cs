using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Net.Http.Handlers;
using Newtonsoft.Json;

namespace HolzShots.Net.Custom;

public class CustomUploader : Uploader
{
    public CustomUploaderSpec UploaderInfo { get; }

    protected CustomUploader(CustomUploaderSpec? customData)
    {
        UploaderInfo = customData ?? throw new ArgumentNullException(nameof(customData));
    }

    public async override Task<UploadResult> InvokeAsync(Stream data, string suggestedFileName, string mimeType, IProgress<TransferProgress> progress, CancellationToken cancellationToken)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        Debug.Assert(UploaderInfo is not null);
        Debug.Assert(UploaderInfo.Uploader is not null);
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

        using var progressHandler = new ProgressMessageHandler(new HttpClientHandler());
        using var cl = new HttpClient(progressHandler);

        progressHandler.HttpSendProgress += (s, e) => progress.Report(TransferProgress.FromHttpProgressEventArgs(e));

        // Add the user-agent first, so the user can override it
        cl.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", SuggestedUserAgent);

        if (uplInfo.Headers is not null)
        {
            foreach (var (header, value) in uplInfo.Headers)
                cl.DefaultRequestHeaders.TryAddWithoutValidation(header, value);
        }

        var responseParser = uplInfo.ResponseParser;
        using var content = new MultipartFormDataContent();

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

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    public static bool TryParse(
        [NotNullWhen(true)] string? value,
        IFormatProvider? provider,
        [MaybeNullWhen(false)][NotNullWhen(true)] out CustomUploader? result
    )
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }
        try
        {
            if (CustomUploaderSpec.TryParse(value, provider, out var info))
                return TryLoad(info, out result);
            result = default;
            return false;
        }
        catch (JsonReaderException)
        {
            result = default;
            return false;
        }
    }

    public static bool TryLoad(CustomUploaderSpec? value, [MaybeNullWhen(false)][NotNullWhen(true)] out CustomUploader? result)
    {
        try
        {
            result = new CustomUploader(value);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}
