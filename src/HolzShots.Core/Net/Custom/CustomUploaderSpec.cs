using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public Parser ResponseParser { get; }

        public string Method { get; } = "POST";
        public IReadOnlyDictionary<string, string> Headers { get; } = null;
        public IReadOnlyDictionary<string, string> PostParams { get; } = null;
        public IReadOnlyList<string> RegexPatterns { get; } = null;
        public long? MaxFileSize { get; } = null;
        public string FileName { get; } = null;

        public UploaderConfig(
            string fileFormName,
            string requestUrl,
            Parser responseParser,
            string method,
            IReadOnlyDictionary<string, string> headers,
            IReadOnlyDictionary<string, string> postParams,
            IReadOnlyList<string> regexPatterns,
            long? maxFileSize,
            string fileName
        )
        {
            FileFormName = fileFormName;
            RequestUrl = requestUrl;
            ResponseParser = responseParser;
            Method = method;
            Headers = headers;
            PostParams = postParams;
            RegexPatterns = regexPatterns;
            MaxFileSize = maxFileSize;
            FileName = fileName;
        }

        public string GetEffectiveFileName(string fallback) => FileName ?? fallback;

        public bool Validate(SemVersion schemaVersion)
        {
            if (string.IsNullOrWhiteSpace(FileFormName))
                return false;
            if (string.IsNullOrWhiteSpace(RequestUrl))
                return false;
            if (ResponseParser?.Validate(schemaVersion) != true)
                return false;
            if (Headers != null && !ValidateHeaders(schemaVersion, Headers))
                return false;
            if (Method != null && !ValidMethods.Contains(Method.ToUpperInvariant()))
                return false;

            // PostParams can be anything
            // FileName can be anything

            if (MaxFileSize != null && MaxFileSize < 0)
                return false;
            return true;
        }

        private static bool ValidateHeaders(SemVersion schemaVersion, IReadOnlyDictionary<string, string> headers)
        {
            if (headers == null)
                return false;
            // TODO: Validate if all headers have valid values/names
            return true;
        }
    }

    [Serializable]
    public class Parser
    {
        private static readonly string[] SupportedKinds = { "REGEX", "JSON" /*, "xml" */ };
        public string Kind { get; }
        public string UrlTemplate { get; }
        public string Success { get; }

        public string Failure { get; } = null;

        public Parser(string kind, string urlTemplate, string success, string failure)
        {
            Kind = kind;
            UrlTemplate = urlTemplate;
            Success = success;
            Failure = failure;
        }

        public bool Validate(SemVersion schemaVersion)
        {
            if (string.IsNullOrWhiteSpace(Kind))
                return false;
            if (string.IsNullOrWhiteSpace(UrlTemplate))
                return false;

            var upperKind = Kind.ToUpperInvariant();

            Debug.Assert(SupportedKinds.Contains(upperKind));

            switch (upperKind)
            {
                case "REGEX": return ValidateRegEx(schemaVersion);
                case "JSON": return ValidateJson(schemaVersion);
                case "XML": return ValidateXml(schemaVersion);
                default: return false;
            }
        }

        private bool ValidateRegEx(SemVersion schemaVersion)
        {
            if (string.IsNullOrEmpty(Success))
                return false;

            if (!IsValidRegularExpression(Success))
                return false;

            if (Failure != null)
            {
                if (!IsValidRegularExpression(Failure))
                    return false;
            }

            return true;
        }
        private static bool IsValidRegularExpression(string exp)
        {
            try
            {
                _ = new Regex(exp); // TODO: RegEx options
                return true;
            }
            catch (Exception) { return false; }
        }

        private bool ValidateJson(SemVersion schemaVersion)
        {
            if (string.IsNullOrWhiteSpace(Success))
                return false;
            // Failure can be everything
            return true;

        }
        private bool ValidateXml(SemVersion schemaVersion) => false; // TODO: Implement
    }
}
