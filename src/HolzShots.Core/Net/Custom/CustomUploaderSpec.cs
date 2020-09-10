using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using HolzShots.Composition;
using Semver;

namespace HolzShots.Net.Custom
{
    [Serializable]
    public class CustomUploaderSpec
    {
        public SemVersion SchemaVersion { get; }
        public UploaderMeta Meta { get; }
        public UploaderConfig Uploader { get; }

        public CustomUploaderSpec(SemVersion schemaVersion, UploaderMeta meta, UploaderConfig uploader)
        {
            SchemaVersion = schemaVersion;
            Meta = meta;
            Uploader = uploader;
        }
    }

    [Serializable]
    public class UploaderMeta : IPluginMetadata
    {
        public SemVersion Version { get; }
        public string Name { get; }

        public string Author { get; } = null;
        public string Contact { get; } = null;
        public string BugsUrl { get; } = null;
        public string UpdateUrl { get; } = null;
        public string Website { get; } = null;
        public string Description { get; } = null;
        public string License { get; } = null;

        public UploaderMeta(SemVersion version, string name, string author, string contact, string bugsUrl, string updateUrl, string website, string description, string license)
        {
            Version = version;
            Name = name;
            Author = author;
            Contact = contact;
            BugsUrl = bugsUrl;
            UpdateUrl = updateUrl;
            Website = website;
            Description = description;
            License = license;
        }
    }

    [Serializable]
    public class UploaderConfig
    {
        private static readonly string[] ValidMethods = { "POST", "PUT" };
        public string FileFormName { get; }
        public string RequestUrl { get; }
        public ResponseParser ResponseParser { get; }

        public string Method { get; } = "POST";
        public IReadOnlyDictionary<string, string> Headers { get; } = null;
        public IReadOnlyDictionary<string, string> PostParams { get; } = null;
        public long? MaxFileSize { get; } = null;
        public string FileName { get; } = null;

        public UploaderConfig(string fileFormName, string requestUrl, ResponseParser responseParser, string method, IReadOnlyDictionary<string, string> headers, IReadOnlyDictionary<string, string> postParams, long? maxFileSize, string fileName)
        {
            FileFormName = fileFormName;
            RequestUrl = requestUrl;
            ResponseParser = responseParser;
            Method = method;
            Headers = headers;
            PostParams = postParams;
            MaxFileSize = maxFileSize;
            FileName = fileName;
        }

        public string GetEffectiveFileName(string fallback) => FileName ?? fallback;
    }

    [Serializable]
    public class ResponseParser
    {
        [IgnoreDataMember]
        [field: NonSerialized]
        public IReadOnlyList<string> RegexPatterns { get; } = null;
        public IReadOnlyList<Regex> ParsedRegexPatterns { get; }
        public string UrlTemplate { get; }
        // public string Failure { get; } = null;

        [IgnoreDataMember]
        [field: NonSerialized]
        public UrlTemplateSpec UrlTemplateSpec { get; }

        public ResponseParser(IReadOnlyList<string> regexPatterns, string urlTemplate)
        {
            RegexPatterns = regexPatterns;
            UrlTemplate = urlTemplate;
            if (regexPatterns != null)
                ParsedRegexPatterns = regexPatterns.Select(pattern => new Regex(pattern)).ToImmutableList();

            UrlTemplateSpec = UrlTemplateSpec.Parse(this, urlTemplate.AsSpan());
        }
    }
}
