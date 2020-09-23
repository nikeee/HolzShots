Imports HolzShots.Interop
Imports HolzShots.UI.Dialogs
Imports HolzShots.Windows.Forms

Namespace Net
    Friend Class UploadHelper
        Private Sub New()
        End Sub

        Friend Shared Function GetUploadReporterForCurrentSettingsContext(ByVal settingsContext As HSSettings, ByVal parentWindow As IWin32Window) As IUploadProgressReporter

#If DEBUG Then
            Dim reporters = New List(Of IUploadProgressReporter)(3) From {
                New ConsoleProgressReporter()
            }
#Else
            Dim reporters = New List(Of IUploadProgressReporter)(2)
#End If

            If settingsContext.ShowUploadProgress Then
                reporters.Add(New StatusToaster())
            End If

            If parentWindow.GetHandle() <> IntPtr.Zero Then
                reporters.Add(New TaskbarItemProgressReporter(parentWindow.Handle))
            End If

            Return New AggregateProgressReporter(reporters)
        End Function

        Friend Shared Sub InvokeUploadFinishedUi(result As UploadResult, settingsContext As HSSettings)
            Debug.Assert(result IsNot Nothing)
            Debug.Assert(Not String.IsNullOrWhiteSpace(result.Url))

            Select Case settingsContext.ActionAfterUpload
                Case UploadHandlingAction.Flyout
                    Dim lv As New UploadResultForm(result, settingsContext)
                    lv.Show()

                Case UploadHandlingAction.CopyToClipboard

                    If ClipboardEx.SetText(result.Url) Then
                        HumanInterop.ShowCopyConfirmation(result.Url)
                    ElseIf settingsContext.ShowCopyConfirmation Then
                        HumanInterop.CopyingFailed(result.Url)
                    End If

                Case UploadHandlingAction.None ' Intentionally do nothing
                Case Else ' Intentionally do nothing
            End Select
        End Sub
    End Class
End Namespace
