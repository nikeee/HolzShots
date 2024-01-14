using System.Runtime.InteropServices;

namespace HolzShots.Native;

[StructLayout(LayoutKind.Sequential)]
public readonly struct Size(int width, int height)
{
    public readonly int Width = width;
    public readonly int Height = height;

    public static implicit operator System.Drawing.Size(Size sz) => new(sz.Width, sz.Height);
    public static implicit operator Size(System.Drawing.Size size) => new(size.Width, size.Height);
}
