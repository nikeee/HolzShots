using System.Diagnostics;

namespace HolzShots.Input.Keyboard
{
    /// <summary>Event Args for the event that is fired after the hotkey has been pressed.</summary>
    public class HotkeyPressedEventArgs : EventArgs
    {
        public Hotkey Hotkey { get; }
        public KeyboardHook Hook { get; }

        public HotkeyPressedEventArgs(KeyboardHook hook, Hotkey hotkey)
        {
            Debug.Assert(hook != null);
            Debug.Assert(hotkey != null);
            Hook = hook;
            Hotkey = hotkey;
        }
    }
}
