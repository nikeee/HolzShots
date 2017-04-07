using HolzShots.Common.NativeTypes;
using System;
using System.Runtime.InteropServices;

namespace HolzShots.Common
{
    internal static class NativeMethods
    {
        private const string shcore = "shcore.dll";
        private const string gdi32 = "gdi32.dll";

        #region shcore

        [DllImport(shcore)]
        public static extern int SetProcessDpiAwareness(NativeTypes.ProcessDPIAwareness value);

        #endregion
        #region gdi32

        [DllImport(gdi32)]
        public static extern int SetROP2(IntPtr hdc, RasterOperation2 drawMode);
        [DllImport(gdi32)]
        public static extern IntPtr CreatePen(PenStyle penStyle, int width, uint color);
        [DllImport(gdi32)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr gdiObject);
        [DllImport(gdi32)]
        public static extern bool DeleteObject(IntPtr obj);
        [DllImport(gdi32)]
        public static extern bool MoveToEx(IntPtr hdc, int x, int y, IntPtr point);
        [DllImport(gdi32)]
        public static extern bool LineTo(IntPtr hdc, int xEnd, int yEnd);
        [DllImport(gdi32)]
        public static extern bool BitBlt(IntPtr hdcDst, int x1, int y1, int cx, int cy, IntPtr hdcSrc, int x2, int y2, RasterOperation op);
        [DllImport(gdi32)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport(gdi32)]
        public static extern bool DeleteDC(IntPtr hdc);

        #endregion
    }

    namespace NativeTypes
    {
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

        public enum RasterOperation
        {
            SrcCopy = 0x00CC0020,
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
