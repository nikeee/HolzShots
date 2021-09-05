using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HolzShots.Input
{
    public class HolzShotsActionCollection : HotkeyActionCollection, IDisposable
    {
        private readonly object _lockObj = new();
        private bool disposedValue;

        public HolzShotsActionCollection(KeyboardHook hook, params IHotkeyAction[] actions)
            : base(hook, actions) { }

        public override void Refresh()
        {
            Debug.Assert(Hook != null);
            Debug.Assert(_lockObj != null);

            List<Exception> exeptions = new List<Exception>(Count);
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
                            exeptions.Add(ex);
                            continue;
                        }

                        h.KeyPressed += (sender, e) => { action.Invoke(sender, e); };
                    }
                }
            }

            if (exeptions.Count > 0)
                throw new AggregateException("A number of Hotkeys failed to register", exeptions);
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
}
