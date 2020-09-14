Imports HolzShots.Net

Namespace UI.Dialogs
    Friend Class StatusToaster
        Inherits HolzShots.Windows.Forms.FlyoutForm
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
                Case Net.UploadState.NotStarted
                    SetProgressBarStyleLabel(ProgressBarStyle.Marquee)
                    SetUploadedBytesLabel("Starting Upload...")
                    SetSpeed(String.Empty)
                Case Net.UploadState.Finished
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

        Private Sub StatusToasterLoad(sender As Object, e As EventArgs) Handles MyBase.Load
            _animator.AnimateIn(300)
        End Sub

        Private Sub CloseDialog()
            If Visible Then
                _animator.AnimateOut(150, AddressOf Close)
            End If
        End Sub

        Public Shadows Sub ShowProgress() Implements IUploadProgressReporter.ShowProgress
            Show()
        End Sub
        Public Shadows Sub CloseProgress() Implements IUploadProgressReporter.CloseProgress
            CloseDialog()
        End Sub
    End Class
End Namespace
