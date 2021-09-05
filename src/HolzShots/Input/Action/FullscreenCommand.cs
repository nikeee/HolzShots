using System.Diagnostics;
using System.Windows.Forms;
using HolzShots.Composition.Command;
using HolzShots.Drawing;
using HolzShots.Threading;

namespace HolzShots.Input.Actions
{
    [Command("captureEntireScreen")]
    public class FullscreenCommand : CapturingCommand
    {
        protected override async Task InvokeInternal(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
        {
            // TODO: Add proper assertion
            // Debug.Assert(ManagedSettings.EnableFullscreenScreenshot)

            // TODO: Re-add proper if condition
            // If ManagedSettings.EnableFullscreenScreenshot Then

            var shot = CaptureFullScreen();
            Debug.Assert(shot != null);
            await ProcessCapturing(shot, settingsContext).ConfigureAwait(true);
        }

        public static Screenshot CaptureFullScreen()
        {
            using var prio = new ProcessPriorityRequest();
            var screen = ScreenshotCreator.CaptureScreenshot(SystemInformation.VirtualScreen);
            return Screenshot.FromImage(screen, Cursor.Position, ScreenshotSource.Fullscreen);
        }
    }
}
