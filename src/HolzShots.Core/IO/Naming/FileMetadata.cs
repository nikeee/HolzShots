using System.Drawing;

namespace HolzShots.IO.Naming;

public record FileMetadata(DateTime Timestamp, MemSize FileSize, Size Dimensions)
{
    public static FileMetadata DemoMetadata { get; } = new FileMetadata(new DateTime(2010, 6, 14, 21, 47, 33), new MemSize(2, MemSizeUnit.MibiByte), new Size(800, 600));
}
