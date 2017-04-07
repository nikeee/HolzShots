Imports System.Text

Namespace ScreenshotRelated.Selection
    Friend Class Nomination2Decoration
        Inherits QuickInfoDecoration

        Private _infoText As String = ""
        Private _infoTextRectangle As Rectangle = Nothing
        Private _infoTextBrush As Brush = QuickInfoTextBrushLight

        Public Overrides Sub DrawSelection(ByRef g As Graphics, ByRef selectionBorderPen As Pen)
            g.DrawRectangle(selectionBorderPen, CurrentSelection)

            g.FillRectangle(QuickInfoBrush, _infoTextRectangle)
            g.DrawString(_infoText, QuickInfoTextFont, _infoTextBrush, _infoTextRectangle)
        End Sub

        Private ReadOnly _sb As New StringBuilder(20)
        Protected Overrides Sub OnUpdateCoordinates()
            _sb.Length = 0

            _sb.Append(CurrentSelection.Width)
            _sb.Append("x"c)
            _sb.Append(CurrentSelection.Height)

            _infoText = _sb.ToString()

            _infoTextRectangle.Size = TextRenderer.MeasureText(_infoText, QuickInfoTextFont)
            _infoTextRectangle.X = CurrentSelection.X + CurrentSelection.Width + 1
            _infoTextRectangle.Y = CurrentSelection.Y + CurrentSelection.Height - _infoTextRectangle.Height + 1

            If _infoTextRectangle.X + _infoTextRectangle.Width > CurrentWholeScreen.Width Then
                _infoTextRectangle.X -= _infoTextRectangle.Width + 1
                _infoTextRectangle.Y -= 1
                _infoTextBrush = QuickInfoTextBrushDark
            Else
                _infoTextBrush = QuickInfoTextBrushLight
            End If
        End Sub
    End Class
End Namespace
