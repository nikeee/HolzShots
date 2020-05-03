using System.Drawing;
using System.Windows.Forms;
using HolzShots.NativeTypes.Custom;

namespace HolzShots.Drawing
{
    public static class ScreenshotCreator
    {
        public static Bitmap CaptureScreenshot() => CaptureScreenshot(SystemInformation.VirtualScreen);
        public static Bitmap CaptureScreenshot(Rectangle area)
        {
            var desktopWindowHandle = Native.User32.GetDesktopWindow();
            var source = DeviceContext.FromWindow(desktopWindowHandle);
            try
            {
                Bitmap screenshot;
                using (var destination = DeviceContext.CreateCompatible(source))
                using (var bitmap = source.CreateCompatibleBitmap(area.Size))
                {
                    var oldBitmap = destination.SelectObject(bitmap);
                    var destinationRectangle = new Rectangle(0, 0, area.Width, area.Height);

                    destination.BitBlt(destinationRectangle, source, area.Location, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);

                    screenshot = bitmap.ToImage();
                    destination.SelectObject(oldBitmap);
                }
                return screenshot;
            }
            finally
            {
                Native.User32.ReleaseDC(desktopWindowHandle, source.DC);
            }
        }
    }
}
