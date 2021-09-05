using System.Drawing;

namespace HolzShots.IO.Naming
{
    public struct FileMetadata : IEquatable<FileMetadata>
    {
        public static FileMetadata DemoMetadata { get; } = new FileMetadata(new DateTime(2010, 6, 14, 21, 47, 33), new MemSize(2, MemSizeUnit.MibiByte), new Size(800, 600));

        public DateTime Timestamp { get; }
        public MemSize FileSize { get; }
        public Size Dimensions { get; }

        public FileMetadata(DateTime timestamp, MemSize fileSize, Size dimensions)
        {
            Timestamp = timestamp;
            FileSize = fileSize;
            Dimensions = dimensions;
        }

        public override bool Equals(object? obj) => obj is FileMetadata m && m == this;
        public bool Equals(FileMetadata other) => other == this;

        public override int GetHashCode() => HashCode.Combine(Timestamp, FileSize, Dimensions);

        public static bool operator ==(FileMetadata left, FileMetadata right) => left.FileSize == right.FileSize && left.Timestamp == right.Timestamp && left.Dimensions == right.Dimensions;

        public static bool operator !=(FileMetadata left, FileMetadata right) => !(left == right);
    }
}
