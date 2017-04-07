
Namespace UI.Dialogs
    Friend Class FlyoutNotifier
        Inherits FlyoutWindow

        Private ReadOnly _animator As FlyoutAnimator
        Private ReadOnly _tmr As Timer

        Sub New(title As String, bodyText As String, autoTimeout As Integer)
            InitializeComponent()
            _animator = New FlyoutAnimator(Me)
            _tmr = New Timer() With {
                .Interval = 1000 * autoTimeout
            }

            titleLabel.Text = title
            bodyLabel.Text = bodyText
        End Sub

        Private Sub StatusToasterLoad(sender As Object, e As EventArgs) Handles MyBase.Load
            _animator.AnimateIn(300)
        End Sub

        Private Sub CloseDialog()
            If Visible Then
                _animator.AnimateOut(150, AddressOf Close)
            End If
        End Sub

        Public Sub Notify()
            Visible = True
            _tmr.Enabled = True

            AddHandler _tmr.Tick, Sub()
                                      _tmr.Enabled = False
                                      CloseDialog()
                                  End Sub
        End Sub

        Public Shared Sub Notify(title As String, bodyText As String, Optional autoTimeout As Integer = 3)
            Dim msg As New FlyoutNotifier(title, bodyText, autoTimeout)
            msg.Notify()
        End Sub
    End Class
End Namespace
