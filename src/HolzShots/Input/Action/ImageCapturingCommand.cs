using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HolzShots.Composition.Command;
using HolzShots.Drawing;
using HolzShots.IO;
using HolzShots.Net;
using HolzShots.UI;
using HolzShots.Windows.Forms;
using HolzShots.Windows.Net;
using System.Collections.Generic;
using System.IO;

namespace HolzShots.Input.Actions;

public abstract class ImageCapturingCommand : ICommand<HSSettings>
{
    protected static async Task ProcessCapturing(Screenshot screenshot, HSSettings settingsContext)
    {
        ArgumentNullException.ThrowIfNull(screenshot);
        ArgumentNullException.ThrowIfNull(settingsContext);

        ScreenshotAggregator.HandleScreenshot(screenshot, settingsContext);

        switch (settingsContext.ActionAfterImageCapture)
        {
            case ImageCaptureHandlingAction.OpenEditor:
                var shower = new ShotEditor(screenshot, HolzShotsApplication.Instance.Uploaders, settingsContext);
                shower.Show();
                return;
            case ImageCaptureHandlingAction.Upload:
                try
                {
                    var result = await UploadDispatcher.InitiateUploadToDefaultUploader(screenshot.Image, settingsContext, HolzShotsApplication.Instance.Uploaders, null, null).ConfigureAwait(true);
                    UploadHelper.InvokeUploadFinishedUI(result, settingsContext);
                }
                catch (UploadCanceledException)
                {
                    NotificationManager.ShowOperationCanceled();
                }
                catch (UploadException ex)
                {
                    await NotificationManager.UploadFailed(ex);
                }
                return;
            case ImageCaptureHandlingAction.Copy:
                try
                {
                    Clipboard.SetImage(screenshot.Image);
                    NotificationManager.ShowImageCopiedConfirmation();
                }
                catch (Exception ex) when (ex is ExternalException
                       || ex is System.Threading.ThreadStateException
                       || ex is ArgumentNullException)
                {
                    NotificationManager.CopyImageFailed(ex);
                }
                return;
            case ImageCaptureHandlingAction.SaveAs:
                PromptSaveAs(screenshot, settingsContext);
                return;
            case ImageCaptureHandlingAction.None:
                return; // Intentionally do nothing
            default:
                return;
        }
    }

    private static void PromptSaveAs(Screenshot screenshot, HSSettings settingsContext)
    {
        // TODO: Move this
        using var sfd = new SaveFileDialog();
        sfd.Filter = $"{Localization.PngImage}|*.png|{Localization.JpgImage}|*.jpg";
        sfd.DefaultExt = ".png";
        sfd.CheckPathExists = true;
        sfd.Title = Localization.ChooseDestinationFileName;

        var res = sfd.ShowDialog();
        if (res != DialogResult.OK)
            return;

        var f = sfd.FileName;
        if (string.IsNullOrWhiteSpace(f))
            return;

        var format = ImageFormatInformation.GetImageFormatFromFileName(f);
        Debug.Assert(format is not null);

        try
        {
            using var fileStream = File.OpenWrite(f);
            screenshot.Image.SaveExtended(fileStream, format);
        }
        catch (PathTooLongException)
        {
            NotificationManager.PathIsTooLong(f);
        }
        catch (Exception ex)
        {
            NotificationManager.ErrorSavingImage(ex);
        }
    }

    public async Task Invoke(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
    {
        ArgumentNullException.ThrowIfNull(settingsContext);

        if (settingsContext.CaptureDelay > 0)
            await Task.Delay(TimeSpan.FromSeconds(settingsContext.CaptureDelay));

        await InvokeInternal(parameters, settingsContext);
    }

    protected abstract Task InvokeInternal(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext);
}
