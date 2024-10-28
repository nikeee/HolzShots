Imports System.Drawing.Drawing2D
Imports System.Numerics
Imports HolzShots.Drawing.Tools.UI

Namespace Drawing.Tools
    Friend Class Crop
        Implements ITool(Of ToolSettingsBase)

        Private ReadOnly _alphaBrush As New SolidBrush(Color.FromArgb(128, 0, 0, 0))
        Private ReadOnly _redCornerPen As New Pen(Color.FromArgb(255, 255, 0, 0)) With {.DashStyle = DashStyle.Dash}

        Private Shared ReadOnly CursorInstance As New Cursor(My.Resources.cropperCursor.Handle)
        Public ReadOnly Property Cursor As Cursor = CursorInstance Implements ITool(Of ToolSettingsBase).Cursor
        Public ReadOnly Property ToolType As ShotEditorTool = ShotEditorTool.Crop Implements ITool(Of ToolSettingsBase).ToolType
        Public ReadOnly Property SettingsControl As ISettingsControl(Of ToolSettingsBase) = Nothing Implements ITool(Of ToolSettingsBase).SettingsControl

        Public Property BeginCoordinates As Vector2 Implements ITool(Of ToolSettingsBase).BeginCoordinates
        Public Property EndCoordinates As Vector2 Implements ITool(Of ToolSettingsBase).EndCoordinates

        Public Sub LoadInitialSettings() Implements ITool(Of ToolSettingsBase).LoadInitialSettings
            ' Nothing to do here
        End Sub
        Public Sub PersistSettings() Implements ITool(Of ToolSettingsBase).PersistSettings
            ' Nothing to do here
        End Sub

        Public Sub RenderFinalImage(ByRef rawImage As Image) Implements ITool(Of ToolSettingsBase).RenderFinalImage
            Dim rect = Rectangle.Round(New RectangleF(
                Math.Min(EndCoordinates.X, BeginCoordinates.X),
                Math.Min(EndCoordinates.Y, BeginCoordinates.Y),
                Math.Abs(BeginCoordinates.X - EndCoordinates.X),
                Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
            ))

            If rect.X + rect.Width > rawImage.Width Then
                rect.Width = Math.Max(1, Math.Abs(rawImage.Width - rect.X))
            End If
            If rect.Y + rect.Height > rawImage.Height Then
                rect.Height = Math.Max(1, Math.Abs(rawImage.Height - rect.Y))
            End If

            If rect.X < 0 Then
                rect.Width += rect.X
                rect.X = 0
            End If
            If rect.Y < 0 Then
                rect.Height += rect.Y
                rect.Y = 0
            End If

            Dim newBmp As New Bitmap(rect.Width, rect.Height)

            Using g As Graphics = Graphics.FromImage(newBmp)
                g.InterpolationMode = InterpolationMode.HighQualityBilinear
                g.DrawImage(rawImage, New Rectangle(0, 0, newBmp.Width, newBmp.Height), rect, GraphicsUnit.Pixel)
            End Using

            rawImage = newBmp
        End Sub

        Public Sub RenderPreview(rawImage As Image, g As Graphics) Implements ITool(Of ToolSettingsBase).RenderPreview
            Dim rect = Rectangle.Round(New RectangleF(
                Math.Min(EndCoordinates.X, BeginCoordinates.X),
                Math.Min(EndCoordinates.Y, BeginCoordinates.Y),
                Math.Abs(BeginCoordinates.X - EndCoordinates.X),
                Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
            ))

            If rect.X + rect.Width > rawImage.Width Then
                rect.Width = rawImage.Width - rect.X
                rect.Width = Math.Max(1, Math.Abs(rect.Width))
            End If
            If rect.Y + rect.Height > rawImage.Height Then
                rect.Height = rawImage.Height - rect.Y
                rect.Height = Math.Max(1, Math.Abs(rect.Height))
            End If

            If rect.X < 0 Then
                rect.Width += rect.X
                rect.X = 0
            End If
            If rect.Y < 0 Then
                rect.Height += rect.Y
                rect.Y = 0
            End If

            g.FillRectangle(_alphaBrush, rawImage.GetBounds(GraphicsUnit.Pixel))
            g.DrawImage(rawImage, rect, rect, GraphicsUnit.Pixel)
            g.DrawRectangle(_redCornerPen, rect)
        End Sub

        Protected Sub Dispose() Implements ITool(Of ToolSettingsBase).Dispose
            _alphaBrush.Dispose()
            _redCornerPen.Dispose()
        End Sub
        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of ToolSettingsBase).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Vector2, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of ToolSettingsBase).MouseClicked
            ' Nothing to do here
        End Sub
    End Class
End Namespace
