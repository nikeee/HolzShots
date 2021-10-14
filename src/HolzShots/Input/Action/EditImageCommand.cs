using System.Drawing;
using System.Collections.Generic;
using HolzShots.Composition.Command;

namespace HolzShots.Input.Actions
{
    [Command("editImage")]
    public class EditImageCommand : ImageFileDependentCommand, ICommand<HSSettings>
    {
        private const string OpenInShotEditor = "Open Image in ShotEditor";

        public Task Invoke(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            if (settingsContext == null)
                throw new ArgumentNullException(nameof(settingsContext));

            var fileName = parameters.Count != 1 || !parameters.ContainsKey(FileNameParameter) ? ShowFileSelector(OpenInShotEditor) : parameters[FileNameParameter];

            if (fileName == null)
                return Task.CompletedTask; // We did not get a valid file name (user cancelled or something else was strange)

            if (!CanProcessFile(fileName))
                return Task.CompletedTask; // TODO: Error Message

            var bmp = new Bitmap(fileName);
            var shot = Screenshot.FromImported(bmp);
            var editor = new UI.ShotEditor(shot, HolzShotsApplication.Instance.Uploaders, settingsContext);
            editor.Disposed += (s, e) => bmp.Dispose();
            editor.Show();

            return Task.CompletedTask;
        }
    }
}
