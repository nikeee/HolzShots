
namespace HolzShots.IO.Naming;

public static class FileNamePatternFormatter
{
    public static string GetFileNameFromPattern(FileMetadata info, string pattern)
    {
        ArgumentNullException.ThrowIfNull(pattern);

        var parsedPattern = FileNamePattern.Parse(pattern);
        if (parsedPattern.IsEmpty)
            throw new PatternSyntaxException();

        return parsedPattern.FormatMetadata(info);
    }
}
