using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
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

        public IReadOnlyDictionary<ICompileTimePluginMetadata, T> Plugins { get; private set; } = new Dictionary<ICompileTimePluginMetadata, T>(0);

        public string PluginDirectory { get; }
        public bool Loaded { get; private set; } = false;

        public PluginManager(string pluginDirectory) => PluginDirectory = pluginDirectory;

        public async Task Load()
        {
            Debug.Assert(!Loaded);

            var config = new ContainerConfiguration();
            // HolzShots' own executables are also a source for plugin instances
            config.WithAssembly(Assembly.GetEntryAssembly());
            config.WithAssembly(typeof(PluginManager<T>).Assembly);

            try
            {
                DirectoryEx.EnsureDirectory(PluginDirectory);
            }
            catch (Exception e)
            {
                throw new PluginLoadingFailedException(e);
            }

            var pluginDlls = Directory.EnumerateFiles(PluginDirectory, AssemblyFilter, SearchOption.AllDirectories);
            foreach (var pluginDll in pluginDlls)
            {
                var assembly = await LoadAssemblyPlugin(pluginDll);
                if (assembly == null)
                    continue;

                config.WithAssembly(assembly);
            }

            var container = config.CreateContainer();

            var pluginInstances = container.GetExports<T>();

            var res = new Dictionary<ICompileTimePluginMetadata, T>(pluginInstances.Count);
            foreach (var instance in pluginInstances)
            {
                var instanceType = instance.GetType();
                res[]
            }

            Loaded = true;
        }

        private static Task<Assembly> LoadAssemblyPlugin(string path)
        {
            try
            {
                // Wrapping this (although this is not recommended practice) as loading assemblies from disk can take some time
                return Task.Run(() => Assembly.LoadFrom(path));
            }
            catch (Exception e)
            {
                Debugger.Break();
                // TODO: Log?
                return null;
            }
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
                Debug.Assert(p.Metadata is ICompileTimePluginMetadata);
                Debug.Assert(.IsAssignableFrom(p.Metadata.GetType()));

                var runtime = new PluginMetadata(p.Metadata as ICompileTimePluginMetadata);
                res.Add(runtime);
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
                }
                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
