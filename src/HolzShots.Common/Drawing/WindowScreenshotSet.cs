using System;
using System.Drawing;

namespace HolzShots.Drawing
{
    public struct WindowScreenshotSet : IDisposable, IEquatable<WindowScreenshotSet>
    {
        public Image Result { get; }
        public CursorPosition? CursorPosition { get; }
        public string ProcessName { get; }
        public string WindowTitle { get; }
        public WindowScreenshotSet(Image result, CursorPosition? cursorPosition, string processName, string windowTitle)
        {
            Result = result ?? throw new ArgumentNullException(nameof(result));
            CursorPosition = cursorPosition;
            ProcessName = processName;
            WindowTitle = windowTitle;
        }

        public void Dispose() => Result.Dispose();

        #region Equals/operators

        public override bool Equals(object? obj) => obj is WindowScreenshotSet w && w == this;
        public bool Equals(WindowScreenshotSet other) => other == this;
        public override int GetHashCode() => Result.GetHashCode();
        public static bool operator ==(WindowScreenshotSet left, WindowScreenshotSet right)
        {
            return left.Result == right.Result
                && left.CursorPosition == right.CursorPosition
                && left.ProcessName == right.ProcessName
                && left.WindowTitle == right.WindowTitle;
        }
        public static bool operator !=(WindowScreenshotSet left, WindowScreenshotSet right) => !(left == right);

        #endregion

    }
}
