using System.Runtime.InteropServices;

namespace HolzShots.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Size
    {
        public readonly int Width;
        public readonly int Height;

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static implicit operator System.Drawing.Size(Size sz) => new System.Drawing.Size(sz.Width, sz.Height);
        public static implicit operator Size(System.Drawing.Size size) => new Size(size.Width, size.Height);
    }
}
