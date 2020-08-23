using System;
using Microsoft.Win32;

namespace HolzShots.Windows.Forms
{
    public static class EnvironmentEx
    {
        public static bool IsVistaOrHigher => Environment.OSVersion.Version.Major >= 6;
        public static bool IsSevenOrHigher => (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major > 6;
        public static bool IsTenOrHigher => Environment.OSVersion.Version.Major >= 10;

        public static bool AppsUseLightTheme()
        {
            if (!IsTenOrHigher)
                return false;

            var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            return (int)key.GetValue("AppsUseLightTheme", 1) != 0;
        }

        /// <summary> It's a function instead of a property to singal that this call might be expensive (it involves a p/invoke) </summary>
        public static bool IsAeroEnabled() => IsVistaOrHigher && Native.DwmApi.DwmIsCompositionEnabled();
    }
}
