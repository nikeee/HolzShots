Imports System.Drawing.Drawing2D
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Pixelate
        Inherits Tool

        ReadOnly _thePen As New Pen(Color.Red) With {.DashStyle = DashStyle.Dash}
        Private Shared ReadOnly CursorInstance As Cursor = New Cursor(My.Resources.crossMedium.GetHicon())
        Public Overrides ReadOnly Property Cursor As Cursor = CursorInstance
        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Blur

        Public Overrides Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel)
            If rawImage IsNot Nothing Then
                Dim rawSrcRect As New Rectangle(If(BeginCoords.X > EndCoords.X, EndCoords.X, BeginCoords.X),
                                                If(BeginCoords.Y > EndCoords.Y, EndCoords.Y, BeginCoords.Y),
                                                If(BeginCoords.X > EndCoords.X, BeginCoords.X - EndCoords.X, EndCoords.X - BeginCoords.X),
                                                If(BeginCoords.Y > EndCoords.Y, BeginCoords.Y - EndCoords.Y, EndCoords.Y - BeginCoords.Y))
                If rawSrcRect.X < 0 Then
                    rawSrcRect.Width += rawSrcRect.X
                    rawSrcRect.X = 0
                End If
                If rawSrcRect.Y < 0 Then
                    rawSrcRect.Height += rawSrcRect.Y
                    rawSrcRect.Y = 0
                End If

                If rawSrcRect.Width = 0 OrElse rawSrcRect.Height = 0 Then Exit Sub

                Using g As Graphics = Graphics.FromImage(rawImage)

                    Using img As Bitmap = BlurImage(rawImage, sender.BlurFactor, rawSrcRect)
                        If img Is Nothing Then Exit Sub

                        g.FillRectangle(Brushes.White, rawSrcRect)
                        g.CompositingMode = CompositingMode.SourceOver
                        g.DrawImage(img, rawSrcRect)
                    End Using
                End Using

            End If
        End Sub

        Public Overrides Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel)
            If rawImage IsNot Nothing Then
                Dim rawSrcRect As New Rectangle(If(BeginCoords.X > EndCoords.X, EndCoords.X, BeginCoords.X),
                                                If(BeginCoords.Y > EndCoords.Y, EndCoords.Y, BeginCoords.Y),
                                                If(BeginCoords.X > EndCoords.X, BeginCoords.X - EndCoords.X, EndCoords.X - BeginCoords.X),
                                                If(BeginCoords.Y > EndCoords.Y, BeginCoords.Y - EndCoords.Y, EndCoords.Y - BeginCoords.Y))

                If rawSrcRect.X < 0 Then
                    rawSrcRect.Width += rawSrcRect.X
                    rawSrcRect.X = 0
                End If
                If rawSrcRect.Y < 0 Then
                    rawSrcRect.Height += rawSrcRect.Y
                    rawSrcRect.Y = 0
                End If


                If rawSrcRect.Width = 0 OrElse rawSrcRect.Height = 0 Then Exit Sub

                Using img As Bitmap = BlurImage(rawImage, sender.BlurFactor, rawSrcRect)
                    If img Is Nothing Then Exit Sub

                    g.CompositingMode = CompositingMode.SourceOver
                    g.FillRectangle(Brushes.White, rawSrcRect)
                    g.DrawImage(img, rawSrcRect)
                End Using
                g.DrawRectangle(_thePen, rawSrcRect)
            End If
        End Sub

        Private Shared Function BlurImage(img As Image, factor As Integer, rawSrcRect As Rectangle) As Bitmap
            Dim width As Integer = CInt(Math.Round(Math.Ceiling(rawSrcRect.Width / factor)))
            Dim height As Integer = CInt(Math.Round(Math.Ceiling(rawSrcRect.Height / factor)))

            If width <= 0 OrElse height <= 0 Then Return Nothing

            Using smallBitmap As New Bitmap(width, height)
                Using g As Graphics = Graphics.FromImage(smallBitmap)
                    g.CompositingMode = CompositingMode.SourceOver
                    g.InterpolationMode = InterpolationMode.HighQualityBilinear
                    g.DrawImage(img, New Rectangle(0, 0, smallBitmap.Width, smallBitmap.Height), rawSrcRect, GraphicsUnit.Pixel)
                End Using
                Using secondBitmap As New Bitmap(rawSrcRect.Width, rawSrcRect.Height)
                    Using g As Graphics = Graphics.FromImage(secondBitmap)
                        g.CompositingMode = CompositingMode.SourceOver
                        g.InterpolationMode = InterpolationMode.HighQualityBilinear
                        g.DrawImage(smallBitmap,
                                    New Rectangle(0, 0, rawSrcRect.Width + factor, rawSrcRect.Height + factor),
                                    New Rectangle(0, 0, smallBitmap.Width, smallBitmap.Height), GraphicsUnit.Pixel)
                    End Using
                    Return DirectCast(secondBitmap.Clone(), Bitmap)
                End Using
            End Using
        End Function
    End Class
End Namespace
