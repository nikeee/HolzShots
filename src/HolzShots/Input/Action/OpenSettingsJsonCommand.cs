using System.Collections.Generic;
using HolzShots.Composition.Command;

namespace HolzShots.Input.Actions;

[Command("openSettingsJson")]
public class OpenSettingsJsonCommand : ICommand<HSSettings>
{
    public Task Invoke(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        ArgumentNullException.ThrowIfNull(settingsContext);

        UserSettings.OpenSettingsInDefaultEditor();
        return Task.CompletedTask;
    }
}
