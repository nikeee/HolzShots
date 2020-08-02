using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using HolzShots.Input;

namespace HolzShots.Composition.Command
{

    public class CommandManager
    {
        private Dictionary<string, Func<ICommand>> Actions { get; } = new Dictionary<string, Func<ICommand>>();

        public void RegisterCommand(string command, Func<ICommand> ctor)
        {
            Debug.Assert(command != null);
            Debug.Assert(ctor != null);

            if (Actions.ContainsKey(command))
            {
                Debug.Assert(false);
                return;
            }
            Actions[command.ToLowerInvariant()] = ctor;
        }

        public IHotkeyAction GetHotkeyActionFromKeyBinding(KeyBinding binding)
        {
            // We assume that everything is already checked (validation step should have validated that all hotkeys are != null
            // We also assume that every key is only assigned once

            Debug.Assert(binding != null);
            Debug.Assert(binding.Keys != null);
            Debug.Assert(binding.Command != null);

            return new HotkeyCommand(this, binding);
        }

        private ICommand GetCommand(string name)
        {
            Debug.Assert(name != null);
            return Actions.TryGetValue(name.ToLowerInvariant(), out var commandCtor)
                ? commandCtor()
                : null;
        }

        public Task DispatchCommand(string name)
        {
            var cmd = GetCommand(name);
            Debug.Assert(cmd != null);

            return cmd == null
                ? Task.CompletedTask
                : cmd.Invoke();
        }
    }
}
