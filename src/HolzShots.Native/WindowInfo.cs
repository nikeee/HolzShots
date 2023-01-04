using System.Runtime.InteropServices;

namespace HolzShots.Native;

[StructLayout(LayoutKind.Sequential)]
public struct WindowInfo
{
    public uint cbSize;
    public Rect rcWindow;
    public Rect rcClient;
    public uint dwStyle;
    public uint dwExStyle;
    public uint dwWindowStatus;
    public uint cxWindowBorders;
    public uint cyWindowBorders;
    public ushort atomWindowType;
    public ushort wCreatorVersion;

    public WindowInfo(bool? _) : this() => cbSize = (uint)(Marshal.SizeOf(typeof(WindowInfo)));
}
