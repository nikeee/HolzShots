using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace HolzShots.Drawing
{
    public static class ImageExtensions
    {
        private const string _rawDataFieldName = "rawData";
        public static byte[]? GetRawData(this Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            return ReflectionUtil.GetInstanceField<Image, byte[]>(image, _rawDataFieldName);
        }
        internal static void SetRawData(this Image image, byte[] rawData)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            ReflectionUtil.SetInstanceField(image, _rawDataFieldName, rawData);
        }

        public static Image CloneDeep(this Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var rawData = image.GetRawData()!;
            var copy = (image.Clone() as Image)!;
            Debug.Assert(copy is not null);

            copy!.SetRawData(rawData);
            return copy!;
        }

        public static MemSize EstimateFileSize(this Image image, ImageFormat format)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            using (var ms = new System.IO.MemoryStream(image.Width * image.Height * 4))
            {
                image.Save(ms, format);
                return new MemSize(ms.Length);
            }
        }

        public static Image CloneGifBug(this Image image, ImageFormat format)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            return format == ImageFormat.Gif
                ? image.CloneDeep()
                : (image.Clone() as Image)!;
        }

        public static MemoryStream GetImageStream(this Image image, ImageFormat format)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (format == null)
                throw new ArgumentNullException(nameof(format));

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

        public static void SaveExtended(this Image image, Stream destination, ImageFormat format)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (format == null)
                throw new ArgumentNullException(nameof(format));

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
        public static bool ShouldMaximizeEditorWindowForImage(this Image image)
        {
            return image == null
                ? throw new ArgumentNullException(nameof(image))
                : image.Width * image.Height >= 480000;
        }
    }
}
