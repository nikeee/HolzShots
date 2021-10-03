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
using HolzShots.Windows.Forms;

namespace HolzShots.Input.Actions
{
    [Command("captureVideo")]
    public class CaptureVideoCommand : ICommand<HSSettings>
    {
        private static CancellationTokenSource? _currentRecordingCts = null;
        private static bool _throwAwayResult = false;

        public async Task Invoke(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
        {
            // TODO: If it is the first time the user invokes this command, we want to tell him that he can stop recording by pressing the same button again

            if (IsScreenRecorderRunning())
            {
                // We allow only one instance running. If the user invokes this task another time, he likely wanted to stop recording.
                // TODO: Add UI and trigger cancellation manually via mouse
                StopCurrentScreenRecorder(false);
                return;
            }

            var recording = await PerformScreenRecording(settingsContext);
            if (recording == null)
                return; // User likely cancelled recording


            // Every actionAfterCapture depends on the file in some way
            // Sometimes, it will be saved to a specified path
            // In that case, we want to first move the file to the target destination and then perform the action with that new path
            // Otherwise, we use the file in the temp dir

            if (settingsContext.SaveToLocalDisk)
            {
                // TODO: Support file patterns like with screenshots
                // TODO: Unify screenshot and video recording saving stuff

                // This is a temporary solution to get an MVP working

                var destDir = settingsContext.ExpandedSavePath;
                if (!Directory.Exists(destDir))
                    IO.HolzShotsPaths.EnsureDirectory(destDir);


                var timestamp = recording.StartTime.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern);
                var targetFileName = timestamp.Replace(':', '-') + "-" + Path.GetFileName(recording.FilePath);

                var fullTargetFilePath = Path.Combine(destDir, targetFileName);

                File.Move(recording.FilePath, fullTargetFilePath);

                recording = recording with { FilePath = fullTargetFilePath };
            }


            switch (settingsContext.ActionAfterVideoCapture)
            {
                case VideoCaptureHandlingAction.Upload:
                    return; // TODO
                case VideoCaptureHandlingAction.CopyFile:
                    {
                        var success = ClipboardEx.SetFiles(recording.FilePath);
                        if (settingsContext.ShowCopyConfirmation)
                        {
                            if (success)
                                NotificationManager.ShowFileCopyConfirmation();
                            else
                                NotificationManager.CopyingFileFailed();
                        }
                    }
                    break;
                case VideoCaptureHandlingAction.CopyFilePath:
                    {
                        var success = ClipboardEx.SetText(recording.FilePath);
                        if (settingsContext.ShowCopyConfirmation)
                        {
                            if (success)
                                NotificationManager.ShowFilePathCopyConfirmation();
                            else
                                NotificationManager.CopyingFilePathFailed();
                        }
                    }
                    break;
                case VideoCaptureHandlingAction.ShowInExplorer:
                    IO.HolzShotsPaths.OpenSelectedFileInExplorer(recording.FilePath);
                    return;
                case VideoCaptureHandlingAction.OpenInDefaultApp:
                    Process.Start(recording.FilePath);
                    return;
                case VideoCaptureHandlingAction.None: return;
                default: throw new ArgumentException("Unhandled VideoCaptureHandlingAction: " + settingsContext.ActionAfterVideoCapture);
            }
        }

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
