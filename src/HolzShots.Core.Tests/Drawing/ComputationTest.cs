using System.IO;
using HolzShots.Core.Tests;
using Xunit;

namespace HolzShots.Tests.Drawing
{
    public class ComputationTest
    {
        [Theory]
        [FileByteArrayContentData("Files/0-white.png", "Files/0-black.png", "Files/0-expected.png")]
        public void CheckAlphaChannelComputation(byte[] white, byte[] black, byte[] expected)
        {
            Assert.True(white.Length > 0);
            Assert.True(black.Length > 0);
            Assert.True(expected.Length > 0);
        }
    }
}
