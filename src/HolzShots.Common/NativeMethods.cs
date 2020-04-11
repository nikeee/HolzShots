using HolzShots.NativeTypes;
using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace HolzShots
{
    internal static class NativeMethods
    {
        private const string shcore = "shcore.dll";
        private const string gdi32 = "gdi32.dll";
        private const string user32 = "user32.dll";

        #region shcore

        [DllImport(shcore)]
        internal static extern int SetProcessDpiAwareness(NativeTypes.ProcessDPIAwareness value);

        #endregion
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
        #region user32

        [DllImport(user32)]
        internal static extern bool ReleaseDC(IntPtr window, IntPtr hdc);
        [DllImport(user32)]
        internal static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport(user32)]
        internal static extern IntPtr GetDesktopWindow();

        #endregion
    }

    namespace NativeTypes
    {
        namespace Custom
        {
            internal struct DeviceContext : IDisposable
            {
                public IntPtr DC { get; }
                public DeviceContext(IntPtr dc) => DC = dc;
                public void Dispose() => NativeMethods.DeleteDC(DC);
                public BitmapHandle SelectObject(BitmapHandle gdiObject) => new BitmapHandle(NativeMethods.SelectObject(DC, gdiObject.DC));
                public BitmapHandle CreateCompatibleBitmap(Size size) => new BitmapHandle(NativeMethods.CreateCompatibleBitmap(DC, size.Width, size.Height));

                public void BitBlt(Rectangle destination, DeviceContext source, Point sourceLocation, CopyPixelOperation operation)
                {
                    NativeMethods.BitBlt(DC, destination.X, destination.Y, destination.Width, destination.Height, source.DC, sourceLocation.X, sourceLocation.Y, operation);
                }

                public static DeviceContext CreateCompatible(DeviceContext hdc) => new DeviceContext(NativeMethods.CreateCompatibleDC(hdc.DC));
                public static DeviceContext FromWindow(IntPtr window) => new DeviceContext(NativeMethods.GetWindowDC(window));
            }

            struct BitmapHandle : IDisposable
            {
                internal IntPtr DC { get; }
                public BitmapHandle(IntPtr dc) => DC = dc;

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
        public struct Margin : IEquatable<Margin>
        {
            private readonly int _cxLeftWidth;
            private readonly int _cxRightWidth;
            private readonly int _cyTopHeight;
            private readonly int _cyBottomHeight;

            public int LeftWidth => _cxLeftWidth;
            public int RightWidth => _cxRightWidth;
            public int TopHeight => _cyTopHeight;
            public int BottomHeight => _cyBottomHeight;

            public Margin(int all) => _cxLeftWidth = _cxRightWidth = _cyTopHeight = _cyBottomHeight = all;
            public Margin(int leftWidth, int topHeight, int rightWidth, int bottomHeight)
            {
                _cxLeftWidth = leftWidth;
                _cxRightWidth = rightWidth;
                _cyTopHeight = topHeight;
                _cyBottomHeight = bottomHeight;
            }

            public static implicit operator System.Windows.Forms.Padding(Margin margin) => margin.ToPadding();
            public System.Windows.Forms.Padding ToPadding()
            {
                return new System.Windows.Forms.Padding(_cxLeftWidth, _cyTopHeight, _cxRightWidth, _cyBottomHeight);
            }

            public static implicit operator Margin(System.Windows.Forms.Padding padding) => ToMargin(padding);
            public static Margin ToMargin(System.Windows.Forms.Padding padding)
            {
                return new Margin(padding.Left, padding.Top, padding.Right, padding.Bottom);
            }

            public override int GetHashCode()
            {
                var hashCode = -1969345533;
                hashCode = hashCode * -1521134295 + _cxLeftWidth.GetHashCode();
                hashCode = hashCode * -1521134295 + _cxRightWidth.GetHashCode();
                hashCode = hashCode * -1521134295 + _cyTopHeight.GetHashCode();
                hashCode = hashCode * -1521134295 + _cyBottomHeight.GetHashCode();
                return hashCode;
            }

            public override bool Equals(object obj) => (obj is Margin) && Equals((Margin)obj);

            public bool Equals(Margin margin)
            {
                return _cxLeftWidth == margin._cxLeftWidth &&
                        _cxRightWidth == margin._cxRightWidth &&
                        _cyTopHeight == margin._cyTopHeight &&
                        _cyBottomHeight == margin._cyBottomHeight;
            }

            public static bool operator ==(Margin left, Margin right) => left.Equals(right);
            public static bool operator !=(Margin left, Margin right) => !(left == right);
        }

        public enum ProcessDPIAwareness
        {
            ProcessDPIUnaware = 0,
            ProcessSystemDPIAware = 1,
            ProcessPerMonitorDPIAware = 2
        }
    }
}
