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
            var parseResult = CustomUploader.TryParse(content, out var result);
            Assert.True(parseResult);
            Assert.NotNull(result);
            Assert.NotNull(result.CustomData);
            Assert.NotNull(result.CustomData.Info);
            Assert.NotNull(result.CustomData.Uploader);
            Assert.NotNull(result.CustomData.SchemaVersion);

            Assert.True(result.CustomData.Info.Version == new Semver.SemVersion(1, 0, 0));
        }
    }
}
