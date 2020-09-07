using System.Runtime.InteropServices;

namespace HolzShots.Native
{
    public static class Shcore
    {
        private const string DllName = "shcore.dll";

        [DllImport(DllName)]
        public static extern int SetProcessDpiAwareness(ProcessDPIAwareness value);

        #region Types

        public enum ProcessDPIAwareness
        {
            ProcessDPIUnaware = 0,
            ProcessSystemDPIAware = 1,
            ProcessPerMonitorDPIAware = 2
        }

        #endregion
    }
}
