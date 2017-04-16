using HolzShots.Common.NativeTypes;
using System;
using System.Diagnostics;
using System.Drawing;

namespace HolzShots.Common.Drawing
{
    public static class GraphicsExtension
    {
        public static void DrawHighlight(this Graphics g, Bitmap bmp, Point[] points, NativePen pen)
        {
            if (g == null)
                throw new NullReferenceException();
            if (points == null)
                throw new ArgumentNullException(nameof(points));
            if (points.Length <= 1)
                throw new ArgumentException(nameof(points));
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            Debug.Assert(pen.Handle != IntPtr.Zero);

            var hdc = g.GetHdc();
            var hBmp = bmp.GetHbitmap();
            var mDc = NativeMethods.CreateCompatibleDC(hdc);
            var oDc = NativeMethods.SelectObject(mDc, hBmp);
            var xDc = NativeMethods.SelectObject(mDc, pen.Handle);

            try
            {
                NativeMethods.SetROP2(mDc, RasterOperation2.MaskPen);
                for (int i = 1; i <= points.Length - 1; i++)
                {
                    var p1 = points[i - 1];
                    var p2 = points[i];
                    NativeMethods.MoveToEx(mDc, p1.X, p1.Y, IntPtr.Zero);
                    NativeMethods.LineTo(mDc, p2.X, p2.Y);
                }
                NativeMethods.SetROP2(mDc, RasterOperation2.CopyPen);
                NativeMethods.BitBlt(hdc, 0, 0, bmp.Width, bmp.Height, mDc, 0, 0, CopyPixelOperation.SourceCopy);
            }
            finally
            {
                NativeMethods.SelectObject(mDc, xDc);

                g.ReleaseHdc(hdc);
                NativeMethods.SelectObject(mDc, oDc);
                NativeMethods.DeleteDC(mDc);
                NativeMethods.DeleteObject(hBmp);
            }
        }
    }
}
