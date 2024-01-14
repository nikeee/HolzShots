using System.Collections.Generic;
using System.Diagnostics;

namespace HolzShots.Input.Keyboard;

public class HolzShotsActionCollection(KeyboardHook hook, params IHotkeyAction[] actions) : HotkeyActionCollection(hook, actions), IDisposable
{
    private readonly object _lockObj = new();
    private bool disposedValue;

    public override void Refresh()
    {
        Debug.Assert(Hook is not null);
        Debug.Assert(_lockObj is not null);

        var exceptions = new List<Exception>(Count);
        lock (_lockObj)
        {
            Hook.UnregisterAllHotkeys();

            foreach (var action in Actions)
            {
                var h = action.Hotkey;
                if (action.Enabled)
                {
                    try
                    {
                        Hook.RegisterHotkey(h);
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                        continue;
                    }

                    h.KeyPressed += (sender, e) => { action.Invoke(sender, e); };
                }
            }
        }

        if (exceptions.Count > 0)
            throw new AggregateException("A number of Hotkeys failed to register", exceptions);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
            }

            lock (_lockObj)
                Hook.UnregisterAllHotkeys();
            disposedValue = true;
        }
    }

    ~HolzShotsActionCollection()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
