using System.Runtime.InteropServices;

namespace HolzShots.Native;

public static partial class UxTheme
{
    private const string DllName = "uxtheme.dll";


    [LibraryImport(DllName)]
    public static partial int SetWindowThemeAttribute(nint hWnd, int wtype, ref WtaOptions attributes, uint size);

    [LibraryImport(DllName)]
    public static partial int GetThemeMargins(nint hTheme, nint hdc, int iPartId, int iStateId, int iPropId, nint rect, ref Margin pMargins);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf16)]
    public static partial int SetWindowTheme(nint hWnd, string pszSubAppName, int pszSubIdList);


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
