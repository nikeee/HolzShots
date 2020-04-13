using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace HolzShots.Native
{
    public static class User32
    {
        private const string DllName = "user32.dll";

        #region SendMessage

        [DllImport(DllName)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, StringBuilder lParam);

        [DllImport(DllName)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        #endregion
        #region Window Functions

        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int cch);

        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hwnd);

        [DllImport(DllName)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport(DllName)]
        public static extern bool FlashWindowEx(in FlashWindowInfo pwfi);

        #endregion
        #region Threading

        [DllImport(DllName)]
        public static extern bool AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);

        [DllImport(DllName, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, ref int lpdwProcessId);

        #endregion
        #region Drawing

        [DllImport(DllName)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport(DllName)]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDc);

        #endregion

        #region Types


        [StructLayout(LayoutKind.Sequential)]
        public readonly struct FlashWindowInfo
        {
            private readonly uint _size;
            private readonly IntPtr _windowHandle;
            private readonly FlashWindowFlags _flags;
            private readonly uint _count;
            private readonly uint _timeout;

            public FlashWindowInfo(IntPtr handle)
                : this(handle, FlashWindowFlags.TimerNoFg | FlashWindowFlags.Tray, uint.MaxValue) { }

            public FlashWindowInfo(IntPtr handle, FlashWindowFlags flags, uint count)
                : this(handle, flags, count, 0) { }

            public FlashWindowInfo(IntPtr handle, FlashWindowFlags flags, uint count, uint timeout)
            {
                _size = Convert.ToUInt32(Marshal.SizeOf(typeof(FlashWindowInfo)));
                _windowHandle = handle;
                _flags = flags;
                _count = count;
                _timeout = timeout;
            }

            public void Flash() => FlashWindowEx(in this);
        }


        [Flags]
        public enum FlashWindowFlags : uint
        {
            /// <summary>Stop flashing. The system restores the window to its original state.</summary>
            Stop = 0,
            /// <summary>Flash the window caption.</summary>
            Caption = 1,
            /// <summary>Flash the taskbar button.</summary>
            Tray = 2,
            /// <summary>
            /// Flash both the window caption and taskbar button.
            /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
            /// </summary>
            All = 3,
            /// <summary>Flash continuously, until the FLASHW_STOP flag is set.</summary>
            Timer = 4,
            /// <summary>Flash continuously until the window comes to the foreground.</summary>
            TimerNoFg = 12
        }

        #endregion
    }
}
