using HolzShots.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HolzShots.Net.Custom;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HolzShots.Composition
{
    public class CustomUploaderSource : IUploaderSource
    {
        private readonly string _customUploadersFileName;
        private IReadOnlyDictionary<UploaderInfo, CustomUploader> _customUploaders = null;

        public bool Loaded { get; private set; }

        public CustomUploaderSource(string customUploadersFileName)
        {
            if (string.IsNullOrEmpty(customUploadersFileName))
                throw new ArgumentNullException(nameof(customUploadersFileName));
            _customUploadersFileName = customUploadersFileName;
        }

        public async Task Load()
        {
            Debug.Assert(!Loaded);

            try
            {
                using (var reader = System.IO.File.OpenText(_customUploadersFileName))
                {
                    var jsonStr = await reader.ReadToEndAsync();
                    var loaded = JsonConvert.DeserializeObject<CustomUploadersFile>(jsonStr, JsonConfig.JsonSettings);
                    Debug.Assert(loaded != null);

                    var uploaders = loaded.Uploaders;
                    Debug.Assert(uploaders != null);

                    var res = new Dictionary<UploaderInfo, CustomUploader>();
                    foreach (var u in uploaders)
                    {
                        if (CustomUploader.TryLoad(u, out var customUploader))
                        {
                            Debug.Assert(customUploader != null);
                            res.Add(u.Info, customUploader);
                        }
                    }

                    _customUploaders = res;
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                _customUploaders = new Dictionary<UploaderInfo, CustomUploader>();
            }
            finally
            {
                Loaded = true;
            }
        }

        public (IPluginMetadata metadata, Uploader uploader)? GetUploaderByName(string name)
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
                if (info.Name == name)
                    return (metadata: info, uploader: uploader);
            }

            return null;
        }
        public IReadOnlyList<string> GetUploaderNames() => GetMetadata().Select(i => i.Name).ToList();
        public IReadOnlyList<IPluginMetadata> GetMetadata() => _customUploaders.Select(kv => kv.Key).ToList();
    }
}
