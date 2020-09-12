using System.Diagnostics;
using System.Text;

namespace HolzShots.IO.Naming
{
    public abstract class PatternItem
    {
        public const char TokenStartChar = '<';
        public const char TokenEndChar = '>';
        public const char PropertySeparatorChar = ':';

        public string PropertyName { get; }
        public abstract string Keyword { get; }
        public abstract string TextRepresentation { get; }
        public abstract bool IsValid { get; }

        public PatternItem(string propertyName) => PropertyName = propertyName;

        public abstract string FormatMetadata(FileMetadata metadata);

        public override string ToString() => TextRepresentation;

        internal static (string name, string prop) Parse(string value, ref int currentIndex)
        {
            Debug.Assert(value != null);
            Debug.Assert(currentIndex >= 0);
            Debug.Assert(currentIndex < value.Length);
            Debug.Assert(value[currentIndex] == TokenStartChar);

            ++currentIndex; // skip <
            var itemStart = currentIndex;
            var itemSb = new StringBuilder();
            for (; currentIndex < value.Length; ++currentIndex)
            {
                char c = value[currentIndex];
                if (c == PropertySeparatorChar || c == TokenEndChar)
                    break;
                itemSb.Append(c);
            }

            if (itemSb.Length <= 0)
                throw new PatternSyntaxException();

            if (currentIndex >= value.Length)
                return (name: itemSb.ToString(), prop: null);

            if (value[currentIndex] != PropertySeparatorChar && value[currentIndex] != TokenEndChar)
                throw new PatternSyntaxException();

            Debug.Assert(value[currentIndex] == PropertySeparatorChar || value[currentIndex] == TokenEndChar);
            if (value[currentIndex] == PropertySeparatorChar)
            {
                ++currentIndex; // Skip :
                var propSb = new StringBuilder();
                for (; currentIndex < value.Length; ++currentIndex)
                {
                    char c = value[currentIndex];
                    if (c == TokenEndChar)
                        break;
                    propSb.Append(c);
                }

                if (currentIndex >= value.Length)
                    return (name: itemSb.ToString(), prop: propSb.ToString());

                if (value[currentIndex] != TokenEndChar)
                    throw new PatternSyntaxException();
                ++currentIndex;
                return (name: itemSb.ToString(), prop: propSb.ToString());
            }

            if (value[currentIndex] != TokenEndChar)
                throw new PatternSyntaxException();
            ++currentIndex;
            return (name: itemSb.ToString(), prop: null);
        }
    }
}
