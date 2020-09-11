using System;

namespace HolzShots.Drawing
{
    public static class WindowRedraw
    {
        public static void StartRedraw(IntPtr windowHandle)
        {
            Native.User32.LockWindowUpdate(IntPtr.Zero);
            Native.User32.SendMessage(windowHandle, Native.WindowMessage.WM_SetRedraw, new IntPtr(1), IntPtr.Zero);
        }
        public static void StopRedraw(IntPtr windowHandle)
        {
            Native.User32.SendMessage(windowHandle, Native.WindowMessage.WM_SetRedraw, IntPtr.Zero, IntPtr.Zero);
            Native.User32.LockWindowUpdate(windowHandle);
        }
    }
}
