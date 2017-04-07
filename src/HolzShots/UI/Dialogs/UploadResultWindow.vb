Imports HolzShots.Interop
Imports HolzShots.Net

Namespace UI.Dialogs
    Public Class UploadResultWindow
        Inherits FlyoutWindow

        Private ReadOnly _animator As FlyoutAnimator
        Public ReadOnly Property Result As UploadResult
        Public ReadOnly Property DisplayUrl As String

        Sub New(res As UploadResult)
            If res Is Nothing Then Throw New ArgumentNullException(NameOf(res))

            InitializeComponent()
            _animator = New FlyoutAnimator(Me)
            Result = res

            If Not String.IsNullOrWhiteSpace(res.Url) Then
                DisplayUrl = FormattingHelpers.ShortenUrlForDisplay(res.Url)
            End If
        End Sub

#Region "Fade and Close"

        Private Sub MightClose()
            If ManagedSettings.AutoCloseLinkViewer Then
                CloseDialog()
            End If
        End Sub

        Private Sub UploadResultWindowLoad(sender As Object, e As EventArgs) Handles MyBase.Load
            _animator.AnimateIn(300)
        End Sub

        Private Sub CloseWindowLabelLinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles closeWindowLabel.LinkClicked
            CloseDialog()
        End Sub

        Private Sub CloseDialog()
            _animator.AnimateOut(150, AddressOf Close)
        End Sub

#End Region
#Region "UI Handlers"

        Private Sub CopyDirectClick(sender As Object, e As EventArgs) Handles copyDirect.Click
            If Result.Url.SetAsClipboardText() Then
                MightClose()
            End If
        End Sub

        Private Sub CopyHtmlClick(sender As Object, e As EventArgs) Handles copyHTML.Click
            If $"<img src=""{Result.Url}"">".SetAsClipboardText() Then
                MightClose()
            End If
        End Sub

        Private Sub CopyBbClick(sender As Object, e As EventArgs) Handles copyBB.Click
            If $"[img]{Result.Url}[/img]".SetAsClipboardText() Then
                MightClose()
            End If
        End Sub

#End Region

    End Class

End Namespace
