using System;
using System.Drawing;
using HolzShots.Common;

namespace HolzShots.IO.Naming
{
    public struct FileMetadata : IEquatable<FileMetadata>
    {
        public DateTime Timestamp { get; }
        public MemSize FileSize { get; }
        public uint SequenceNumber { get; }
        public Size Dimensions { get; }

        public FileMetadata(DateTime timestamp, MemSize fileSize, uint sequenceNumber, Size dimensions)
        {
            Timestamp = timestamp;
            FileSize = fileSize;
            SequenceNumber = sequenceNumber;
            Dimensions = dimensions;
        }

        public static FileMetadata DemoMetadata { get; } = new FileMetadata(new DateTime(2010, 6, 14, 21, 47, 33), new MemSize(2, MemSizeUnit.MibiByte), 1339, new Size(800, 600));

        public override bool Equals(object obj) => obj is FileMetadata m && m == this;
        public bool Equals(FileMetadata other) => other == this;

        public override int GetHashCode()
        {
            // See: https://stackoverflow.com/a/263416
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash * 16777619) ^ Timestamp.GetHashCode();
                hash = (hash * 16777619) ^ FileSize.GetHashCode();
                hash = (hash * 16777619) ^ SequenceNumber.GetHashCode();
                hash = (hash * 16777619) ^ Dimensions.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(FileMetadata left, FileMetadata right) => left.FileSize == right.FileSize && left.Timestamp == right.Timestamp && left.Dimensions == right.Dimensions && left.SequenceNumber == right.SequenceNumber;

        public static bool operator !=(FileMetadata left, FileMetadata right) => !(left == right);
    }
}
