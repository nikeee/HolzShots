using System;
using System.Runtime.InteropServices;
using System.Text;

namespace HolzShots
{
    public class Kernel32
    {
        private const string DllName = "kernel32.dll";

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern uint GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);
    }
}
