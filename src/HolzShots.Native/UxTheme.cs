using System.Runtime.InteropServices;

namespace HolzShots.Native
{
    public static class UxTheme
    {
        private const string DllName = "uxtheme.dll";


        [DllImport(DllName)]
        public extern static int SetWindowThemeAttribute(IntPtr hWnd, int wtype, ref WtaOptions attributes, uint size);

        [DllImport(DllName)]
        public extern static int GetThemeMargins(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId, IntPtr rect, ref Margin pMargins);

        [DllImport(DllName, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, int pszSubIdList);


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
}