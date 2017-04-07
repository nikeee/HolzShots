using System;

namespace HolzShots.IO.Naming
{
    class ImagePatternItem : PatternItem
    {
        public ImagePatternItem(string propertyName)
            : base(propertyName)
        {
            if (propertyName == null)
            {
                InfoType = ImageInfoType.Invalid;
                return;
            }
            switch (propertyName.ToLowerInvariant())
            {
                case "width":
                    InfoType = ImageInfoType.Width;
                    break;
                case "height":
                    InfoType = ImageInfoType.Height;
                    break;
                default:
                    InfoType = ImageInfoType.Invalid;
                    break;
            }
        }

        public ImageInfoType InfoType { get; }
        public override string Keyword => "Image";
        public override string TextRepresentation => $"<{Keyword}:{PropertyName}>";
        public override bool IsValid => InfoType != ImageInfoType.Invalid;

        public override string FormatMetadata(FileMetadata metadata)
        {
            switch(InfoType)
            {
                case ImageInfoType.Width:
                    return metadata.Dimensions.Width.ToString();
                case ImageInfoType.Height:
                    return metadata.Dimensions.Height.ToString();
                default:
                    throw new InvalidOperationException();
            }
        }

        public enum ImageInfoType
        {
            Invalid,
            Width,
            Height,
        }
    }
}
