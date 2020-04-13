Imports System.Drawing.Drawing2D
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Crop
        Inherits Tool
        Implements IDisposable

        Private ReadOnly _alphaBrush As New SolidBrush(Color.FromArgb(128, 0, 0, 0))
        Private ReadOnly _redCornerPen As Pen = New Pen(Color.FromArgb(255, 255, 0, 0)) With {.DashStyle = DashStyle.Dash}
        Private _rct As New Rectangle

        Private Shared ReadOnly CursorInstance As Cursor = New Cursor(My.Resources.cropperCursor.Handle)
        Public Overrides ReadOnly Property Cursor As Cursor = CursorInstance

        Public Overrides Sub RenderFinalImage(ByRef rawImage As Image, ByVal sender As PaintPanel)

            _rct.X = If(BeginCoords.X > EndCoords.X, EndCoords.X, BeginCoords.X)
            _rct.Y = If(BeginCoords.Y > EndCoords.Y, EndCoords.Y, BeginCoords.Y)
            _rct.Width = Math.Abs(BeginCoords.X - EndCoords.X)
            _rct.Height = Math.Abs(BeginCoords.Y - EndCoords.Y)

            If _rct.X + _rct.Width > rawImage.Width Then
                _rct.Width = rawImage.Width - _rct.X
                If _rct.Width < 0 Then _rct.Width = Math.Abs(_rct.Width)
                If _rct.Width = 0 Then _rct.Width = 1
            End If
            If _rct.Y + _rct.Height > rawImage.Height Then
                _rct.Height = rawImage.Height - _rct.Y
                If _rct.Height < 0 Then _rct.Height = Math.Abs(_rct.Height)
                If _rct.Height = 0 Then _rct.Height = 1
            End If

            If _rct.X < 0 Then
                _rct.Width += _rct.X
                _rct.X = 0
            End If
            If _rct.Y < 0 Then
                _rct.Height += _rct.Y
                _rct.Y = 0
            End If

            Dim newBmp As New Bitmap(_rct.Width, _rct.Height)

            Using g As Graphics = Graphics.FromImage(newBmp)
                g.InterpolationMode = InterpolationMode.HighQualityBilinear
                g.DrawImage(rawImage, New Rectangle(0, 0, newBmp.Width, newBmp.Height), _rct, GraphicsUnit.Pixel)
            End Using


            rawImage = newBmp
            sender.RawBox.Image = newBmp
            GC.Collect()
        End Sub

        Public Overrides Sub RenderPreview(ByVal rawImage As Image, ByVal g As Graphics, ByVal sender As PaintPanel)

            _rct.X = Math.Min(EndCoords.X, BeginCoords.X)
            'If(BeginCoords.X > SecondCoords.X, SecondCoords.X, BeginCoords.X)
            _rct.Y = Math.Min(EndCoords.Y, BeginCoords.Y)
            'If(BeginCoords.Y > SecondCoords.Y, SecondCoords.Y, BeginCoords.Y)
            _rct.Width = Math.Abs(BeginCoords.X - EndCoords.X)
            _rct.Height = Math.Abs(BeginCoords.Y - EndCoords.Y)

            If _rct.X + _rct.Width > rawImage.Width Then
                _rct.Width = rawImage.Width - _rct.X
                If _rct.Width < 0 Then _rct.Width = Math.Abs(_rct.Width)
                If _rct.Width = 0 Then _rct.Width = 1
            End If
            If _rct.Y + _rct.Height > rawImage.Height Then
                _rct.Height = rawImage.Height - _rct.Y
                If _rct.Height < 0 Then _rct.Height = Math.Abs(_rct.Height)
                If _rct.Height = 0 Then _rct.Height = 1
            End If

            If _rct.X < 0 Then
                _rct.Width += _rct.X
                _rct.X = 0
            End If
            If _rct.Y < 0 Then
                _rct.Height += _rct.Y
                _rct.Y = 0
            End If


            g.FillRectangle(_alphaBrush, rawImage.GetBounds(GraphicsUnit.Pixel))

            g.DrawImage(rawImage, _rct, _rct, GraphicsUnit.Pixel)

            g.DrawRectangle(_redCornerPen, _rct)
        End Sub

        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Crop

        Public Sub Dispose() Implements IDisposable.Dispose
            _alphaBrush.Dispose()
            _redCornerPen.Dispose()
        End Sub
    End Class
End Namespace
