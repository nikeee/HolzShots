using HolzShots.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolzShots.Composition
{
    public interface IUploaderSource
    {
        bool Loaded { get; }
        UploaderEntry /*?*/ GetUploaderByName(string name);
        IReadOnlyList<string> GetUploaderNames();
        IReadOnlyList<IPluginMetadata> GetMetadata();
        Task Load();
    }

    public class UploaderEntry : IEquatable<UploaderEntry>
    {
        public IPluginMetadata Metadata { get; }
        public Uploader Uploader { get; }

        public UploaderEntry(IPluginMetadata metadata, Uploader uploader)
        {
            Metadata = metadata ?? throw new ArgumentNullException(nameof(IPluginMetadata));
            Uploader = uploader ?? throw new ArgumentNullException(nameof(uploader));
        }

        public override int GetHashCode() => HashCode.Combine(Metadata, Uploader);
        public static bool operator ==(UploaderEntry left, UploaderEntry right) => left.Equals(right);
        public static bool operator !=(UploaderEntry left, UploaderEntry right) => !(left == right);
        public override bool Equals(object obj) => obj is UploaderEntry other && Equals(other);
        public bool Equals(UploaderEntry other) => other.Metadata.Equals(Metadata) && other.Uploader.Equals(Uploader);
    }
}
