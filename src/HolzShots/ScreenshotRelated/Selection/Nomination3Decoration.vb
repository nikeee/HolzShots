
Namespace ScreenshotRelated.Selection
    Friend Class Nomination3Decoration
        Inherits QuickInfoDecoration

        Private Shared _infoText1 As String = ""
        Private _infoTextRectangle1 As Rectangle = Nothing

        Private Shared _infoText2 As String = ""
        Private _infoTextRectangle2 As Rectangle = Nothing

        Private _infoTextBrush1 As Brush = QuickInfoTextBrushLight
        Private _infoTextBrush2 As Brush = QuickInfoTextBrushLight

        Public Overrides Sub DrawSelection(ByRef g As Graphics, ByRef selectionBorderPen As Pen)
            g.DrawRectangle(selectionBorderPen, CurrentSelection)

            g.FillRectangle(QuickInfoBrush, _infoTextRectangle1)
            g.DrawString(_infoText1, QuickInfoTextFont, _infoTextBrush1, _infoTextRectangle1)

            g.TranslateTransform(_infoTextRectangle2.X, _infoTextRectangle2.Y)
            g.RotateTransform(270)

            g.FillRectangle(QuickInfoBrush, New Rectangle(0, 0, _infoTextRectangle2.Width, _infoTextRectangle2.Height))
            g.DrawString(_infoText2, QuickInfoTextFont, _infoTextBrush2, New Rectangle(0, 0, _infoTextRectangle2.Width, _infoTextRectangle2.Height))
            g.RotateTransform(-270)
            g.TranslateTransform(-_infoTextRectangle2.X, -_infoTextRectangle2.Y)
        End Sub

        Protected Overrides Sub OnUpdateCoordinates()

            _infoText1 = CurrentSelection.Width.ToString()

            _infoTextRectangle1.Size = TextRenderer.MeasureText(_infoText1, QuickInfoTextFont)
            _infoTextRectangle1.X = CurrentSelection.X + CInt(CurrentSelection.Width / 2) - CInt(_infoTextRectangle1.Width / 2)
            _infoTextRectangle1.Y = CurrentSelection.Y - _infoTextRectangle1.Height

            If _infoTextRectangle1.Y + _infoTextRectangle1.Height < _infoTextRectangle1.Height Then
                _infoTextRectangle1.Y = CurrentSelection.Y
                _infoTextBrush1 = QuickInfoTextBrushDark
            Else
                _infoTextBrush1 = QuickInfoTextBrushLight
            End If

            _infoText2 = CurrentSelection.Height.ToString()

            _infoTextRectangle2.Size = TextRenderer.MeasureText(_infoText2, QuickInfoTextFont)
            _infoTextRectangle2.X = CurrentSelection.X - _infoTextRectangle2.Height
            _infoTextRectangle2.Y = CurrentSelection.Y + CInt(CurrentSelection.Height / 2) + CInt(_infoTextRectangle2.Width / 2)

            If _infoTextRectangle2.X + _infoTextRectangle2.Width < _infoTextRectangle2.Width Then
                _infoTextRectangle2.X = CurrentSelection.X
                _infoTextBrush2 = QuickInfoTextBrushDark
            Else
                _infoTextBrush2 = QuickInfoTextBrushLight
            End If
        End Sub
    End Class
End Namespace
