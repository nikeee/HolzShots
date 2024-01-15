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
        public void ComputeAlphaChannel(Stream whiteData, Stream blackData, Stream expectedData)
        {
            Assert.True(whiteData.Length > 0);
            Assert.True(blackData.Length > 0);
            Assert.True(expectedData.Length > 0);

            var white = new Bitmap(whiteData);
            var black = new Bitmap(blackData);
            var expected = new Bitmap(expectedData);

            // Sanity check for input data
            Assert.Equal(white.Width, black.Width);
            Assert.Equal(white.Height, black.Height);
            Assert.Equal(expected.Height, black.Height);

            var actual = new Bitmap(white.Width, white.Height);
            Computation.ComputeAlphaChannel(white, black, ref actual);

            AssertImage(expected, actual);
        }

        [Theory]
        [FileStreamContentData("Files/0-white.png", "Files/0-black.png", "Files/0-expected.png")]
        public void ComputeAlphaChannel2(Stream whiteData, Stream blackData, Stream expectedData)
        {
            Assert.True(whiteData.Length > 0);
            Assert.True(blackData.Length > 0);
            Assert.True(expectedData.Length > 0);

            var white = new Bitmap(whiteData);
            var black = new Bitmap(blackData);
            var expected = new Bitmap(expectedData);

            // Sanity check for input data
            Assert.Equal(white.Width, black.Width);
            Assert.Equal(white.Height, black.Height);
            Assert.Equal(expected.Height, black.Height);

            var actual = new Bitmap(white.Width, white.Height);
            Computation.ComputeAlphaChannel2(white, black, ref actual);

            AssertImage(expected, actual);
        }

        private static void AssertImage(Bitmap expected, Bitmap actual)
        {
            Assert.Equal(expected.Width, actual.Width);
            Assert.Equal(expected.Height, actual.Height);

            for (var x = 0; x < expected.Width; ++x)
            {
                for (var y = 0; y < expected.Height; ++y)
                {
                    Assert.Equal(expected.GetPixel(x, y), actual.GetPixel(x, y));
                }
            }
        }
    }
}
