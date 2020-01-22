using System;
using System.Composition;
using System.ComponentModel.Composition;
using Semver;

namespace HolzShots.Composition
{
    [System.ComponentModel.Composition.MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PluginAttribute : Attribute, ICompileTimePluginMetadata
    {
        public string Name { get; }
        public string Author { get; }
        public string Version { get; }

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

    public interface ICompileTimePluginMetadata
    {
        string Name { get; }
        string Author { get; }
        string Version { get; }

        string Url { get; }
        string BugsUrl { get; }
        string Contact { get; }
        string Description { get; }
    }

    public class PluginMetadata : IPluginMetadata
    {
        public string Name { get; }
        public string Author { get; }
        public SemVersion Version { get; }
        public string Url { get; }
        public string BugsUrl { get; }
        public string Contact { get; }
        public string Description { get; }
        public PluginMetadata(ICompileTimePluginMetadata sourceAttribute)
        {
            if (sourceAttribute == null)
                throw new ArgumentNullException(nameof(sourceAttribute));
            Name = sourceAttribute.Name;
            Author = sourceAttribute.Author;
            Version = SemVersion.Parse(sourceAttribute.Version);
            Url = sourceAttribute.Url;
            BugsUrl = sourceAttribute.BugsUrl;
            Contact = sourceAttribute.Contact;
            Description = sourceAttribute.Description;
        }
    }
}
