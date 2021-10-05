using Xunit;
using HolzShots.IO.Naming;

namespace HolzShots.Core.Tests.IO.Naming
{
    public class FileNamePatternTest
    {
        [Theory]
        [InlineData("lol")]
        [InlineData("<size:width><size:height>")]
        [InlineData("<size:width>lol")]
        [InlineData("<date:ISO>lol")]
        public void ParseTest(string pattern)
        {
            // var data = FileMetadata.DemoMetadata;
            var res = FileNamePattern.Parse(pattern);
            Assert.NotNull(res);
            Assert.NotEmpty(res.Tokens);
            Assert.False(res.IsEmpty);
        }

        [Theory]
        [InlineData("")]
        public void ParseEmpty(string pattern)
        {
            var res = FileNamePattern.Parse(pattern);
            Assert.NotNull(res);
            Assert.Empty(res.Tokens);
            Assert.True(res.IsEmpty);
        }

        [Theory]
        [InlineData('\"')]
        [InlineData('<')]
        [InlineData('>')]
        [InlineData('|')]
        [InlineData('\0')]
        [InlineData((char)1)]
        [InlineData((char)2)]
        [InlineData((char)3)]
        [InlineData((char)4)]
        [InlineData((char)5)]
        [InlineData((char)6)]
        [InlineData((char)7)]
        [InlineData((char)8)]
        [InlineData((char)9)]
        [InlineData((char)10)]
        [InlineData((char)11)]
        [InlineData((char)12)]
        [InlineData((char)13)]
        [InlineData((char)14)]
        [InlineData((char)15)]
        [InlineData((char)16)]
        [InlineData((char)17)]
        [InlineData((char)18)]
        [InlineData((char)19)]
        [InlineData((char)20)]
        [InlineData((char)21)]
        [InlineData((char)22)]
        [InlineData((char)23)]
        [InlineData((char)24)]
        [InlineData((char)25)]
        [InlineData((char)26)]
        [InlineData((char)27)]
        [InlineData((char)28)]
        [InlineData((char)29)]
        [InlineData((char)30)]
        [InlineData((char)31)]
        [InlineData(':')]
        [InlineData('*')]
        [InlineData('?')]
        [InlineData('\\')]
        [InlineData('/')]
        public void InvalidPathChars(char pattern)
        {
            Assert.Throws<PatternSyntaxException>(() => FileNamePattern.Parse(pattern.ToString()));
            Assert.Throws<PatternSyntaxException>(() => FileNamePattern.Parse("pre" + pattern.ToString()));
            Assert.Throws<PatternSyntaxException>(() => FileNamePattern.Parse(pattern.ToString() + "post"));
            Assert.Throws<PatternSyntaxException>(() => FileNamePattern.Parse("pre" + pattern.ToString() + "post"));
        }
    }
}
