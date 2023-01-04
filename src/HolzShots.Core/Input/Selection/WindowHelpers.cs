using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using HolzShots.Native;

namespace HolzShots.Input.Selection;

public static class WindowHelpers
{
    /// <summary>
    /// Ref: https://devblogs.microsoft.com/oldnewthing/20200302-00/?p=103507
    /// </summary>
    public static bool IsWindowVisibleOnScreen(IntPtr windowHandle)
    {
        return User32.IsWindowVisible(windowHandle) && !IsWindowCloaked(windowHandle);
    }

    private static bool IsWindowCloaked(IntPtr windowHandle)
    {
        Debug.Assert(Environment.OSVersion.Version.Major >= 6); // DWM API is available since vista

        var hResult = DwmApi.DwmGetWindowAttribute(windowHandle, DwmWindowAttribute.Cloaked, out bool isCloaked, Marshal.SizeOf(typeof(bool)));
        return hResult == 0
            ? isCloaked
            : throw new Win32Exception(hResult);
    }

    public static Rectangle? GetWindowRectangle(IntPtr windowHandle)
    {
        if (IsDwmCompositionEnabled())
        {
            var res = GetExtendedFrameBounds(windowHandle);
            if (res == null)
            {
                return User32.GetWindowRect(windowHandle, out var dwmFallback)
                    ? dwmFallback
                    : (Rectangle?)null;
            }

            return res.Value;
        }

        return User32.GetWindowRect(windowHandle, out var fallback)
            ? fallback
            : (Rectangle?)null;
    }

    public static string? GetWindowClass(IntPtr windowHandle)
    {
        var sb = new System.Text.StringBuilder();
        var charsWritten = User32.GetClassName(windowHandle, sb, sb.Capacity);

        return charsWritten == 0
            ? null // Call to GetClassName failed (pretent it doesnt have a class name)
            : sb.ToString();
    }

    private static Rectangle? GetExtendedFrameBounds(IntPtr windowHandle)
    {
        var hResult = DwmApi.DwmGetWindowAttribute(windowHandle, DwmWindowAttribute.ExtendedFrameBounds, out Rect rect, Marshal.SizeOf(typeof(Rect)));
        return hResult == 0
            ? rect
            : (Rectangle?)null;
    }

    private static bool IsDwmCompositionEnabled()
    {
        return Environment.OSVersion.Version.Major >= 6 && DwmApi.DwmIsCompositionEnabled();
    }


    private static Rectangle MaximizedWindowFix(IntPtr handle, Rectangle windowRect)
    {
        if (GetBorderSize(handle, out var size))
        {
            windowRect = new Rectangle(
                windowRect.X + size.Width,
                windowRect.Y + size.Height,
                windowRect.Width - (size.Width * 2),
                windowRect.Height - (size.Height * 2)
            );
        }

        return windowRect;
    }


    private static bool GetBorderSize(IntPtr handle, out System.Drawing.Size size)
    {
        var info = new WindowInfo(null);
        if (User32.GetWindowInfo(handle, ref info))
        {
            size = new System.Drawing.Size((int)info.cxWindowBorders, (int)info.cyWindowBorders);
            return true;
        }
        size = System.Drawing.Size.Empty;
        return false;
    }

    public static Rectangle? GetClientRectangle(IntPtr windowHandle)
    {
        return User32.GetClientRect(windowHandle, out var res)
            ? res
            : (Rectangle?)null;
    }

    public static string GetWindowTitle(IntPtr windowHandle)
    {
        Debug.Assert(windowHandle != IntPtr.Zero);

        var textLength = User32.GetWindowTextLength(windowHandle);
        var windowTitleBuffer = new System.Text.StringBuilder(textLength + 1);

        _ = User32.GetWindowText(windowHandle, windowTitleBuffer, windowTitleBuffer.Capacity);

        return windowTitleBuffer.ToString();
    }

    public static Rectangle? MapWindowPoints(IntPtr from, IntPtr to, Rectangle rectangle)
    {
        Rect clientRectangelToConvert = rectangle;
        return User32.MapWindowPoints(from, to, ref clientRectangelToConvert, 2) != 0
            ? clientRectangelToConvert
            : (Rectangle?)null;
    }
}
