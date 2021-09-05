using System.Collections.Immutable;
using System.Diagnostics;
using HolzShots.IO;
using HolzShots.Net;
using HolzShots.Net.Custom;
using Newtonsoft.Json;

namespace HolzShots.Composition
{
    public class CustomUploaderSource : IUploaderSource
    {
        private readonly string _customUploadersDirectory;
        private IReadOnlyDictionary<UploaderMeta, CustomUploader> _customUploaders = ImmutableDictionary<UploaderMeta, CustomUploader>.Empty;

        public bool Loaded { get; private set; }

        public CustomUploaderSource(string customUploadersDirectory)
        {
            if (string.IsNullOrEmpty(customUploadersDirectory))
                throw new ArgumentNullException(nameof(customUploadersDirectory));
            _customUploadersDirectory = customUploadersDirectory;
        }

        public async Task Load()
        {
            Debug.Assert(!Loaded);

            try
            {
                HolzShotsPaths.EnsureDirectory(_customUploadersDirectory);
                // var res = new Dictionary<UploaderMeta, CustomUploader>();

                var res = ImmutableDictionary.CreateBuilder<UploaderMeta, CustomUploader>();

                foreach (var jsonFile in Directory.EnumerateFiles(_customUploadersDirectory, HolzShotsPaths.CustomUploadersFilePattern))
                {
                    using var reader = File.OpenText(jsonFile);
                    var jsonStr = await reader.ReadToEndAsync().ConfigureAwait(false);

                    // TODO: Catch parsing errors
                    var uploader = JsonConvert.DeserializeObject<CustomUploaderSpec>(jsonStr, JsonConfig.JsonSettings);

                    // TODO: Aggregate errors of invalid files (and display them to the user)
                    Debug.Assert(uploader != null);

                    if (CustomUploader.TryLoad(uploader, out var loadedUploader))
                    {
                        Debug.Assert(loadedUploader != null);
                        res.Add(uploader.Meta, loadedUploader);
                    }
                }

                _customUploaders = res.ToImmutable();
            }
            catch (FileNotFoundException)
            {
                _customUploaders = ImmutableDictionary<UploaderMeta, CustomUploader>.Empty;
            }
            finally
            {
                Loaded = true;
            }
        }

        public UploaderEntry? GetUploaderByName(string name)
        {
            var cupls = _customUploaders;
            Debug.Assert(cupls != null);

            foreach (var (info, uploader) in _customUploaders)
            {
                if (info == null)
                    continue;
                Debug.Assert(info != null);
                Debug.Assert(!string.IsNullOrEmpty(info.Name));
                Debug.Assert(uploader != null);

                if (Uploader.HasEqualName(info.Name, name))
                    return new UploaderEntry(info, uploader);
            }

            return null;
        }
        public IReadOnlyList<string> GetUploaderNames() => GetMetadata().Select(i => i.Name).ToList();
        public IReadOnlyList<IPluginMetadata> GetMetadata() => _customUploaders.Select(kv => kv.Key).ToList();
    }

}
