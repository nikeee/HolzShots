using System.Collections.Generic;
using HolzShots.Composition.Command;
using HolzShots.IO;

namespace HolzShots.Input.Actions;

[Command("openSaveDirectory")]
public class OpenSaveDirectory : ICommand<HSSettings>
{
    public Task Invoke(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        ArgumentNullException.ThrowIfNull(settingsContext);

        ScreenshotAggregator.OpenPictureSaveDirectory(settingsContext);
        return Task.CompletedTask;
    }
}
