Imports System.Drawing.Drawing2D
Imports System.Numerics
Imports System.Runtime.CompilerServices
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Arrow
        Inherits Tool

        Private _arrowFirstpoint As Vector2
        Private _arrowSecondpoint As Vector2
        Private ReadOnly _arrowdrawpoints(3) As Point
        Const ArrowRotationconstant As Single = 2.2 * Math.PI / 1.2
        Private _arrowBtwn2 As Vector2

        Public Overrides Property BeginCoords As Point
            Get
                Return InternalBeginCoords
            End Get
            Set(ByVal value As Point)
                If value <> InternalBeginCoords Then
                    InternalBeginCoords = value
                    _arrowFirstpoint = New Vector2(value.X, value.Y)
                End If
            End Set
        End Property

        Public Overrides Property EndCoords As Point
            Get
                Return InternalEndCoords
            End Get
            Set(ByVal value As Point)
                If value <> InternalEndCoords Then
                    InternalEndCoords = value
                    _arrowSecondpoint = New Vector2(value.X, value.Y)
                End If
            End Set
        End Property

        Private Shared ReadOnly TheCursor As Cursor = New Cursor(My.Resources.crossMedium.GetHicon())
        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Arrow
        Public Overrides ReadOnly Property Cursor As Cursor = TheCursor

        Public Overrides Sub RenderFinalImage(ByRef rawImage As Image, ByVal sender As PaintPanel)
            If _arrowFirstpoint <> Vector2.Zero AndAlso _arrowSecondpoint <> Vector2.Zero Then
                Using g = Graphics.FromImage(rawImage)
                    g.SmoothingMode = SmoothingMode.AntiAlias

                    Dim pen As New Pen(sender.ArrowColor) With {
                        .Width = If(sender.ArrowWidth <= 0, _arrowBtwn2.Length / 90, sender.ArrowWidth),
                        .EndCap = LineCap.Triangle,
                        .StartCap = LineCap.Round
                    }

                    g.DrawLine(pen, _arrowdrawpoints(0), _arrowdrawpoints(1))
                    pen.EndCap = LineCap.Round
                    g.DrawLine(pen, _arrowdrawpoints(1), _arrowdrawpoints(2))
                    g.DrawLine(pen, _arrowdrawpoints(1), _arrowdrawpoints(3))
                End Using
            End If
        End Sub

        Public Overrides Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel)

            _arrowSecondpoint = New Vector2(EndCoords.X, EndCoords.Y)
            If _arrowFirstpoint <> Vector2.Zero AndAlso _arrowSecondpoint <> Vector2.Zero Then
                If _arrowFirstpoint <> _arrowSecondpoint Then
                    _arrowBtwn2 = Vector2Ex.FromTwoVectors(_arrowFirstpoint, _arrowSecondpoint)
                    Dim btwn = Vector2.Normalize(_arrowBtwn2) * _arrowBtwn2.Length / 5
                    Dim c = btwn.Rotate(ArrowRotationconstant) + _arrowFirstpoint
                    Dim d = btwn.Rotate(-ArrowRotationconstant) + _arrowFirstpoint
                    _arrowdrawpoints(0) = _arrowSecondpoint.ToPoint2D()
                    _arrowdrawpoints(1) = _arrowFirstpoint.ToPoint2D()
                    _arrowdrawpoints(2) = c.ToPoint2D()
                    _arrowdrawpoints(3) = d.ToPoint2D()

                End If
                If _arrowSecondpoint <> Vector2.Zero Then
                    g.SmoothingMode = SmoothingMode.AntiAlias
                    Dim pen As New Pen(sender.ArrowColor) With {
                        .Width = If(sender.ArrowWidth <= 0, _arrowBtwn2.Length / 90, sender.ArrowWidth),
                        .EndCap = LineCap.Round,
                        .StartCap = LineCap.Round
                    }
                    g.DrawLine(pen, _arrowdrawpoints(0), _arrowdrawpoints(1))
                    g.DrawLine(pen, _arrowdrawpoints(1), _arrowdrawpoints(2))
                    g.DrawLine(pen, _arrowdrawpoints(1), _arrowdrawpoints(3))
                End If
            End If
        End Sub
    End Class

    Module Vector2Ex
        Public Function FromTwoVectors(p1 As Vector2, p2 As Vector2) As Vector2
            Return New Vector2(p2.X - p1.X, p2.Y - p1.Y)
        End Function

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
