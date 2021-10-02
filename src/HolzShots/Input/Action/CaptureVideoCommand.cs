using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HolzShots.Capture.Video;
using HolzShots.Capture.Video.FFmpeg;
using HolzShots.Composition.Command;

namespace HolzShots.Input.Actions
{
    [Command("captureVideo")]
    public class CaptureVideoCommand : ICommand<HSSettings>
    {
        private static CancellationTokenSource? _currentRecordingCts = null;

        public async Task Invoke(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
        {
            if (_currentRecordingCts != null)
                return; // We only allow one recorder and use the CTS as a global indicator if a recorder is running

            _currentRecordingCts = new CancellationTokenSource();

            try
            {
                var ffmpegPath = await FFmpegManagerUi.EnsureAvailableFFmpeg(settingsContext);
                if (ffmpegPath == null)
                {
                    MessageBox.Show("No FFmpeg available :("); // TODO: Make properly
                    return; // Nope, the user did not select anything. Abort.
                }

                using var selectionBackground = Drawing.ScreenshotCreator.CaptureScreenshot(SystemInformation.VirtualScreen);
                using var selector = Selection.AreaSelector.Create(selectionBackground, settingsContext);

                var selection = await selector.PromptSelectionAsync();

                var recorder = ScreenRecorderSelector.CreateScreenRecorderForCurrentPlatform();

                var ffmpegPathToPrefix = System.IO.Path.GetDirectoryName(ffmpegPath!)!;
                Debug.Assert(ffmpegPathToPrefix != null);

                using var environmentPrevix = new HolzShots.IO.PrefixEnvironmentVariable(ffmpegPathToPrefix);

                _ = recorder.Invoke(selection, @"C:\Temp\loltest.mp4", _currentRecordingCts.Token, settingsContext);

                await Task.Delay(5000);

                _currentRecordingCts.Cancel();
            }
            finally
            {
                _currentRecordingCts = null;
            }
        }
    }
}
