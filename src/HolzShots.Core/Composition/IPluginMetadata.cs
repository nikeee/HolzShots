
namespace HolzShots.Composition
{
    public interface IPluginMetadata
    {
        string Name { get; }
        string Author { get; }
        string Version { get; }

        string Url { get; }
        string BugsUrl { get; }
        string Contact { get; }
        string Description { get; }
    }
}
