Imports System.Drawing.Drawing2D
Imports System.Numerics
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Arrow
        Implements ITool(Of ToolSettingsBase)

        Private _arrowFirstPoint As Vector2
        Private _arrowSecondPoint As Vector2
        Private ReadOnly _arrowDrawPoints(3) As Point
        Const ArrowRotationConstant As Single = 2.2 * Math.PI / 1.2
        Private _arrowBtwn2 As Vector2

        Private _beginCoordinates As Point
        Public Property BeginCoordinates As Point Implements ITool(Of ToolSettingsBase).BeginCoordinates
            Get
                Return _beginCoordinates
            End Get
            Set(value As Point)
                If value <> _beginCoordinates Then
                    _beginCoordinates = value
                    _arrowFirstPoint = New Vector2(value.X, value.Y)
                End If
            End Set
        End Property

        Private _endCoordinates As Point
        Public Property EndCoordinates As Point Implements ITool(Of ToolSettingsBase).EndCoordinates
            Get
                Return _endCoordinates
            End Get
            Set(value As Point)
                If value <> _endCoordinates Then
                    _endCoordinates = value
                    _arrowSecondPoint = New Vector2(value.X, value.Y)
                End If
            End Set
        End Property

        Private Shared ReadOnly TheCursor As New Cursor(My.Resources.crossMedium.GetHicon())
        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Arrow Implements ITool(Of ToolSettingsBase).ToolType
        Public ReadOnly Property Cursor As Cursor = TheCursor Implements ITool(Of ToolSettingsBase).Cursor
        Public ReadOnly Property SettingsControl As ISettingsControl(Of ToolSettingsBase) = Nothing Implements ITool(Of ToolSettingsBase).SettingsControl

        Public Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderFinalImage
            If _arrowFirstPoint = Vector2.Zero OrElse _arrowSecondPoint = Vector2.Zero Then Return

            Using g = Graphics.FromImage(rawImage)
                g.SmoothingMode = SmoothingMode.AntiAlias

                Dim pen As New Pen(sender.ArrowColor) With {
                    .Width = If(sender.ArrowWidth <= 0, _arrowBtwn2.Length / 90, sender.ArrowWidth),
                    .EndCap = LineCap.Triangle,
                    .StartCap = LineCap.Round
                }

                g.DrawLine(pen, _arrowDrawPoints(0), _arrowDrawPoints(1))
                pen.EndCap = LineCap.Round
                g.DrawLine(pen, _arrowDrawPoints(1), _arrowDrawPoints(2))
                g.DrawLine(pen, _arrowDrawPoints(1), _arrowDrawPoints(3))
            End Using
        End Sub

        Public Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderPreview

            _arrowSecondPoint = New Vector2(EndCoordinates.X, EndCoordinates.Y)
            If _arrowFirstPoint <> Vector2.Zero AndAlso _arrowSecondPoint <> Vector2.Zero Then
                If _arrowFirstPoint <> _arrowSecondPoint Then
                    _arrowBtwn2 = _arrowSecondPoint - _arrowFirstPoint
                    Dim btwn = Vector2.Normalize(_arrowBtwn2) * _arrowBtwn2.Length / 5
                    Dim c = btwn.Rotate(ArrowRotationConstant) + _arrowFirstPoint
                    Dim d = btwn.Rotate(-ArrowRotationConstant) + _arrowFirstPoint
                    _arrowDrawPoints(0) = _arrowSecondPoint.ToPoint2D()
                    _arrowDrawPoints(1) = _arrowFirstPoint.ToPoint2D()
                    _arrowDrawPoints(2) = c.ToPoint2D()
                    _arrowDrawPoints(3) = d.ToPoint2D()

                End If
                If _arrowSecondPoint <> Vector2.Zero Then
                    g.SmoothingMode = SmoothingMode.AntiAlias
                    Dim pen As New Pen(sender.ArrowColor) With {
                        .Width = If(sender.ArrowWidth <= 0, _arrowBtwn2.Length / 90, sender.ArrowWidth),
                        .EndCap = LineCap.Round,
                        .StartCap = LineCap.Round
                    }
                    g.DrawLine(pen, _arrowDrawPoints(0), _arrowDrawPoints(1))
                    g.DrawLine(pen, _arrowDrawPoints(1), _arrowDrawPoints(2))
                    g.DrawLine(pen, _arrowDrawPoints(1), _arrowDrawPoints(3))
                End If
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
