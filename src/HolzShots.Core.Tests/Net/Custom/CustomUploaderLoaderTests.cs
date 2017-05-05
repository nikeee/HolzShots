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
            Assert.NotNull(result.UploaderInfo);
            Assert.NotNull(result.UploaderInfo.Meta);
            Assert.NotNull(result.UploaderInfo.Uploader);
            Assert.NotNull(result.UploaderInfo.SchemaVersion);

            Assert.True(result.UploaderInfo.Meta.Version == new Semver.SemVersion(1, 0, 0));
        }
    }
}
