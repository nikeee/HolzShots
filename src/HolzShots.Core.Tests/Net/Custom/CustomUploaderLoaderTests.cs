using HolzShots.Net.Custom;
using Xunit;

namespace HolzShots.Core.Tests.Net.Custom
{
    public class CustomUploaderLoaderTests
    {

        [Theory]
        [FileStringContentData("Files/DirectUpload.net.hsjson")]
        [FileStringContentData("Files/FotosHochladen.hsjson")]
        public void ValidateTest(string content)
        {
            var parseResult = CustomUploaderLoader.TryLoad(content, out var result);
            Assert.True(parseResult);
            Assert.NotNull(result);
            Assert.NotNull(result.Info);
            Assert.NotNull(result.Uploader);
            Assert.NotNull(result.SchemaVersion);
        }
    }
}
