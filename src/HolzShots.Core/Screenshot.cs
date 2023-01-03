using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using HolzShots.Drawing;
using HolzShots.IO.Naming;

namespace HolzShots
{
    public sealed class Screenshot // : IDisposable
    {
        public DateTime Timestamp { get; }
        public string? ProcessName { get; }
        public string? WindowTitle { get; }
        public CursorPosition? CursorPosition { get; }
        public ScreenshotSource Source { get; }
        public Bitmap Image { get; }
        public Size Size { get; }

        private Screenshot(Bitmap image, DateTime timestamp, CursorPosition? cursorPosition, ScreenshotSource source, string? processName, string? windowTitle)
        {
            Image = image ?? throw new ArgumentNullException(nameof(image));
            Size = image.Size;
            Timestamp = timestamp;
            CursorPosition = cursorPosition;
            Source = source;

            // Assert that the window title / process name are passed set when source if window
            Debug.Assert(source != ScreenshotSource.Window ? string.IsNullOrEmpty(processName) && string.IsNullOrEmpty(windowTitle) : true);
            ProcessName = processName;
            WindowTitle = windowTitle;
        }

        /// <summary>
        /// TODO: Should we clone this?
        /// </summary>
        public static Screenshot FromWindow(WindowScreenshotSet set) => new(
            (set.Result.Clone() as Bitmap)!,
            DateTime.Now,
            set.CursorPosition,
            ScreenshotSource.Window,
            set.ProcessName,
            set.WindowTitle
        );

        public static Screenshot FromImage(Bitmap image, CursorPosition? cursorPosition, ScreenshotSource source) => new(image, DateTime.Now, cursorPosition, source, null, null);
        public static Screenshot FromImported(Bitmap image) => new(image, DateTime.Now, null, ScreenshotSource.Unknown, null, null);

        public FileMetadata GetFileMetadata(ImageFormat format) => new(Timestamp, Image.EstimateFileSize(format), Size);

        // public Image GetBitmapCopy() => _image.CloneDeep();

        /*
        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Debug.Assert(_image is not null);
                    _image.Dispose();
                }
                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                disposedValue = true;
            }
        }

        public void Dispose() => Dispose(true);

        #endregion
        */
    }

    public enum ScreenshotSource
    {
        Unknown = 0,
        Selected = 1,
        Window,
        Fullscreen,
        Clipboard,
    }
}
