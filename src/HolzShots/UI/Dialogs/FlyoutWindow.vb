
Namespace UI.Dialogs
    Public Class FlyoutWindow
        Sub New()
            InitializeComponent()
            StartPosition = FormStartPosition.Manual
        End Sub

        Private Sub FlyoutWindowLoad(sender As Object, e As EventArgs) Handles Me.Load
            MaximumSize = Size
            MinimumSize = Size
        End Sub

        Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean = True
    End Class
End Namespace
