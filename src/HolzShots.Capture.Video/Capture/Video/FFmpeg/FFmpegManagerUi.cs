using System.Windows.Forms;
using System.Threading.Tasks;

namespace HolzShots.Capture.Video.FFmpeg
{
    public static class FFmpegManagerUi
    {
        public static async Task<string> EnsureAvailableFFmpeg(HSSettings settingsContext)
        {
            var path = FFmpegManager.GetAbsoluteFFmpegPath(true);
            if (path != null)
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

                var cancel = new TaskDialogCommandLinkButton()
                {
                    Text = "Cancel",
                    AllowCloseDialog = false,
                };

                var initialPage = GetInitialPage(downloadButton, manualDownload);

                var manualSetupPage = GetManualSetupPage(quit);
                manualDownload.Click += (s, e) => initialPage.Navigate(manualSetupPage);

                var errorPage = GetErrorPage();
                cancel.Click += (s, e) => initialPage.Navigate(errorPage);

                var downloadPage = GetDownloadPage();
                downloadButton.Click += (s, e) => initialPage.Navigate(downloadPage);

                var answer = TaskDialog.ShowDialog(initialPage);
                if (answer == TaskDialogButton.OK)
                {
                    // The user clicked "Cancel" and then OK
                    return null; // TODO: Proper cancellation
                }
                if (answer == downloadButton)
                {
                    // TODO: fetch ffmpeg with download progress
                    return "";
                }
                if (answer == quit)
                {

                }

            }
            return path!;
        }

        private static void DownloadButton_Click(object? sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        static TaskDialogPage GetInitialPage(TaskDialogButton downloadButton, TaskDialogButton manualDownload) => new()
        {
            Icon = TaskDialogIcon.Information,
            AllowMinimize = true,
            Caption = "HolzShots is missing FFmpeg :(",
            Heading = "Could not find FFmpeg",
            Text = "FFmpeg is an awesome tool that HolzShots needs to do screen recording, and it seems like it's not installed.\n\nDo you want HolzShots to automatically download and set up FFmpeg from ffbinaries.com?",
            Expander = new TaskDialogExpander()
            {
                Text = "Automatic setup does not interfere with existing installations. It does not require administrative privileges. FFmpeg will be saved to %APPDATA%\\HolzShots\\ffmpeg",
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

        static TaskDialogPage GetErrorPage() => new()
        {
            Icon = TaskDialogIcon.Information,
            AllowMinimize = true,
            Caption = "HolzShots is missing FFmpeg :(",
            Heading = "Could not find FFmpeg",
            Text = "You chose to do nothing, so the screen recording will now abort.",
            Buttons = new TaskDialogButtonCollection() { TaskDialogButton.OK },
            DefaultButton = TaskDialogButton.OK,
        };

        static TaskDialogPage GetManualSetupPage(TaskDialogButton quitButton) => new()
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

        static TaskDialogPage GetDownloadPage()
        {
            var downloadProgressBar = new TaskDialogProgressBar()
            {
                Minimum = 0,
                Maximum = 100,
                State = TaskDialogProgressBarState.Marquee,
            };

            var downloadPage = new TaskDialogPage()
            {
                AllowMinimize = true,
                Caption = "FFmpeg missing",
                Heading = "Downloading FFmpeg...",
                Text = "We're shifting some bits around to get you ready.",
                ProgressBar = downloadProgressBar,
                Buttons = new TaskDialogButtonCollection() {
                    TaskDialogButton.Yes,
                },
                DefaultButton = TaskDialogButton.Yes,
                Expander = new TaskDialogExpander()
                {
                    Text = "Initializing...",
                    CollapsedButtonText = "More status info",
                    Position = TaskDialogExpanderPosition.AfterFootnote,
                },
            };

            downloadPage.Created += async (o, e) =>
            {
                downloadPage.Expander.Text = "Starting download...";
                // TODO: Fetch FFmpeg
                await Task.Delay(5000);

                downloadProgressBar.State = TaskDialogProgressBarState.Normal;
                downloadProgressBar.Value = 50;
                await Task.Delay(5000);

                var finished = GetDownloadFinishedPage();
                downloadPage.Navigate(finished);
            };

            return downloadPage;
        }

        static TaskDialogPage GetDownloadFinishedPage() => new()
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
    }
}
