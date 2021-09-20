using System.Windows.Forms;

namespace HolzShots.Input.Keyboard
{
    /// <summary>Event Args for the event that is fired after the hotkey has been pressed.</summary>
    public class KeyPressedEventArgs : EventArgs
    {
        public ModifierKeys Modifier { get; }
        public Keys Key { get; }

        public KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            Modifier = modifier;
            Key = key;
        }

        public int GetIdentifier() => (int)Key << 16 | (int)Modifier;
    }
}
