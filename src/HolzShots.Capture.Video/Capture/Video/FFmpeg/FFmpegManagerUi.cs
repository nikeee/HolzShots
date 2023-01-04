using System.Windows.Forms;
using HolzShots.Net;
using System.Diagnostics;

namespace HolzShots.Capture.Video.FFmpeg;

public static class FFmpegManagerUi
{
    public static AfterSetupAction StartGuidedSetupDialog()
    {
        var quit = new TaskDialogButton("Quit HolzShots");

        var downloadButton = new TaskDialogCommandLinkButton()
        {
            Text = "Download automatically",
            AllowCloseDialog = false,
        };

        var manualDownload = new TaskDialogCommandLinkButton()
        {
            Text = "Set up manually",
            AllowCloseDialog = false,
        };

        var doNothing = new TaskDialogCommandLinkButton()
        {
            Text = "Cancel",
            AllowCloseDialog = false,
        };

        var initialPage = CreateInitialPage(downloadButton, manualDownload);

        var manualSetupPage = CreateManualSetupPage(quit);
        manualDownload.Click += (s, e) => initialPage.Navigate(manualSetupPage);

        var noActionPage = CreateNoActionPage();
        doNothing.Click += (s, e) => initialPage.Navigate(noActionPage);

        var downloadPage = CreatetDownloadPage();
        downloadButton.Click += (s, e) => initialPage.Navigate(downloadPage);

        var answer = TaskDialog.ShowDialog(initialPage, TaskDialogStartupLocation.CenterScreen);
        if (answer == TaskDialogButton.OK)
        {
            // The user clicked "Cancel" and then OK (or an error ocurred)
            return AfterSetupAction.AbortCurrentAction;
        }

        if (answer == TaskDialogButton.Cancel)
        {
            // The user closed the dialog via "X"
            return AfterSetupAction.AbortCurrentAction;
        }


        if (answer == quit)
            return AfterSetupAction.QuitApplication;

        return AfterSetupAction.Coninue;
    }


    static TaskDialogPage CreateInitialPage(TaskDialogButton downloadButton, TaskDialogButton manualDownload) => new()
    {
        Icon = TaskDialogIcon.Information,
        AllowMinimize = true,
        Caption = "HolzShots is missing FFmpeg :(",
        Heading = "Could not find FFmpeg",
        Text = "FFmpeg is an awesome tool that HolzShots needs to do screen recording, and it seems like it's not installed.\n\nDo you want HolzShots to automatically download and set up FFmpeg from ffbinaries.com?",
        Expander = new TaskDialogExpander()
        {
            Text = "Automatic setup does not interfere with existing installations. It does not require administrative privileges. FFmpeg will be saved to %APPDATA%\\HolzShots\\ffmpeg. It will NOT be added to your PATH.",
            CollapsedButtonText = "Show nerd info about automatic setup",
            Position = TaskDialogExpanderPosition.AfterFootnote,
        },
        AllowCancel = true,
        Buttons = new TaskDialogButtonCollection() {
            TaskDialogButton.Cancel,
            downloadButton,
            manualDownload,
        },
        DefaultButton = downloadButton,
    };

    static TaskDialogPage CreateNoActionPage() => new()
    {
        Icon = TaskDialogIcon.Information,
        AllowMinimize = true,
        Caption = "HolzShots is missing FFmpeg :(",
        Heading = "Could not find FFmpeg",
        Text = "You chose to do nothing, so the screen recording will now abort.",
        Buttons = new TaskDialogButtonCollection() {
            TaskDialogButton.OK, // This must be "OK" because OK will abort the screen recording
        },
        DefaultButton = TaskDialogButton.OK,
    };

    static TaskDialogPage CreateManualSetupPage(TaskDialogButton quitButton) => new()
    {
        Icon = TaskDialogIcon.Information,
        AllowMinimize = true,
        Caption = "HolzShots is missing FFmpeg :(",
        Heading = "Manual global installation",
        Text = $"To install it manually, you can use chocolatey, scoop or winget:\n\n\tscoop install ffmpeg\n\tchoco install ffmpeg\n\twinget install ffmpeg\n\nOr just download it from ffmpeg.org. Make sure the ffmpeg.exe is in your PATH after installation.\n\nClick on \"{quitButton.Text}\" to shut down HolzShots and perform the installation.",
        Buttons = new TaskDialogButtonCollection() {
            quitButton,
        },
        DefaultButton = quitButton,
    };

