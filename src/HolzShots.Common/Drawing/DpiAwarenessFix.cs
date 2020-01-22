using System;

namespace HolzShots.Drawing
{
    public static class DpiAwarenessFix
    {
        public static void SetDpiAwareness()
        {
            try
            {
                if (Environment.OSVersion.Version.Major >= 6)
                    _ = NativeMethods.SetProcessDpiAwareness(NativeTypes.ProcessDPIAwareness.ProcessPerMonitorDPIAware);
            }
            catch (EntryPointNotFoundException) //this exception occures if OS does not implement this API, just ignore it.
            { }
        }
    }
}
