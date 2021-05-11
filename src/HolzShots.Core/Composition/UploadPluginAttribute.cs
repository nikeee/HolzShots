using System;
using System.Composition;

namespace HolzShots.Composition
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class UploadPluginAttribute : PluginAttribute
    {
        public UploadPluginAttribute(string name, string author, string version)
            : this(name, author, version, null, null, null, null)
        { }
        public UploadPluginAttribute(string name, string author, string version, string? description, string? contact, string? url, string? bugsUrl)
            : base(name, author, version, description, contact, url, bugsUrl)
        { }
    }
}
