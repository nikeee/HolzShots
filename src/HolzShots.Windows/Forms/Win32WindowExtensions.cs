using System.Windows.Forms;

namespace HolzShots.Windows.Forms;

public static class Win32WindowExtensions
{
    public static IntPtr GetHandle(this IWin32Window window) => window?.Handle ?? IntPtr.Zero;
}
