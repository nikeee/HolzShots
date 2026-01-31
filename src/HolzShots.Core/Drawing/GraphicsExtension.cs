using System.Drawing;
using HolzShots.NativeTypes;

namespace HolzShots.Drawing;

public static class GraphicsExtension
{
    public static void DrawHighlight(this Graphics g, Bitmap bmp, Point[] points, NativePen pen)
    {
        ArgumentNullException.ThrowIfNull(g);
        ArgumentNullException.ThrowIfNull(bmp);
        ArgumentNullException.ThrowIfNull(points);
        ArgumentNullException.ThrowIfNull(pen);
        if (points.Length <= 1)
            throw new ArgumentException(nameof(points));
        if (pen.Handle == 0)
            throw new ArgumentNullException(nameof(pen));

        var hdc = g.GetHdc();
        var hBmp = bmp.GetHbitmap();
        var mDc = NativeMethods.CreateCompatibleDC(hdc);
        var oDc = NativeMethods.SelectObject(mDc, hBmp);
        var xDc = NativeMethods.SelectObject(mDc, pen.Handle);

        try
        {
            _ = NativeMethods.SetROP2(mDc, RasterOperation2.MaskPen);
            for (int i = 1; i <= points.Length - 1; i++)
            {
                var p1 = points[i - 1];
                var p2 = points[i];
                NativeMethods.MoveToEx(mDc, p1.X, p1.Y, 0);
                NativeMethods.LineTo(mDc, p2.X, p2.Y);
            }
            _ = NativeMethods.SetROP2(mDc, RasterOperation2.CopyPen);
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
