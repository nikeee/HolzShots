using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using HolzShots.Composition;
using Semver;

namespace HolzShots.Net.Custom;

[Serializable]
public record CustomUploaderSpec(SemVersion SchemaVersion, UploaderMeta Meta, UploaderConfig Uploader) : IParsable<CustomUploaderSpec>
{
    public static CustomUploaderSpec Parse(string value, IFormatProvider? provider)
    {
        try
        {
            var res = JsonSerializer.Deserialize<CustomUploaderSpec>(value, JsonConfig.JsonOptions);
            return res ?? throw new FormatException("Unable to parse JSON value, result was null");
        }
        catch (JsonException ex)
        {
            throw new FormatException("Unable to parse JSON value", ex);
        }
    }

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    public static bool TryParse(
        [NotNullWhen(true)] string? value,
        IFormatProvider? provider,
        [MaybeNullWhen(false)][NotNullWhen(true)] out CustomUploaderSpec? result
    )
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    {
        if (value == null)
        {
            result = default;
            return false;
        }

        try
        {
            result = Parse(value, provider);
            return true;
        }
        catch (FormatException)
        {
            result = default;
            return false;
        }
    }
}

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
    [JsonIgnore]
    public IReadOnlyList<string>? RegexPatterns { get; }

    [JsonIgnore]
    public IReadOnlyList<Regex>? ParsedRegexPatterns { get; }

    public string? UrlTemplate { get; }
    // public string Failure { get; } = null;

    [JsonIgnore]
    public UrlTemplateSpec? UrlTemplateSpec { get; }

    [JsonConstructor]
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
