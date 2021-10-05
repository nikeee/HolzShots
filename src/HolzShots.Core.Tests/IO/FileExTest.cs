using HolzShots.IO;
using Xunit;

namespace HolzShots.Tests.IO
{
    public class FileExTest
    {
        [Theory]
        [InlineData("a", 1, "a (1)")]
        [InlineData("a", 2, "a (2)")]
        [InlineData("a (1)", 1, "a (1) (1)")]
        [InlineData("a (1)", 2, "a (1) (2)")]
        [InlineData("a.png", 1, "a (1).png")]
        [InlineData("a.png", 2, "a (2).png")]
        [InlineData("a (1).png", 1, "a (1) (1).png")]
        [InlineData("a (1).png", 2, "a (1) (2).png")]
        [InlineData(@"C:\test\a", 1, @"C:\test\a (1)")]
        [InlineData(@"C:\test\a", 2, @"C:\test\a (2)")]
        [InlineData(@"C:\test\a (1)", 1, @"C:\test\a (1) (1)")]
        [InlineData(@"C:\test\a (1)", 2, @"C:\test\a (1) (2)")]
        [InlineData(@"C:\test\a.png", 1, @"C:\test\a (1).png")]
        [InlineData(@"C:\test\a.png", 2, @"C:\test\a (2).png")]
        [InlineData(@"C:\test\a (1).png", 1, @"C:\test\a (1) (1).png")]
        [InlineData(@"C:\test\a (1).png", 2, @"C:\test\a (1) (2).png")]
        [InlineData(@"test\a", 1, @"test\a (1)")]
        [InlineData(@"test\a", 2, @"test\a (2)")]
        [InlineData(@"test\a (1)", 1, @"test\a (1) (1)")]
        [InlineData(@"test\a (1)", 2, @"test\a (1) (2)")]
        [InlineData(@"test\a.png", 1, @"test\a (1).png")]
        [InlineData(@"test\a.png", 2, @"test\a (2).png")]
        [InlineData(@"test\a (1).png", 1, @"test\a (1) (1).png")]
        [InlineData(@"test\a (1).png", 2, @"test\a (1) (2).png")]
        public void DeriveNameTest(string fileName, int counter, string expected)
        {
            var actual = FileEx.DeriveFileNameWithCounter(fileName, counter);
            Assert.Equal(expected, actual);
        }
    }
}
