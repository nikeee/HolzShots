Namespace ScreenshotRelated
    Friend Class BackgroundForm
        Inherits Form

        Sub New(ByVal location As Point, ByVal size As Size)
            StartPosition = FormStartPosition.Manual
            FormBorderStyle = FormBorderStyle.None
            BackColor = Color.Black
            Me.Size = size
            Me.Location = location
            ShowInTaskbar = False
        End Sub
    End Class
End Namespace
