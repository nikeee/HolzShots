using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace HolzShots
{
    public static class ClipboardEx
    {
        /// <summary> Wrapper around Clipboard.SetText that catches exceptions. </summary>
        public static bool SetText(string text)
        {
            Debug.Assert(text != null);

            try
            {
                Clipboard.SetText(text);
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return false;
            }
        }
    }
}
