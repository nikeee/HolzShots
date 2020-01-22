using HolzShots.Drawing;
using System;
using System.Diagnostics;
using System.Drawing;

namespace HolzShots
{
    public class Screenshot // : IDisposable
    {
        public DateTime Timestamp { get; }
        public string ProcessName { get; }
        public string WindowTitle { get; }
        public Point CursorPosition { get; }
        public ScreenshotSource Source { get; }
        public Image Image { get; }
        public Size Size { get; }


        internal Screenshot(Image image, DateTime timestamp, Point cursorPosition, ScreenshotSource source, string processName, string windowTitle)
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

        public static Screenshot FromWindow(WindowScreenshotSet set)
        {
            // TODO: Clone this?
            return new Screenshot((Bitmap)set.Result.Clone(), DateTime.Now, set.CursorPosition, ScreenshotSource.Window, set.ProcessName, set.WindowTitle);
        }
        public static Screenshot FromSelection(Image image, Point cursorPosition) => new Screenshot(image, DateTime.Now, cursorPosition, ScreenshotSource.Selected, null, null);
        public static Screenshot FromFullscreen(Image image, Point cursorPosition) => new Screenshot(image, DateTime.Now, cursorPosition, ScreenshotSource.Fullscreen, null, null);
        public static Screenshot FromImported(Image image) => new Screenshot(image, DateTime.Now, Point.Empty, ScreenshotSource.Unknown, null, null);

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
                    Debug.Assert(_image != null);
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
    }
}
