using System.Drawing;
using System.Runtime.InteropServices;

namespace HolzShots.Native;

/// <summary>
/// The System.Drawing.Rectangle has an incompatible struct layout. It uses X, Y, Width and Height.
/// The Windows API uses Left (X), Top (Y), Right and Bottom. So we need to wrap that.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Rect(int left, int top, int right, int bottom)
{
    public readonly int Left = left;
    public readonly int Top = top;
    public readonly int Right = right;
    public readonly int Bottom = bottom;

    public static Rectangle ToRectangle(Rect rct) => Rectangle.FromLTRB(rct.Left, rct.Top, rct.Right, rct.Bottom);

    public static implicit operator Rectangle(Rect rct) => Rectangle.FromLTRB(rct.Left, rct.Top, rct.Right, rct.Bottom);
    public static implicit operator Rect(Rectangle rectangle) => new(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);


    public static bool operator ==(Rect left, Rect right) => left.Equals(right);
    public static bool operator !=(Rect left, Rect right) => !(left == right);

    public override bool Equals(object? obj) => obj is not null && obj is Rect r && Equals(r);
    public bool Equals(Rect obj) => Left == obj.Left && Top == obj.Top && Right == obj.Right && Bottom == obj.Bottom;
    public override int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);
}
