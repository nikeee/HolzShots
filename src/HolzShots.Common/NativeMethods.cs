using System.Drawing;
using System.Runtime.InteropServices;
using HolzShots.NativeTypes;

namespace HolzShots
{
    internal static class NativeMethods
    {
        private const string gdi32 = "gdi32.dll";

        #region gdi32

        [DllImport(gdi32)]
        internal static extern int SetROP2(IntPtr hdc, RasterOperation2 drawMode);
        [DllImport(gdi32)]
        internal static extern IntPtr CreatePen(PenStyle penStyle, int width, uint color);
        [DllImport(gdi32)]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr gdiObject);
        [DllImport(gdi32)]
        internal static extern bool DeleteObject(IntPtr obj);
        [DllImport(gdi32)]
        internal static extern bool MoveToEx(IntPtr hdc, int x, int y, IntPtr point);
        [DllImport(gdi32)]
        internal static extern bool LineTo(IntPtr hdc, int xEnd, int yEnd);
        [DllImport(gdi32)]
        internal static extern bool BitBlt(IntPtr hdcDst, int x1, int y1, int cx, int cy, IntPtr hdcSrc, int x2, int y2, CopyPixelOperation op);
        [DllImport(gdi32)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport(gdi32)]
        internal static extern bool DeleteDC(IntPtr hdc);
        [DllImport(gdi32)]
        internal static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int width, int height);

        #endregion
    }

    namespace NativeTypes
    {
        namespace Custom
        {
            internal readonly struct DeviceContext(IntPtr dc) : IDisposable
            {
                public IntPtr DC { get; } = dc;

                public void Dispose() => NativeMethods.DeleteDC(DC);
                public BitmapHandle SelectObject(BitmapHandle gdiObject) => new(NativeMethods.SelectObject(DC, gdiObject.DC));
                public BitmapHandle CreateCompatibleBitmap(Size size) => new(NativeMethods.CreateCompatibleBitmap(DC, size.Width, size.Height));

                public void BitBlt(Rectangle destination, DeviceContext source, Point sourceLocation, CopyPixelOperation operation)
                {
                    NativeMethods.BitBlt(DC, destination.X, destination.Y, destination.Width, destination.Height, source.DC, sourceLocation.X, sourceLocation.Y, operation);
                }

                public static DeviceContext CreateCompatible(DeviceContext hdc) => new(NativeMethods.CreateCompatibleDC(hdc.DC));
                public static DeviceContext FromWindow(IntPtr window) => new(Native.User32.GetWindowDC(window));
            }

            readonly struct BitmapHandle(IntPtr dc) : IDisposable
            {
                internal IntPtr DC { get; } = dc;

                public Bitmap ToImage() => Image.FromHbitmap(DC);
                public void Dispose() => NativeMethods.DeleteObject(DC);
            }
        }

        public enum PenStyle
        {
            Solid = 0,
            Dash = 1,
            Dot = 2,
            DashDot = 3,
            DashDotDot = 4,
            Null = 5,
            InsideFrame = 6,
        }

        public enum RasterOperation2
        {
            Black = 1,
            NotMergePen = 2,
            MaskNotPen = 3,
            NotCopyPen = 4,
            MaskPenNot = 5,
            Not = 6,
            XorPen = 7,
            NotMaskPen = 8,
            MaskPen = 9,
            NotXorPen = 10,
            Nop = 11,
            MergeNotPen = 12,
            CopyPen = 13,
            MergePenNot = 14,
            MergePen = 15,
            White = 16,
        }

        [StructLayout(LayoutKind.Sequential)]
        public readonly struct Margin : IEquatable<Margin>
        {
            public int LeftWidth { get; }
            public int RightWidth { get; }
            public int TopHeight { get; }
            public int BottomHeight { get; }

            public Margin(int all) => LeftWidth = RightWidth = TopHeight = BottomHeight = all;
            public Margin(int leftWidth, int topHeight, int rightWidth, int bottomHeight)
            {
                LeftWidth = leftWidth;
                RightWidth = rightWidth;
                TopHeight = topHeight;
                BottomHeight = bottomHeight;
            }

            public override readonly int GetHashCode() => HashCode.Combine(LeftWidth, RightWidth, TopHeight, BottomHeight);

            public override readonly bool Equals(object? obj) => obj is Margin other && Equals(other);

            public readonly bool Equals(Margin margin)
            {
                return LeftWidth == margin.LeftWidth &&
                        RightWidth == margin.RightWidth &&
                        TopHeight == margin.TopHeight &&
                        BottomHeight == margin.BottomHeight;
            }

            public static bool operator ==(Margin left, Margin right) => left.Equals(right);
            public static bool operator !=(Margin left, Margin right) => !(left == right);
        }
    }
}