    static TaskDialogPage CreatetDownloadPage()
    {
        var downloadCancellationSource = new CancellationTokenSource();

        var downloadProgressBar = new TaskDialogProgressBar()
        {
            Minimum = 0,
            Maximum = 100,
            State = TaskDialogProgressBarState.Marquee,
        };
        var cancelDownloadButton = new TaskDialogButton()
        {
            Text = "Cancel Download",
        };
        cancelDownloadButton.Click += (s, e) => downloadCancellationSource.Cancel();

        var downloadPage = new TaskDialogPage()
        {
            AllowMinimize = true,
            Caption = "FFmpeg missing",
            Heading = "Downloading FFmpeg...",
            Text = "We're shifting some bits around to get you ready.",
            ProgressBar = downloadProgressBar,
            Buttons = new TaskDialogButtonCollection() {
                cancelDownloadButton,
            },
            // DefaultButton = None,
            Expander = new TaskDialogExpander()
            {
                Text = "Initializing...",
                CollapsedButtonText = "More status info",
                Position = TaskDialogExpanderPosition.AfterFootnote,
            },
        };

        downloadPage.Created += async (o, e) =>
        {
            downloadPage.Expander.Text = "Fetching info...";

            var url = await FFmpegFetcher.GetUrlOfLatestBinary();
            if (url == null)
            {
                downloadPage.Navigate(CreateDownloadFailedPage("Unable to get link to FFmpeg download"));
                return;
            }

            var progress = new DialogTransferProgress(downloadPage);

            downloadPage.Expander.Text = "Downloading...";
            try
            {
                await FFmpegFetcher.LoadAndUnzipToDirectory(FFmpegManager.FFmpegAppDataPath, url, progress, downloadCancellationSource.Token);
            }
            catch (TaskCanceledException)
            {
                // Well, it's actually not an error, but we're too lazy for a separate doaloge for this
                downloadPage.Navigate(CreateDownloadFailedPage("You cancelled the download."));
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to download FFmpeg.");
                Debug.WriteLine(ex);
                downloadPage.Navigate(CreateDownloadFailedPage(ex.Message));
                return;
            }

            downloadPage.Expander.Text = "Done!";

            // Cross-check if downloading ffmpeg fixed the problem
            var path = FFmpegManager.GetAbsoluteFFmpegPath(true);
            if (path == null)
                downloadPage.Navigate(CreateDownloadFailedPage("The download was successful, but it did not fix the issue."));
            else
                downloadPage.Navigate(CreateDownloadFinishedPage());

        };

        return downloadPage;
    }

    static TaskDialogPage CreateDownloadFinishedPage() => new()
    {
        AllowMinimize = true,
        Caption = "FFmpeg missing",
        Heading = "Done!",
        Text = "HolzShots has set up FFmpeg.\n\nWe will now continue with the screen recording that you started.",
        Buttons = new TaskDialogButtonCollection() {
            TaskDialogButton.Continue,
        },
        DefaultButton = TaskDialogButton.Continue,
    };

    static TaskDialogPage CreateDownloadFailedPage(string message) => new()
    {
        AllowMinimize = true,
        Caption = "FFmpeg missing",
        Heading = "Something went wrong",
        Text = $"We tried our best, but somehow it did not work out. Try installing FFmpeg manually or to restart HolzShots.\nHere's some information on the error:\n\n{message}\n\nThe screen recording that you tried to do will now abort.\n\n",
        Buttons = new TaskDialogButtonCollection() {
            TaskDialogButton.OK, // This must be "OK" because OK will abort the screen recording
        },
        DefaultButton = TaskDialogButton.OK,
    };
}

public enum AfterSetupAction
{
    QuitApplication,
    AbortCurrentAction,
    Coninue,
}


public class DialogTransferProgress : Progress<TransferProgress>
{
    private readonly TaskDialogPage _dialogPage;
    public DialogTransferProgress(TaskDialogPage dialogPage)
    {
        _dialogPage = dialogPage;

        var pBar = dialogPage.ProgressBar;
        Debug.Assert(pBar is not null);

        pBar.Minimum = 0;
        pBar.Maximum = 100;
    }

    protected override void OnReport(TransferProgress value)
    {
        var pBar = _dialogPage.ProgressBar;
        Debug.Assert(pBar is not null);

        switch (value.State)
        {
            case UploadState.NotStarted:
                pBar.State = TaskDialogProgressBarState.Marquee;
                break;
            case UploadState.Processing:
                pBar.State = TaskDialogProgressBarState.Normal;
                pBar.Value = unchecked((int)value.ProgressPercentage);
                return;
            case UploadState.Paused:
                pBar.State = TaskDialogProgressBarState.Paused;
                pBar.Value = unchecked((int)value.ProgressPercentage);
                break;
            case UploadState.Finished:
            default:
                return;
        }
    }
}
