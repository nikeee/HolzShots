using System.Runtime.InteropServices;

namespace HolzShots.Native
{
    public static class DwmApi
    {
        private const string DllName = "dwmapi.dll";

        [DllImport(DllName, PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();
    }
}
