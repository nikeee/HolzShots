using System.Drawing;
using System.Runtime.InteropServices;
using HolzShots.NativeTypes;

namespace HolzShots
{
    internal static partial class NativeMethods
    {
        private const string gdi32 = "gdi32.dll";

        #region gdi32

        [LibraryImport(gdi32)]
        internal static partial int SetROP2(nint hdc, RasterOperation2 drawMode);
        [LibraryImport(gdi32)]
        internal static partial nint CreatePen(PenStyle penStyle, int width, uint color);
        [LibraryImport(gdi32)]
        internal static partial nint SelectObject(nint hdc, nint gdiObject);
        [LibraryImport(gdi32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool DeleteObject(nint obj);
        [LibraryImport(gdi32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MoveToEx(nint hdc, int x, int y, nint point);
        [LibraryImport(gdi32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool LineTo(nint hdc, int xEnd, int yEnd);
        [LibraryImport(gdi32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool BitBlt(nint hdcDst, int x1, int y1, int cx, int cy, nint hdcSrc, int x2, int y2, CopyPixelOperation op);
        [LibraryImport(gdi32)]
        internal static partial nint CreateCompatibleDC(nint hdc);
        [LibraryImport(gdi32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool DeleteDC(nint hdc);
        [LibraryImport(gdi32)]
        internal static partial nint CreateCompatibleBitmap(nint hdc, int width, int height);

        #endregion
    }

    namespace NativeTypes
    {
        namespace Custom
        {
            internal readonly struct DeviceContext(nint dc) : IDisposable
            {
                public nint DC { get; } = dc;

                public void Dispose() => NativeMethods.DeleteDC(DC);
                public BitmapHandle SelectObject(BitmapHandle gdiObject) => new(NativeMethods.SelectObject(DC, gdiObject.DC));
                public BitmapHandle CreateCompatibleBitmap(Size size) => new(NativeMethods.CreateCompatibleBitmap(DC, size.Width, size.Height));

                public void BitBlt(Rectangle destination, DeviceContext source, Point sourceLocation, CopyPixelOperation operation)
                {
                    NativeMethods.BitBlt(DC, destination.X, destination.Y, destination.Width, destination.Height, source.DC, sourceLocation.X, sourceLocation.Y, operation);
                }

                public static DeviceContext CreateCompatible(DeviceContext hdc) => new(NativeMethods.CreateCompatibleDC(hdc.DC));
                public static DeviceContext FromWindow(nint window) => new(Native.User32.GetWindowDC(window));
            }

            readonly struct BitmapHandle(nint dc) : IDisposable
            {
                internal nint DC { get; } = dc;

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
