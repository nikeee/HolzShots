using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using HolzShots.Composition;
using HolzShots.Input.Keyboard;
using HolzShots.Net;
using HolzShots.Net.Custom;

namespace HolzShots.Windows.Forms
{
    public static class NotificationManager
    {
        private const string GenericErrorTitle = "Oh snap! :(";

        public static void CopyImageFailed(Exception ex)
        {
            Show(GenericErrorTitle, "Copying failed", $@"Error copying image to clipboard:\n{ex.Message}", TaskDialogIcon.Error, TaskDialogButton.OK);
        }
        public static void RetrievingImageFromClipboardFailed(Exception ex)
        {
            Show(GenericErrorTitle, "Could not fetch image from clipboard", $"Maybe you know what this means:\n{ex.Message}", TaskDialogIcon.Error, TaskDialogButton.OK);
        }
        internal static void NoAdmin()
        {
            Show(GenericErrorTitle, "Missing permissions", "We need administrative permissions to change stuff in the explorer context menu.", TaskDialogIcon.Error, TaskDialogButton.OK);
        }

        public static void UnauthorizedAccessExceptionDirectory(string directory)
        {
            Show(GenericErrorTitle, "Access to folder denied", $"We tried to access the folder\n{directory}\nbut the access was denied.", TaskDialogIcon.Error, TaskDialogButton.OK);
        }
        public static void PathIsTooLong(string directory, IWin32Window? parent = null /* TODO Change to default(_) if this is not a reference type */)
        {
            Show(parent, GenericErrorTitle,
                "Path is too long",
                $"The path\n{directory}\nis longer than 255 characters. We can only work with path with lesser characters.",
                TaskDialogIcon.Error,
                TaskDialogButton.OK
            );
        }

        public static void PluginLoadingFailed(PluginLoadingFailedException ex)
        {
            Debug.Assert(ex != null);
            var message = ex.InnerException?.Message ?? "No Message Provided";
            NotifierFlyout.ShowNotification("Plugins not loaded", $"We could not load the plugins. Here's the error message:\n{message}");
        }

        public static void SettingsUpdated() => NotifierFlyout.ShowNotification("Settings Updated", $"HolzShots has detected and loaded new settings.");

        public static async Task UploadFailed(UploadException result)
        {
            if (result.InnerException is UnableToFillTemplateException templateException)
            {
                var tempFile = System.IO.Path.GetTempFileName();

                await System.IO.File.WriteAllTextAsync(tempFile, templateException.ServerResponse);

                var page = new TaskDialogPage()
                {
                    Text = $"The provided \"urlTemplate\" didn't work. It raised the following error:\n\n{templateException.Message}\n\nWe've saved the server's response to this file, so you can investigate the issue:\n\n${tempFile}",
                    Footnote = new TaskDialogFootnote()
                    {
                        Text = "Pro tip: You can press Ctrl+C while having this dialog focused to copy its contents.",
                    },
                };
                TaskDialog.ShowDialog(page);
                return;
            }

            Show("Error Uploading Image", string.Empty, result.Message, TaskDialogIcon.Error, TaskDialogButton.OK);
        }
        public static void CopyingFailed(string text)
        {
            NotifierFlyout.ShowNotification("Could not copy link :(", $"We could not copy the link to your image to your clipboard.");
        }
        public static void ShowCopyConfirmation(string text) => NotifierFlyout.ShowNotification("Link copied!", "The link has been copied to your clipboard.");
        public static void ShowFileCopyConfirmation() => NotifierFlyout.ShowNotification("File copied!", "The file has been copied to your clipboard.");
        public static void CopyingFileFailed()
        {
            NotifierFlyout.ShowNotification("Could not copy file :(", $"We could not copy the file to your clipboard. Please try again!");
        }
        public static void ShowFilePathCopyConfirmation() => NotifierFlyout.ShowNotification("File path copied!", "The file path has been copied to your clipboard.");
        public static void CopyingFilePathFailed()
        {
            NotifierFlyout.ShowNotification("Could not copy file path :(", $"We could not copy the file path to your clipboard. Please try again!");
        }
        public static void ShowImageCopiedConfirmation() => NotifierFlyout.ShowNotification("Image copied!", "The image has been copied to your clipboard.");
        public static void ShowOperationCanceled() => NotifierFlyout.ShowNotification("Canceled", "You canceled the task.");

        public static void NoPathSpecified()
        {
            Show(GenericErrorTitle, "No path specified.", "You did not specify any path that could be used.", TaskDialogIcon.Error, TaskDialogButton.OK);
        }
        public static void PathDoesNotExist(string path)
        {
            Show(GenericErrorTitle, "The path was not found.", $"The path\n{path}\nyou provided does not exist.", TaskDialogIcon.Error, TaskDialogButton.OK);
        }
        public static void InvalidFilePattern(string pattern)
        {
            // If Not ManagedSettings.SaveImagesToLocalDiskPolicy.IsSet Then
            var res = Show(GenericErrorTitle,
                "No valid naming pattern provided.",
                $"The file naming pattern you provided is not valid. Therefore, the image was not saved. Please set a valid naming pattern in settings.\nWould you like to turn off automatic saving of your screenshots?",
                TaskDialogIcon.Error,
                TaskDialogButton.Yes,
                TaskDialogButton.No
            );

            if (res == TaskDialogButton.Yes)
            {
                // UserSettings.Current.SaveImagesToLocalDisk = False
                // TODO: Setting settings not supported yet
                // Maybe we want to remove the "do you want to turn it off now" feature off
            }
        }
        public static void ErrorSavingImage(Exception ex, IWin32Window? parent = null)
        {
            Show(parent, GenericErrorTitle, "There was an error saving your image.", ex.Message, TaskDialogIcon.Error, TaskDialogButton.OK);
        }

        private static TaskDialogButton Show(string title, string instructionText, string text, TaskDialogIcon icon, params TaskDialogButton[] buttons)
        {
            return Show(
                null /* TODO Change to default(_) if this is not a reference type */,
                title,
                instructionText,
                text,
                icon,
                buttons
            );
        }

        private static TaskDialogButton Show(IWin32Window? parent, string title, string instructionText, string text, TaskDialogIcon icon, params TaskDialogButton[] buttons)
        {
            var owner = parent?.Handle ?? IntPtr.Zero;

            var buttonCollection = new TaskDialogButtonCollection();
            foreach (var b in buttons)
                buttonCollection.Add(b);

            var page = new TaskDialogPage()
            {
                Caption = title,
                Heading = instructionText,
                Text = text,
                Icon = icon,
                Buttons = buttonCollection
            };

            return TaskDialog.ShowDialog(owner, page);
        }

        public static void ErrorRegisteringHotkeys(IEnumerable<HotkeyRegistrationException> exs)
        {
            var hotkeyList = string.Join("\n", exs.Select(e => "- " + e.Hotkey?.ToString() ?? "<unknown hotkey>"));

            var affectedHotkeys = hotkeyList.Length > 0
                ? "\nAffected hotkeys:\n" + hotkeyList
                : "";

            Show(
                GenericErrorTitle,
                "Error registering hotkeys.",
                "We could not register the hotkeys you specified. Check if some other application has one or more of these hotkeys already registered or choose different hotkeys." + affectedHotkeys,
                TaskDialogIcon.Error,
                TaskDialogButton.OK
            );
        }
    }
}
