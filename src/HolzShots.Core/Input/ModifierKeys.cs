using System;

namespace HolzShots.Input
{

    /// <summary>The enumeration of possible modifiers.</summary>
    [Flags]
    public enum ModifierKeys
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8,
        NoRepeat = 0x4000,
    }
}
