using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HolzShots.Input;

namespace HolzShots.Composition.Command
{
    public class HotkeyCommand : IHotkeyAction
    {
        public bool Enabled { get; }
        public Hotkey Hotkey { get; }

        private readonly CommandManager _parentManager;
        private readonly string _commandToDispatch;

        public HotkeyCommand(CommandManager parentManager, KeyBinding binding)
        {
            Debug.Assert(parentManager != null);
            Debug.Assert(binding != null);

            _parentManager = parentManager;
            Hotkey = binding.Keys ?? throw new ArgumentNullException(nameof(binding.Keys));
            Enabled = binding.Enabled;
            _commandToDispatch = binding.Command ?? throw new ArgumentNullException(nameof(binding.Command));
        }

        public Task Invoke(object sender, HotkeyPressedEventArgs e) => _parentManager.Dispatch(_commandToDispatch);
    }
}
