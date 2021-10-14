using System.Collections.Generic;
using HolzShots.Composition.Command;

namespace HolzShots.Input.Actions
{
    [Command("openSettingsJson")]
    public class OpenSettingsJsonCommand : ICommand<HSSettings>
    {
        public Task Invoke(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            if (settingsContext == null)
                throw new ArgumentNullException(nameof(settingsContext));

            UserSettings.OpenSettingsInDefaultEditor();
            return Task.CompletedTask;
        }
    }
}
