using System.Diagnostics;
using System.Text;

namespace HolzShots.Windows.Forms;

public static class WindowInformation
{
    public static string GetWindowTitle(nint windowHandle)
    {
        var windowTitleLength = Native.User32.GetWindowTextLength(windowHandle);

        // This may be a nice use-case for string.Create and C#'s Span<T>
        // We don't have these APIs available (yet), so we need a StringBuilder
        var windowTitleBuffer = new StringBuilder(windowTitleLength);

        _ = Native.User32.GetWindowText(windowHandle, windowTitleBuffer, windowTitleBuffer.Capacity);

        return windowTitleBuffer.ToString();
    }

    public static string GetProcessNameOfWindow(nint windowHandle)
    {
        var pid = 0;
        _ = Native.User32.GetWindowThreadProcessId(windowHandle, ref pid);

        var process = Process.GetProcessById(pid);
        return process.ProcessName;
    }
}
