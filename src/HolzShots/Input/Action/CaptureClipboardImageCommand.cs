using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HolzShots.Composition.Command;
using HolzShots.Windows.Forms;

namespace HolzShots.Input.Actions;

[Command("captureClipboardImage")]
public class CaptureClipboardImageCommand : ImageCapturingCommand
{
    protected override async Task InvokeInternal(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
    {
        if (parameters == null)
            throw new ArgumentNullException(nameof(parameters));
        if (settingsContext == null)
            throw new ArgumentNullException(nameof(settingsContext));

        var image = GetClipboardImage();
        if (image == null)
            return;

        var shot = Screenshot.FromImage(image, null, ScreenshotSource.Clipboard);
        await ProcessCapturing(shot, settingsContext).ConfigureAwait(true);
    }

    private static Bitmap? GetClipboardImage()
    {
        try
        {
            return Clipboard.GetImage() as Bitmap;
        }
        catch (Exception ex) when (ex is System.Runtime.InteropServices.ExternalException || ex is ThreadStateException)
        {
            NotificationManager.RetrievingImageFromClipboardFailed(ex);
            return default;
        }
    }
}
