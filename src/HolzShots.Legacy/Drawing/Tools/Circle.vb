Imports System.Configuration
Imports System.Drawing.Drawing2D
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Circle
        Implements ITool(Of ToolSettingsBase)
        Public Property BeginCoordinates As Point Implements ITool(Of ToolSettingsBase).BeginCoordinates
        Public Property EndCoordinates As Point Implements ITool(Of ToolSettingsBase).EndCoordinates

        Private Shared ReadOnly CursorInstance As New Cursor(My.Resources.crossMedium.GetHicon())
        Public ReadOnly Property Cursor As Cursor = CursorInstance Implements ITool(Of ToolSettingsBase).Cursor
        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Ellipse Implements ITool(Of ToolSettingsBase).ToolType
        Public ReadOnly Property SettingsControl As ISettingsControl(Of ToolSettingsBase) = Nothing Implements ITool(Of ToolSettingsBase).SettingsControl

        Private ReadOnly _pen As New Pen(Color.Black, 1) With {.DashStyle = DashStyle.Solid}
        Private _rct As New Rectangle()

        Public Sub RenderFinalImage(ByRef rawImage As Image, ByVal sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderFinalImage
            _rct.X = Math.Min(EndCoordinates.X, BeginCoordinates.X)
            _rct.Y = Math.Min(EndCoordinates.Y, BeginCoordinates.Y)
            _rct.Width = CInt(Math.Max(Math.Abs(BeginCoordinates.X - EndCoordinates.X), _pen.Width))
            _rct.Height = CInt(Math.Max(Math.Abs(BeginCoordinates.Y - EndCoordinates.Y), _pen.Width))

            _pen.Width = sender.EllipseWidth
            _pen.Color = sender.EllipseColor

            Using g As Graphics = Graphics.FromImage(rawImage)
                If sender.UseBoxInsteadOfCircle Then
                    g.DrawRectangle(_pen, _rct)
                Else
                    g.SmoothingMode = SmoothingMode.AntiAlias
                    g.DrawEllipse(_pen, _rct)
                End If
            End Using
        End Sub

        Public Sub RenderPreview(ByVal rawImage As Image, ByVal g As Graphics, ByVal sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderPreview
            _rct.X = Math.Min(EndCoordinates.X, BeginCoordinates.X)
            _rct.Y = Math.Min(EndCoordinates.Y, BeginCoordinates.Y)
            _rct.Width = CInt(Math.Max(Math.Abs(BeginCoordinates.X - EndCoordinates.X), _pen.Width))
            _rct.Height = CInt(Math.Max(Math.Abs(BeginCoordinates.Y - EndCoordinates.Y), _pen.Width))

            _pen.Width = sender.EllipseWidth
            _pen.Color = sender.EllipseColor

            If sender.UseBoxInsteadOfCircle Then
                g.DrawRectangle(_pen, _rct)
            Else
                g.SmoothingMode = SmoothingMode.AntiAlias
                g.DrawEllipse(_pen, _rct)
            End If
        End Sub

        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of ToolSettingsBase).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of ToolSettingsBase).MouseClicked
            ' Nothing to do here
        End Sub
        Public Sub Dispose() Implements ITool(Of ToolSettingsBase).Dispose
        End Sub
    End Class
End Namespace
