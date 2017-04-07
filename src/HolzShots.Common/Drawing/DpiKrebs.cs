using System;

namespace HolzShots.Common.Drawing
{
    public static class DpiKrebs
    {
        public static void SetDpiAwareness()
        {
            try
            {
                if (Environment.OSVersion.Version.Major >= 6)
                    NativeMethods.SetProcessDpiAwareness(NativeTypes.ProcessDPIAwareness.ProcessPerMonitorDPIAware);
            }
            catch (EntryPointNotFoundException) //this exception occures if OS does not implement this API, just ignore it.
            { }
        }
    }
}
