using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace HolzShots.Input
{
    internal class HotkeyWindowHost : NativeWindow, IDisposable
    {
        public HotkeyWindowHost() => CreateHandle(new CreateParams()); // create the handle for the window.

        public sealed override void CreateHandle(CreateParams cp) => base.CreateHandle(cp);

        /// <summary>Overridden to get the notifications.</summary>
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            // check if we got a hotkey pressed.
            if (m.Msg == WM_HOTKEY)
            {
                // get the keys.
                var key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                var modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                // invoke the event to notify the parent.
                KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifier, key));
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public void RegisterHotkey(ModifierKeys modifiers, Keys key, int id)
        {
            Trace.WriteLine($"REGISTERING hotkey: {id} {modifiers} {key}");
            if (!NativeMethods.RegisterHotKey(Handle, id, modifiers | ModifierKeys.NoRepeat, key))
                throw new Win32Exception();
        }

        public void UnregisterHotkey(int id)
        {
            Trace.WriteLine($"Unregistering hotkey: {id}");
            if (!NativeMethods.UnregisterHotKey(Handle, id))
                throw new Win32Exception();
        }

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        #region IDisposable Members

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

#pragma warning disable RECS0154 // Parameter is never used
        protected virtual void Dispose(bool disposing)
#pragma warning restore RECS0154 // Parameter is never used
        {
            if (!_disposed)
            {
                DestroyHandle();
                _disposed = true;
            }
        }

        ~HotkeyWindowHost() => Dispose(false);

        #endregion
    }
}
