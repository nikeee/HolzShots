using System;
using System.ComponentModel;
using System.Diagnostics;

namespace HolzShots.Threading
{
    public static class ThreadingExtensions
    {
        public static void InvokeOnUIThread(this ISynchronizeInvoke target, Action action)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            Debug.Assert(action != null);

            if (target.InvokeRequired)
                target.BeginInvoke(action, null);
            else
                action();
        }
    }
}
