Imports HolzShots.Net
Imports HolzShots.Windows.Forms

Namespace UI.Dialogs
    Friend Class StatusToaster
        Inherits NoFocusedFlyoutForm
        Implements IUploadProgressReporter

        Private ReadOnly _animator As FlyoutAnimator

        Sub New()
            InitializeComponent()
            uploadedBytesLabel.Text = String.Empty
            _animator = New FlyoutAnimator(Me)
        End Sub

        Private Sub SetSpeed(value As String)
            Debug.Assert(Not speedLabel.InvokeRequired)
            speedLabel.Text = value
        End Sub

        Private Sub SetUploadedBytesLabel(value As String)
            Debug.Assert(Not uploadedBytesLabel.InvokeRequired)
            uploadedBytesLabel.Text = value
        End Sub

        Private Sub SetProgressBarStyleLabel(value As ProgressBarStyle)
            Debug.Assert(Not stuffUploadedBar.InvokeRequired)
            stuffUploadedBar.Style = value
        End Sub

        Private Sub SetProgressBarValueLabel(value As Integer)
            If stuffUploadedBar.InvokeRequired Then
                stuffUploadedBar.Invoke(Sub() stuffUploadedBar.Value = value)
            Else
                stuffUploadedBar.Value = value
            End If
        End Sub


        Public Sub UpdateProgress(report As UploadProgress, speed As Speed(Of MemSize)) Implements IUploadProgressReporter.UpdateProgress
            Select Case report.State
                Case UploadState.NotStarted
                    SetProgressBarStyleLabel(ProgressBarStyle.Marquee)
                    SetUploadedBytesLabel("Starting Upload...")
                    SetSpeed(String.Empty)
                Case UploadState.Finished
                    SetProgressBarStyleLabel(ProgressBarStyle.Marquee)
                    SetUploadedBytesLabel("Waiting for server reply...")
                    SetSpeed(String.Empty)
                Case Else
                    SetProgressBarStyleLabel(ProgressBarStyle.Continuous)

                    SetSpeed(speed.ToString())
                    SetProgressBarValueLabel(report.ProgressPercentage)
                    SetUploadedBytesLabel($"{report.Current}/{report.Total}")
            End Select
        End Sub

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            _animator.AnimateIn(300)
        End Sub

        Private Async Function CloseDialog() As Task
            If Visible Then
                Await _animator.AnimateOut(150).ConfigureAwait(True)
                Close()
            End If
        End Function

        Public Shadows Sub ShowProgress() Implements IUploadProgressReporter.ShowProgress
            Show()
        End Sub
        Public Shadows Sub CloseProgress() Implements IUploadProgressReporter.CloseProgress
            Dim unused = CloseDialog()
        End Sub
    End Class
End Namespace
