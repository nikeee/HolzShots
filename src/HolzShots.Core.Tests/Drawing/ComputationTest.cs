using System.Drawing;
using System.IO;
using HolzShots.Core.Tests;
using HolzShots.Drawing;
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

            // Sanity check for input data
            Assert.Equal(w.Width, b.Width);
            Assert.Equal(w.Height, b.Height);
            Assert.Equal(e.Height, b.Height);

            var actual = new Bitmap(w.Width, w.Height);
            Computation.ComputeAlphaChannel(w, b, ref actual);

            Assert.Equal(e.Width, actual.Width);
            Assert.Equal(e.Height, actual.Height);

            for (var x = 0; x < w.Width; ++x)
            {
                for (var y = 0; y < w.Height; ++y)
                {
                    Assert.Equal(e.GetPixel(x, y), actual.GetPixel(x, y));
                }
            }
        }
    }
}
