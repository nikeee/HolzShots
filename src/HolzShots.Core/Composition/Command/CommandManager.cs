using System;
using System.Collections.Generic;
using System.Diagnostics;
using HolzShots.Input;

namespace HolzShots.Composition.Command
{
    public class CommandManager
    {
        private Dictionary<string, Func<KeyBinding, IHotkeyAction>> Actions { get; } = new Dictionary<string, Func<KeyBinding, IHotkeyAction>>();

        public void RegisterCommand(string command, Func<KeyBinding, IHotkeyAction> creator)
        {
            Debug.Assert(command != null);
            Debug.Assert(creator != null);

            if (Actions.ContainsKey(command))
            {
                Debug.Assert(false);
                return;
            }
            Actions[command.ToLowerInvariant()] = creator;
        }

        public IHotkeyAction GetHotkeyActionFromKeyBinding(KeyBinding binding)
        {
            // We assume that everything is already checked (validation step should have validated that all hotkeys are != null
            // We also assume that every key is only assigned once

            Debug.Assert(binding != null);
            Debug.Assert(binding.Keys != null);
            Debug.Assert(binding.Command != null);

            var standardizedCommand = binding.Command.ToLowerInvariant();

            return Actions.TryGetValue(standardizedCommand, out var ctor)
                ? ctor(binding)
                : null;
        }
    }
}
