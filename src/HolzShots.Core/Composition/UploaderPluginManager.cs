using HolzShots.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HolzShots.Composition
{
    public class UploaderPluginManager : PluginManager<Uploader>
    {
        public UploaderPluginManager(string pluginDirectory)
            : base(pluginDirectory)
        {
        }

        public (IPluginMetadata metadata, Uploader uploader)? GetUploaderByName(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            Debug.Assert(Loaded);
            var pls = Plugins;
            Debug.Assert(pls != null);

            foreach (var p in pls)
            {
                Debug.Assert(p.Metadata != null);
                Debug.Assert(p.Metadata is IPluginMetadata);
                Debug.Assert(typeof(IPluginMetadata).IsAssignableFrom(p.Metadata.GetType()));

                var m = p.Metadata as IPluginMetadata;
                try
                {
                    if (m.Name == name)
                        return (metadata: m, uploader:  p.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Plugin failed to load: {ex.Message}");
                    Debugger.Break();
                }
            }
            return null;
        }

        public IReadOnlyList<string> GetUploaderNames()
        {
            var meta = GetPluginMetadata();
            Debug.Assert(meta != null);
            return new List<string>(meta.Select(i => i.Name));
        }
    }
}
