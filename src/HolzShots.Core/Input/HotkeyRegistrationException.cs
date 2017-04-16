using System;

namespace HolzShots.Input
{
    [Serializable]
    public class HotkeyRegistrationException : Exception
    {
        public HotkeyRegistrationException()
            : base()
        { }
        public HotkeyRegistrationException(string message)
            : base(message)
        { }
        public HotkeyRegistrationException(string message, Exception innerException)
            : base(message, innerException)
        { }
        public HotkeyRegistrationException(Hotkey hotkey, Exception innerException)
            : this($"Failed to register/unregister hotkey {hotkey}.", innerException)
        { }
    }
}
