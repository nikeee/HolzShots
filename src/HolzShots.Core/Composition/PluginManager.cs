using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public Task Load()
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
                var assembly = Assembly.LoadFrom(pluginDll);
                if (assembly != null)
                    config.WithAssembly(assembly);
            }

            try
            {
                using var container = config.CreateContainer();

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
            catch (Exception)
            {
                MessageBox.Show("Error loading plugins. Try updating them, they seem to be incompatible.");
            }

            Loaded = true;

            return Task.CompletedTask;
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
