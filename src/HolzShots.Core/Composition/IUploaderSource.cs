using HolzShots.Net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolzShots.Composition
{
    public interface IUploaderSource
    {
        bool Loaded { get; }
        (IPluginMetadata metadata, Uploader uploader)? GetUploaderByName(string name);
        IReadOnlyList<string> GetUploaderNames();
        IReadOnlyList<IPluginMetadata> GetMetadata();
        Task Load();
    }
}
