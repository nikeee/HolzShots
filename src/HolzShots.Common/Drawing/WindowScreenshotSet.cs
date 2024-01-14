using System.Drawing;

namespace HolzShots.Drawing;

public readonly struct WindowScreenshotSet(Image result, CursorPosition? cursorPosition, string processName, string windowTitle) : IDisposable, IEquatable<WindowScreenshotSet>
{
    public Image Result { get; } = result ?? throw new ArgumentNullException(nameof(result));
    public CursorPosition? CursorPosition { get; } = cursorPosition;
    public string ProcessName { get; } = processName;
    public string WindowTitle { get; } = windowTitle;

    public readonly void Dispose() => Result.Dispose();

    #region Equals/operators

    public override readonly bool Equals(object? obj) => obj is WindowScreenshotSet w && w == this;
    public readonly bool Equals(WindowScreenshotSet other) => other == this;
    public override readonly int GetHashCode() => Result.GetHashCode();
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
