using System.Windows.Forms;

namespace HolzShots.Windows.Forms;

public static class Win32WindowExtensions
{
    public static nint GetHandle(this IWin32Window window) => window?.Handle ?? nint.Zero;
}
