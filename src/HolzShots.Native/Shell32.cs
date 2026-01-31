using System.Runtime.InteropServices;

namespace HolzShots.Native;

public static partial class Shell32
{
    private const string DllName = "shell32.dll";

    [DllImport(DllName, BestFitMapping = false, ThrowOnUnmappableChar = true)]
    public static extern void SHAddToRecentDocs(ShellAddToRecentDocsFlags flag, [MarshalAs(UnmanagedType.LPStr)] string path);

    [LibraryImport(DllName)]
    public static partial nint SHAppBarMessage(Abm msg, ref AppBarData data);

    #region Types

    [Flags]
    public enum ShellAddToRecentDocsFlags
    {
        Pidl = 1,
        Path = 2,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AppBarData
    {
        public int cbSize;
        public nint hWnd;
        public int uCallbackMessage;
        public TaskbarPosition uEdge;
        public Rect rc;
        public nint lParam;
    }

    public enum Abm : uint
    {
        GetTaskBarPos = 5,
    }

    public enum TaskbarPosition : int
    {
        Unknown = -1,
        Left,
        Top,
        Right,
        Bottom,
    }

    #endregion
}
