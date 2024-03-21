using System.IO;

namespace HolzShots;

public static class StringExtensions
{
    private static readonly char[] _illegalFileNameChars = Path.GetInvalidFileNameChars();
    public static string SanitizeFileName(this string fileName) => SanitizeFileName(fileName, null);
    public static string SanitizeFileName(this string fileName, string? replaceWith)
    {
        ArgumentNullException.ThrowIfNull(fileName);
        if (fileName.Length == 0)
            return string.Empty;
        return string.Join(replaceWith ?? string.Empty, fileName.Split(_illegalFileNameChars));
    }
    public static bool ContainsInvalidChars(this string fileName)
    {
        ArgumentNullException.ThrowIfNull(fileName);
        if (fileName.Length == 0)
            return false;
        return fileName.IndexOfAny(_illegalFileNameChars) > -1;
    }
}
