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

        #region Window Position

        [DllImport(DllName)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Native.Rect lpRect);

        [DllImport(DllName)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int x, int y, int width, int height, int flags);

        /// <summary>
        /// The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative to the upper-left corner of the parent window's client area.
        /// </summary>
        /// <param name="hWnd">Handle to the window.</param>
        /// <param name="x">Specifies the new position of the left side of the window.</param>
        /// <param name="y">Specifies the new position of the top of the window.</param>
        /// <param name="nWidth">Specifies the new width of the window.</param>
        /// <param name="nHeight">Specifies the new height of the window.</param>
        /// <param name="bRepaint">Specifies whether the window is to be repainted. If this parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of moving a child window.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para></returns>
        [DllImport(DllName)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport(DllName)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport(DllName)]
        public static extern IntPtr GetForegroundWindow();

        #endregion

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

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowPlacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point minPosition;
            public System.Drawing.Point maxPosition;
            public Rect normalPosition;
            public Rect device;
        }

        #endregion
    }
}
