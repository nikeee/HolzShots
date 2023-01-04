using System.Runtime.InteropServices;
using System.Text;

namespace HolzShots;

public static class Shlwapi
{
    const string DllName = "shlwapi.dll";

    [DllImport(DllName, CharSet = CharSet.Unicode, SetLastError = false)]
    public static extern bool PathFindOnPath([In, Out] StringBuilder file, [In] string[]? otherDirs);

    public const int MAX_PATH = 260;
}
