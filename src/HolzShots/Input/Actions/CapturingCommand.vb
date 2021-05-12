Imports System.Runtime.InteropServices
Imports HolzShots.Composition.Command
Imports HolzShots.IO
Imports HolzShots.Net
Imports HolzShots.UI
Imports HolzShots.Windows.Forms
Imports HolzShots.Windows.Net

Namespace Input.Actions
    Public MustInherit Class CapturingCommand
        Implements ICommand(Of HSSettings)

        Protected Shared Async Function ProcessCapturing(screenshot As Screenshot, settingsContext As HSSettings) As Task
            If screenshot Is Nothing Then Throw New ArgumentNullException(NameOf(screenshot))
            If settingsContext Is Nothing Then Throw New ArgumentNullException(NameOf(settingsContext))

            ScreenshotAggregator.HandleScreenshot(screenshot, settingsContext)

            Select Case settingsContext.ActionAfterCapture
                Case CaptureHandlingAction.OpenEditor
                    Dim shower As New ShotEditor(screenshot, settingsContext)
                    shower.Show()

                Case CaptureHandlingAction.Upload

                    Try
                        Dim result = Await UploadDispatcher.InitiateUploadToDefaultUploader(screenshot.Image, settingsContext, My.Application.Uploaders, Nothing, Nothing).ConfigureAwait(True)
                        UploadHelper.InvokeUploadFinishedUI(result, settingsContext)
                    Catch ex As UploadCanceledException
                        NotificationManager.ShowOperationCanceled()
                    Catch ex As UploadException
                        NotificationManager.UploadFailed(ex)
                    End Try

                Case CaptureHandlingAction.Copy
                    Try
                        Clipboard.SetImage(screenshot.Image)
                        NotificationManager.ShowImageCopiedConfirmation()
                    Catch ex As Exception When _
                            TypeOf ex Is ExternalException _
                            OrElse TypeOf ex Is System.Threading.ThreadStateException _
                            OrElse TypeOf ex Is ArgumentNullException
                        NotificationManager.CopyImageFailed(ex)
                    End Try
                Case CaptureHandlingAction.None ' Intentionally do nothing
                Case Else ' Intentionally do nothing
            End Select
        End Function

        Public MustOverride Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task Implements ICommand(Of HSSettings).Invoke

    End Class
End Namespace
