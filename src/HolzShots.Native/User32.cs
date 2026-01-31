using System.Runtime.InteropServices;
using System.Text;

namespace HolzShots.Native;

public static partial class User32
{
    private const string DllName = "user32.dll";

    #region SendMessage

    [LibraryImport(DllName, EntryPoint = "SendMessageW")]
    public static partial nint SendMessage(nint hWnd, WindowMessage msg, nint wParam, nint lParam);

    [DllImport(DllName, EntryPoint = "SendMessageW")]
    public static extern nint SendMessage(nint hWnd, WindowMessage msg, nint wParam, StringBuilder lParam);

    [LibraryImport(DllName, EntryPoint = "SendMessageW")]
    public static partial nint SendMessage(nint hWnd, WindowMessage msg, nint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

    #endregion
    #region Window Functions

    [DllImport(DllName, SetLastError = true, EntryPoint = "GetWindowText")]
    public static extern int GetWindowText(nint hwnd, StringBuilder lpString, int cch);

    [LibraryImport(DllName, SetLastError = true, EntryPoint = "GetWindowTextLengthW")]
    public static partial int GetWindowTextLength(nint hwnd);

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsIconic(nint hWnd);

    [DllImport(DllName, EntryPoint = "GetClassName")]
    public static extern int GetClassName(nint hWnd, StringBuilder lpClassName, int nMaxCount);

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool FlashWindowEx(in FlashWindowInfo window);

    /// <summary>
    /// Callback EnumWindowsCallback should return true to continue enumerating or false to stop.
    /// Refs:
    /// http://pinvoke.net/default.aspx/user32.EnumWindows
    /// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms633498(v=vs.85)
    /// </summary>
    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool EnumWindows(EnumWindowsCallback enumWindowFunction, nint lParam);

    /// <summary>
    /// Callback EnumWindowsCallback should return true to continue enumerating or false to stop.
    /// Refs:
    /// http://pinvoke.net/default.aspx/user32.EnumWindows
    /// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms633498(v=vs.85)
    /// </summary>
    public delegate bool EnumWindowsCallback(nint windowHandle, int lParam);

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsWindowVisible(nint windowHandle);

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool GetClientRect(nint hWnd, out Rect lpRect);

    [LibraryImport(DllName, SetLastError = true)]
    public static partial int MapWindowPoints(nint hWndFrom, nint hWndTo, ref Rect rect, [MarshalAs(UnmanagedType.U4)] uint cPoints);

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsZoomed(nint hWnd);

    [LibraryImport(DllName, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool GetWindowInfo(nint hwnd, ref WindowInfo windowInfo);

    #region Window Position

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool GetWindowRect(nint hWnd, out Rect result);

    [DllImport(DllName)]
    public static extern bool GetWindowPlacement(nint hWnd, out WindowPlacement result);

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool SetWindowPos(nint hWnd, nint hWndAfter, int x, int y, int width, int height, int flags);

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
    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool MoveWindow(nint hWnd, int x, int y, int nWidth, int nHeight, [MarshalAs(UnmanagedType.Bool)] bool bRepaint);

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool SetForegroundWindow(nint hWnd);

    [LibraryImport(DllName)]
    public static partial nint GetForegroundWindow();

    public static bool SetForegroundWindowEx(nint windowHandle)
    {
        var threadIdOfTargetWindow = GetWindowThreadProcessId(windowHandle, nint.Zero);
        var threadIdOfForegroundWindow = GetWindowThreadProcessId(GetForegroundWindow(), nint.Zero);

        if (threadIdOfTargetWindow == threadIdOfForegroundWindow)
            return SetForegroundWindow(windowHandle);

        AttachThreadInput(threadIdOfForegroundWindow, threadIdOfTargetWindow, true);
        var res = SetForegroundWindow(windowHandle);
        AttachThreadInput(threadIdOfForegroundWindow, threadIdOfTargetWindow, false);
        return res;
    }

    #endregion

    #endregion
    #region Threading

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool AttachThreadInput(int idAttach, int idAttachTo, [MarshalAs(UnmanagedType.Bool)] bool fAttach);

    [LibraryImport(DllName, SetLastError = true)]
    public static partial int GetWindowThreadProcessId(nint hWnd, nint processIdPointer);
    [LibraryImport(DllName, SetLastError = true)]
    public static partial int GetWindowThreadProcessId(nint hWnd, ref int processId);

    #endregion
    #region Drawing

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool DestroyIcon(nint hIcon);

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool LockWindowUpdate(nint hWndLock);

    [LibraryImport(DllName)]
    public static partial int ReleaseDC(nint hWnd, nint hDc);

    [LibraryImport(DllName)]
    public static partial nint GetWindowDC(nint window);

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool GetCursorInfo(ref CursorInfo pci);

    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool DrawIcon(nint hDC, int x, int y, nint iconHandle);

    #endregion
    #region DC / DesktopWindow

    [LibraryImport(DllName)]
    public static partial nint GetDesktopWindow();

    #endregion

    #region Types


    [StructLayout(LayoutKind.Sequential)]
    public struct CursorInfo
    {
        public int cbSize;
        public readonly CursorFlags flags;
        public readonly nint cursorHandle;

        // This is actually a point, but we assume ABI compatibility and no padding because we're LayoutKind.Sequential
        public readonly int screenPosX;
        public readonly int screenPosY;
    }

    [Flags]
    public enum CursorFlags : int
    {
        Showing = 0x00000001,
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct FlashWindowInfo(nint handle, User32.FlashWindowFlags flags, uint count, uint timeout)
    {
        private readonly uint _size = Convert.ToUInt32(Marshal.SizeOf(typeof(FlashWindowInfo)));
        private readonly nint _windowHandle = handle;
        private readonly FlashWindowFlags _flags = flags;
        private readonly uint _count = count;
        private readonly uint _timeout = timeout;

        public FlashWindowInfo(nint handle)
            : this(handle, FlashWindowFlags.TimerNoFg | FlashWindowFlags.Tray, uint.MaxValue) { }

        public FlashWindowInfo(nint handle, FlashWindowFlags flags, uint count)
            : this(handle, flags, count, 0) { }

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
