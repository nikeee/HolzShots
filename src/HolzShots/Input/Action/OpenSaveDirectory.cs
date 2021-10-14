using System.Collections.Generic;
using HolzShots.Composition.Command;
using HolzShots.IO;

namespace HolzShots.Input.Actions
{
    [Command("openSaveDirectory")]
    public class OpenSaveDirectory : ICommand<HSSettings>
    {
        public Task Invoke(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            if (settingsContext == null)
                throw new ArgumentNullException(nameof(settingsContext));

            ScreenshotAggregator.OpenPictureSaveDirectory(settingsContext);
            return Task.CompletedTask;
        }
    }
}
