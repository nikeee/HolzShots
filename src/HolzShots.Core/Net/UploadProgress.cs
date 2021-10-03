using System;
using System.Diagnostics;
using System.Net.Http.Handlers;

namespace HolzShots.Net
{
    public record UploadProgress(MemSize Current, MemSize Total, UploadState State)
    {
        public uint ProgressPercentage => Total.ByteCount == 0 ? 100 : (uint)((float)Current.ByteCount / Total.ByteCount * 100);

        public static UploadProgress FromHttpProgressEventArgs(HttpProgressEventArgs args)
        {
            Debug.Assert(args != null);
            return new UploadProgress(new MemSize(args.BytesTransferred), new MemSize(args.TotalBytes ?? args.BytesTransferred), UploadState.Processing);
        }
    }

    public enum UploadState
    {
        NotStarted,
        Processing,
        Paused,
        Finished
    }
}
