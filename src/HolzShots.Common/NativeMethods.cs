using HolzShots.Common.NativeTypes;
using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace HolzShots.Common
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

                public Image ToImage() => Image.FromHbitmap(DC);
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

        public enum ProcessDPIAwareness
        {
            ProcessDPIUnaware = 0,
            ProcessSystemDPIAware = 1,
            ProcessPerMonitorDPIAware = 2
        }
    }
}
