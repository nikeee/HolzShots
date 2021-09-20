using System;

namespace HolzShots.Net
{
    // TODO: Use record
    public class UploadResult
    {
        public string Url { get; }
        public Uploader Source { get; }
        public DateTime Timestamp { get; }

        public UploadResult(Uploader source, string url, DateTime timestamp)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Timestamp = timestamp;
        }

        public override string ToString() => $"{Source}: {Url}";
    }
}
