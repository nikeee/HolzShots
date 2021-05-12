using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HolzShots.IO.Naming
{
    public class FileNamePattern
    {
        public IReadOnlyList<PatternItem> Tokens { get; }

        private static readonly Dictionary<string, Func<string, PatternItem>> _availablePatterns = new Dictionary<string, Func<string, PatternItem>>
        {
            ["text"] = str => new TextPatternItem(str),
            ["image"] = str => new ImagePatternItem(str),
            ["date"] = str => new DatePatternItem(str),
        };

        public FileNamePattern() : this(null) { }
        public FileNamePattern(IReadOnlyList<PatternItem>? tokens)
        {
            Tokens = tokens ?? Array.Empty<PatternItem>();
        }

        public bool IsEmpty => Tokens.Count == 0;

        public string FormatMetadata(FileMetadata metadata)
        {
            var sb = new StringBuilder();
            foreach (var item in Tokens)
                sb.Append(item.FormatMetadata(metadata));
            return sb.ToString().SanitizeFileName();
        }

        public static FileNamePattern Parse(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (value.Length == 0)
                return new FileNamePattern();

            var tokens = new List<PatternItem>();
            var textSb = new StringBuilder();

            int currentIndex = 0;
            while (currentIndex < value.Length)
            {
                var c = value[currentIndex];
                switch (c)
                {
                    case PatternItem.TokenStartChar:
                        {
                            if (textSb.Length > 0)
                            {
                                var newToken = new TextPatternItem(textSb.ToString());
                                if (!newToken.IsValid)
                                    throw new PatternSyntaxException();
                                tokens.Add(newToken);
                                textSb = new StringBuilder();
                            }
                            var (tokenName, tokenProperty) = PatternItem.Parse(value, ref currentIndex);
                            Debug.Assert(!string.IsNullOrEmpty(tokenName));

                            if (!_availablePatterns.TryGetValue(tokenName.ToLowerInvariant(), out var ctor))
                                throw new PatternSyntaxException();

                            Debug.Assert(ctor != null);
                            var item = ctor(tokenProperty);

                            if (!item.IsValid)
                                throw new PatternSyntaxException();

                            tokens.Add(item);
                            break;
                        }
                    default:
                        ++currentIndex;
                        textSb.Append(c);
                        break;
                }
            }
            Debug.Assert(currentIndex == value.Length);

            if (textSb.Length > 0)
            {
                var newToken = new TextPatternItem(textSb.ToString());
                if (!newToken.IsValid)
                    throw new PatternSyntaxException();
                tokens.Add(newToken);
            }
            return new FileNamePattern(tokens);
        }
    }
}
