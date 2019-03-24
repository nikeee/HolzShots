using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HolzShots.IO;

namespace HolzShots.Composition
{
    public abstract class PluginManager<T>
    {
        private const string AssemblyFilter = "*.dll";

        public IReadOnlyCollection<(ICompileTimePluginMetadata metadata, T instance)> Plugins { get; private set; } = Array.Empty<(ICompileTimePluginMetadata, T)>();

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
                var assembly = await LoadAssemblyPlugin(pluginDll).ConfigureAwait(false);
                if (assembly == null)
                    continue;

                config.WithAssembly(assembly);
            }

            using (var container = config.CreateContainer())
            {
                var pluginInstances = container.GetExports<T>();

                var res = new List<(ICompileTimePluginMetadata, T)>();
                foreach (var instance in pluginInstances)
                {
                    var instanceType = instance.GetType();
                    var metadata = (PluginAttribute)instanceType.GetCustomAttribute(typeof(PluginAttribute));
                    res.Add((metadata, instance));
                }

                Plugins = res;
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
            return Plugins
                .Select(v => new PluginMetadata(v.metadata))
                .ToList();
        }
    }
}
