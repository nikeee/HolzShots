using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using HolzShots.Drawing;

namespace HolzShots.Net;

public interface IUploadPayload : IDisposable
{
    string MimeType { get; }
    string Extension { get; }
    Stream GetStream();
    string GetSuggestedFileName();
}

public record ImageUploadPayload : IUploadPayload
{
    private const string DefaultUploadFileNameWithoutExtension = LibraryInformation.Name;

    public string MimeType { get; init; }
    public string Extension { get; init; }

    // TODO: refactor to use Bitmap
    private readonly Image _image;
    private readonly ImageFormat _format;

    public ImageUploadPayload(Image image, ImageFormat format)
    {
        ArgumentNullException.ThrowIfNull(format);
        _format = format;
        _image = image.CloneGifBug(_format);

        (Extension, MimeType) = _format.GetExtensionAndMimeType();
        Debug.Assert(!string.IsNullOrWhiteSpace(MimeType));
        Debug.Assert(!string.IsNullOrWhiteSpace(Extension));
    }

    public Stream GetStream() => _image.GetImageStream(_format);
    public string GetSuggestedFileName() => DefaultUploadFileNameWithoutExtension + Extension;

    public void Dispose() => _image.Dispose();
}
