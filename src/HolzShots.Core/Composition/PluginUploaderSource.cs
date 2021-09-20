using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HolzShots.Net;

namespace HolzShots.Composition
{
    public class PluginUploaderSource : PluginManager<Uploader>, IUploaderSource
    {
        public PluginUploaderSource(string pluginDirectory)
            : base(pluginDirectory)
        { }

        public UploaderEntry? GetUploaderByName(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            Debug.Assert(Loaded);

            var pls = Plugins;
            Debug.Assert(pls != null);

            return pls
                .Where(p => Uploader.HasEqualName(p.metadata.Name, name))
                .Select(p => new UploaderEntry(new PluginMetadata(p.metadata), p.instance))
                .FirstOrDefault();
        }

        public IReadOnlyList<string> GetUploaderNames()
        {
            var meta = GetMetadata();
            Debug.Assert(meta != null);
            return meta.Select(i => i.Name).ToList();
        }
    }
}
