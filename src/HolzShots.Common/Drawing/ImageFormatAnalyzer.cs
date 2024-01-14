using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace HolzShots.Drawing;

public static class ImageFormatAnalyzer
{
    private const int RowScanCount = 5;
    private const int ColScanCount = 5;
    private const int LineCount = 10;

    public const AlgorithmKind DefaultAlgorithm = AlgorithmKind.Hybrid;

    public static bool IsOptimizable(Image image)
    {
        ArgumentNullException.ThrowIfNull(image);
        return !image.RawFormat.Equals(ImageFormat.Gif);
    }

    // Implemented some time ago. Now used in HolzShots. Do not touch.
    private unsafe static ImageFormat GetBestFittingFormatAlgorithm(Bitmap image)
    {
        Debug.Assert(image is not null);

        var lockRectangle = new Rectangle(0, 0, image!.Width, image.Height);
        var bits = image.LockBits(lockRectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        try
        {
            var p = (byte*)bits.Scan0;

            var heightIndex = image.Height - 1;
            var widthIndex = image.Width - 1;

            if ((heightIndex < (LineCount * 10 + 1) && widthIndex < (LineCount * 10 + 1))
                || (IsTransculent(p, bits.Stride, 0, 0))
                || (IsTransculent(p, bits.Stride, widthIndex, heightIndex))
                || (IsTransculent(p, bits.Stride, widthIndex, 0))
                || (IsTransculent(p, bits.Stride, 0, heightIndex)))
            {
                return ImageFormat.Png;
            }

            if (CheckForAlpha(p, widthIndex, heightIndex))
                return ImageFormat.Png;
            return GetBestFittingFormatBruteSaving(image);
        }
        finally
        {
            image.UnlockBits(bits);
        }
    }

    //No alpha-channel support
    private static ImageFormat GetBestFittingFormatBruteSaving(Bitmap image)
    {
        using (var ms = new MemoryStream())
        {
            image.Save(ms, ImageFormat.Png);
            long pngLength = ms.Length;

            ms.SetLength(0);

            image.Save(ms, ImageFormat.Jpeg);
            long jpgLength = ms.Length;

            return jpgLength < pngLength ? ImageFormat.Jpeg : ImageFormat.Png;
        }
    }

    private unsafe static ImageFormat GetBestFittingFormatHybrid(Bitmap image)
    {
        var res = PredictedImageFormat.Png;

        var lockRectangle = new Rectangle(0, 0, image.Width, image.Height);
        var bits = image.LockBits(lockRectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

        try
        {
            var p = (byte*)bits.Scan0;

            var heightIndex = image.Height - 1;
            var widthIndex = image.Width - 1;

            if (heightIndex < (LineCount * 10 + 1) && widthIndex < (LineCount * 10 + 1))
                return ImageFormat.Png;

            if (IsTransculent(p, bits.Stride, 0, 0))
                return ImageFormat.Png;

            if (IsTransculent(p, bits.Stride, widthIndex, heightIndex))
                return ImageFormat.Png;

            if (IsTransculent(p, bits.Stride, widthIndex, 0))
                return ImageFormat.Png;

            if (IsTransculent(p, bits.Stride, 0, heightIndex))
                return ImageFormat.Png;

            int x, y;
            int incHor = widthIndex / LineCount;
            int incVer = heightIndex / LineCount;
            int horTrues = 0, verTrues = 0;

            for (x = 1; x <= widthIndex - ColScanCount; x += incHor)
            {
                for (y = 1; y <= heightIndex - RowScanCount; y += incVer)
                {
                    verTrues += ScanCol(p, bits.Stride, x, y);
                    horTrues += ScanRow(p, bits.Stride, x, y);
                }
            }

            if (verTrues > LineCount * 5 || horTrues > LineCount * 5)
                res = PredictedImageFormat.Jpeg;

            if (CheckForAlpha(p, widthIndex, heightIndex))
                res = PredictedImageFormat.Png;

            return res == PredictedImageFormat.Png ? ImageFormat.Png : ImageFormat.Jpeg;
        }
        finally
        {
            image.UnlockBits(bits);
        }
    }

    public static ImageFormat GetBestFittingFormat(Bitmap image) => GetBestFittingFormat(image, DefaultAlgorithm);

    public static ImageFormat GetBestFittingFormat(Bitmap image, AlgorithmKind algorithm)
    {
        ArgumentNullException.ThrowIfNull(image);

        switch (algorithm)
        {
            case AlgorithmKind.ComplexScanning:
                return GetBestFittingFormatAlgorithm(image);
            case AlgorithmKind.BruteSaving:
                return GetBestFittingFormatBruteSaving(image);
            case AlgorithmKind.Hybrid:
                return GetBestFittingFormatHybrid(image);
            default:
                throw new ArgumentException("Impossibru!");
        }
    }

    private enum PredictedImageFormat
    {
        Png,
        Jpeg
    }

    public enum AlgorithmKind
    {
        BruteSaving,
        ComplexScanning,
        Hybrid
    }

    private static unsafe int ScanCol(byte* p, int stride, int x, int y)
    {
        int xBackup = x + ColScanCount;
        bool first = true;
        Color c1;
        Color c2 = Color.FromArgb(0, 0, 0, 0);
        for (; x < xBackup; ++x)
        {
            c1 = GetPixel(p, stride, x, y);
            if (c1.A != 255)
                return 9001;
            if (!first && c1 != c2)
                return 1;
            if (first)
                first = false;
            c2 = c1;
        }
        return 0;
    }
    private static unsafe int ScanRow(byte* p, int stride, int x, int y)
    {
        int yBackup = y + RowScanCount;
        bool first = true;
        Color c1;
        Color c2 = Color.FromArgb(0, 0, 0, 0);

        for (; y < yBackup; ++y)
        {
            c1 = GetPixel(p, stride, x, y);
            if (c1.A != 255)
                return 9001;
            if (!first && c1 != c2)
                return 1;
            if (first)
                first = false;
            c2 = c1;
        }
        return 0;
    }

    private static unsafe bool CheckForAlpha(byte* p, int widthIndex, int heightIndex)
    {
        p += 3;
        for (byte* max = p + widthIndex * 4 * heightIndex - 3; p < max; p += 4)
            if (*p < 255)
                return true;
        return false;
    }

    private static byte _red, _green, _blue, _alpha;
    private static unsafe Color GetPixel(byte* p, int stride, int x, int y)
    {
        stride *= y;
        stride += x * 4;

        _blue = p[stride];
        _green = p[stride + 1];
        _red = p[stride + 2];
        _alpha = p[stride + 3];
        return Color.FromArgb(_alpha, _red, _green, _blue);
    }
    private static unsafe bool IsTransculent(byte* p, int stride, int x, int y)
    {
        stride *= y;
        stride += x * 4;
        return p[stride + 3] != 255;
    }
}
