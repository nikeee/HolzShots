using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolzShots.Drawing
{
    public static class ImageFormatInformation
    {
        private const string DefaultUploadFileNameWithoutExtension = LibraryInformation.Name;
        private const string JpegMimeType = "image/jpeg";

        private static readonly IReadOnlyDictionary<ImageFormat, ExtensionAndMimeType> _imageFormats = new Dictionary<ImageFormat, ExtensionAndMimeType>()
        {
            [ImageFormat.Png] = new ExtensionAndMimeType(".png", "image/png"),
            [ImageFormat.Jpeg] = new ExtensionAndMimeType(".jpg", JpegMimeType),
            [ImageFormat.Gif] = new ExtensionAndMimeType(".gif", "image/gif"),
            [ImageFormat.Bmp] = new ExtensionAndMimeType(".bmp", "image/bmp"),
            [ImageFormat.Tiff] = new ExtensionAndMimeType(".tiff", "image/tiff"),
        };

        public static ExtensionAndMimeType GetExtensionAndMimeType(this ImageFormat format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));
            return _imageFormats[format];
        }

        public static ImageFormat/*?*/ GetImageFormatFromFileName(string fileName) => GetImageFormatFromFileExtension(Path.GetExtension(fileName));
        public static ImageFormat/*?*/ GetImageFormatFromFileExtension(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
                throw new ArgumentNullException(nameof(fileExtension));
            return _imageFormats
                .SingleOrDefault(kv => kv.Value.FileExtension.Equals(fileExtension, StringComparison.OrdinalIgnoreCase)).Key;
        }


        /// <summary>Retrieves the Encoder Information for a given MimeType</summary>
        /// <param name="mimeType">String: Mimetype</param>
        /// <returns>ImageCodecInfo: Mime info or null if not found</returns>
        private static ImageCodecInfo GetEncoderInfo(string mimeType) => ImageCodecInfo.GetImageEncoders().FirstOrDefault(e => e.MimeType == mimeType);

        /// <summary>Save an Image as a Jpeg with a given compression</summary>
        /// <param name="image">Image to save</param>
        /// <param name="destination">The destination stream</param>
        /// <param name="compression">Value between 0 and 100</param>
        public static void SaveAsJpeg(Image image, Stream destination, byte compression = 100)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            compression = Math.Min(compression, (byte)100);

            var encodeParameters = new EncoderParameters(1);
            encodeParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)compression);

            var encoderInfo = GetEncoderInfo(JpegMimeType);
            image.Save(destination, encoderInfo, encodeParameters);
        }


        public static string GetSuggestedFileName(ImageFormat format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            var ext = GetExtensionAndMimeType(format);
            return GetSuggestedFileName(ext);
        }
        public static string GetSuggestedFileName(ExtensionAndMimeType extensionAndMimeType)
        {
            Debug.Assert(extensionAndMimeType != null);
            Debug.Assert(extensionAndMimeType.FileExtension != null);
            Debug.Assert(extensionAndMimeType.MimeType != null);

            return DefaultUploadFileNameWithoutExtension + extensionAndMimeType.FileExtension;
        }
    }

    /// <summary> TODO: Maybe find better name </summary>
    /// TODO: Use a record as soon as we can. This looks bulky.
    public readonly struct ExtensionAndMimeType : IEquatable<ExtensionAndMimeType>
    {
        public string FileExtension { get; }
        public string MimeType { get; }
        public ExtensionAndMimeType(string fileExtension, string mimeType)
        {
            FileExtension = fileExtension ?? throw new ArgumentNullException(nameof(fileExtension));
            MimeType = mimeType ?? throw new ArgumentNullException(nameof(mimeType));
        }

        public override bool Equals(object obj) => obj is ExtensionAndMimeType other && Equals(other);
        public bool Equals(ExtensionAndMimeType other) => other.FileExtension == FileExtension && other.MimeType == MimeType;

        public override int GetHashCode() => HashCode.Combine(MimeType, FileExtension);
        public static bool operator ==(ExtensionAndMimeType left, ExtensionAndMimeType right) => left.Equals(right);
        public static bool operator !=(ExtensionAndMimeType left, ExtensionAndMimeType right) => !(left == right);
    }
}
