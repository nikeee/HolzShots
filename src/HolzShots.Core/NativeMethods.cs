using HolzShots.Input;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HolzShots
{
    internal static class NativeMethods
    {
        private const string User32 = "user32.dll";

        [DllImport(User32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, ModifierKeys fsModifiers, Keys vk);

        [DllImport(User32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
