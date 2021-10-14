
namespace HolzShots.Input.Keyboard
{
    public interface IHotkeyAction
    {
        bool Enabled { get; }
        Hotkey Hotkey { get; }

        Task Invoke(object? sender, HotkeyPressedEventArgs e);
    }
}
