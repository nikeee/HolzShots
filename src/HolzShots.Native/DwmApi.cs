using System.Runtime.InteropServices;

namespace HolzShots.Native;

public static partial class DwmApi
{
    private const string DllName = "dwmapi.dll";

    [DllImport(DllName, PreserveSig = false)]
    public static extern bool DwmIsCompositionEnabled();

    [LibraryImport(DllName)]
    public static partial int DwmGetWindowAttribute(IntPtr windowHandle, DwmWindowAttribute attribute, [MarshalAs(UnmanagedType.Bool)] out bool pvAttribute, int cbAttribute);
    [LibraryImport(DllName)]
    public static partial int DwmGetWindowAttribute(IntPtr windowHandle, DwmWindowAttribute attribute, out Rect pvAttribute, int cbAttribute);
}
