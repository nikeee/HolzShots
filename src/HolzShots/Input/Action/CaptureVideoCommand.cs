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
using HolzShots.Net;
using HolzShots.Windows.Net;
using HolzShots.IO;
using HolzShots.IO.Naming;

namespace HolzShots.Input.Actions
{
    [Command("startOrStopVideoCapture")]
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
                var destDir = settingsContext.ExpandedSavePath;
                if (!Directory.Exists(destDir))
                    IO.HolzShotsPaths.EnsureDirectory(destDir);

                var extensionWithDot = Path.GetExtension(recording.FilePath);

                var targetFileName = FileNamePatternFormatter.GetFileNameFromPattern(
                    recording.GetMetadata(),
                    settingsContext.SaveVideoFileNamePattern
                );

                var fullTargetFilePath = Path.Combine(destDir, targetFileName + extensionWithDot);

                FileEx.MoveAndRenameInsteadOfOverwrite(recording.FilePath, fullTargetFilePath);

                recording = recording with { FilePath = fullTargetFilePath };
            }

            await InvokeAfterCaptureAction(recording, settingsContext);
        }

        static async Task InvokeAfterCaptureAction(ScreenRecording recording, HSSettings settingsContext)
        {
            switch (settingsContext.ActionAfterVideoCapture)
            {
                case VideoCaptureHandlingAction.Upload:
                    try
                    {
                        var payload = new VideoUploadPayload(recording);

                        var result = await UploadDispatcher.InitiateUploadToDefaultUploader(payload, settingsContext, HolzShotsApplication.Instance.Uploaders, null).ConfigureAwait(true);
                        UploadHelper.InvokeUploadFinishedUI(result, settingsContext);
                    }
                    catch (UploadCanceledException)
                    {
                        NotificationManager.ShowOperationCanceled();
                    }
                    catch (UploadException ex)
                    {
                        NotificationManager.UploadFailed(ex);
                    }
                    return;
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

        private static async Task<ScreenRecording?> PerformScreenRecording(HSSettings settingsContext)
        {
            _currentRecordingCts = new CancellationTokenSource();
            _throwAwayResult = false;

            try
            {
                var ffmpegPath = FFmpegManagerUi.EnsureAvailableFFmpegAndPotentiallyStartSetup(settingsContext);
                if (ffmpegPath == null)
                    return null; // We don't have ffmpeg available and the user didn't do anything to fix this. We act like it was aborted.

                using var selectionBackground = Drawing.ScreenshotCreator.CaptureScreenshot(SystemInformation.VirtualScreen);
                using var selector = Selection.AreaSelector.Create(selectionBackground, settingsContext);

                var selection = await selector.PromptSelectionAsync();

                var recorder = ScreenRecorderSelector.CreateScreenRecorderForCurrentPlatform();

                var ffmpegPathToPrefix = Path.GetDirectoryName(ffmpegPath!)!;
                Debug.Assert(ffmpegPathToPrefix != null);

                using var environmentPrevix = new IO.PrefixEnvironmentVariable(ffmpegPathToPrefix);

                var tempRecordingDir = Path.Combine(Path.GetTempPath(), "hs-" + Path.GetRandomFileName());
                Directory.CreateDirectory(tempRecordingDir);

                var extension = VideoUploadPayload.GetExtensionForVideoFormat(settingsContext.VideoOutputFormat);
                var targetFile = Path.Combine(tempRecordingDir, "HS" + extension);

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


        public static bool IsScreenRecorderRunning() => _currentRecordingCts != null;
        public static void StopCurrentScreenRecorder(bool throwAwayResult)
        {
            _throwAwayResult = throwAwayResult;
            _currentRecordingCts?.Cancel();
        }
    }

    public record VideoUploadPayload : IUploadPayload
    {
        private const string DefaultUploadFileNameWithoutExtension = LibraryInformation.Name;

        public string MimeType { get; init; }
        public string Extension { get; init; }

        private readonly ScreenRecording _recording;
        public VideoUploadPayload(ScreenRecording recording)
        {
            _recording = recording ?? throw new ArgumentNullException(nameof(recording));
            MimeType = GetMimeTypeForVideoFormat(recording.Format);
            Extension = GetExtensionForVideoFormat(recording.Format);
        }

        public Stream GetStream() => File.OpenRead(_recording.FilePath);
        public string GetSuggestedFileName() => DefaultUploadFileNameWithoutExtension + Extension;

        public void Dispose()
        {
            // We don't dispose anything here
            // The file recoded will either stay in the temp dir and will be deleted on next boot
            // ...or it will be saved in the respective folder, so deletion would be against the user's will
        }

        static string GetMimeTypeForVideoFormat(VideoCaptureFormat format) => format switch
        {
            VideoCaptureFormat.Gif => "image/gif",
            VideoCaptureFormat.Mp4 => "video/mp4",
            VideoCaptureFormat.Webm => "video/webm",
            _ => throw new ArgumentException("Unhaned VideoCaptureFormat: " + format),
        };
        public static string GetExtensionForVideoFormat(VideoCaptureFormat format) => format switch
        {
            VideoCaptureFormat.Gif => ".gif",
            VideoCaptureFormat.Mp4 => ".mp4",
            VideoCaptureFormat.Webm => ".webm",
            _ => throw new ArgumentException("Unhaned VideoCaptureFormat: " + format),
        };
    }
}
