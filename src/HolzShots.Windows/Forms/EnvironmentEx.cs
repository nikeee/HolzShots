using System;
using System.Windows.Forms;
using HolzShots.Input.Selection;
using Microsoft.Win32;
using StartupHelper;

namespace HolzShots.Windows.Forms
{
    public static class EnvironmentEx
    {
        public static bool IsVistaOrHigher => Environment.OSVersion.Version.Major >= 6;
        public static bool IsSevenOrHigher => (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major > 6;
        public static bool IsTenOrHigher => Environment.OSVersion.Version.Major >= 10;

        private static Lazy<StartupManager> _currentStartupManager = new(() => new StartupManager(
            System.Reflection.Assembly.GetEntryAssembly()!.Location,
            LibraryInformation.Name,
            RegistrationScope.Local,
            false,
            StartupProviders.Registry,
            CommandLine.AutorunParamter
        ));

        public static StartupManager CurrentStartupManager => _currentStartupManager.Value;

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

            if (className == "WorkerW")
                return false;

            if (!Native.User32.GetWindowRect(foregroundWindowHandle, out var fgWindowRect))
                return false; // Call to GetWindowRect failed. Just assume that there is no app running in fullscreen

            // This is pretty unreliable. But it works at least a little bit.
            // It doesn't work if the application is running full screen on a different monitor (or over all monitors).
            return Screen.PrimaryScreen.Bounds.Height == fgWindowRect.Bottom
                && Screen.PrimaryScreen.Bounds.Width == fgWindowRect.Right;
        }


        private static Lazy<ToolStripRenderer> _rendererInstance = new(() =>
            AppsUseLightTheme()
                ? new HolzShotsToolStripRenderer()
                : new AeroToolStripRenderer(ToolBarTheme.Toolbar)
        );

        public static ToolStripRenderer GetToolStripRendererForCurrentTheme() => _rendererInstance.Value;

        /// <summary> TODO: Move this somewhere else </summary>
        public static string ShortenViaEllipsisIfNeeded(string value, int maxLength)
        {
            return value.Length > maxLength + 1
                ? value.Remove(maxLength) + "…"
                : value;
        }
    }
}
