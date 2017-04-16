using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HolzShots.Composition
{
    public abstract class PluginManager<T> : IDisposable
    {
        private const string AssemblyFilter = "*.dll";

        [ImportMany]
        public IList<Lazy<T, IPluginMetadata>> Plugins { get; private set; } = new List<Lazy<T, IPluginMetadata>>();

        private readonly AggregateCatalog _aggregate = new AggregateCatalog();
        private readonly CompositionContainer _container;

        public string PluginDirectory { get; }
        public bool Loaded { get; private set; } = false;

        public PluginManager(string pluginDirectory)
        {
            PluginDirectory = pluginDirectory;
            _container = new CompositionContainer(_aggregate, CompositionOptions.DisableSilentRejection);
        }

        public async Task Load()
        {
            Debug.Assert(!Loaded);

            var ags = _aggregate.Catalogs;
            ags.Clear();

            Debug.Assert(ags != null);
            Debug.Assert(ags.Count == 0);

            // TODO: Do not load ALL Assemblies involved?
            var asms = new[] {
                Assembly.GetCallingAssembly(),
                Assembly.GetExecutingAssembly(),
                Assembly.GetEntryAssembly()
            }.Distinct();

            foreach (var asm in asms)
                ags.Add(new AssemblyCatalog(asm));

            try
            {
                // TODO: This can be made better.
                if (!(await Task.Run(() => Directory.Exists(PluginDirectory)).ConfigureAwait(false)))
                    await Task.Run(() => Directory.CreateDirectory(PluginDirectory)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new PluginLoadingFailedException(e);
            }

            _aggregate.Catalogs.Add(new DirectoryCatalog(PluginDirectory, AssemblyFilter));
            try
            {
                _container.ComposeParts(this);
            }
            catch (Exception e)
            {
                throw new PluginLoadingFailedException(e);
            }

            Loaded = true;
        }

        public IReadOnlyList<IPluginMetadata> GetMetadata()
        {
            Debug.Assert(Loaded);
            var pls = Plugins;
            Debug.Assert(pls != null);

            var res = new List<IPluginMetadata>(pls.Count);
            foreach (var p in pls)
            {
                Debug.Assert(p.Metadata != null);
                Debug.Assert(p.Metadata is IPluginMetadata);
                Debug.Assert(typeof(IPluginMetadata).IsAssignableFrom(p.Metadata.GetType()));

                res.Add(p.Metadata as IPluginMetadata);
            }
            return res;
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _container.Dispose();
                    _aggregate.Dispose();
                }
                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
