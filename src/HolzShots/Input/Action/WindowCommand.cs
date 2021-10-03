using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using HolzShots.Composition.Command;
using HolzShots.Drawing;
using HolzShots.Threading;
using HolzShots.Windows.Forms;

namespace HolzShots.Input.Actions
{
    [Command("captureWindow")]
    public class WindowCommand : CapturingCommand
    {
        protected override async Task InvokeInternal(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
        {
            // TODO: Add proper assertion
            // Debug.Assert(ManagedSettings.EnableWindowScreenshot)

            // TODO: Re-add proper if condition
            // If ManagedSettings.EnableWindowScreenshot Then
            var h = Native.User32.GetForegroundWindow();

            Native.User32.GetWindowPlacement(h, out Native.User32.WindowPlacement info);

            var shot = CaptureWindow(h);
            await ProcessCapturing(shot, settingsContext).ConfigureAwait(true);
        }

        private static Screenshot CaptureWindow(IntPtr windowHandle, bool includeMargin = true)
        {
            if (Native.User32.IsIconic(windowHandle))
                return default;

            using var prio = new ProcessPriorityRequest();
            using var shotSet = GetShotSet(windowHandle, includeMargin);
            return Screenshot.FromWindow(shotSet);
        }

        private static WindowScreenshotSet GetShotSet(IntPtr windowHandle, bool includeMargin)
        {
            // TODO: Refactor methods to WindowScreenshotSet?
            if (HolzShots.Windows.Forms.EnvironmentEx.IsAeroEnabled())
                return DoAeroOn(windowHandle, includeMargin, false);
            else if (HolzShots.Windows.Forms.EnvironmentEx.IsVistaOrHigher)
                return DoAeroOff(windowHandle);
            else
            {
                Debugger.Break(); // wait, you prick!
                throw new InvalidOperationException("Unsupported operating system.");
            }
        }

        // TODO: Rewrite this whole mess

        private static WindowScreenshotSet DoAeroOn(IntPtr wndHandle, bool includeMargin, bool smallMargin)
        {
            Native.User32.GetWindowRect(wndHandle, out Native.Rect nativeRectangle);

            Native.User32.GetWindowPlacement(wndHandle, out Native.User32.WindowPlacement placement);

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
                    Rectangle tmprect = nativeRectangle;
                    Point center = new Point(tmprect.X + System.Convert.ToInt32(tmprect.Width / (double)2), tmprect.Y + System.Convert.ToInt32(tmprect.Height / (double)2));
                    nativeRectangle = Screen.GetWorkingArea(center); // NativeTypes.Rect.FromRectangle(Screen.GetWorkingArea(center))
                }
            }

            Rectangle drawingRectangle = nativeRectangle;

            if (drawingRectangle.Size.Height < 0 || drawingRectangle.Size.Width < 0)
                return default;

            var cursorPositonOnScreenshot = new Point(Cursor.Position.X - drawingRectangle.Location.X, Cursor.Position.Y - drawingRectangle.Location.Y);


            using (BackgroundForm bg = new BackgroundForm(drawingRectangle.Location, drawingRectangle.Size))
            {
                // New Point(rct.Left, rct.Top), New Size(rct.Right - rct.Left, rct.Bottom - rct.Top))
                // Using bg As New FloatingWindow(nrct.X, nrct.Y, nrct.Width, nrct.Height)
                using (Bitmap bmpBlack = new Bitmap(drawingRectangle.Width, drawingRectangle.Height, PixelFormat.Format32bppPArgb))
                {
                    using (Bitmap bmpWhite = new Bitmap(drawingRectangle.Width, drawingRectangle.Height, PixelFormat.Format32bppPArgb))
                    {
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

                        return new WindowScreenshotSet(result, cursorPositonOnScreenshot, windowTitle, processName);
                    }
                }
            }
        }

        private static WindowScreenshotSet DoAeroOff(IntPtr wndHandle)
        {
            Native.User32.GetWindowRect(wndHandle, out Native.Rect nativeRectangle);
            Rectangle drawingRectangle = nativeRectangle;

            var cursorPositonOnScreenshot = new Point(Cursor.Position.X - drawingRectangle.Location.X, Cursor.Position.Y - drawingRectangle.Location.Y);

            var bmp = ScreenshotCreator.CaptureScreenshot(drawingRectangle);

            var windowTitle = WindowInformation.GetWindowTitle(wndHandle);
            var processName = WindowInformation.GetProcessNameOfWindow(wndHandle);

            return new WindowScreenshotSet(bmp, cursorPositonOnScreenshot, windowTitle, processName);
        }
    }
}
