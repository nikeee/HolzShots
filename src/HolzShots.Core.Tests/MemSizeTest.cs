using Xunit;

namespace HolzShots.Tests;

public class MemSizeTest
{
    [Theory]
    [InlineData(0L, "0 bytes")]
    [InlineData(1023L, "1023 bytes")]
    [InlineData(1536L, "1.5 KiB")]
    [InlineData(3L * 1024 * 1024 / 2, "1.5 MiB")]
    [InlineData(1024L * 1024 * 1024 * 1024, "1.0 TiB")]
    public void ToString_Binary(long bytes, string expected)
    {
        var formatted = new MemSize(bytes).ToString().Replace(',', '.');
        Assert.Equal(expected, formatted);
    }

    [Fact]
    public void Constructor_LargeUnits_DoNotOverflow()
    {
        Assert.Equal(1024L * 1024 * 1024 * 1024, new MemSize(1, MemSizeUnit.TebiByte).ByteCount);
        Assert.Equal(1000L * 1000 * 1000 * 1000, new MemSize(1, MemSizeUnit.TeraByte).ByteCount);
    }
}
