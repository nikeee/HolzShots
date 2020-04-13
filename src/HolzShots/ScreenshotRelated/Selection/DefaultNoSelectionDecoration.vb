Imports System.Drawing.Text

Namespace ScreenshotRelated.Selection
    Public Class DefaultNoSelectionDecoration
        Implements INoSelectionDecoration

        Protected Shared ReadOnly QuickInfoBrush As Brush = New SolidBrush(Color.FromArgb(70, 255, 255, 255))

        Private Shared ReadOnly InfoText As String() = {
            "Left Mouse: Select area.",
            "Right Mouse: Move selected area",
            "Space Bar: Toggle magnifier"}

        Private Shared ReadOnly RichInfoTextFont As New Font("Segoe UI", 24, FontStyle.Regular, GraphicsUnit.Point)

        Private ReadOnly _richInfoTextSize As Size
        Private ReadOnly _richInfoTextLocation As PointF

        Sub New()
            _richInfoTextSize = TextRenderer.MeasureText(String.Join(Environment.NewLine, InfoText), RichInfoTextFont) ' Fuck dem clusters!
            _richInfoTextLocation = New PointF(CInt(Screen.PrimaryScreen.WorkingArea.X - SystemInformation.VirtualScreen.X + Screen.PrimaryScreen.WorkingArea.Width / 2) - CInt(_richInfoTextSize.Width / 2),
                                               CInt(Screen.PrimaryScreen.WorkingArea.Y - SystemInformation.VirtualScreen.Y + Screen.PrimaryScreen.WorkingArea.Height / 2) - CInt(_richInfoTextSize.Height / 2))
        End Sub
        Public Sub DrawNoSelection(ByRef g As Graphics, ByRef wholeScreen As Rectangle) Implements INoSelectionDecoration.DrawNoSelection
            If g Is Nothing Then Throw New ArgumentNullException(NameOf(g))

            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
            Dim sz As SizeF
            Dim height As Single = 0
            For i As Integer = 0 To InfoText.Length - 1
                sz = g.MeasureString(InfoText(i), RichInfoTextFont)
                g.FillRectangle(QuickInfoBrush, _richInfoTextLocation.X, _richInfoTextLocation.Y + height, sz.Width, sz.Height)
                g.DrawString(InfoText(i), RichInfoTextFont, Brushes.White, New RectangleF(_richInfoTextLocation.X, _richInfoTextLocation.Y + height, sz.Width, sz.Height))
                height += sz.Height
            Next
        End Sub
    End Class
End Namespace
