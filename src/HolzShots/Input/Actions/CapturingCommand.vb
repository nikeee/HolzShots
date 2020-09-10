Imports System.Runtime.InteropServices
Imports System.Threading.Tasks
Imports HolzShots.Composition.Command
Imports HolzShots.Interop
Imports HolzShots.Interop.LocalDisk
Imports HolzShots.Net
Imports HolzShots.UI.Specialized

Namespace Input.Actions
    Public MustInherit Class CapturingCommand
        Implements ICommand(Of HSSettings)

        Protected Shared Async Function ProcessCapturing(screenshot As Screenshot, settingsContext As HSSettings) As Task
            Debug.Assert(screenshot IsNot Nothing)

            ScreenshotDumper.HandleScreenshot(screenshot, settingsContext)

            Select Case settingsContext.ActionAfterCapture
                Case CaptureHandlingAction.OpenEditor
                    Dim shower As New ShotEditor(screenshot, settingsContext)
                    shower.Show()

                Case CaptureHandlingAction.Upload

                    Try
                        Dim result = Await UploadHelper.UploadToDefaultUploader(screenshot.Image, settingsContext).ConfigureAwait(True)
                        UploadHelper.InvokeUploadFinishedUi(result, settingsContext)
                    Catch ex As UploadCanceledException
                        HumanInterop.ShowOperationCanceled()
                    Catch ex As UploadException
                        UploadHelper.InvokeUploadFailedUi(ex)
                    End Try

                Case CaptureHandlingAction.Copy
                    Try
                        Clipboard.SetImage(screenshot.Image)
                        HumanInterop.ShowImageCopiedConfirmation()
                    Catch ex As Exception When _
                            TypeOf ex Is ExternalException _
                            OrElse TypeOf ex Is System.Threading.ThreadStateException _
                            OrElse TypeOf ex Is ArgumentNullException
                        HumanInterop.CopyImageFailed(ex)
                    End Try
                Case CaptureHandlingAction.None ' Intentionally do nothing
                Case Else ' Intentionally do nothing
            End Select
        End Function

        Public MustOverride Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task Implements ICommand(Of HSSettings).Invoke

    End Class
End Namespace
