Imports System.Threading.Tasks
Imports HolzShots.Interop
Imports HolzShots.Interop.LocalDisk
Imports HolzShots.Net
Imports HolzShots.ScreenshotRelated
Imports HolzShots.ScreenshotRelated.Selection
Imports HolzShots.UI.Specialized
Imports HolzShots.Composition.Command
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports System.IO

Namespace Input.Actions
    ' TODO: Split this into multiple files

    Public MustInherit Class FileDependentCommand
        Protected Function ShowFileSelector(title As String) As String
            Using ofd As New CommonOpenFileDialog()
                ofd.Title = title
                ofd.Filters.Add(New CommonFileDialogFilter(UI.Localization.DialogFilterImages, SupportedFilesFilter))
                ofd.Multiselect = False
                If ofd.ShowDialog() = CommonFileDialogResult.Ok AndAlso File.Exists(ofd.FileName) Then
                    Return ofd.FileName
                End If
            End Using
            Return Nothing
        End Function
    End Class

    <Command("uploadImage")>
    Public Class UploadImageCommand
        Inherits FileDependentCommand
        Implements ICommand

        Private Const UploadImage = "Select Image to Upload"

        Public Function Invoke() As Task Implements ICommand.Invoke
            Dim fileName = ShowFileSelector(UploadImage)
            If fileName IsNot Nothing Then
                UploadSpecificImage(fileName)
            End If
            Return Task.CompletedTask
        End Function
    End Class
    <Command("editImage")>
    Public Class EditImageCommand
        Inherits FileDependentCommand
        Implements ICommand

        Private Const OpenInShotEditor = "Open Image in ShotEditor"

        Public Function Invoke() As Task Implements ICommand.Invoke
            Dim fileName = ShowFileSelector(OpenInShotEditor)
            If fileName IsNot Nothing Then
                OpenSpecificImage(fileName)
            End If
            Return Task.CompletedTask
        End Function
    End Class

    <Command("openSettingsJson")>
    Public Class OpenSettingsJsonCommand
        Implements ICommand

        Public Function Invoke() As Task Implements ICommand.Invoke
            UserSettings.OpenSettingsInDefaultEditor()
            Return Task.CompletedTask
        End Function
    End Class

    <Command("openImages")>
    Public Class OpenImagesFolderCommand
        Implements ICommand

        Public Function Invoke() As Task Implements ICommand.Invoke
            ScreenshotDumper.OpenPictureDumpFolder()
            Return Task.CompletedTask
        End Function
    End Class

    <Command("captureArea")>
    Public Class SelectAreaCommand
        Implements ICommand

        Public Async Function Invoke() As Task Implements ICommand.Invoke

            ' TODO: Add proper assertion
            ' Debug.Assert(ManagedSettings.EnableAreaScreenshot)
            Debug.Assert(Not AreaSelector.IsInAreaSelector)
            If UserSettings.Current.EnableIngameMode AndAlso HolzShotsEnvironment.IsFullScreen Then Return

            ' TODO: Re-add proper if condition
            'If ManagedSettings.EnableAreaScreenshot AndAlso Not AreaSelector.IsInAreaSelector Then
            If Not AreaSelector.IsInAreaSelector Then
                Dim shot As Screenshot
                Try
                    shot = Await ScreenshotMethods.CaptureSelection().ConfigureAwait(True)
                    Debug.Assert(shot IsNot Nothing)
                    If shot Is Nothing Then Throw New TaskCanceledException()
                Catch cancelled As TaskCanceledException
                    Debug.WriteLine("Area Selection cancelled")
                    Return
                End Try
                Debug.Assert(shot IsNot Nothing)
                Await ProceedWithScreenshot(shot).ConfigureAwait(True)
            End If

        End Function
    End Class

    <Command("captureEntireScreen")>
    Public Class FullscreenCommand
        Implements ICommand

        Public Async Function Invoke() As Task Implements ICommand.Invoke
            ' TODO: Add proper assertion
            ' Debug.Assert(ManagedSettings.EnableFullscreenScreenshot)

            ' TODO: Re-add proper if condition
            ' If ManagedSettings.EnableFullscreenScreenshot Then
            Dim shot = ScreenshotMethods.CaptureFullscreen()
            Debug.Assert(shot IsNot Nothing)
            Await ProceedWithScreenshot(shot).ConfigureAwait(True)
            'End If
        End Function
    End Class

    <Command("captureWindow")>
    Public Class WindowCommand
        Implements ICommand

        Public Async Function Invoke() As Task Implements ICommand.Invoke
            ' TODO: Add proper assertion
            ' Debug.Assert(ManagedSettings.EnableWindowScreenshot)

            ' TODO: Re-add proper if condition
            ' If ManagedSettings.EnableWindowScreenshot Then
            Dim h As IntPtr = Native.User32.GetForegroundWindow()

            Dim info As Native.User32.WindowPlacement
            Native.User32.GetWindowPlacement(h, info)

            Dim shot = ScreenshotMethods.CaptureWindow(h)
            Await ProceedWithScreenshot(shot).ConfigureAwait(True)
            ' End If
        End Function
    End Class
End Namespace
