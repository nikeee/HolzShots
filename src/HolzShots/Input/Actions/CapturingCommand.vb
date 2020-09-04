Imports System.Runtime.InteropServices
Imports System.Threading.Tasks
Imports HolzShots.Composition.Command
Imports HolzShots.Interop
Imports HolzShots.Interop.LocalDisk
Imports HolzShots.Net
Imports HolzShots.UI.Specialized

Namespace Input.Actions
    Public MustInherit Class CapturingCommand
        Implements ICommand

        Protected Shared Async Function ProcessCapturing(screenshot As Screenshot) As Task
            Debug.Assert(screenshot IsNot Nothing)

            ScreenshotDumper.HandleScreenshot(screenshot)

            Select Case UserSettings.Current.ActionAfterCapture
                Case CaptureHandlingAction.OpenEditor
                    Dim shower As New ShotEditor(screenshot)
                    shower.Show()

                Case CaptureHandlingAction.Upload

                    Try
                        Dim result = Await UploadHelper.UploadToDefaultUploader(screenshot.Image).ConfigureAwait(True)
                        UploadHelper.InvokeUploadFinishedUi(result)
                    Catch ex As UploadCanceledException
                        HumanInterop.ShowOperationCanceled()
                    Catch ex As UploadException
                        UploadHelper.InvokeUploadFailedUi(ex)
                    End Try

                Case CaptureHandlingAction.Copy
                    Try
                        Clipboard.SetImage(screenshot.Image)
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

        Public MustOverride Function Invoke(parameters As IReadOnlyDictionary(Of String, String)) As Task Implements ICommand.Invoke

    End Class
End Namespace
