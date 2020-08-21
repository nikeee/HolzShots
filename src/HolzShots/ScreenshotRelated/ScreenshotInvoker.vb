Imports System.Drawing.Imaging
Imports HolzShots.Interop
Imports HolzShots.Net
Imports HolzShots.Interop.LocalDisk
Imports HolzShots.UI.Specialized
Imports System.Threading.Tasks

Namespace ScreenshotRelated

    Module ScreenshotInvoker

        Public Async Function ProceedWithScreenshot(screenshot As Screenshot) As Task
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

                Case CaptureHandlingAction.None ' Intentionally do nothing
                Case Else ' Intentionally do nothing
            End Select
        End Function
    End Module
End Namespace
