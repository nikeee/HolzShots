using System.Drawing;
using System.Windows.Forms;
using HolzShots.Composition.Command;
using HolzShots.Windows.Forms;

namespace HolzShots.Input.Actions
{
    [Command("captureClipboard")]
    public class CaptureClipboardCommand : CapturingCommand
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

            var shot = Screenshot.FromImage(image, Cursor.Position, ScreenshotSource.Clipboard);
            await ProcessCapturing(shot, settingsContext).ConfigureAwait(true);
        }

        private static Bitmap? GetClipboardImage()
        {
            try
            {
                return Clipboard.ContainsImage()
                    ? (Bitmap)Clipboard.GetImage()
                    : default;
            }
            catch (Exception ex) when (ex is System.Runtime.InteropServices.ExternalException || ex is System.Threading.ThreadStateException)
            {
                NotificationManager.RetrievingImageFromClipboardFailed(ex);
                return default;
            }
        }
    }
}
