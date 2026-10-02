using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace HolzShots.Drawing;

public static class ImageExtensions
{
    private const string _rawDataFieldName = "rawData";
    /// <summary>Gets the raw data of the image.</summary>
    /// <exception cref="System.ArgumentNullException">An argument is null.</exception>
    public static byte[]? GetRawData(this Image image)
    {
        ArgumentNullException.ThrowIfNull(image);

        return ReflectionUtil.GetInstanceField<Image, byte[]>(image, _rawDataFieldName);
    }
    /// <summary>Sets the raw data of the image.</summary>
    /// <exception cref="System.ArgumentNullException">An argument is null.</exception>
    internal static void SetRawData(this Image image, byte[] rawData)
    {
        ArgumentNullException.ThrowIfNull(image);

        ReflectionUtil.SetInstanceField(image, _rawDataFieldName, rawData);
    }

    /// <summary>Clones the image including its raw data.</summary>
    /// <exception cref="System.ArgumentNullException">An argument is null.</exception>
    public static Image CloneDeep(this Image image)
    {
        ArgumentNullException.ThrowIfNull(image);

        var rawData = image.GetRawData()!;
        var copy = (image.Clone() as Image)!;
        Debug.Assert(copy is not null);

        copy!.SetRawData(rawData);
        return copy!;
    }

    /// <summary>Estimates the file size of the image in the given format.</summary>
    /// <exception cref="System.ArgumentNullException">An argument is null.</exception>
    public static MemSize EstimateFileSize(this Image image, ImageFormat format)
    {
        ArgumentNullException.ThrowIfNull(image);
        ArgumentNullException.ThrowIfNull(format);

        using var ms = new System.IO.MemoryStream(image.Width * image.Height * 4);

        image.Save(ms, format);
        return new MemSize(ms.Length);
    }

    /// <summary>Clones the image, working around the GIF raw data bug.</summary>
    /// <exception cref="System.ArgumentNullException">An argument is null.</exception>
    public static Image CloneGifBug(this Image image, ImageFormat format)
    {
        ArgumentNullException.ThrowIfNull(image);
        ArgumentNullException.ThrowIfNull(format);

        return format == ImageFormat.Gif
            ? image.CloneDeep()
            : (image.Clone() as Image)!;
    }

    /// <summary>Gets a stream containing the image in the given format.</summary>
    /// <exception cref="System.ArgumentNullException">An argument is null.</exception>
    public static MemoryStream GetImageStream(this Image image, ImageFormat format)
    {
        ArgumentNullException.ThrowIfNull(image);
        ArgumentNullException.ThrowIfNull(format);

        if (format == ImageFormat.Gif)
        {
            var buffer = image.GetRawData();
            Debug.Assert(buffer is not null);
            Debug.Assert(buffer!.Length > 0);

            var gifStream = new MemoryStream(buffer);
            gifStream.Seek(0, SeekOrigin.Begin);
            return gifStream;
        }

        var ms = new MemoryStream();
        image.SaveExtended(ms, format);
        ms.Seek(0, SeekOrigin.Begin);
        return ms;
    }

    /// <summary>Saves the image to a stream in the given format.</summary>
    /// <exception cref="System.ArgumentNullException">An argument is null.</exception>
    public static void SaveExtended(this Image image, Stream destination, ImageFormat format)
    {
        ArgumentNullException.ThrowIfNull(image);
        ArgumentNullException.ThrowIfNull(destination);
        ArgumentNullException.ThrowIfNull(format);

        if (format == ImageFormat.Jpeg)
        {
            ImageFormatInformation.SaveAsJpeg(image, destination);
        }
        else
        {
            image.Save(destination, format);
        }
    }

    // TODO: Move this somewhere else, this is GUI code
    /// <summary>Determines whether the editor should be maximized for the image.</summary>
    /// <exception cref="System.ArgumentNullException">An argument is null.</exception>
    public static bool ShouldMaximizeEditorWindowForImage(this Image image)
    {
        return image == null
            ? throw new ArgumentNullException(nameof(image))
            : image.Width * image.Height >= 480000;
    }
}
