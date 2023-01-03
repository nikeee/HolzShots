using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using HolzShots.Composition.Command;
using HolzShots.Drawing;
using HolzShots.Input.Selection;
using HolzShots.Threading;

namespace HolzShots.Input.Actions
{
    [Command("captureArea")]
    public class CaptureSelectedAreaCommand : ImageCapturingCommand
    {
        protected override async Task InvokeInternal(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            if (settingsContext == null)
                throw new ArgumentNullException(nameof(settingsContext));

            // TODO: Add proper assertion
            // Debug.Assert(ManagedSettings.EnableAreaScreenshot)
            Debug.Assert(!SelectionSemaphore.IsInAreaSelection);

            if (!settingsContext.EnableHotkeysDuringFullscreen && HolzShots.Windows.Forms.EnvironmentEx.IsFullscreenAppRunning())
                return;

            // TODO: Re-add proper if condition
            // If ManagedSettings.EnableAreaScreenshot AndAlso Not SelectionSemaphore.IsInAreaSelection Then
            if (!SelectionSemaphore.IsInAreaSelection)
            {
                Screenshot? shot;
                try
                {
                    shot = await CaptureSelection(settingsContext).ConfigureAwait(true);
                    Debug.Assert(shot is not null);
                    if (shot == null)
                        throw new TaskCanceledException();
                }
                catch (TaskCanceledException)
                {
                    Debug.WriteLine("Area Selection cancelled");
                    return;
                }
                Debug.Assert(shot is not null);
                await ProcessCapturing(shot, settingsContext).ConfigureAwait(true);
            }
        }

        public static async Task<Screenshot?> CaptureSelection(HSSettings settingsContext)
        {
            if (settingsContext == null)
                throw new ArgumentNullException(nameof(settingsContext));

            Debug.Assert(!SelectionSemaphore.IsInAreaSelection);

            if (SelectionSemaphore.IsInAreaSelection)
                return null;

            using var prio = new ProcessPriorityRequest();
            var (screen, cursorPosition) = ScreenshotCreator.CaptureScreenshot(SystemInformation.VirtualScreen, settingsContext.CaptureCursor);
            using var selector = AreaSelector.Create(screen, true, settingsContext);

            var (selectedArea, _) = await selector.PromptSelectionAsync().ConfigureAwait(true);

            Debug.Assert(selectedArea.Width > 0);
            Debug.Assert(selectedArea.Height > 0);

            var selectedImage = new Bitmap(selectedArea.Width, selectedArea.Height);

            var cursorPositionOnSelectedImage = cursorPosition == null
                ? null
                : cursorPosition with { OnImage = new Point(cursorPosition.OnImage.X - selectedArea.X, cursorPosition.OnImage.Y - selectedArea.Y) };

            using var g = Graphics.FromImage(selectedImage);

            g.DrawImage(screen, new Rectangle(0, 0, selectedArea.Width, selectedArea.Height), selectedArea, GraphicsUnit.Pixel);

            return Screenshot.FromImage(selectedImage, cursorPosition, ScreenshotSource.Selected);
        }
    }
}
