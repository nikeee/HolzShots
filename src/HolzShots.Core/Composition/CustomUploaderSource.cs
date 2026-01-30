using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using HolzShots.IO;
using HolzShots.Net;
using HolzShots.Net.Custom;
using System.Linq;

namespace HolzShots.Composition;

public class CustomUploaderSource(string customUploadersDirectory) : IUploaderSource
{
    private readonly string _customUploadersDirectory = string.IsNullOrEmpty(customUploadersDirectory)
        ? throw new ArgumentNullException(nameof(customUploadersDirectory))
        : customUploadersDirectory;

    private IReadOnlyDictionary<UploaderMeta, CustomUploader> _customUploaders = ImmutableDictionary<UploaderMeta, CustomUploader>.Empty;
    private IReadOnlyDictionary<string, CustomUploaderSpec> _uploadersFiles = ImmutableDictionary<string, CustomUploaderSpec>.Empty;

    public bool Loaded { get; private set; }

    /// <summary> Can also be safely called for reloads. </summary>
    public async Task Load()
    {
        try
        {
            HolzShotsPaths.EnsureDirectory(_customUploadersDirectory);
            // var res = new Dictionary<UploaderMeta, CustomUploader>();

            var res = ImmutableDictionary.CreateBuilder<UploaderMeta, CustomUploader>();
            var files = ImmutableDictionary.CreateBuilder<string, CustomUploaderSpec>();

            foreach (var jsonFile in Directory.EnumerateFiles(_customUploadersDirectory, HolzShotsPaths.CustomUploadersFilePattern))
            {
                using var reader = File.OpenText(jsonFile);
                var jsonStr = await reader.ReadToEndAsync().ConfigureAwait(false);

                // TODO: Catch parsing errors
                var uploader = JsonSerializer.Deserialize<CustomUploaderSpec>(jsonStr, JsonConfig.JsonOptions);

                // TODO: Aggregate errors of invalid files (and display them to the user)
                Debug.Assert(uploader is not null);

                if (CustomUploader.TryLoad(uploader, out var loadedUploader))
                {
                    Debug.Assert(loadedUploader is not null);
                    res.Add(uploader.Meta, loadedUploader);
                    files.Add(jsonFile, uploader);
                }
            }

            _uploadersFiles = files.ToImmutable();
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
        Debug.Assert(cupls is not null);

        foreach (var (info, uploader) in _customUploaders)
        {
            if (info == null)
                continue;
            Debug.Assert(info is not null);
            Debug.Assert(!string.IsNullOrEmpty(info.Name));
            Debug.Assert(uploader is not null);

            if (Uploader.HasEqualName(info.Name, name))
                return new UploaderEntry(info, uploader);
        }

        return null;
    }
    public IReadOnlyList<string> GetUploaderNames() => GetMetadata().Select(i => i.Name).ToList();
    public IReadOnlyList<IPluginMetadata> GetMetadata() => _customUploaders.Select(kv => kv.Key).ToList();
    public IReadOnlyList<(string filePath, CustomUploaderSpec spec)> GetCustomUploaderSpecs() => _uploadersFiles.Select(x => (x.Key, x.Value)).ToList();
}
