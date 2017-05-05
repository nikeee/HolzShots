using System;
using System.ComponentModel.Composition;
using Semver;

namespace HolzShots.Composition
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PluginAttribute : Attribute, IPluginMetadata
    {
        public string Name { get; }
        public string Author { get; }
        public SemVersion Version { get; }

        public string Url { get; }
        public string BugsUrl { get; }
        public string Contact { get; }
        public string Description { get; }

        public PluginAttribute(string name, string author, string version)
            : this(name, author, version, null, null, null, null)
        { }
        public PluginAttribute(string name, string author, string version, string description, string contact, string url, string bugsUrl)
        {
            Name = name;
            Author = author;
            Version = version;
            Description = description;
            Contact = contact;
            Url = url;
            BugsUrl = bugsUrl;
        }
    }
}
