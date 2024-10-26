Imports System.Drawing.Drawing2D
Imports System.Numerics
Imports System.Runtime.CompilerServices
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Arrow
        Inherits Tool

        Private _arrowFirstPoint As Vector2
        Private _arrowSecondPoint As Vector2
        Private ReadOnly _arrowDrawPoints(3) As Point
        Const ArrowRotationConstant As Single = 2.2 * Math.PI / 1.2
        Private _arrowBtwn2 As Vector2

        Public Overrides Property BeginCoordinates As Point
            Get
                Return InternalBeginCoordinates
            End Get
            Set(value As Point)
                If value <> InternalBeginCoordinates Then
                    InternalBeginCoordinates = value
                    _arrowFirstPoint = New Vector2(value.X, value.Y)
                End If
            End Set
        End Property

        Public Overrides Property EndCoordinates As Point
            Get
                Return InternalEndCoordinates
            End Get
            Set(value As Point)
                If value <> InternalEndCoordinates Then
                    InternalEndCoordinates = value
                    _arrowSecondPoint = New Vector2(value.X, value.Y)
                End If
            End Set
        End Property

        Private Shared ReadOnly TheCursor As New Cursor(My.Resources.crossMedium.GetHicon())
        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Arrow
        Public Overrides ReadOnly Property Cursor As Cursor = TheCursor

        Public Overrides Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel)
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

        Public Overrides Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel)

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
    End Class

    Module Vector2Ex
        <Extension>
        Public Function Rotate(vector As Vector2, angle As Single) As Vector2
            Return New Vector2(CSng(Math.Cos(angle) * vector.X - Math.Sin(angle) * vector.Y), CSng(Math.Sin(angle) * vector.X + Math.Cos(angle) * vector.Y))
        End Function

        <Extension>
        Public Function ToPoint2D(vector As Vector2) As Point
            Return New Point(CInt(vector.X), CInt(vector.Y))
        End Function
    End Module
End Namespace
