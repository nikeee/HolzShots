
namespace HolzShots;

public static class LibraryInformation
{
    public const string Name = ThisAssembly.Project.RootNamespace;

    public const string SiteUrl = ThisAssembly.Constants.Website;
    public const string SourceUrl = ThisAssembly.Project.RepositoryUrl;

    public const string IssuesUrl = SourceUrl + "/issues";
    public const string LicenseUrl = SourceUrl + "/blob/master/LICENSE";
}
