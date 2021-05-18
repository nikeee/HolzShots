using System;
using System.Text;
using System.Runtime.InteropServices;

namespace HolzShots
{
    public class Kernel32
    {
        private const string DllName = "kernel32.dll";

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern uint GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);
    }
}
