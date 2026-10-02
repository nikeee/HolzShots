
namespace HolzShots.IO.Naming;

/// <summary>Pattern item containing plain text.</summary>
/// <exception cref="System.ArgumentNullException">The property name is null.</exception>
class TextPatternItem(string? propertyName) : PatternItem(propertyName ?? throw new ArgumentNullException(nameof(propertyName)))
{
    public override string Keyword => "text";

    public override string TextRepresentation => PropertyName!;
    public override bool IsValid => !string.IsNullOrEmpty(PropertyName) && !PropertyName.ContainsInvalidChars();
    public override string FormatMetadata(FileMetadata metadata) => PropertyName!.SanitizeFileName(string.Empty);
}
