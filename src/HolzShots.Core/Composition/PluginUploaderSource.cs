using HolzShots.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HolzShots.Composition
{
    public class PluginUploaderSource : PluginManager<Uploader>, IUploaderSource
    {

        public PluginUploaderSource(string pluginDirectory)
            : base(pluginDirectory)
        { }

        public (IPluginMetadata metadata, Uploader uploader)? GetUploaderByName(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            Debug.Assert(Loaded);

            var pls = Plugins;
            Debug.Assert(pls != null);

            return pls
                .Where(p => Uploader.HasEqualName(p.metadata.Name, name))
                .Select(p => ((IPluginMetadata metadata, Uploader uploader)?)(new PluginMetadata(p.metadata), p.instance) /* cast is needed to make FirstOrDefault produce null instead of (null, null) */)
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
