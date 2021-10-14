using System.Collections.Generic;
using HolzShots.Net;

namespace HolzShots.Composition
{
    public interface IUploaderSource
    {
        bool Loaded { get; }
        UploaderEntry? GetUploaderByName(string name);
        IReadOnlyList<string> GetUploaderNames();
        IReadOnlyList<IPluginMetadata> GetMetadata();
        Task Load();
    }

    public record UploaderEntry(IPluginMetadata Metadata, Uploader Uploader);
}
