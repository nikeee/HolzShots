using System.Threading.Tasks;

namespace HolzShots.Input
{
    public interface IHotkeyAction
    {
        bool Enabled { get; }
        Hotkey Hotkey { get; }

        Task Invoke(object sender, HotkeyPressedEventArgs e);
    }
}