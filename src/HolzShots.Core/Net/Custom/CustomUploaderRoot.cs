using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using HolzShots.Composition;
using Semver;

namespace HolzShots.Net.Custom
{
    interface IValidatable
    {
        bool Validate(SemVersion schemaVersion);
    }

    [Serializable]
    public class CustomUploaderRoot : IValidatable
    {
        public SemVersion SchemaVersion { get; }
        public UploaderMeta Meta { get; }
        public UploaderConfig Uploader { get; }

        public CustomUploaderRoot(SemVersion schemaVersion, UploaderMeta meta, UploaderConfig uploader)
        {
            SchemaVersion = schemaVersion;
            Meta = meta;
            Uploader = uploader;
        }

        public bool Validate(SemVersion schemaVersion)
        {
            if (SchemaVersion != schemaVersion)
                return false;
            return Uploader?.Validate(schemaVersion) == true && Meta?.Validate(schemaVersion) == true;
        }
    }

    [Serializable]
    public class UploaderMeta : IValidatable, IPluginMetadata
    {
        public SemVersion Version { get; }
        public string Name { get; }

        public string Author { get; } = null;
        public string Contact { get; } = null;
        public string BugsUrl { get; } = null;
        public string UpdateUrl { get; } = null;
        public string Url { get; } = null;
        public string Description { get; } = null;

        public UploaderMeta(SemVersion version, string name, string author, string contact, string bugsUrl, string updateUrl, string url, string description)
        {
            Version = version;
            Name = name;
            Author = author;
            Contact = contact;
            bugsUrl = BugsUrl;
            UpdateUrl = updateUrl;
            Url = url;
            Description = description;
        }

        public bool Validate(SemVersion schemaVersion)
        {
            // TODO: Check if Version is valid semver
            return Version != null && !string.IsNullOrWhiteSpace(Name);
        }
    }

    [Serializable]
    public class UploaderConfig
    {
        private static readonly string[] ValidMethods = { "POST", "PUT" };
        public string FileFormName { get; }
        public string RequestUrl { get; }
        public Parser Parser { get; }

        public string Method { get; } = "POST";
        public ReadOnlyDictionary<string, string> Headers { get; } = null;
        public ReadOnlyDictionary<string, string> PostParams { get; } = null;
        public long? MaxFileSize { get; } = null;
        public string FileName { get; } = null;

        public UploaderConfig(string fileFormName, string requestUrl, Parser parser, string method, ReadOnlyDictionary<string, string> headers, ReadOnlyDictionary<string, string> postParams, long? maxFileSize, string fileName)
        {
            FileFormName = fileFormName;
            RequestUrl = requestUrl;
            Parser = parser;
            Method = method;
            Headers = headers;
            PostParams = postParams;
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
            if (Parser?.Validate(schemaVersion) != true)
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

        private static bool ValidateHeaders(SemVersion schemaVersion, ReadOnlyDictionary<string, string> headers)
        {
            if (headers == null)
                return false;
            // TODO: Validate if all headers have valid values/names
            return true;
        }
    }

    /*
    [Serializable]
    public class UploaderHeaders : ReadOnlyDictionary<string, string>
    {
        public string UserAgent { get; } = null;
        public string Referer { get; } = null;

        public UploaderHeaders(string userAgent, string referer)
        {
            UserAgent = userAgent;
            Referer = referer;
        }

        public string GetUserAgent(string fallback) => UserAgent ?? fallback;

        public bool Validate(SemVersion schemaVersion)
        {
            // Referer can be anything
            return ProductInfoHeaderValue.TryParse(UserAgent, out var _);
        }
    }
    */

    [Serializable]
    public class Parser
    {
        private static readonly string[] SupportedKinds = { "REGEX", "JSON" /*, "xml" */ };
        public string Kind { get; }
        public string Url { get; }
        public string Success { get; }

        public string Failure { get; } = null;

        public Parser(string kind, string url, string success, string failure)
        {
            Kind = kind;
            Url = url;
            Success = success;
            Failure = failure;
        }

        public bool Validate(SemVersion schemaVersion)
        {
            if (string.IsNullOrWhiteSpace(Kind))
                return false;
            if (string.IsNullOrWhiteSpace(Url))
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
            try
            {
                new Regex(Success); // TODO: RegEx options
            }
            catch (Exception) { return false; }
            if (Failure != null)
            {
                try
                {
                    new Regex(Failure); // TODO: RegEx options
                }
                catch (Exception) { return false; }
            }
            return true;
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
