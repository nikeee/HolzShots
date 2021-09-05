using System.Runtime.InteropServices;

namespace HolzShots.Native
{
    public static class DwmApi
    {
        private const string DllName = "dwmapi.dll";

        [DllImport(DllName, PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport(DllName)]
        public static extern int DwmGetWindowAttribute(IntPtr windowHandle, DwmWindowAttribute attribute, out bool pvAttribute, int cbAttribute);
        [DllImport(DllName)]
        public static extern int DwmGetWindowAttribute(IntPtr windowHandle, DwmWindowAttribute attribute, out Rect pvAttribute, int cbAttribute);
    }
}
