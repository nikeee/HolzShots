
namespace HolzShots.Drawing;

public static class WindowRedraw
{
    public static void StartRedraw(nint windowHandle)
    {
        Native.User32.LockWindowUpdate(0);
        Native.User32.SendMessage(windowHandle, Native.WindowMessage.WM_SetRedraw, 1, 0);
    }
    public static void StopRedraw(nint windowHandle)
    {
        Native.User32.SendMessage(windowHandle, Native.WindowMessage.WM_SetRedraw, 0, 0);
        Native.User32.LockWindowUpdate(windowHandle);
    }
}
