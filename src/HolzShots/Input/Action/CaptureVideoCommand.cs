using System;
using System.IO;
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
        private static bool _throwAwayResult = false;


        private async Task<ScreenRecording?> PerformScreenRecording(HSSettings settingsContext)
        {
            _currentRecordingCts = new CancellationTokenSource();
            _throwAwayResult = false;

            try
            {
                var ffmpegPath = await FFmpegManagerUi.EnsureAvailableFFmpeg(settingsContext);
                if (ffmpegPath == null)
                {
                    MessageBox.Show("No FFmpeg available :("); // TODO: Make properly
                    return null; // Nope, the user did not select anything. Abort.
                }

                using var selectionBackground = Drawing.ScreenshotCreator.CaptureScreenshot(SystemInformation.VirtualScreen);
                using var selector = Selection.AreaSelector.Create(selectionBackground, settingsContext);

                var selection = await selector.PromptSelectionAsync();

                var recorder = ScreenRecorderSelector.CreateScreenRecorderForCurrentPlatform();

                var ffmpegPathToPrefix = Path.GetDirectoryName(ffmpegPath!)!;
                Debug.Assert(ffmpegPathToPrefix != null);

                using var environmentPrevix = new IO.PrefixEnvironmentVariable(ffmpegPathToPrefix);

                var tempRecordingDir = Path.Combine(Path.GetTempPath(), "hs-" + Path.GetRandomFileName());
                Directory.CreateDirectory(tempRecordingDir);

                var extension = GetFileExtensionForVideoFormat(settingsContext.VideoOutputFormat);
                var targetFile = Path.Combine(tempRecordingDir, "HS." + extension);

                // TODO: Add UI and trigger cancellation manually
                _currentRecordingCts.CancelAfter(5000);

                var recording = await recorder.Invoke(selection, targetFile, settingsContext, _currentRecordingCts.Token);

                if (_throwAwayResult)
                {
                    // The user cancelled the video recording
                    try
                    {
                        if (File.Exists(targetFile))
                            File.Delete(targetFile);
                    }
                    catch
                    {
                        // Not _that_ important to handle this because it's in a temp dir and willb e gone on reboot anyway
                    }
                    return null;
                }
                return recording;
            }
            finally
            {
                _currentRecordingCts = null;
                _throwAwayResult = false;
            }
        }

        public async Task Invoke(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
        {
            if (IsScreenRecorderRunning())
                return; // We only allow one recorder and use the CTS as a global indicator if a recorder is running

            var recording = await PerformScreenRecording(settingsContext);
            if (recording == null)
                return; // User likely cancelled recording

            // TODO: Process video
            // IO.HolzShotsPaths.OpenSelectedFileInExplorer(recording.FilePath);
        }

        private static string GetFileExtensionForVideoFormat(VideoCaptureFormat format) => format switch
        {
            VideoCaptureFormat.Gif => "gif",
            VideoCaptureFormat.Mp4 => "mp4",
            VideoCaptureFormat.Webm => "webm",
            _ => throw new ArgumentException("Unhandled VideoCaptureFormat" + format),
        };

        public static bool IsScreenRecorderRunning() => _currentRecordingCts != null;
        public static void StopCurrentScreenRecorder(bool throwAwayResult)
        {
            _throwAwayResult = throwAwayResult;
            _currentRecordingCts?.Cancel();
        }
    }
}
