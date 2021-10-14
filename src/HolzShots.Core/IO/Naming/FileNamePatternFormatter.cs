
namespace HolzShots.IO.Naming
{
    public static class FileNamePatternFormatter
    {
        public static string GetFileNameFromPattern(FileMetadata info, string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));

            var parsedPattern = FileNamePattern.Parse(pattern);
            if (parsedPattern.IsEmpty)
                throw new PatternSyntaxException();

            return parsedPattern.FormatMetadata(info);
        }
    }
}
