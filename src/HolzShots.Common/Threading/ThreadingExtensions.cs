using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
