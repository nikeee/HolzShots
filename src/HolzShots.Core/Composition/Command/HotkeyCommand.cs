using System.Diagnostics;
using HolzShots.Input.Keyboard;

namespace HolzShots.Composition.Command;

public class HotkeyCommand<TSettings> : IHotkeyAction
    where TSettings : new()
{
    public bool Enabled { get; }
    public Hotkey Hotkey { get; }

    private readonly CommandManager<TSettings> _parentManager;
    private readonly CommandDeclaration _commandToDispatch;
    private readonly Func<TSettings> _currentSettingsGetter;

    public HotkeyCommand(CommandManager<TSettings> parentManager, KeyBinding binding, Func<TSettings> currentSettingsGetter)
    {
        Debug.Assert(parentManager is not null);
        Debug.Assert(binding is not null);
        Debug.Assert(currentSettingsGetter is not null);

        _parentManager = parentManager;
        Hotkey = binding.Keys ?? throw new ArgumentNullException(nameof(binding.Keys));
        Enabled = binding.Enabled;
        _commandToDispatch = binding.Command ?? throw new ArgumentNullException(nameof(binding.Command));
        _currentSettingsGetter = currentSettingsGetter ?? throw new ArgumentNullException(nameof(currentSettingsGetter));
    }

    public Task Invoke(object? sender, HotkeyPressedEventArgs e)
    {
        return _parentManager.Dispatch(_commandToDispatch, _currentSettingsGetter());
    }
}
