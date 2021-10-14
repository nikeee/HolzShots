using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using HolzShots.Composition.Command;
using HolzShots.Drawing;
using HolzShots.Threading;

namespace HolzShots.Input.Actions
{
    [Command("captureEntireScreen")]
    public class CaptureEntireScreenCommand : ImageCapturingCommand
    {
        protected override async Task InvokeInternal(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
        {
            // TODO: Add proper assertion
            // Debug.Assert(ManagedSettings.EnableFullscreenScreenshot)

            // TODO: Re-add proper if condition
            // If ManagedSettings.EnableFullscreenScreenshot Then

            var shot = CaptureFullScreen(settingsContext);
            Debug.Assert(shot != null);
            await ProcessCapturing(shot, settingsContext).ConfigureAwait(true);
        }

        public static Screenshot CaptureFullScreen(HSSettings settingsContext)
        {
            using var prio = new ProcessPriorityRequest();
            var (screen, cursorPosition) = ScreenshotCreator.CaptureScreenshot(SystemInformation.VirtualScreen, settingsContext.CaptureCursor);
            return Screenshot.FromImage(screen, cursorPosition, ScreenshotSource.Fullscreen);
        }
    }
}
