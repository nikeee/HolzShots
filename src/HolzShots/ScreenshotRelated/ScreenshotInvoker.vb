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

                Case CaptureHandlingAction.None
                Case Else

            End Select
        End Function

#Region "CustomImage"

        Public Sub OpenSpecificImage(fileName As String)
            Dim bmp As New Bitmap(fileName)
            Dim shot = Screenshot.FromImported(bmp)
            Dim editor As New ShotEditor(shot)
            AddHandler editor.Disposed, Sub() bmp.Dispose()
            editor.Show()
        End Sub

        Friend Async Sub UploadSpecificImage(ByVal fileName As String)
            Using bmp As New Bitmap(fileName)
                Dim format As ImageFormat = fileName.GetImageFormatFromFileExtension()
                Try
                    Dim result = Await UploadHelper.UploadToDefaultUploader(bmp, format).ConfigureAwait(True)
                    UploadHelper.InvokeUploadFinishedUi(result)
                Catch ex As UploadCanceledException
                    HumanInterop.ShowOperationCanceled()
                Catch ex As UploadException
                    UploadHelper.InvokeUploadFailedUi(ex)
                End Try
            End Using
        End Sub

#End Region
    End Module
End Namespace
