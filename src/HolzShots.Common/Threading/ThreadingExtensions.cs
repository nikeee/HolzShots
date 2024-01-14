using System.ComponentModel;

namespace HolzShots.Threading;

public static class ThreadingExtensions
{
    public static void InvokeIfNeeded(this ISynchronizeInvoke target, Action action)
    {
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(action);

        if (target.InvokeRequired)
            target.BeginInvoke(action, null);
        else
            action();
    }
}
