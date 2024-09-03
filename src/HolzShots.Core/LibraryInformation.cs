
namespace HolzShots;

public static class LibraryInformation
{
    public const string Name = ThisAssembly.Project.RootNamespace;

    // These info is filled by build script
    // 1.0.0-beta.2+deadbeef
    public const string SemanticVersion = "%VERSION%" + "%RELEASE-SUFFIX%" + "%COMMITS-SINCE-TAG%" + "%COMMIT-ID%";
    public static readonly DateTime VersionDate = DateTime.ParseExact(ThisAssembly.Constants.BuildDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

    public const string SiteUrl = ThisAssembly.Constants.Website;
    public const string SourceUrl = ThisAssembly.Project.RepositoryUrl;

    public const string IssuesUrl = SourceUrl + "/issues";
    public const string LicenseUrl = SourceUrl + "/blob/master/LICENSE";
}
