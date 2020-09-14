Imports HolzShots.Interop
Imports HolzShots.Net
Imports HolzShots.Windows.Forms

Namespace UI.Dialogs
    Public Class UploadResultWindow
        Inherits FlyoutForm

        Private ReadOnly _animator As FlyoutAnimator
        Private ReadOnly _result As UploadResult
        Private ReadOnly _displayUrl As String
        Private ReadOnly _settingsContext As HSSettings

        Sub New(res As UploadResult, settingsContext As HSSettings)
            If res Is Nothing Then Throw New ArgumentNullException(NameOf(res))
            If settingsContext Is Nothing Then Throw New ArgumentNullException(NameOf(settingsContext))

            _settingsContext = settingsContext

            InitializeComponent()

            _animator = New FlyoutAnimator(Me)
            _result = res

            If Not String.IsNullOrWhiteSpace(res.Url) Then
                _displayUrl = EnvironmentEx.ShortenViaEllipsisIfNeeded(res.Url, 26)
            End If
        End Sub

#Region "Fade and Close"

        Private Sub MightClose()
            If _settingsContext.AutoCloseLinkViewer Then
                Dim unused = CloseDialog()
            End If
        End Sub

        Private Sub UploadResultWindowLoad(sender As Object, e As EventArgs) Handles MyBase.Load
            _animator.AnimateIn(300)
        End Sub

        Private Sub CloseWindowLabelLinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles closeWindowLabel.LinkClicked
            Dim unused = CloseDialog()
        End Sub

        Private Async Function CloseDialog() As Task
            Await _animator.AnimateOut(150).ConfigureAwait(True)
            Close()
        End Function


#End Region
#Region "UI Handlers"

        Private Sub CopyDirectClick(sender As Object, e As EventArgs) Handles copyDirect.Click
            If _result.Url.SetAsClipboardText() Then
                MightClose()
            End If
        End Sub

        Private Sub CopyHtmlClick(sender As Object, e As EventArgs) Handles copyHTML.Click
            If $"<img src=""{_result.Url}"">".SetAsClipboardText() Then
                MightClose()
            End If
        End Sub

        Private Sub CopyBbClick(sender As Object, e As EventArgs) Handles copyBB.Click
            If $"[img]{_result.Url}[/img]".SetAsClipboardText() Then
                MightClose()
            End If
        End Sub

#End Region

    End Class

End Namespace
