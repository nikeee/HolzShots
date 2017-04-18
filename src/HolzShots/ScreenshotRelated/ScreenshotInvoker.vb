Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports HolzShots.Interop
Imports HolzShots.Interop.LocalDisk
Imports HolzShots.Net
Imports HolzShots.ScreenshotRelated.Selection
Imports HolzShots.UI.Specialized
Imports Microsoft.WindowsAPICodePack.Dialogs

Namespace ScreenshotRelated

    Module ScreenshotInvoker

#Region "Fullscreen"

        Public Async Function DoFullscreen() As Task
            Debug.Assert(ManagedSettings.EnableFullscreenScreenshot)

            If ManagedSettings.EnableFullscreenScreenshot Then
                Dim shot = ScreenshotMethods.CaptureFullscreen()
                Debug.Assert(shot IsNot Nothing)
                Await ProceedScreenshot(shot)
            End If
        End Function

#End Region
#Region "Selector"

        Public Async Function DoSelector() As Task
            Debug.Assert(ManagedSettings.EnableAreaScreenshot)
            Debug.Assert(Not AreaSelector.IsInAreaSelector)
            If ManagedSettings.EnableIngameMode AndAlso HolzShotsEnvironment.IsFullScreen AndAlso Not HolzShotsEnvironment.IsInMetroApplication() Then Return

            If ManagedSettings.EnableAreaScreenshot AndAlso Not AreaSelector.IsInAreaSelector Then
                Dim shot As Screenshot = Nothing
                Try
                    shot = Await ScreenshotMethods.CaptureSelection()
                    Debug.Assert(shot IsNot Nothing)
                    If shot Is Nothing Then Throw New TaskCanceledException()
                Catch cancelled As TaskCanceledException
                    Debug.WriteLine("Area Selection cancelled")
                    Return
                End Try
                Debug.Assert(shot IsNot Nothing)
                Await ProceedScreenshot(shot)
            End If
        End Function

#End Region
#Region "Taskbar"

        Public Async Function DoTaskbar() As Task
            Dim shot = ScreenshotMethods.CaptureTaskbar()
            Await ProceedScreenshot(shot)
        End Function

#End Region
#Region "Window"

        Public Async Function DoWindow() As Task
            Debug.Assert(ManagedSettings.EnableWindowScreenshot)

            If ManagedSettings.EnableWindowScreenshot Then
                Dim h As IntPtr = NativeMethods.GetForegroundWindow()

                Dim info As NativeTypes.WindowPlacement
                NativeMethods.GetWindowPlacement(h, info)

                Dim shot = ScreenshotMethods.CaptureWindow(h)
                Await ProceedScreenshot(shot)
            End If
        End Function

#End Region

        Private Async Function ProceedScreenshot(shot As Screenshot) As Task
            Debug.Assert(shot IsNot Nothing)
            ScreenshotDumper.HandleScreenshot(shot)
            If ManagedSettings.EnableShotEditor Then
                Dim shower As New ShotEditor(shot)
                shower.Show()
            Else
                Try
                    Dim result = Await UploadHelper.UploadToDefaultUploader(shot.Image)
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
                ofd.Filters.Add(New CommonFileDialogFilter(Common.UI.Localization.DialogFilterImages, SupportedFilesFilter))
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
                    Dim result = Await UploadHelper.UploadToDefaultUploader(bmp, format)
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
