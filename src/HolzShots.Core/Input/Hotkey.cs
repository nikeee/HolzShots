using System.ComponentModel;
using System.Configuration;
using System.Text;

namespace HolzShots.Input
{
    [Serializable]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    [TypeConverter(typeof(HotkeyTypeConverter))]
    public class Hotkey
    {
        private const char KeySeparator = '+';

        public ModifierKeys Modifiers { get; }
        public Keys Key { get; }

        /// <summary> Do not use. Only there for serialization. </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2222:Do not decrease inherited member visibility", Justification = "Only there for serialization.")]
        private Hotkey() { }

        public Hotkey(ModifierKeys modifiers, Keys key)
        {
            Modifiers = modifiers;
            Key = key;
        }

        public bool IsNone() => Modifiers == ModifierKeys.None && Key == Keys.None;

        public override int GetHashCode() => (int)Key << 16 | (int)Modifiers;

        internal static Hotkey FromHashCode(int hashCode)
        {
            int key = hashCode >> 16;
            int mod = hashCode & 0xFFFF;
            return new Hotkey((ModifierKeys)mod, (Keys)key);
        }

        public static Hotkey? Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            ModifierKeys mods = ModifierKeys.None;
            Keys keys = Keys.None;

            var split = value.Split(KeySeparator);
            foreach (var key in split)
            {
                var keyStr = key.Trim().ToLowerInvariant();
                switch (keyStr)
                {
                    case "ctrl":
                    case "control":
                        mods |= ModifierKeys.Control;
                        continue;
                    case "alt":
                        mods |= ModifierKeys.Alt;
                        continue;
                    case "win":
                    case "super":
                        mods |= ModifierKeys.Win;
                        continue;
                    case "shift":
                        mods |= ModifierKeys.Shift;
                        continue;
                }

                if (Enum.TryParse(keyStr, true, out keys))
                    break;
            }

            return new Hotkey(mods, keys);
        }
        public static Hotkey FromKeyboardEvent(KeyEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            ModifierKeys modKeys = ModifierKeys.None;
            Keys k = e.KeyCode;

            if ((e.Modifiers & Keys.Control) == Keys.Control)
            {
                if (k == Keys.None)
                    return new Hotkey(ModifierKeys.None, Keys.ControlKey);
                modKeys |= ModifierKeys.Control;
            }

            if ((e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                if (k == Keys.None)
                    return new Hotkey(ModifierKeys.None, Keys.ShiftKey);
                modKeys |= ModifierKeys.Shift;
            }

            if ((e.Modifiers & Keys.Alt) == Keys.Alt)
            {
                if (k == Keys.None)
                    return new Hotkey(ModifierKeys.None, Keys.Menu);
                modKeys |= ModifierKeys.Alt;
            }

            if (k == Keys.None)
                return new Hotkey(modKeys, k);
            return new Hotkey(modKeys, k);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if ((Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
                sb.Append("Alt").Append(KeySeparator);
            if ((Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                sb.Append("Ctrl").Append(KeySeparator);
            if ((Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                sb.Append("Shift").Append(KeySeparator);
            if ((Modifiers & ModifierKeys.Win) == ModifierKeys.Win)
                sb.Append("Win").Append(KeySeparator);

            sb.Append(Key == Keys.Space ? "Space" : Key.ToString());
            return sb.ToString();
        }

        /// <remarks> https://stackoverflow.com/questions/91778 </remarks>
        internal void RemoveAllEventHandlers()
        {
            lock (_delegates)
            {
                foreach (var e in _delegates)
                    _keyPressed -= e;
                _delegates.Clear();
            }
        }

        private readonly System.Collections.Generic.HashSet<EventHandler<HotkeyPressedEventArgs>> _delegates = new System.Collections.Generic.HashSet<EventHandler<HotkeyPressedEventArgs>>();
        private event EventHandler<HotkeyPressedEventArgs>? _keyPressed;

        /// <summary>The hotkey has been pressed.</summary>
        public event EventHandler<HotkeyPressedEventArgs> KeyPressed
        {
            add
            {
                _keyPressed += value;
                _delegates.Add(value);
            }
            remove
            {
                _keyPressed -= value;
                _delegates.Remove(value);
            }
        }

        internal void InvokePressed(KeyboardHook hook) => _keyPressed?.Invoke(this, new HotkeyPressedEventArgs(hook, this));
    }
}
