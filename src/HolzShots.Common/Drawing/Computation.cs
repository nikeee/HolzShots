using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace HolzShots.Drawing;

public static class Computation
{
    public static void ComputeAlphaChannel(Bitmap white, Bitmap black, ref Bitmap alpha)
    {
        // Fuck dem clusters
        // Do not touch or things will explode.

        ArgumentNullException.ThrowIfNull(white);
        ArgumentNullException.ThrowIfNull(black);
        ArgumentNullException.ThrowIfNull(alpha);

        Debug.Assert(white.Size == black.Size);
        Debug.Assert(alpha.Size == black.Size);

        unchecked
        {
            int width = white.Width;
            int height = white.Height;

            var lockRectangle = new Rectangle(0, 0, width, height);

            var whiteBits = white.LockBits(lockRectangle, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var blackBits = black.LockBits(lockRectangle, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var alphaBits = alpha.LockBits(lockRectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            var alphaStride = alphaBits.Stride;
            var whiteStride = whiteBits.Stride;
            try
            {
                unsafe
                {
                    var whitePointer = (byte*)whiteBits.Scan0;
                    var blackPointer = (byte*)blackBits.Scan0;
                    var alphaPointer = (byte*)alphaBits.Scan0;

                    int i, j;
                    int wso = whiteStride - (width * 3);
                    int aso = alphaStride - (width * 4);

                    for (i = 0; i < height; i++)
                    {
                        for (j = 0; j < width; j++)
                        {
                            *(alphaPointer++) = (byte)(*blackPointer == 0 && whitePointer[0] == 255
                                                          ? 0
                                                          : (255 * *blackPointer / (*blackPointer - whitePointer[0] + 255)));
                            *(alphaPointer++) = (byte)(*(++blackPointer) == 0 && whitePointer[1] == 255
                                                          ? 0
                                                          : (255 * *blackPointer / (*blackPointer - whitePointer[1] + 255)));

                            *(alphaPointer++) = (byte)(*(++blackPointer) == 0 && whitePointer[2] == 255
                                                          ? 0
                                                          : (255 * *blackPointer / (*blackPointer - whitePointer[2] + 255)));

                            *(alphaPointer++) = (byte)(*(blackPointer++) - whitePointer[2] + 255);

                            whitePointer += 3;
                        }
                        whitePointer += wso;
                        blackPointer += wso;
                        alphaPointer += aso;
                    }
                }
            }
            finally
            {
                white.UnlockBits(whiteBits);
                black.UnlockBits(blackBits);
                alpha.UnlockBits(alphaBits);
            }
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    struct NativeColor4
    {
        [FieldOffset(0)]
        public byte R;
        [FieldOffset(1)]
        public byte G;
        [FieldOffset(2)]
        public byte B;
        [FieldOffset(3)]
        public byte A;
    }

    public static void ComputeAlphaChannel2(Bitmap white, Bitmap black, ref Bitmap alpha)
    {
        ArgumentNullException.ThrowIfNull(white);
        ArgumentNullException.ThrowIfNull(black);
        ArgumentNullException.ThrowIfNull(alpha);

        Debug.Assert(white.Size == black.Size);
        Debug.Assert(alpha.Size == black.Size);

        unchecked
        {
            int width = white.Width;
            int height = white.Height;

            var lockRectangle = new Rectangle(0, 0, width, height);

            var whiteBits = white.LockBits(lockRectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var blackBits = black.LockBits(lockRectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var alphaBits = alpha.LockBits(lockRectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            var alphaStride = alphaBits.Stride;
            var whiteStride = whiteBits.Stride;
            try
            {
                unsafe
                {
                    var whitePointer = (NativeColor4*)whiteBits.Scan0;
                    var blackPointer = (NativeColor4*)blackBits.Scan0;
                    var alphaPointer = (NativeColor4*)alphaBits.Scan0;

                    int i, j;
                    int wso = whiteStride - width;
                    int aso = alphaStride - width;

                    for (i = 0; i < height; ++i)
                    {
                        for (j = 0; j < width; ++j)
                        {
                            alphaPointer->R = blackPointer->R == 0 && whitePointer->R == 255
                                ? (byte)0
                                : (byte)(255 * blackPointer->R / (blackPointer->R - whitePointer->R + 255));
                            alphaPointer->G = blackPointer->G == 0 && whitePointer->G == 255
                                ? (byte)0
                                : (byte)(255 * blackPointer->G / (blackPointer->G - whitePointer->G + 255));
                            alphaPointer->B = blackPointer->B == 0 && whitePointer->B == 255
                                ? (byte)0
                                : (byte)(255 * blackPointer->B / (blackPointer->B - whitePointer->B + 255));
                            alphaPointer->A = (byte)(blackPointer->B - whitePointer->B + 255);
                            ++blackPointer;
                            ++whitePointer;
                        }
                        whitePointer += wso;
                        blackPointer += wso;
                        alphaPointer += aso;
                    }
                }
            }
            finally
            {
                white.UnlockBits(whiteBits);
                black.UnlockBits(blackBits);
                alpha.UnlockBits(alphaBits);
            }
        }
    }
}
