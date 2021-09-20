using System;

namespace HolzShots.Net
{
    [Serializable]
    public class UploadException : Exception
    {
        public UploadException() : this("An error during upload occurred.") { }
        public UploadException(string message) : this(message, null) { }
        public UploadException(string message, Exception? innerException) : base(message, innerException) { }
    }

    [Serializable]
    public class UploadCanceledException : UploadException
    {
        public UploadCanceledException(OperationCanceledException innerException) : base("Upload was canceled", innerException) { }
        public UploadCanceledException() : this("Upload was canceled") { }
        public UploadCanceledException(string message) : base(message) { }
        public UploadCanceledException(string message, Exception innerException) : base(message, innerException) { }
    }
}
