using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace HolzShots.Drawing
{
    public static class ImageExtensions
    {
        private const string _rawDataFieldName = "rawData";
        public static byte[] GetRawData(this Image image)
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

            if (image == null)
                return null;
            var rawData = image.GetRawData();
            var copy = image.Clone() as Image;
            Debug.Assert(copy != null);
            copy.SetRawData(rawData);
            return copy;
        }

        public static MemSize EstimateFileSize(this Image image, ImageFormat format)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if(format == null)
                throw new ArgumentNullException(nameof(format));

            using (var ms = new System.IO.MemoryStream(image.Width * image.Height * 4))
            {
                image.Save(ms, format);
                return new MemSize(ms.Length);
            }
        }
    }
}
