Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports HolzShots.Interop
Imports HolzShots.Net
Imports HolzShots.Interop.LocalDisk
Imports HolzShots.UI.Specialized
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports System.Threading.Tasks

Namespace ScreenshotRelated

    Module ScreenshotInvoker

        Public Async Function ProceedWithScreenshot(screenshot As Screenshot) As Task
            Debug.Assert(screenshot IsNot Nothing)

            ScreenshotDumper.HandleScreenshot(screenshot)
            If ManagedSettings.EnableShotEditor Then
                Dim shower As New ShotEditor(screenshot)
                shower.Show()
            Else
                Try
                    Dim result = Await UploadHelper.UploadToDefaultUploader(screenshot.Image).ConfigureAwait(True)
                    UploadHelper.InvokeUploadFinishedUi(result)
                Catch ex As UploadCanceledException
                    HumanInterop.ShowOperationCanceled()
                Catch ex As UploadException
                    UploadHelper.InvokeUploadFailedUi(ex)
                End Try
            End If
        End Function

#Region "CustomImage"

        Private Class Localization
            Private Sub New()
            End Sub
            Public Const OpenInShotEditor = "Open Image in ShotEditor"
            Public Const UploadImage = "Select Image to Upload"
        End Class

        Friend Sub OpenSelectedImage()
            ShowFileSelector(Localization.OpenInShotEditor, AddressOf OpenSpecificImage)
        End Sub

        Friend Sub UploadSelectedImage()
            ShowFileSelector(Localization.UploadImage, AddressOf UploadSpecificImage)
        End Sub

        Private Sub ShowFileSelector(title As String, callback As Action(Of String))
            Using ofd As New CommonOpenFileDialog()
                ofd.Title = title
                ofd.Filters.Add(New CommonFileDialogFilter(UI.Localization.DialogFilterImages, SupportedFilesFilter))
                ofd.Multiselect = False
                If ofd.ShowDialog() = CommonFileDialogResult.Ok AndAlso File.Exists(ofd.FileName) Then
                    callback(ofd.FileName)
                End If
            End Using
        End Sub

        Friend Sub OpenSpecificImage(fileName As String)
            Dim bmp As New Bitmap(fileName)
            Dim shot = Screenshot.FromImported(bmp)
            Dim editor As New ShotEditor(shot)
            AddHandler editor.Disposed, Sub() bmp.Dispose()
            editor.Show()
        End Sub

        Friend Sub TryOpenSpecificImage(path As String)
            If CheckPath(path) Then OpenSpecificImage(path)
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

        Friend Sub TryUploadSpecificImage(ByVal path As String)
            If CheckPath(path) Then UploadSpecificImage(path)
        End Sub

        Private ReadOnly AllowedExtensions As String() = {".bmp", ".jpg", ".jpeg", ".png", ".tif", ".tiff"}

        Private Function CheckPath(directory As String) As Boolean
            Dim ext = Path.GetExtension(directory)
            Return AllowedExtensions.Contains(ext)
        End Function

#End Region
    End Module
End Namespace
