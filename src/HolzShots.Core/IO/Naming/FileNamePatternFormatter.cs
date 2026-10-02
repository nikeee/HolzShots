
namespace HolzShots.IO.Naming;

public static class FileNamePatternFormatter
{
    /// <summary>Formats a file name from the given pattern and metadata.</summary>
    /// <exception cref="System.ArgumentNullException"><paramref name="pattern"/> is null.</exception>
    /// <exception cref="HolzShots.IO.Naming.PatternSyntaxException">The pattern is invalid or empty.</exception>
    public static string GetFileNameFromPattern(FileMetadata info, string pattern)
    {
        ArgumentNullException.ThrowIfNull(pattern);

        var parsedPattern = FileNamePattern.Parse(pattern);
        if (parsedPattern.IsEmpty)
            throw new PatternSyntaxException();

        return parsedPattern.FormatMetadata(info);
    }
}
