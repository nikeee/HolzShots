using HolzShots.Drawing;
using System;
using System.Drawing.Imaging;

namespace HolzShots.IO.Naming
{
    public static class FileNamePatternFormatter
    {
        public static string GetFileNameFromPattern(Screenshot screenshot, ImageFormat format, string pattern)
        {
            if (screenshot == null)
                throw new ArgumentNullException(nameof(screenshot));
            if (format == null)
                throw new ArgumentNullException(nameof(format));
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));

            var parsedPattern = FileNamePattern.Parse(pattern);
            if (parsedPattern.IsEmpty)
                throw new PatternSyntaxException();

            var fileSize = screenshot.Image.EstimateFileSize(format);

            var info = new FileMetadata(screenshot.Timestamp, fileSize, screenshot.Size);
            return parsedPattern.FormatMetadata(info);
        }
    }
}
