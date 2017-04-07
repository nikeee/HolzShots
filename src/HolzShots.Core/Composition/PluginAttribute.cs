using System;
using System.ComponentModel.Composition;

namespace HolzShots.Composition
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PluginAttribute : ExportAttribute, IPluginMetadata
    {
        public string Name { get; }
        public string Author { get; }
        public string Version { get; }

        public string Url { get; }
        public string BugsUrl { get; }
        public string Contact { get; }
        public string Description { get; }

        public PluginAttribute(Type contractType, string name, string author, string version)
            : this(contractType, name, author, version, null, null, null, null)
        { }
        public PluginAttribute(Type contractType, string name, string author, string version, string description, string contact, string url, string bugsUrl)
            : base(contractType)
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
