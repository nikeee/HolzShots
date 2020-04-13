Imports System.Drawing.Drawing2D
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Circle
        Inherits Tool
        Private Shared ReadOnly CursorInstance As Cursor = New Cursor(My.Resources.crossMedium.GetHicon())
        Public Overrides ReadOnly Property Cursor As Cursor = CursorInstance
        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Ellipse

        Private ReadOnly _pen As New Pen(Color.Black, 1) With {.DashStyle = DashStyle.Solid}
        Private _rct As New Rectangle()

        Public Overrides Sub RenderFinalImage(ByRef rawImage As Image, ByVal sender As PaintPanel)
            _rct.X = Math.Min(InternalEndCoords.X, InternalBeginCoords.X)
            _rct.Y = Math.Min(InternalEndCoords.Y, InternalBeginCoords.Y)
            _rct.Width = CInt(Math.Max(Math.Abs(InternalBeginCoords.X - InternalEndCoords.X), _pen.Width))
            _rct.Height = CInt(Math.Max(Math.Abs(InternalBeginCoords.Y - InternalEndCoords.Y), _pen.Width))

            _pen.Width = sender.EllipseWidth
            _pen.Color = sender.EllipseColor

            Using g As Graphics = Graphics.FromImage(rawImage)
                If sender.UseBoxInsteadOfCirlce Then
                    g.DrawRectangle(_pen, _rct)
                Else
                    g.SmoothingMode = SmoothingMode.AntiAlias
                    g.DrawEllipse(_pen, _rct)
                End If
            End Using
        End Sub

        Public Overrides Sub RenderPreview(ByVal rawImage As Image, ByVal g As Graphics, ByVal sender As PaintPanel)
            _rct.X = Math.Min(InternalEndCoords.X, InternalBeginCoords.X)
            _rct.Y = Math.Min(InternalEndCoords.Y, InternalBeginCoords.Y)
            _rct.Width = CInt(Math.Max(Math.Abs(InternalBeginCoords.X - InternalEndCoords.X), _pen.Width))
            _rct.Height = CInt(Math.Max(Math.Abs(InternalBeginCoords.Y - InternalEndCoords.Y), _pen.Width))

            _pen.Width = sender.EllipseWidth
            _pen.Color = sender.EllipseColor

            If sender.UseBoxInsteadOfCirlce Then
                g.DrawRectangle(_pen, _rct)
            Else
                g.SmoothingMode = SmoothingMode.AntiAlias
                g.DrawEllipse(_pen, _rct)
            End If
        End Sub
    End Class
End Namespace
