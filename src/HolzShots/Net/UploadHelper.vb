Imports System.Drawing.Imaging
Imports System.Threading.Tasks
Imports HolzShots.Interop
Imports HolzShots.UI.Dialogs

Namespace Net
    Friend Class UploadHelper
        Private Sub New()
        End Sub

        Public Shared Function UploadToDefaultUploader(image As Image) As Task(Of UploadResult)
            Return UploadToDefaultUploader(image, Nothing, Nothing)
        End Function

        Friend Shared Function UploadToDefaultUploader(image As Image, Optional format As ImageFormat = Nothing, Optional parentWindow As IWin32Window = Nothing) As Task(Of UploadResult)
            Dim uploaderName = My.Settings.DefaultImageHoster
            Dim info = My.Application.Uploaders.GetUploaderByName(uploaderName)
            Debug.Assert(info.HasValue)
            If Not info.HasValue Then Throw New Exception()
            Dim v = info.Value

            Debug.Assert(info IsNot Nothing)
            Debug.Assert(v.metadata IsNot Nothing)
            Debug.Assert(v.uploader IsNot Nothing)
            Debug.Assert(v.metadata.Name = uploaderName)

            Return Upload(v.uploader, image, format, parentWindow)
        End Function

        ''' <summary>
        ''' Catch the UploadException!
        ''' </summary>
        ''' <param name="uploader"></param>
        ''' <param name="image"></param>
        ''' <param name="format"></param>
        ''' <param name="parentWindow"></param>
        ''' <returns></returns>
        Friend Shared Async Function Upload(uploader As Uploader, image As Image, Optional format As ImageFormat = Nothing, Optional parentWindow As IWin32Window = Nothing) As Task(Of UploadResult)
            format = If(format, GetImageFormat(image))

            If uploader Is Nothing Then Throw New ArgumentNullException(NameOf(uploader))
            If image Is Nothing Then Throw New ArgumentNullException(NameOf(image))
            Debug.Assert(format IsNot Nothing)
            Debug.Assert(format.GetFormatMetadata() IsNot Nothing)

            ' Invoke settings if the uploader wants it
            Dim sc = uploader.GetSupportedSettingsContexts()
            If (sc And SettingsInvocationContexts.OnUse) = SettingsInvocationContexts.OnUse Then
                Await uploader.InvokeSettingsAsync(SettingsInvocationContexts.OnUse).ConfigureAwait(True)
            End If

            Using ui As New UploadUi(uploader, image, format, parentWindow)
                ui.ShowUi()
                Dim res As UploadResult = Nothing
                Try
                    res = Await ui.InvokeUploadAsync().ConfigureAwait(True)
                    Return res
                Catch ex As OperationCanceledException
                    Throw New UploadCanceledException(ex)
                Catch ex As UploadException
                    Throw ex
                Finally
                    ui.HideUi()
                End Try
            End Using
        End Function

        Friend Shared Function GetImageFormat(image As Image) As ImageFormat
            If UserSettings.Current.EnableSmartFormatForUpload AndAlso Drawing.ImageFormatAnalyser.IsOptimizable(image) Then
                Try
                    Dim bmp As Bitmap = If(TypeOf image Is Bitmap, DirectCast(image, Bitmap), New Bitmap(image))
                    Return Drawing.ImageFormatAnalyser.GetBestFittingFormat(bmp) ' Experimental?
                Catch ex As Exception
                    Return ImageFormat.Png
                End Try
            Else
                Return ImageFormat.Png
            End If
        End Function

        Friend Shared Sub InvokeUploadFinishedUi(result As UploadResult)
            Debug.Assert(result IsNot Nothing)
            Debug.Assert(Not String.IsNullOrWhiteSpace(result.Url))

            If ManagedSettings.EnableLinkViewer Then
                Dim lv As New UploadResultWindow(result)
                lv.Show()
            Else
                If Not result.Url.SetAsClipboardText() Then
                    HumanInterop.CopyingFailed(result.Url)
                ElseIf UserSettings.Current.ShowCopyConfirmation Then
                    HumanInterop.ShowCopyConfirmation(result.Url)
                End If
            End If
        End Sub
        Friend Shared Sub InvokeUploadFailedUi(ex As UploadException)
            HumanInterop.UploadFailed(ex)
        End Sub
    End Class
End Namespace
