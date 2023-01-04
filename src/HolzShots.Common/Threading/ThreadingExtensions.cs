using System.ComponentModel;

namespace HolzShots.Threading;

public static class ThreadingExtensions
{
    public static void InvokeIfNeeded(this ISynchronizeInvoke target, Action action)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        if (target.InvokeRequired)
            target.BeginInvoke(action, null);
        else
            action();
    }
}
