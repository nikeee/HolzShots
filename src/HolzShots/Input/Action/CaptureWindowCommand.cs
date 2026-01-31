using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using HolzShots.Composition.Command;
using HolzShots.Drawing;
using HolzShots.Threading;
using HolzShots.Windows.Forms;

namespace HolzShots.Input.Actions;

[Command("captureWindow")]
public class CaptureWindowCommand : ImageCapturingCommand
{
    protected override async Task InvokeInternal(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
    {
        // TODO: Add proper assertion
        // Debug.Assert(ManagedSettings.EnableWindowScreenshot)

        // TODO: Re-add proper if condition
        // If ManagedSettings.EnableWindowScreenshot Then
        var h = Native.User32.GetForegroundWindow();

        Native.User32.GetWindowPlacement(h, out var _);

        var shot = CaptureWindow(h, settingsContext);
        if (shot is not null)
            await ProcessCapturing(shot, settingsContext).ConfigureAwait(true);
    }

    private static Screenshot? CaptureWindow(nint windowHandle, HSSettings settingsContext, bool includeMargin = true)
    {
        if (Native.User32.IsIconic(windowHandle))
            return default;

        using var priority = new ProcessPriorityRequest();
        using var shotSet = GetShotSet(windowHandle, includeMargin, settingsContext);
        return Screenshot.FromWindow(shotSet);
    }

    private static WindowScreenshotSet GetShotSet(nint windowHandle, bool includeMargin, HSSettings settingsContext)
    {
        // TODO: Refactor methods to WindowScreenshotSet?
        if (EnvironmentEx.IsAeroEnabled())
            return DoAeroOn(windowHandle, includeMargin, false);
        else if (EnvironmentEx.IsVistaOrHigher)
            return DoAeroOff(windowHandle, settingsContext);
        else
        {
            Debugger.Break(); // wait, you prick!
            throw new InvalidOperationException("Unsupported operating system.");
        }
    }

    // TODO: Rewrite this whole mess

    private static WindowScreenshotSet DoAeroOn(nint wndHandle, bool includeMargin, bool smallMargin)
    {
        Native.User32.GetWindowRect(wndHandle, out var nativeRectangle);

        Native.User32.GetWindowPlacement(wndHandle, out var placement);

        if (includeMargin)
        {
            if (placement.showCmd != 3)
            {
                var left = nativeRectangle.Left - (smallMargin ? 4 : 17);
                var top = nativeRectangle.Top - (smallMargin ? 4 : 17);
                var right = nativeRectangle.Right + (smallMargin ? 4 : 21);
                var bottom = nativeRectangle.Bottom + (smallMargin ? 4 : 21);

                nativeRectangle = new Native.Rect(Math.Max(left, SystemInformation.VirtualScreen.Left), Math.Max(top, SystemInformation.VirtualScreen.Top), Math.Min(right, SystemInformation.VirtualScreen.Right), Math.Min(bottom, SystemInformation.VirtualScreen.Bottom)
);
            }
            else
            {
                Rectangle tempRectangle = nativeRectangle;
                var center = new Point(tempRectangle.X + Convert.ToInt32(tempRectangle.Width / (double)2), tempRectangle.Y + Convert.ToInt32(tempRectangle.Height / (double)2));
                nativeRectangle = Screen.GetWorkingArea(center); // NativeTypes.Rect.FromRectangle(Screen.GetWorkingArea(center))
            }
        }

        Rectangle drawingRectangle = nativeRectangle;

        if (drawingRectangle.Size.Height < 0 || drawingRectangle.Size.Width < 0)
            return default;

        var cursorPosition = GetCurrentCursorCoordinates(drawingRectangle);

        using var bg = new BackgroundForm(drawingRectangle.Location, drawingRectangle.Size);

        // New Point(rct.Left, rct.Top), New Size(rct.Right - rct.Left, rct.Bottom - rct.Top))
        // Using bg As New FloatingWindow(nrct.X, nrct.Y, nrct.Width, nrct.Height)
        var bmpBlack = new Bitmap(drawingRectangle.Width, drawingRectangle.Height, PixelFormat.Format32bppPArgb);
        using var bmpWhite = new Bitmap(drawingRectangle.Width, drawingRectangle.Height, PixelFormat.Format32bppPArgb);

        bg.Visible = true;

        WindowRedraw.StopRedraw(wndHandle);

        Native.User32.SetForegroundWindowEx(bg.Handle);
        Native.User32.SetForegroundWindowEx(wndHandle);

        using (var ga = Graphics.FromImage(bmpBlack))
        {
            ga.CompositingQuality = CompositingQuality.HighQuality;
            ga.CopyFromScreen(drawingRectangle.X, drawingRectangle.Y, 0, 0, bg.Size);
        }

        bg.BackColor = Color.White;
        bg.Refresh();

        using (var ga = Graphics.FromImage(bmpWhite))
        {
            ga.CopyFromScreen(drawingRectangle.X, drawingRectangle.Y, 0, 0, bg.Size);
        }

        WindowRedraw.StartRedraw(wndHandle);

        bg.Visible = false;

        var result = new Bitmap(bmpWhite.Width, bmpWhite.Height);
        Computation.ComputeAlphaChannel(bmpWhite, bmpBlack, ref result);

        // Old method:
        // ScreenshotMethodsHelper.ComputeAlphaChannel(bmpWhite, bmpBlack)

        var windowTitle = WindowInformation.GetWindowTitle(wndHandle);
        var processName = WindowInformation.GetProcessNameOfWindow(wndHandle);

        return new WindowScreenshotSet(result, cursorPosition, windowTitle, processName);
    }

    private static WindowScreenshotSet DoAeroOff(nint wndHandle, HSSettings settingsContext)
    {
        Native.User32.GetWindowRect(wndHandle, out var nativeRectangle);
        Rectangle drawingRectangle = nativeRectangle;

        var (bmp, cursorPosition) = ScreenshotCreator.CaptureScreenshot(drawingRectangle, settingsContext.CaptureCursor);

        var windowTitle = WindowInformation.GetWindowTitle(wndHandle);
        var processName = WindowInformation.GetProcessNameOfWindow(wndHandle);

        return new WindowScreenshotSet(bmp, cursorPosition, windowTitle, processName);
    }

    static CursorPosition GetCurrentCursorCoordinates(Rectangle referenceRectangle)
    {
        var positionOnScreen = Cursor.Position;
        return new(
            positionOnScreen,
            new Point(positionOnScreen.X - referenceRectangle.Location.X, positionOnScreen.Y - referenceRectangle.Location.Y)
        );
    }
}
