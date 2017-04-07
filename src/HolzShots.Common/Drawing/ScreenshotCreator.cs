using System.Drawing;
using System.Windows.Forms;

namespace HolzShots.Common.Drawing
{
    public static class ScreenshotCreator
    {
        private static readonly Color _gdiBugColor = Color.FromArgb(255, 13, 11, 12);

        public static Bitmap CaptureScreenshot() => CaptureScreenshot(SystemInformation.VirtualScreen);
        public static Bitmap CaptureScreenshot(Rectangle area)
        {
            var bmp = new Bitmap(area.Width, area.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(_gdiBugColor);
                g.CopyFromScreen(area.X, area.Y, 0, 0, area.Size);
            }
            return bmp;
        }
    }
}
