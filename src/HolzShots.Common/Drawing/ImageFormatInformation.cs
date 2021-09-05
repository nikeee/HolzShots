using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace HolzShots.Drawing
{
    public static class ImageFormatInformation
    {
        private const string DefaultUploadFileNameWithoutExtension = LibraryInformation.Name;
        private const string JpegMimeType = "image/jpeg";

        private static readonly IReadOnlyDictionary<ImageFormat, FormatDefinition> _imageFormats = new Dictionary<ImageFormat, FormatDefinition>()
        {
            [ImageFormat.Png] = new FormatDefinition(".png", "image/png"),
            [ImageFormat.Jpeg] = new FormatDefinition(".jpg", JpegMimeType),
            [ImageFormat.Gif] = new FormatDefinition(".gif", "image/gif"),
            [ImageFormat.Bmp] = new FormatDefinition(".bmp", "image/bmp"),
            [ImageFormat.Tiff] = new FormatDefinition(".tiff", "image/tiff"),
        };

        public static FormatDefinition GetExtensionAndMimeType(this ImageFormat format)
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
        public static string GetSuggestedFileName(FormatDefinition extensionAndMimeType)
        {
            Debug.Assert(extensionAndMimeType.FileExtension != null);
            Debug.Assert(extensionAndMimeType.MimeType != null);

            return DefaultUploadFileNameWithoutExtension + extensionAndMimeType.FileExtension;
        }
    }

    public record FormatDefinition(string FileExtension, string MimeType);
}
