using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HolzShots.Input
{
    public abstract class KeyboardHook : IDisposable
    {
        private readonly Dictionary<int, Hotkey> registeredKeys = new Dictionary<int, Hotkey>();

        protected Dictionary<int, Hotkey> RegisteredKeys => registeredKeys;

        /// <summary>Registers a hotkey in the system.</summary>
        public abstract void RegisterHotkey(Hotkey hotkey);

        /// <summary>Unregisters a hotkey in the system.</summary>
        public abstract void UnregisterHotkey(Hotkey hotkey);

        public abstract void UnregisterAllHotkeys();

        protected void KeyPressed(object sender, KeyPressedEventArgs args)
        {
            Debug.Assert(args != null);

            var key = args.GetIdentifier();

            if (RegisteredKeys.TryGetValue(key, out Hotkey hk) && hk != null)
                hk.InvokePressed(this);
        }

        #region IDisposable Members

        //private bool _isDisposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //if (!_isDisposed)
            //{
            //    UnregisterAllHotkeys();
            //    _isDisposed = true;
            //}
        }

        ~KeyboardHook() => Dispose(false);

        #endregion
    }
}
