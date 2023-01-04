using System.Text;
using System.Windows.Forms;
using HolzShots.Input.Selection;
using Microsoft.Win32;
using StartupHelper;

namespace HolzShots.Windows.Forms;

public static class EnvironmentEx
{
    public static bool IsVistaOrHigher => Environment.OSVersion.Version.Major >= 6;
    public static bool IsSevenOrHigher => (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major > 6;
    public static bool IsTenOrHigher => Environment.OSVersion.Version.Major >= 10;

    private static readonly Lazy<StartupManager> _currentStartupManager = new(() => new StartupManager(
        ExecutablePath,
        LibraryInformation.Name,
        RegistrationScope.Local,
        false,
        StartupProviders.Registry,
        CommandLine.AutorunParamter
    ));

    public static StartupManager CurrentStartupManager => _currentStartupManager.Value;

    // https://github.com/dotnet/runtime/issues/13051#issuecomment-510267727
    private static string _executablePath = null!;
    private static string ExecutablePath => _executablePath ??= GetExecutablePath();

    private static string GetExecutablePath()
    {
        var sb = new StringBuilder(10000);
        var res = Kernel32.GetModuleFileName(IntPtr.Zero, sb, sb.Capacity);
        if (res == 0)
            return System.Diagnostics.Process.GetCurrentProcess()?.MainModule?.FileName ?? System.Reflection.Assembly.GetEntryAssembly()!.Location;
        return sb.ToString();
    }


    public static bool AppsUseLightTheme()
    {
        if (!IsTenOrHigher)
            return false;

        var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
        if (key == null)
            return true;

        var value = key.GetValue("AppsUseLightTheme", 1);

        return value is null || (int)value != 0;
    }

    /// <summary> It's a function instead of a property to singal that this call might be expensive (it involves a p/invoke) </summary>
    public static bool IsAeroEnabled() => IsVistaOrHigher && Native.DwmApi.DwmIsCompositionEnabled();

    public static bool IsFullscreenAppRunning()
    {
        var foregroundWindowHandle = Native.User32.GetForegroundWindow();

        var className = WindowHelpers.GetWindowClass(foregroundWindowHandle);
        if (className == null)
            return false; // Call to GetClassName failed. Just assume that there is no app running in fullscreen

        // Ignore the desktop as well as the task bar (see GH#111)
        if (className == "WorkerW" || className == "Shell_TrayWnd")
            return false;

        if (!Native.User32.GetWindowRect(foregroundWindowHandle, out var windowNativeRect))
            return false; // Call to GetWindowRect failed. Just assume that there is no app running in fullscreen

        System.Drawing.Rectangle windowBounds = windowNativeRect;

        // This is pretty unreliable. But it works at least a little bit.
        // It doesn't work if the application is running full screen on a different monitor (or over all monitors).
        return Screen.PrimaryScreen.Bounds.Height == windowBounds.Height
            && Screen.PrimaryScreen.Bounds.Width == windowBounds.Width;
    }

    public static ToolStripRenderer ToolStripRendererForCurrentTheme { get; } = new VisualStyleStripRenderer(ToolBarTheme.Toolbar);

    /// <summary> TODO: Move this somewhere else </summary>
    public static string ShortenViaEllipsisIfNeeded(string value, int maxLength)
    {
        return value.Length > maxLength + 1
            ? value.Remove(maxLength) + "…"
            : value;
    }
}
