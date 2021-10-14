using System.Collections.Generic;
using System.ComponentModel;

namespace HolzShots.Input.Keyboard
{
    public sealed class WindowsKeyboardHook : KeyboardHook
    {
        private readonly HotkeyWindowHost _window;
        private readonly ISynchronizeInvoke _synchronizer;
        private readonly object _lockObj = new();

        public WindowsKeyboardHook(ISynchronizeInvoke synchronizer)
        {
            _window = new HotkeyWindowHost();
            _window.KeyPressed += KeyPressed; // register the event of the inner native window.
            _synchronizer = synchronizer;
        }

        /// <summary>Registers a hotkey in the system.</summary>
        public override void RegisterHotkey(Hotkey hotkey) => InvokeWrapper(() => RegisterHotkeyInternal(hotkey));

        private void RegisterHotkeyInternal(Hotkey hotkey)
        {
            System.Diagnostics.Trace.WriteLine($"Thread ID Register: {System.Threading.Thread.CurrentThread.ManagedThreadId}");

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
            System.Diagnostics.Trace.WriteLine($"Thread ID Unregister: {System.Threading.Thread.CurrentThread.ManagedThreadId}");

            if (hotkey == null)
                throw new ArgumentNullException(nameof(hotkey));

            int id = hotkey.GetHashCode();

            if (!RegisteredKeys.ContainsKey(id))
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
            if (_synchronizer == null || !_synchronizer.InvokeRequired)
                action();
            else
                _synchronizer.BeginInvoke(action, null);
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
