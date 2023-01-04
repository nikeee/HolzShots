
namespace HolzShots.IO.Naming;

class SizePatternItem : PatternItem
{
    public SizePatternItem(string? propertyName)
        : base(propertyName)
    {
        InfoType = propertyName?.ToLowerInvariant() switch
        {
            "width" => ImageInfoType.Width,
            "height" => ImageInfoType.Height,
            _ => ImageInfoType.Invalid,
        };
    }

    public ImageInfoType InfoType { get; }
    public override string Keyword => "size";
    public override bool IsValid => InfoType != ImageInfoType.Invalid;
    public override string TextRepresentation => IsValid
            ? $"<{Keyword}:{PropertyName}>"
            : $"<{Keyword}>";

    public override string FormatMetadata(FileMetadata metadata) => InfoType switch
    {
        ImageInfoType.Width => metadata.Dimensions.Width.ToString(),
        ImageInfoType.Height => metadata.Dimensions.Height.ToString(),
        _ => throw new InvalidOperationException(),
    };

    public enum ImageInfoType
    {
        Invalid,
        Width,
        Height,
    }
}
