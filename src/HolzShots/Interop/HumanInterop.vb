Imports HolzShots.Composition
Imports HolzShots.Input
Imports HolzShots.Net
Imports HolzShots.Windows.Forms

Namespace Interop
    ' TODO: Rename to NotificationManager
    Friend Class HumanInterop
        Private Const GenericErrorTitle = "Oh snap! :("

        Public Shared Sub CopyImageFailed(ex As Exception)
            Show(
                GenericErrorTitle,
                "Copying failed",
                $"Error copying image to clipboard:\n{ex.Message}",
                TaskDialogIcon.Error,
                TaskDialogButton.OK
            )
        End Sub
        Public Shared Sub RetrievingImageFromClipboardFailed(ex As Exception)
            Show(
                GenericErrorTitle,
                "Could not fetch image from clipboard",
                $"Maybe you know what this means:\n{ex.Message}",
                TaskDialogIcon.Error,
                TaskDialogButton.OK
            )
        End Sub
        Friend Shared Sub NoAdmin()
            Show(
                GenericErrorTitle,
                "Missing permissions",
                "We need administrative permissions to change stuff in the explorer context menu.",
                TaskDialogIcon.Error,
                TaskDialogButton.OK
            )
        End Sub

        Public Shared Sub UnauthorizedAccessExceptionDirectory(directory As String)
            Show(
                GenericErrorTitle,
                "Access to folder denied",
                $"We tried to access the folder\n{directory}\nbut the access was denied.",
                TaskDialogIcon.Error,
                TaskDialogButton.OK
            )
        End Sub
        Public Shared Sub PathIsTooLong(directory As String, Optional parent As IWin32Window = Nothing)
            Show(
                parent,
                GenericErrorTitle,
                "Path is too long",
                $"The path\n{directory}\nis longer than 255 characters. We can only work with path with lesser characters.",
                TaskDialogIcon.Error,
                TaskDialogButton.OK
            )
        End Sub

        Friend Shared Sub PluginLoadingFailed(ex As PluginLoadingFailedException)
            Debug.Assert(ex IsNot Nothing)
            NotifierFlyout.ShowNotification("Plugins not loaded", $"We could not load the plugins. Here's the error message:\n{ex.InnerException.Message}")
        End Sub

        Friend Shared Sub SettingsUpdated()
            NotifierFlyout.ShowNotification("Settings Updated", $"HolzShots has detected and loaded new settings.")
        End Sub

        Public Shared Sub UploadFailed(result As UploadException)
            Show("Error Uploading Image", String.Empty, result.Message, TaskDialogIcon.Error, TaskDialogButton.OK)
        End Sub
        Public Shared Sub CopyingFailed(text As String)
            NotifierFlyout.ShowNotification("Could not copy link :(", $"We could not copy the link to your image to your clipboard.")
        End Sub
        Public Shared Sub ShowCopyConfirmation(text As String)
            NotifierFlyout.ShowNotification("Link copied!", "The link has been copied to your clipboard.")
        End Sub
        Public Shared Sub ShowImageCopiedConfirmation()
            NotifierFlyout.ShowNotification("Image copied!", "The image has been copied to your clipboard.")
        End Sub
        Public Shared Sub ShowOperationCanceled()
            NotifierFlyout.ShowNotification("Canceled", "You canceled the task.")
        End Sub

        Public Shared Sub NoPathSpecified()
            Show(
                GenericErrorTitle,
                "No path specified.",
                "You did not specify any path that could be used.",
                TaskDialogIcon.Error,
                TaskDialogButton.OK
            )
        End Sub
        Shared Sub PathDoesNotExist(path As String)
            Show(
                GenericErrorTitle,
                "The path was not found.",
                $"The path\n{path}\nyou provided does not exist.",
                TaskDialogIcon.Error,
                TaskDialogButton.OK
            )
        End Sub
        Shared Sub InvalidFilePattern(pattern As String)
            'If Not ManagedSettings.SaveImagesToLocalDiskPolicy.IsSet Then
            Dim res = Show(
                GenericErrorTitle,
                "No valid naming pattern provided.",
                $"The file naming pattern you provided is not valid. Therefore, the image was not saved. Please set a valid naming pattern in settings.\nWould you like to turn off automatic saving of your screenshots?",
                TaskDialogIcon.Error,
                TaskDialogButton.Yes, TaskDialogButton.No
            )

            If res = TaskDialogButton.Yes Then
                ' UserSettings.Current.SaveImagesToLocalDisk = False
                ' TODO: Setting settings not supported yet
                ' Maybe we want to remove the "do you want to turn it off now" feature off
            End If
        End Sub
        Shared Sub ErrorSavingImage(ex As Exception, parent As IWin32Window)
            Show(
                parent,
                GenericErrorTitle,
                "There was an error saving your image.",
                ex.Message,
                TaskDialogIcon.Error,
                TaskDialogButton.OK
            )
        End Sub

        Private Shared Function Show(title As String, instructionText As String, text As String, icon As TaskDialogIcon, ParamArray buttons As TaskDialogButton()) As TaskDialogButton
            Return Show(Nothing, title, instructionText, text, icon, buttons)
        End Function

        Private Shared Function Show(parent As IWin32Window, title As String, instructionText As String, text As String, icon As TaskDialogIcon, ParamArray buttons As TaskDialogButton()) As TaskDialogButton
            Dim owner = If(parent?.Handle, IntPtr.Zero)

            Dim buttonCollection As New TaskDialogButtonCollection()
            For Each b In buttons
                buttonCollection.Add(b)
            Next

            Dim page As New TaskDialogPage() With {
                .Caption = title,
                .Heading = instructionText,
                .Text = text,
                .Icon = icon,
                .Buttons = buttonCollection
            }

            Return TaskDialog.ShowDialog(owner, page)
        End Function

        Friend Shared Sub ErrorRegisteringHotkeys(exs As IEnumerable(Of HotkeyRegistrationException))
            Dim hotkeyList = String.Join("\n", exs.Select(Function(e) "- " + If(e.Hotkey?.ToString(), "<unknown hotkey>")))

            Show(
                 GenericErrorTitle,
                 "Error registering hotkeys.",
                 "We could not register the hotkeys you specified. Check if some other application has one or more of these hotkeys already registered or choose different hotkeys." +
                 If(hotkeyList.Length > 0, "\nAffected hotkeys:\n" + hotkeyList, ""),
                 TaskDialogIcon.Error,
                 TaskDialogButton.OK
            )
        End Sub
    End Class
End Namespace
