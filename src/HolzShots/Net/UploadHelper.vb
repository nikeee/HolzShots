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

        Friend Shared Function UploadToDefaultUploader(image As Image, settingsContext As HSSettings, Optional format As ImageFormat = Nothing, Optional parentWindow As IWin32Window = Nothing) As Task(Of UploadResult)

            Dim info = UserSettings.GetImageServiceForSettingsContext(settingsContext, HolzShots.My.Application.Uploaders)
            Debug.Assert(info IsNot Nothing)
            Debug.Assert(info.Metadata IsNot Nothing)
            Debug.Assert(info.Uploader IsNot Nothing)

#If RELEASE Then
            If info Is Nothing Then Throw New Exception()
#End If

            Return Upload(info.Uploader, image, settingsContext, format, parentWindow)
        End Function

        ''' <summary>
        ''' Catch the UploadException!
        ''' </summary>
        ''' <param name="uploader"></param>
        ''' <param name="image"></param>
        ''' <param name="format"></param>
        ''' <param name="parentWindow"></param>
        ''' <returns></returns>
        Friend Shared Async Function Upload(uploader As Uploader, image As Image, settingsContext As HSSettings, Optional format As ImageFormat = Nothing, Optional parentWindow As IWin32Window = Nothing) As Task(Of UploadResult)
            format = If(format, GetImageFormat(image, settingsContext))

            If uploader Is Nothing Then Throw New ArgumentNullException(NameOf(uploader))
            If image Is Nothing Then Throw New ArgumentNullException(NameOf(image))
            Debug.Assert(format IsNot Nothing)

            ' Invoke settings if the uploader wants it
            Dim sc = uploader.GetSupportedSettingsContexts()
            If (sc And SettingsInvocationContexts.OnUse) = SettingsInvocationContexts.OnUse Then
                Await uploader.InvokeSettingsAsync(SettingsInvocationContexts.OnUse).ConfigureAwait(True)
            End If

            Using ui As New UploadUI(uploader, image, format, parentWindow, settingsContext)
                ui.ShowUi()
                Dim res As UploadResult = Nothing
                Try
                    res = Await ui.InvokeUploadAsync().ConfigureAwait(True)
                    Return res
                Catch ex As OperationCanceledException
                    Throw New UploadCanceledException(ex)
                Catch ex As UploadException
                    Throw
                Catch ex As Exception
                    Throw New UploadException(ex.Message, ex)
                Finally
                    ui.HideUi()
                End Try
            End Using
        End Function

        Friend Shared Function GetImageFormat(image As Image, settingsContext As HSSettings) As ImageFormat
            If settingsContext.EnableSmartFormatForUpload AndAlso Drawing.ImageFormatAnalyser.IsOptimizable(image) Then
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

        Friend Shared Sub InvokeUploadFinishedUi(result As UploadResult, settingsContext As HSSettings)
            Debug.Assert(result IsNot Nothing)
            Debug.Assert(Not String.IsNullOrWhiteSpace(result.Url))

            Select Case settingsContext.ActionAfterUpload
                Case UploadHandlingAction.Flyout
                    Dim lv As New UploadResultWindow(result, settingsContext)
                    lv.Show()

                Case UploadHandlingAction.CopyToClipboard

                    If Not result.Url.SetAsClipboardText() Then
                        HumanInterop.CopyingFailed(result.Url)
                    ElseIf settingsContext.ShowCopyConfirmation Then
                        HumanInterop.ShowCopyConfirmation(result.Url)
                    End If

                Case UploadHandlingAction.None ' Intentionally do nothing
                Case Else ' Intentionally do nothing
            End Select
        End Sub
        Friend Shared Sub InvokeUploadFailedUi(ex As UploadException)
            HumanInterop.UploadFailed(ex)
        End Sub
    End Class
End Namespace
