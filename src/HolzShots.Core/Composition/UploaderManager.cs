using System.Collections.Generic;
using System.Diagnostics;

namespace HolzShots.Composition
{
    public class UploaderManager : IUploaderSource
    {
        public bool Loaded => Plugins.Loaded && Customs.Loaded;
        public PluginUploaderSource Plugins { get; }
        public CustomUploaderSource Customs { get; }
        public UploaderManager(PluginUploaderSource plugins, CustomUploaderSource customs)
        {
            Plugins = plugins ?? throw new ArgumentNullException(nameof(plugins));
            Customs = customs ?? throw new ArgumentNullException(nameof(customs));
        }

        public Task Load() => Task.WhenAll(Plugins.Load(), Customs.Load());

        public UploaderEntry? GetUploaderByName(string name)
        {
            Debug.Assert(Plugins is not null);
            Debug.Assert(Customs is not null);

            // First, look up the installed plugins
            // If nothing found (null), take a look at installed json uploaders
            return Plugins.GetUploaderByName(name) ?? Customs.GetUploaderByName(name);
        }

        public IReadOnlyList<string> GetUploaderNames()
        {
            Debug.Assert(Plugins is not null);
            Debug.Assert(Customs is not null);
            var pn = Plugins.GetUploaderNames();
            Debug.Assert(pn is not null);
            var un = Customs.GetUploaderNames();
            Debug.Assert(un is not null);
            var res = new List<string>(pn.Count + un.Count);
            res.AddRange(pn);
            res.AddRange(un);
            return res;
        }

        public IReadOnlyList<IPluginMetadata> GetMetadata()
        {
            Debug.Assert(Plugins is not null);
            Debug.Assert(Customs is not null);
            var pn = Plugins.GetMetadata();
            Debug.Assert(pn is not null);
            var un = Customs.GetMetadata();
            Debug.Assert(un is not null);
            var res = new List<IPluginMetadata>(pn.Count + un.Count);
            res.AddRange(pn);
            res.AddRange(un);
            return res;
        }
    }
}
