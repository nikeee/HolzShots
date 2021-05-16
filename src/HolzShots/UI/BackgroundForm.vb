Namespace UI
    Friend Class BackgroundForm
        Inherits Form

        Sub New(location As Point, size As Size)
            StartPosition = FormStartPosition.Manual
            FormBorderStyle = FormBorderStyle.None
            BackColor = Color.Black
            Me.Size = size
            Me.Location = location
            ShowInTaskbar = False
        End Sub
    End Class
End Namespace
