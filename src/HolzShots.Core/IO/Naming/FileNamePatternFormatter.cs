using HolzShots.Drawing;
using System;
using System.Drawing.Imaging;

namespace HolzShots.IO.Naming
{
    public static class FileNamePatternFormatter
    {
        public static (string name, uint nextSeqNr) GetFileNameFromPattern(Screenshot screenshot, ImageFormat format, string pattern, uint sequenceNumber)
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

            var info = new FileMetadata(screenshot.Timestamp, fileSize, sequenceNumber, screenshot.Size);
            var nextNr = parsedPattern.UsesSequenceNumber ? sequenceNumber + 1 : sequenceNumber;
            return (name: parsedPattern.FormatMetadata(info), nextSeqNr: nextNr);
        }
    }
}
