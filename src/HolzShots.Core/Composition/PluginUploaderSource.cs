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
        {
        }

        public (IPluginMetadata metadata, Uploader uploader)? GetUploaderByName(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            Debug.Assert(Loaded);

            var pls = Plugins;
            Debug.Assert(pls != null);

            // Look for the uploader in installed plugins (dlls)
            foreach (var p in pls)
            {
                Debug.Assert(p.Metadata != null);
                Debug.Assert(p.Metadata is ICompileTimePluginMetadata);
                Debug.Assert(typeof(ICompileTimePluginMetadata).IsAssignableFrom(p.Metadata.GetType()));

                var m = new PluginMetadata(p.Metadata as ICompileTimePluginMetadata);
                try
                {
                    if (m.Name == name)
                        return (metadata: m, uploader: p.Value);
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
            var meta = GetMetadata();
            Debug.Assert(meta != null);
            return meta.Select(i => i.Name).ToList();
        }
    }
}
