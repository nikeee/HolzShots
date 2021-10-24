using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolzShots.Net;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;

namespace HolzShots.Forms;

public static class ToastNotifications
{
    // TODO: Fallback for non-windows-10-flyouts
    private static void ShowShortNotification(string header, string subHeader) => new ToastContentBuilder()
            .SetToastDuration(ToastDuration.Short)
            .AddText(header, AdaptiveTextStyle.Header)
            .AddText(subHeader, AdaptiveTextStyle.HeaderSubtle)
            .Show(toast => toast.ExpirationTime = DateTime.Now.AddSeconds(1));

    public static void ShowSettingsUpdated() => ShowShortNotification("Settings Updated", "HolzShots has detected and loaded new settings.");
    /// <remarks> Only show this notification when the action was triggered by some uplaod result action, not if it weas triggered by the user directly. </remarks>
    public static void ShowCopyingFailed(string url) => ShowShortNotification("Could not copy link :(", "We could not copy the link to your image to your clipboard.");
    public static void ShowCopyConfirmation(string text) => ShowShortNotification("Link copied!", "The link has been copied to your clipboard.");
    /// <remarks> Only show this notification when the action was triggered by some uplaod result action, not if it weas triggered by the user directly. </remarks>
    public static void ShowFileCopyConfirmation() => ShowShortNotification("File copied!", "The file has been copied to your clipboard.");
    public static void ShowCopyingFileFailed() => ShowShortNotification("Could not copy file :(", "We could not copy the file to your clipboard. Please try again!");
    /// <remarks> Only show this notification when the action was triggered by some uplaod result action, not if it weas triggered by the user directly. </remarks>
    public static void ShowFilePathCopyConfirmation() => ShowShortNotification("File path copied!", "The file path has been copied to your clipboard.");
    public static void ShowCopyingFilePathFailed() => ShowShortNotification("Could not copy file path :(", "We could not copy the file path to your clipboard. Please try again!");
    /// <remarks> Only show this notification when the action was triggered by some uplaod result action, not if it weas triggered by the user directly. </remarks>
    public static void ShowImageCopiedConfirmation() => ShowShortNotification("Image copied!", "The image has been copied to your clipboard.");
    public static void ShowOperationCanceled() => ShowShortNotification("Canceled", "You canceled the task.");
    public static void ShowPluginLoadingFailed(Composition.PluginLoadingFailedException ex)
    {
        Debug.Assert(ex != null);
        var message = ex.InnerException?.Message ?? "No Message Provided";
        ShowShortNotification("Plugins not loaded", $"We could not load the plugins. Here's the error message:\n{message}");
    }

#if false
    public static void ShowUploadResult(UploadResult result, HSSettings settingsContext)
    {
        // TODO: Hero Image: https://stackoverflow.com/questions/46689877
#if DEBUG
        var toastDuration = TimeSpan.FromMinutes(1); // TODO: Make this configurable
#else
        var toastDuration = TimeSpan.FromHours(1); // TODO: Make this configurable
#endif
        // TODO: Support settingsContext.AutoCloseLinkViewer

        new ToastContentBuilder()
            .AddArgument("url", result.Url)
            .AddText("File has been uploaded!", AdaptiveTextStyle.Header)
            .AddText("What do you want to copy?", AdaptiveTextStyle.HeaderSubtle)
            .AddButton(new ToastButton("Markdown", "copyMarkdown"))
            .AddButton(new ToastButton("HTML", "copyHtml"))
            .AddButton(new ToastButton("Link", "copyLink"))
            .Show(toast =>
            {
                toast.ExpirationTime = DateTime.Now + toastDuration;
            });
    }
#endif
}

/// <summary>
/// Based on: https://docs.microsoft.com/en-us/windows/apps/design/shell/tiles-and-notifications/toast-progress-bar?tabs=builder-syntax
/// </summary>
class UploadProgressNotification : ITransferProgressReporter
{
    private const string _group = "holzshots-upload";
    private readonly string _tag = new Guid().ToString();
    private readonly ToastNotifierCompat _toastNofitifier = ToastNotificationManagerCompat.CreateToastNotifier();

    uint _numnberOfUpdates = 1;
    ToastNotification? _toastNotification = null;

    public void CloseProgress()
    {
        if (_toastNotification != null)
        {
            // ToastNotificationManager.History.Remove(_tag);
            // _toastNotification.ExpirationTime = DateTime.Now.AddSeconds(-10);
            _toastNofitifier.Hide(_toastNotification);
        }
    }

    public void Dispose()
    {
        CloseProgress();
    }

    public void ShowProgress()
    {
        var data = new NotificationData
        {
            SequenceNumber = _numnberOfUpdates++,
        };
        data.Values["progressValue"] = "0";
        data.Values["progressValueString"] = "Not yet started";
        data.Values["progressStatus"] = "Initializing...";

        // Construct the toast content with data bound fields
        new ToastContentBuilder()
            .AddText("Uploading file...")
            .AddAudio(null, silent: true)
            .AddVisualChild(new AdaptiveProgressBar()
            {
                Value = new BindableProgressBarValue("progressValue"),
                ValueStringOverride = new BindableString("progressValueString"),
                Status = new BindableString("progressStatus")
            })
            .Show(toast =>
            {
                toast.Tag = _tag;
                toast.Group = _group;
                _toastNotification = toast;
                toast.Dismissed += (a, b) => _toastNotification = null;
            });
    }

    public void UpdateProgress(TransferProgress progress, Speed<MemSize> speed)
    {
        var data = new NotificationData
        {
            SequenceNumber = _numnberOfUpdates++,
        };

        var progressValue = (progress.ProgressPercentage / 100f).ToString();
        Debug.WriteLine($"progressValue: {progressValue}");
        Debug.WriteLine($"ProgressPercentage: {progress.ProgressPercentage}");

        data.Values["progressValue"] = progressValue;
        data.Values["progressValueString"] = speed.ToString();
        data.Values["progressStatus"] = GetDisplayStatusFromUploadState(progress, speed);

        _toastNofitifier.Update(data, _tag, _group);
    }

    private static string GetDisplayStatusFromUploadState(TransferProgress progress, Speed<MemSize> speed) => progress.State switch
    {
        UploadState.NotStarted => "Starting Upload...",
        UploadState.Processing => $"{progress.Current}/{progress.Total}",
        UploadState.Paused => "Paused",
        UploadState.Finished => "Waiting for server reply...",
        _ => throw new ArgumentException(),
    };
}
