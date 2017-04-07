Imports System.Drawing.Drawing2D

Namespace ScreenshotRelated.Selection
    Friend Class MagnifierDecoration

        Private _magnifierView As New Rectangle
        Public PreviousMagnifierLocation As New Rectangle
        Public CurrentInnerCorner As MagnifierCorner

        Private _drawCorner As MagnifierCorner

        Private Const MagnifierDrawDimensions As Integer = 80
        Private Const MagnifierViewPortion As Integer = 40
        Private Const HalfMagnifierViewPortion As Integer = MagnifierViewPortion \ 2

        Private Shared ReadOnly GreyBrush As SolidBrush = New SolidBrush(Color.FromArgb(127, 0, 0, 0))

        Public Sub Draw(ByRef g As Graphics, wholeScreen As Image, magnifierBorderPen As Pen, selectionBorderPen As Pen)
            Using zoomed As New Bitmap(MagnifierViewPortion, MagnifierViewPortion)
                Using ge As Graphics = Graphics.FromImage(zoomed)
                    ge.InterpolationMode = InterpolationMode.NearestNeighbor
                    ge.DrawImage(wholeScreen, New Rectangle(0, 0, MagnifierViewPortion, MagnifierViewPortion), _magnifierView, GraphicsUnit.Pixel)

                    If CurrentInnerCorner <> MagnifierCorner.TopLeft Then
                        ge.FillRectangle(GreyBrush, New Rectangle(0, 0, HalfMagnifierViewPortion, HalfMagnifierViewPortion))
                    End If
                    If CurrentInnerCorner <> MagnifierCorner.TopRight Then
                        ge.FillRectangle(GreyBrush, New Rectangle(HalfMagnifierViewPortion, 0, HalfMagnifierViewPortion, HalfMagnifierViewPortion))
                    End If
                    If CurrentInnerCorner <> MagnifierCorner.BottomLeft Then
                        ge.FillRectangle(GreyBrush, New Rectangle(0, HalfMagnifierViewPortion, HalfMagnifierViewPortion, HalfMagnifierViewPortion))
                    End If
                    If CurrentInnerCorner <> MagnifierCorner.BottomRight Then
                        ge.FillRectangle(GreyBrush, New Rectangle(HalfMagnifierViewPortion, HalfMagnifierViewPortion, HalfMagnifierViewPortion, HalfMagnifierViewPortion))
                    End If
                    'ge.DrawLine(magnifierBorderPen, 0, MagnifierViewPortion \ 2, MagnifierViewPortion, MagnifierViewPortion \ 2)
                    'ge.DrawLine(magnifierBorderPen, MagnifierViewPortion \ 2, 0, MagnifierViewPortion \ 2, MagnifierViewPortion)
                End Using
                g.DrawImage(zoomed, PreviousMagnifierLocation)
                g.DrawRectangle(selectionBorderPen, PreviousMagnifierLocation.X, PreviousMagnifierLocation.Y, PreviousMagnifierLocation.Width - 1, PreviousMagnifierLocation.Height - 1)
            End Using
        End Sub

        Public Sub Update(currentSelection As Rectangle, drawingFrame As Rectangle)
            Dim cLoc = Cursor.Position

            Const mouseOffset As Integer = 10

            Dim rightFromMouse = Not cLoc.X + MagnifierDrawDimensions + mouseOffset > drawingFrame.Width
            Dim lowerFromMouse = Not cLoc.Y + MagnifierDrawDimensions + mouseOffset > drawingFrame.Height

            If lowerFromMouse Then
                If rightFromMouse Then
                    _drawCorner = MagnifierCorner.BottomRight
                Else
                    _drawCorner = MagnifierCorner.BottomLeft
                End If
            Else
                If rightFromMouse Then
                    _drawCorner = MagnifierCorner.TopRight
                Else
                    _drawCorner = MagnifierCorner.TopLeft
                End If
            End If

            PreviousMagnifierLocation.Width = MagnifierDrawDimensions
            PreviousMagnifierLocation.Height = MagnifierDrawDimensions

            Select Case _drawCorner
                Case MagnifierCorner.BottomRight
                    PreviousMagnifierLocation.X = cLoc.X + mouseOffset
                    PreviousMagnifierLocation.Y = cLoc.Y + mouseOffset
                Case MagnifierCorner.BottomLeft
                    PreviousMagnifierLocation.X = cLoc.X - mouseOffset - MagnifierDrawDimensions
                    PreviousMagnifierLocation.Y = cLoc.Y + mouseOffset

                Case MagnifierCorner.TopLeft
                    PreviousMagnifierLocation.X = cLoc.X - mouseOffset - MagnifierDrawDimensions
                    PreviousMagnifierLocation.Y = cLoc.Y - mouseOffset - MagnifierDrawDimensions
                Case MagnifierCorner.TopRight
                    PreviousMagnifierLocation.X = cLoc.X + mouseOffset
                    PreviousMagnifierLocation.Y = cLoc.Y - mouseOffset - MagnifierDrawDimensions
            End Select

            _magnifierView.X = cLoc.X - MagnifierViewPortion \ 2
            _magnifierView.Y = cLoc.Y - MagnifierViewPortion \ 2
            _magnifierView.Width = MagnifierViewPortion
            _magnifierView.Height = MagnifierViewPortion

            If cLoc.X = currentSelection.X Then
                If cLoc.Y = currentSelection.Y Then
                    CurrentInnerCorner = MagnifierCorner.BottomRight
                Else
                    CurrentInnerCorner = MagnifierCorner.TopRight
                End If
            Else
                If cLoc.Y = currentSelection.Y Then
                    CurrentInnerCorner = MagnifierCorner.BottomLeft
                Else
                    CurrentInnerCorner = MagnifierCorner.TopLeft
                End If
            End If
        End Sub

        Public Enum MagnifierCorner
            TopLeft
            TopRight
            BottomLeft
            BottomRight
        End Enum
    End Class
End Namespace
