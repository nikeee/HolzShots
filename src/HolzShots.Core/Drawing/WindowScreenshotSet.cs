using System;
using System.Drawing;

namespace HolzShots.Drawing
{
    // TODO: Move to HolzShots.Common?
    public struct WindowScreenshotSet : IDisposable
    {
        public Image Result { get; }
        public Point CursorPosition { get; }
        public string ProcessName { get; }
        public string WindowTitle { get; }
        public WindowScreenshotSet(Image result, Point cursorPosition, string processName, string windowTitle)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            Result = result;
            CursorPosition = cursorPosition;
            ProcessName = processName;
            WindowTitle = windowTitle;
        }

        public void Dispose() => Result.Dispose();
    }
}
