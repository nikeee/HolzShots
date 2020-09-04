Imports System.Linq
Imports HolzShots
Imports HolzShots.Composition
Imports HolzShots.Input
Imports HolzShots.Net
Imports HolzShots.UI.Dialogs
Imports Microsoft.WindowsAPICodePack.Dialogs

Namespace Interop
    ' TODO: Rename to NotificationManager
    Friend Class HumanInterop
        Private Const GenericErrorTitle = "Oh snap! :("
        Private Const HS = LibraryInformation.Name

        Public Shared Sub CopyImageFailed(ex As Exception)
            Show(
                    GenericErrorTitle,
                    "Copying failed",
                    $"Error copying image to clipboard:\n{ex.Message}",
                    TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error
            )
        End Sub
        Public Shared Sub RetrievingImageFromClipboardFailed(ex As Exception)
            Show(
                    GenericErrorTitle,
                    "Could not fetch image from clipboard",
                    $"Maybe you know what this means:\n{ex.Message}",
                    TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error
            )
        End Sub
        Friend Shared Sub NoAdmin()
            Show(
                GenericErrorTitle,
                "Missing permissions",
                "We need administrative permissions to change stuff in the explorer context menu.",
                TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error
            )
        End Sub

        Public Shared Sub UnauthorizedAccessExceptionDirectory(directory As String)
            Show(
                GenericErrorTitle,
                "Access to folder denied",
                $"We tried to access the folder\n{directory}\nbut the access was denied.",
                TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error
            )
        End Sub
        Public Shared Sub PathIsTooLong(directory As String, Optional parent As IWin32Window = Nothing)
            Show(
                GenericErrorTitle,
                "Path is too long",
                $"The path\n{directory}\nis longer than 255 characters. We can only work with path with lesser characters.",
                TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error,
                parent
            )
        End Sub

        Friend Shared Sub PluginLoadingFailed(ex As PluginLoadingFailedException)
            Debug.Assert(ex IsNot Nothing)
            FlyoutNotifier.Notify("Plugins not loaded", $"We could not load the plugins. Here's the error message:\n{ex.InnerException.Message}")
        End Sub

        Friend Shared Sub SettingsUpdated()
            FlyoutNotifier.Notify("Settings Updated", $"HolzShots has detected and loaded new settings.")
        End Sub

        Public Shared Sub UploadFailed(result As UploadException)
            Show("Error Uploading Image", String.Empty, result.Message, TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error)
        End Sub
        Public Shared Sub CopyingFailed(text As String)
            FlyoutNotifier.Notify("Could not copy link :(", $"We could not copy the link to your image to your clipboard.")
        End Sub
        Public Shared Sub ShowCopyConfirmation(text As String)
            FlyoutNotifier.Notify("Link copied!", "The link was copied to your clipboard.")
        End Sub
        Public Shared Sub ShowOperationCanceled()
            FlyoutNotifier.Notify("Canceled", "You canceled the task.")
        End Sub

        Public Shared Sub NoPathSpecified()
            Show(
                GenericErrorTitle,
                "No path specified.",
                "You did not specify any path that could be used.",
                TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error
            )
        End Sub
        Shared Sub PathDoesNotExist(path As String)
            Show(
                GenericErrorTitle,
                "The path was not found.",
                $"The path\n{path}\nyou provided does not exist.",
                TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error
            )
        End Sub
        Shared Sub InvalidFilePattern(pattern As String)
            'If Not ManagedSettings.SaveImagesToLocalDiskPolicy.IsSet Then
            Dim res = Show(
                GenericErrorTitle,
                "No valid naming pattern provided.",
                $"The file naming pattern you provided is not valid. Therefore, the image was not saved. Please set a valid naming pattern in settings.\nWould you like to turn off automatic saving of your screenshots?",
                TaskDialogStandardButtons.Yes Or TaskDialogStandardButtons.No,
                TaskDialogStandardIcon.Error
            )
            If res = DialogResult.Yes Then
                ' UserSettings.Current.SaveImagesToLocalDisk = False
                ' TODO: Setting settings not supported yet
                ' Maybe we want to remove the "do you want to turn it off now" feature off
            End If
        End Sub
        Shared Sub ErrorSavingImage(ex As Exception, parent As IWin32Window)
            Show(
                GenericErrorTitle,
                "There was an error saving your image.",
                ex.Message,
                TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error,
                parent
            )
        End Sub

        Private Shared Function Show(title As String, instructionText As String, text As String, buttons As TaskDialogStandardButtons, icon As TaskDialogStandardIcon, Optional parent As IWin32Window = Nothing) As TaskDialogResult
            If Not TaskDialog.IsPlatformSupported Then
                MessageBox.Show(parent, $"{instructionText}\n{text}\n{HS} will now crash because you seem to use an unsupported operating system.", title)
                Throw New DivideByZeroException()
            End If

            Using diag As New TaskDialog()
                diag.Caption = title
                diag.InstructionText = instructionText
                diag.Text = text
                diag.Icon = icon
                diag.StandardButtons = buttons
                diag.OwnerWindowHandle = If(parent?.Handle, IntPtr.Zero)
                Return diag.Show()
            End Using
        End Function

        Friend Shared Sub ErrorRegisteringHotkeys(exs As IEnumerable(Of HotkeyRegistrationException))

            Dim hotkeyList = String.Join("\n", exs.Select(Function(e) "- " + If(e.Hotkey?.ToString(), "<unknown hotkey>")))

            Show(GenericErrorTitle,
                 "Error registering hotkeys.",
                 "We could not register the hotkeys you specified. Check if some other application has one or more of these hotkeys already registered or choose different hotkeys." +
                 If(hotkeyList.Length > 0, "\nAffected hotkeys:\n" + hotkeyList, ""),
                 TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error
            )
        End Sub
    End Class
End Namespace
