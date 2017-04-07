Imports HolzShots.Interop

Namespace UI.Dialogs
    Public Class PipettenColorViewer

        ReadOnly _cssRgb As String
        ReadOnly _cssHex As String
        ReadOnly _stdHex As String
        ReadOnly _values As String
        ReadOnly _defval As String

        Public Sub New(ByVal theColor As Color, ByVal startposi As Point)
            InitializeComponent()
            TopMost = True
            ColorBox.Color = theColor
            StartPosition = FormStartPosition.Manual
            Location = New Point(startposi.X - (CopyLinkLabel2.Location.X + 20), startposi.Y - (CopyLinkLabel2.Location.Y + 20))

            _cssRgb = $"rgb({ColorBox.Color.R}, {ColorBox.Color.G}, {ColorBox.Color.B})"
            _cssHex = $"#{ColorBox.Color.R:X2}{ColorBox.Color.G:X2}{ColorBox.Color.B:X2}"
            _stdHex = $"{ColorBox.Color.R:X2}{ColorBox.Color.G:X2}{ColorBox.Color.B:X2}"
            _values = $"R:{ColorBox.Color.R} G:{ColorBox.Color.G} B:{ColorBox.Color.B}"
            _defval = $"{ColorBox.Color.R}, {ColorBox.Color.G}, {ColorBox.Color.B}"

            CopyLinkLabel1.Text = _cssRgb
            CopyLinkLabel2.Text = _cssHex
            CopyLinkLabel3.Text = _stdHex
            CopyLinkLabel4.Text = _values
            CopyLinkLabel5.Text = _defval

            CopyLinkLabel1.Tag = _cssRgb
            CopyLinkLabel2.Tag = _cssHex
            CopyLinkLabel3.Tag = _stdHex
            CopyLinkLabel4.Tag = _values
            CopyLinkLabel5.Tag = _defval
        End Sub

        Private Sub GlassLabelButton1Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyLinkLabel1.Click, CopyLinkLabel2.Click, CopyLinkLabel3.Click, CopyLinkLabel4.Click, CopyLinkLabel5.Click
            DirectCast(DirectCast(sender, LinkLabel).Tag, String).SetAsClipboardText()
            Close()
        End Sub
    End Class
End Namespace
