using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using HolzShots.Composition.Command;
using HolzShots.Drawing;
using HolzShots.Net;
using HolzShots.Windows.Forms;
using HolzShots.Windows.Net;

namespace HolzShots.Input.Actions
{
    /// <summary> TODO: Support other formats than images </summary>
    [Command("uploadFile")]
    public class UploadFileCommand : ImageFileDependentCommand, ICommand<HSSettings>
    {
        private const string UploadFile = "Select File to Upload";

        public async Task Invoke(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            if (settingsContext == null)
                throw new ArgumentNullException(nameof(settingsContext));

            var fileName = parameters.Count != 1 || !parameters.ContainsKey(FileNameParameter) ? ShowFileSelector(UploadFile) : parameters[FileNameParameter];

            if (fileName == null)
                return; // We did not get a valid file name (user cancelled or something else was strange)

            if (!CanProcessFile(fileName))
                // TODO: Error Message
                return;

            using var bmp = new Bitmap(fileName);

            var format = ImageFormatInformation.GetImageFormatFromFileName(fileName);
            Debug.Assert(format != null);

            try
            {
                var result = await UploadDispatcher.InitiateUploadToDefaultUploader(bmp, settingsContext, HolzShotsApplication.Instance.Uploaders, format, null).ConfigureAwait(true);
                UploadHelper.InvokeUploadFinishedUI(result, settingsContext);
            }
            catch (UploadCanceledException)
            {
                Forms.ToastNotifications.ShowOperationCanceled();
            }
            catch (UploadException ex)
            {
                await NotificationManager.UploadFailed(ex);
            }
        }
    }
}
