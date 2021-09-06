using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using HolzShots.Input;

namespace HolzShots.Composition.Command
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CommandAttribute : Attribute
    {
        public string Name { get; }
        public CommandAttribute(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public class CommandManager<TSettings>
        where TSettings : new()
    {
        private Dictionary<string, ICommand<TSettings>> Actions { get; } = new Dictionary<string, ICommand<TSettings>>();

        private readonly SettingsManager<TSettings> _settingsManager;
        public CommandManager(SettingsManager<TSettings> settingsManager) => _settingsManager = settingsManager ?? throw new ArgumentNullException(nameof(settingsManager));

        public void RegisterCommand(ICommand<TSettings> command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var name = GetCommandNameForType(command.GetType());
            Debug.Assert(name != null);
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name of command is null or white space");

            name = name.ToLowerInvariant();

            if (Actions.ContainsKey(name))
            {
                Debug.Fail($"Unhandled command: '{name}'");
                return;
            }
            Actions[name] = command;
        }

        public IHotkeyAction GetHotkeyActionFromKeyBinding(KeyBinding binding)
        {
            // We assume that everything is already checked (validation step should have validated that all hotkeys are != null
            // We also assume that every key is only assigned once

            Debug.Assert(binding != null);
            Debug.Assert(binding.Keys != null);
            Debug.Assert(binding.Command != null);

            return new HotkeyCommand<TSettings>(this, binding, () => _settingsManager.CurrentSettings);
        }

        private ICommand<TSettings>? GetCommand(string name)
        {
            Debug.Assert(IsRegisteredCommand(name), $"Command must be registered: {name ?? "<null>"}");
            return Actions.TryGetValue(name.ToLowerInvariant(), out var command)
                ? command
                : null;
        }

        private string? GetCommandNameForType(Type t) => t.GetCustomAttribute<CommandAttribute>(false)?.Name;
        private string? GetCommandNameForType<T>() where T : ICommand<TSettings> => GetCommandNameForType(typeof(T));

        public bool IsRegisteredCommand(string name) => !string.IsNullOrWhiteSpace(name) && Actions.ContainsKey(name.ToLowerInvariant());

        public Task Dispatch<T>(TSettings currentSettings) where T : ICommand<TSettings> => Dispatch<T>(currentSettings, ImmutableDictionary<string, string>.Empty);
        public Task Dispatch<T>(TSettings currentSettings, IReadOnlyDictionary<string, string> parameters) where T : ICommand<TSettings>
        {
            var name = GetCommandNameForType<T>();
            if (name == null)
                throw new InvalidOperationException();
            return Dispatch(name, currentSettings, parameters);
        }

        public Task Dispatch(CommandDeclaration command, TSettings currentSettings)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            Debug.Assert(IsRegisteredCommand(command.CommandName));

            var commandInstance = GetCommand(command.CommandName);
            Debug.Assert(commandInstance != null);

            var contextEffectiveSettings = _settingsManager.DeriveContextEffectiveSettings(currentSettings, command.Overrides);
            return commandInstance == null
                ? Task.CompletedTask
                : commandInstance.Invoke(command.Parameters, contextEffectiveSettings);
        }

        public Task Dispatch(string name, TSettings currentSettings) => Dispatch(name, currentSettings, ImmutableDictionary<string, string>.Empty);

        public Task Dispatch(string name, TSettings currentSettings, IReadOnlyDictionary<string, string> parameters)
        {
            var commandDeclaration = new CommandDeclaration()
            {
                CommandName = name,
                Parameters = parameters,
                Overrides = ImmutableDictionary<string, dynamic>.Empty,
            };
            return Dispatch(commandDeclaration, currentSettings);
        }
    }
}
