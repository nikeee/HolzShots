using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace HolzShots.Net.Custom
{
    interface IValidatable
    {
        bool Validate(string schema);
    }

    [Serializable]
    class CustomUploader : IValidatable
    {
        public string SchemaVersion { get; }
        public UploaderInfo Info { get; }
        public UploaderConfig Uploader { get; }

        public CustomUploader(string schemaVersion, UploaderInfo info, UploaderConfig uploader)
        {
            SchemaVersion = schemaVersion;
            Info = info;
            Uploader = uploader;
        }

        public bool Validate(string schema)
        {
            if (SchemaVersion != schema)
                return false;
            return Uploader?.Validate(schema) == true && Info?.Validate(schema) == true;
        }
    }

    [Serializable]
    class UploaderInfo : IValidatable
    {
        public string Version { get; }
        public string Name { get; }

        public string Author { get; } = null;
        public string Contact { get; } = null;
        public string UpdateUrl { get; } = null;
        public string Description { get; } = null;

        public UploaderInfo(string version, string name, string author, string contact, string updateUrl, string description)
        {
            Version = version;
            Name = name;
            Author = author;
            Contact = contact;
            UpdateUrl = updateUrl;
            Description = description;
        }

        public bool Validate(string schema)
        {
            // TODO: Check if Version is valid semver
            return Version != null && !string.IsNullOrWhiteSpace(Name);
        }
    }

    [Serializable]
    class UploaderConfig
    {
        private static readonly string[] ValidMethods = { "POST", "PUT" };
        public string FileFormName { get; }
        public string RequestUrl { get; }
        public Parser Parser { get; }

        public string Method { get; } = "POST";
        public UploaderHeaders Headers { get; } = null;
        public ReadOnlyDictionary<string, string> PostParams { get; } = null;
        public long? MaxFileSize { get; } = null;
        public string FileName { get; } = null;

        public UploaderConfig(string fileFormName, string requestUrl, Parser parser, string method, UploaderHeaders headers, ReadOnlyDictionary<string, string> postParams, long? maxFileSize, string fileName)
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

        public bool Validate(string schema)
        {
            if (string.IsNullOrWhiteSpace(FileFormName))
                return false;
            if (string.IsNullOrWhiteSpace(RequestUrl))
                return false;
            if (Parser?.Validate(schema) != true)
                return false;
            if (Headers != null && !Headers.Validate(schema))
                return false;
            if (Method != null && !ValidMethods.Contains(Method.ToUpperInvariant()))
                return false;

            // PostParams can be anything
            // FileName can be anything

            if (MaxFileSize != null && MaxFileSize < 0)
                return false;
            return true;
        }
    }

    [Serializable]
    class UploaderHeaders
    {
        public string UserAgent { get; } = null;
        public string Referer { get; } = null;

        public UploaderHeaders(string userAgent, string referer)
        {
            UserAgent = userAgent;
            Referer = referer;
        }

        public bool Validate(string schema)
        {
            // Referer can be anything
            return ProductInfoHeaderValue.TryParse(UserAgent, out var _);
        }
    }

    [Serializable]
    class Parser
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

        public bool Validate(string schema)
        {
            if (string.IsNullOrWhiteSpace(Kind))
                return false;
            if (string.IsNullOrWhiteSpace(Url))
                return false;

            var upperKind = Kind.ToUpperInvariant();

            Debug.Assert(SupportedKinds.Contains(upperKind));

            switch (upperKind)
            {
                case "REGEX": return ValidateRegEx(schema);
                case "JSON": return ValidateJson(schema);
                case "XML": return ValidateXml(schema);
                default: return false;
            }
        }

        private bool ValidateRegEx(string schema)
        {
            if (string.IsNullOrEmpty(Success))
                return false;
            try
            {
                new Regex(schema); // TODO: RegEx options
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
        private bool ValidateJson(string schema)
        {
            if (string.IsNullOrWhiteSpace(Success))
                return false;
            // Failure can be everything
            return true;

        }
        private bool ValidateXml(string schema) => false; // TODO: Implement
    }
}
