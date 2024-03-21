using System.Diagnostics;
using System.Drawing;
using HolzShots.NativeTypes.Custom;
using static HolzShots.Native.User32;

namespace HolzShots.Drawing;

public static class ScreenshotCreator
{
    public static (Bitmap, CursorPosition?) CaptureScreenshot(Rectangle area, bool captureCursor)
    {
        var desktopWindowHandle = GetDesktopWindow();
        var source = DeviceContext.FromWindow(desktopWindowHandle);
        try
        {
            using var destination = DeviceContext.CreateCompatible(source);
            using var bitmap = source.CreateCompatibleBitmap(area.Size);

            var oldBitmap = destination.SelectObject(bitmap);
            var destinationRectangle = new Rectangle(0, 0, area.Width, area.Height);

            destination.BitBlt(destinationRectangle, source, area.Location, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);

            var cursorInfo = GetCursorInfo();


            // Regardless of whether the cursor is visible, we want to return the position
            CursorPosition? cursorPos = null;
            if (cursorInfo is not null)
            {
                var info = cursorInfo.Value;

                // The coordinates might be negative if the task bar is on the right screen,
                // so we need to convert the screen coordinates to image coordinates by subtracting the base offset of the cirtual screen
                var (imageX, imageY) = (info.screenPosX - area.X, info.screenPosY - area.Y);
                cursorPos = new(
                    new Point(info.screenPosX, info.screenPosY),
                    new Point(imageX, imageY)
                );

                if (captureCursor)
                    DrawCurrentCursorToImageIfVisible(info, cursorPos.OnImage, destination);
            }

            var screenshot = bitmap.ToImage();
            destination.SelectObject(oldBitmap);

            return (screenshot, cursorPos);
        }
        finally
        {
            _ = ReleaseDC(desktopWindowHandle, source.DC);
        }
    }

    private static CursorInfo? GetCursorInfo()
    {
        var cursorInfo = new CursorInfo
        {
            cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CursorInfo)),
        };
        if (!Native.User32.GetCursorInfo(ref cursorInfo))
        {
            Debug.WriteLine("Failed to get cursor info");
            return null;
        }
        return cursorInfo;
    }

    private static void DrawCurrentCursorToImageIfVisible(CursorInfo cursorInfo, Point posOnImage, DeviceContext destination)
    {
        if ((cursorInfo.flags & CursorFlags.Showing) != 0)
            DrawIcon(destination.DC, posOnImage.X, posOnImage.Y, cursorInfo.cursorHandle);
    }
}

public record CursorPosition(Point OnScreen, Point OnImage);
