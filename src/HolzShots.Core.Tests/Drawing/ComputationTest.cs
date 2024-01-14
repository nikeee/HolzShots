using System.Drawing;
using System.IO;
using HolzShots.Core.Tests;
using Xunit;

namespace HolzShots.Tests.Drawing
{
    public class ComputationTest
    {
        [Theory]
        [FileStreamContentData("Files/0-white.png", "Files/0-black.png", "Files/0-expected.png")]
        public void CheckAlphaChannelComputation(Stream white, Stream black, Stream expected)
        {
            Assert.True(white.Length > 0);
            Assert.True(black.Length > 0);
            Assert.True(expected.Length > 0);

            var w = new Bitmap(white);
            var b = new Bitmap(black);
            var e = new Bitmap(expected);
        }
    }
}
