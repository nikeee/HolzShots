using System;

namespace HolzShots.Input
{
    [Serializable]
    public class HotkeyRegistrationException : Exception
    {
        public HotkeyRegistrationException(Hotkey hotkey, Exception innerException)
            : base("Failed to register/unregister hotkey " + hotkey + ".", innerException)
        { }
    }
}
