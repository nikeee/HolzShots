using System.Runtime.InteropServices;

namespace HolzShots.Native;

public static partial class UxTheme
{
    private const string DllName = "uxtheme.dll";


    [LibraryImport(DllName)]
    public static partial int SetWindowThemeAttribute(IntPtr hWnd, int wtype, ref WtaOptions attributes, uint size);

    [LibraryImport(DllName)]
    public static partial int GetThemeMargins(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId, IntPtr rect, ref Margin pMargins);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf16)]
    public static partial int SetWindowTheme(IntPtr hWnd, string pszSubAppName, int pszSubIdList);


    #region Types

    [StructLayout(LayoutKind.Sequential)]
    public struct WtaOptions
    {
        public Wtnca Flags;
        public Wtnca Mask;
    }

    [Flags()]
    public enum Wtnca : uint
    {
        NoDrawCaption = 0x1,
        NoDrawIcon = 0x2,
        NoSysMenu = 0x4,
        NoMirrorHelp = 0x8
    }

    #endregion
}
