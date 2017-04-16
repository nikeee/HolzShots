using HolzShots.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HolzShots.Net.Custom;

namespace HolzShots.Composition
{
    public class UploaderPluginManager : PluginManager<Uploader>
    {
        private readonly IReadOnlyDictionary<UploaderInfo, CustomUploader> _customUpoaders;

        public UploaderPluginManager(string pluginDirectory)
            : base(pluginDirectory)
        {
        }

        public (IPluginMetadata metadata, Uploader uploader)? GetUploaderByName(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            Debug.Assert(Loaded);

            var pls = Plugins;
            var cupls = _customUpoaders;
            Debug.Assert(pls != null);
            Debug.Assert(cupls != null);

            // Look for the uploader in instaled plugins (dlls)
            foreach (var p in pls)
            {
                Debug.Assert(p.Metadata != null);
                Debug.Assert(p.Metadata is IPluginMetadata);
                Debug.Assert(typeof(IPluginMetadata).IsAssignableFrom(p.Metadata.GetType()));

                var m = p.Metadata as IPluginMetadata;
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

            // If not found, look in custom uploaders (json files)
            foreach (var (info, uploader) in _customUpoaders)
            {
                if (info == null)
                    continue;
                Debug.Assert(info != null);
                Debug.Assert(!string.IsNullOrEmpty(info.Name));
                Debug.Assert(uploader != null);
                if (info.Name == name)
                    return (metadata: info, uploader: uploader);
            }

            return null;
        }

        public IReadOnlyList<string> GetUploaderNames()
        {
            var meta = GetPluginMetadata();
            Debug.Assert(meta != null);
            var customMeta = GetCustomUploaderMetadata();
            Debug.Assert(customMeta != null);
            var concat = meta.Concat(customMeta);
            return new List<string>(concat.Select(i => i.Name));
        }

        private IEnumerable<IPluginMetadata> GetCustomUploaderMetadata() => _customUpoaders.Select(kv => kv.Key);
    }
}
