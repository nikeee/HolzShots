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
    public record CustomUploaderSpec(SemVersion SchemaVersion, UploaderMeta Meta, UploaderConfig Uploader);

    [Serializable]
    public record UploaderMeta(
        SemVersion Version,
        string Name,
        string License,
        string Author,
        string? Contact = null,
        string? BugsUrl = null,
        string? UpdateUrl = null,
        string? Website = null,
        string? Description = null
    ) : IPluginMetadata;

    [Serializable]
    public record UploaderConfig(
        string FileFormName,
        string RequestUrl,
        ResponseParser ResponseParser,
        string FileName,
        IReadOnlyDictionary<string, string>? Headers = null,
        IReadOnlyDictionary<string, string>? PostParams = null,
        long? MaxFileSize = null,
        string Method = "POST"
    )
    {
        // private static readonly string[] ValidMethods = { "POST", "PUT" };
        public string GetEffectiveFileName(string fallback) => FileName ?? fallback;
    }

    [Serializable]
    public class ResponseParser
    {
        [IgnoreDataMember]
        [field: NonSerialized]
        public IReadOnlyList<string>? RegexPatterns { get; } = null;
        public IReadOnlyList<Regex>? ParsedRegexPatterns { get; }
        public string? UrlTemplate { get; }
        // public string Failure { get; } = null;

        [IgnoreDataMember]
        [field: NonSerialized]
        public UrlTemplateSpec? UrlTemplateSpec { get; }

        public ResponseParser(IReadOnlyList<string>? regexPatterns, string? urlTemplate)
        {
            RegexPatterns = regexPatterns;
            UrlTemplate = urlTemplate;
            ParsedRegexPatterns = regexPatterns?.Select(pattern => new Regex(pattern)).ToImmutableList();
            UrlTemplateSpec = urlTemplate == null
                ? null
                : UrlTemplateSpec.Parse(this, urlTemplate.AsSpan());
        }
    }
}
