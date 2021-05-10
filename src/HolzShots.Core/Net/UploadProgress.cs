using System;
using System.Diagnostics;
using System.Net.Http.Handlers;

namespace HolzShots.Net
{
    public struct UploadProgress : IEquatable<UploadProgress>
    {
        public MemSize Current { get; }
        public MemSize Total { get; }
        public uint ProgressPercentage => Total.ByteCount == 0 ? 100 : (uint)((float)Current.ByteCount / Total.ByteCount * 100);
        public UploadState State { get; }

        public UploadProgress(MemSize current, MemSize target, UploadState state)
        {
            Current = current;
            Total = target;
            State = state;
        }

        internal UploadProgress(HttpProgressEventArgs eventArgs)
        {
            Debug.Assert(eventArgs != null);
            Current = new MemSize(eventArgs.BytesTransferred);
            Total = new MemSize(eventArgs.TotalBytes ?? eventArgs.BytesTransferred);
            State = UploadState.Processing;
        }

        public override bool Equals(object? obj) => obj is UploadProgress other && other == this;
        public bool Equals(UploadProgress other) => other == this;

        public override int GetHashCode()
        {
            // See: https://stackoverflow.com/a/263416
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash * 16777619) ^ State.GetHashCode();
                hash = (hash * 16777619) ^ Current.GetHashCode();
                hash = (hash * 16777619) ^ Total.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(UploadProgress left, UploadProgress right)
        {
            return left.State == right.State && left.Current == right.Current && left.Total == right.Total;
        }

        public static bool operator !=(UploadProgress left, UploadProgress right) => !(left == right);
    }

    public enum UploadState
    {
        NotStarted,
        Processing,
        Paused,
        Finished
    }
}
