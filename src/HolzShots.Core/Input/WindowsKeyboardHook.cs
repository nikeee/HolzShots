using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace HolzShots.Input
{
    public sealed class WindowsKeyboardHook : KeyboardHook
    {
        private readonly HotkeyWindowHost _window;
        private ISynchronizeInvoke _invoke;
        private readonly object _lockObj = new object();

        public WindowsKeyboardHook(ISynchronizeInvoke invoke)
        {
            _window = new HotkeyWindowHost();
            _window.KeyPressed += KeyPressed; // register the event of the inner native window.
            _invoke = invoke;
        }

        /// <summary>Registers a hotkey in the system.</summary>
        public override void RegisterHotkey(Hotkey hotkey) => InvokeWrapper(() => RegisterHotkeyInternal(hotkey));

        private void RegisterHotkeyInternal(Hotkey hotkey)
        {
            Debug.WriteLine($"LOL THREAD ID REGISTER: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            if (hotkey == null)
                throw new ArgumentNullException(nameof(hotkey));

            int id = hotkey.GetHashCode();
            if (RegisteredKeys.ContainsKey(id))
                throw new HotkeyRegistrationException(hotkey, new InvalidOperationException("Hotkey already registered."));

            try
            {
                _window.RegisterHotkey(hotkey.Modifiers, hotkey.Key, id);
            }
            catch (Win32Exception ex)
            {
                throw new HotkeyRegistrationException(hotkey, ex);
            }
            RegisteredKeys.Add(id, hotkey);
        }

        /// <summary>Unregisters a hotkey in the system.</summary>
        public override void UnregisterHotkey(Hotkey hotkey) => InvokeWrapper(() => UnregisterHotkeyInternal(hotkey));

        private void UnregisterHotkeyInternal(Hotkey hotkey)
        {
            Debug.WriteLine($"LOL THREAD ID UNREGISTER: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            if (hotkey == null)
                throw new ArgumentNullException(nameof(hotkey));

            int id = hotkey.GetHashCode();

            Hotkey hk;
            if (!RegisteredKeys.TryGetValue(id, out hk))
                throw new HotkeyRegistrationException(hotkey, new InvalidOperationException("Hotkey not registered."));

            try
            {
                _window.UnregisterHotkey(id);
            }
            catch (Win32Exception ex)
            {
                throw new HotkeyRegistrationException(hotkey, ex);
            }
            hotkey.RemoveAllEventHandlers();
            RegisteredKeys.Remove(id);
        }

        public override void UnregisterAllHotkeys()
        {
            lock (_lockObj)
            {
                // unregister all the registered hotkeys.
                var toUnregister = new List<Hotkey>(RegisteredKeys.Values);
                foreach (var key in toUnregister)
                    UnregisterHotkey(key);
            }
        }

        private void InvokeWrapper(Action action)
        {
            if (_invoke == null || !_invoke.InvokeRequired)
                action();
            else
                _invoke.BeginInvoke(action, null);
        }

        private bool _isDisposed;
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!_isDisposed)
            {
                _window.Dispose(); // dispose the inner native window.
                _isDisposed = true;
            }
        }
    }
}
