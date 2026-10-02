
namespace HolzShots.IO.Naming;

class SizePatternItem(string? propertyName) : PatternItem(propertyName)
{
    public ImageInfoType InfoType { get; } = propertyName?.ToLowerInvariant() switch
    {
        "width" => ImageInfoType.Width,
        "height" => ImageInfoType.Height,
        _ => ImageInfoType.Invalid,
    };
    public override string Keyword => "size";
    public override bool IsValid => InfoType != ImageInfoType.Invalid;
    public override string TextRepresentation => IsValid
            ? $"<{Keyword}:{PropertyName}>"
            : $"<{Keyword}>";

    /// <summary>Formats the selected image dimension.</summary>
    /// <exception cref="System.InvalidOperationException">The item has an invalid info type.</exception>
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
