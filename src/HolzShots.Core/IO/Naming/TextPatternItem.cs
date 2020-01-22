
namespace HolzShots.IO.Naming
{
    class TextPatternItem : PatternItem
    {
        public override string Keyword => "Text";

        public TextPatternItem(string propertyName)
            : base(propertyName)
        { }

        public override string TextRepresentation => PropertyName;
        public override bool IsValid => !string.IsNullOrEmpty(PropertyName) && !PropertyName.ContainsInvalidChars();
        public override string FormatMetadata(FileMetadata metadata) => PropertyName.SanitizeFileName(string.Empty);
    }
}
