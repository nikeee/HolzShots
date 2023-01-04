
namespace HolzShots;

public static class LibraryInformation
{
    public const string Name = ThisAssembly.Project.RootNamespace;

    // 1.0.0-beta.2+deadbeef
    // These info is filled by build script
    public const string VersionFormal = "%VERSION%"; // 1.0.0
    private const string ReleaseSuffix = "%RELEASE-SUFFIX%"; // -beta
    public const string CommitsSinceTag = "%COMMITS-SINCE-TAG%"; // .2
    public const string CommitId = "%COMMIT-ID%"; // +deadbeef
#if RELEASE && CI_BUILD
    public static readonly DateTime VersionDate = DateTime.ParseExact(ThisAssembly.Constants.BuildDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
#else
    public static readonly DateTime VersionDate = DateTime.Now;
#endif
    public const string SemanticVersion = VersionFormal + ReleaseSuffix + CommitsSinceTag + CommitId;

    public const string SiteUrl = ThisAssembly.Constants.Website;
    public const string SourceUrl = ThisAssembly.Metadata.RepositoryUrl;

    public const string IssuesUrl = SourceUrl + "/issues";
    public const string LicenseUrl = SourceUrl + "/blob/master/LICENSE";
}
