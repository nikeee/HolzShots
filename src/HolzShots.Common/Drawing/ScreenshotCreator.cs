using System.Drawing;
using System.Windows.Forms;
using HolzShots.Common.NativeTypes.Custom;

namespace HolzShots.Common.Drawing
{
    public static class ScreenshotCreator
    {
        public static Image CaptureScreenshot() => CaptureScreenshot(SystemInformation.VirtualScreen);
        public static Image CaptureScreenshot(Rectangle area)
        {
            Image screenshot;
            var desktopWindowHandle = NativeMethods.GetDesktopWindow();
            var source = DeviceContext.FromWindow(desktopWindowHandle);
            try
            {
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
                NativeMethods.ReleaseDC(desktopWindowHandle, source.DC);
            }
        }
    }
}
