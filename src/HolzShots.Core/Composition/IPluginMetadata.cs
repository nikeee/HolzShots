using Semver;

namespace HolzShots.Composition;

public interface IPluginMetadata
{
    string Name { get; }
    string Author { get; }
    SemVersion Version { get; }

    string? Website { get; }
    string? BugsUrl { get; }
    string? Contact { get; }
    string? Description { get; }
}
