Imports System.Drawing.Drawing2D
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Crop
        Implements ITool(Of ToolSettingsBase)

        Private ReadOnly _alphaBrush As New SolidBrush(Color.FromArgb(128, 0, 0, 0))
        Private ReadOnly _redCornerPen As New Pen(Color.FromArgb(255, 255, 0, 0)) With {.DashStyle = DashStyle.Dash}
        Private _rct As New Rectangle

        Private Shared ReadOnly CursorInstance As New Cursor(My.Resources.cropperCursor.Handle)
        Public ReadOnly Property Cursor As Cursor = CursorInstance Implements ITool(Of ToolSettingsBase).Cursor
        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Crop Implements ITool(Of ToolSettingsBase).ToolType
        Public ReadOnly Property SettingsControl As ISettingsControl(Of ToolSettingsBase) = Nothing Implements ITool(Of ToolSettingsBase).SettingsControl

        Public Property BeginCoordinates As Point Implements ITool(Of ToolSettingsBase).BeginCoordinates
        Public Property EndCoordinates As Point Implements ITool(Of ToolSettingsBase).EndCoordinates

        Public Sub LoadInitialSettings() Implements ITool(Of ToolSettingsBase).LoadInitialSettings
            ' Nothing to do here
        End Sub
        Public Sub PersistSettings() Implements ITool(Of ToolSettingsBase).PersistSettings
            ' Nothing to do here
        End Sub

        Public Sub RenderFinalImage(ByRef rawImage As Image, ByVal sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderFinalImage

            _rct.X = Math.Min(EndCoordinates.X, BeginCoordinates.X)
            _rct.Y = Math.Min(EndCoordinates.Y, BeginCoordinates.Y)
            _rct.Width = Math.Abs(BeginCoordinates.X - EndCoordinates.X)
            _rct.Height = Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)

            If _rct.X + _rct.Width > rawImage.Width Then
                _rct.Width = rawImage.Width - _rct.X
                _rct.Width = Math.Max(1, Math.Abs(_rct.Width))
            End If
            If _rct.Y + _rct.Height > rawImage.Height Then
                _rct.Height = rawImage.Height - _rct.Y
                _rct.Height = Math.Max(1, Math.Abs(_rct.Height))
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

        Public Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderPreview

            _rct.X = Math.Min(EndCoordinates.X, BeginCoordinates.X)
            _rct.Y = Math.Min(EndCoordinates.Y, BeginCoordinates.Y)
            _rct.Width = Math.Abs(BeginCoordinates.X - EndCoordinates.X)
            _rct.Height = Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)

            If _rct.X + _rct.Width > rawImage.Width Then
                _rct.Width = rawImage.Width - _rct.X
                _rct.Width = Math.Max(1, Math.Abs(_rct.Width))
            End If
            If _rct.Y + _rct.Height > rawImage.Height Then
                _rct.Height = rawImage.Height - _rct.Y
                _rct.Height = Math.Max(1, Math.Abs(_rct.Height))
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

        Protected Sub Dispose() Implements ITool(Of ToolSettingsBase).Dispose
            _alphaBrush.Dispose()
            _redCornerPen.Dispose()
        End Sub
        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of ToolSettingsBase).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of ToolSettingsBase).MouseClicked
            ' Nothing to do here
        End Sub
    End Class
End Namespace
