Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Scale
        Implements ITool(Of ToolSettingsBase)

        Public ReadOnly Property Cursor As Cursor = Cursors.Arrow Implements ITool(Of ToolSettingsBase).Cursor
        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Scale Implements ITool(Of ToolSettingsBase).ToolType
        Public ReadOnly Property SettingsControl As ISettingsControl(Of ToolSettingsBase) = Nothing Implements ITool(Of ToolSettingsBase).SettingsControl
        Public Property BeginCoordinates As Point Implements ITool(Of ToolSettingsBase).BeginCoordinates
        Public Property EndCoordinates As Point Implements ITool(Of ToolSettingsBase).EndCoordinates
        Public Sub LoadInitialSettings() Implements ITool(Of ToolSettingsBase).LoadInitialSettings
            ' Nothing to do here
        End Sub
        Public Sub PersistSettings() Implements ITool(Of ToolSettingsBase).PersistSettings
            ' Nothing to do here
        End Sub
        Public Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderFinalImage

            Using s As New ScaleWindow(rawImage)

                If s.ShowDialog(sender) <> DialogResult.OK Then
                    sender.CurrentTool = PaintPanel.ShotEditorTool.None
                    Return
                End If

                Dim widh = s.WidthBoxV
                Dim hei = s.HeightBoxV

                Dim unit As ScaleWindow.ScaleUnit = s.CurrentScaleUnit

                Dim newWidth As Integer
                Dim newHeight As Integer

                ' This is wrong and may fail if there was no cursor position
                ' We ignore this for now, since we're not caring about legacy stuff
                Dim newCursorCoods As Point = sender.Screenshot.CursorPosition.OnImage

                Dim newCursorWidth As Integer = My.Resources.windowsCursorMedium.Width
                Dim newCursorHeight As Integer = My.Resources.windowsCursorMedium.Height

                If unit = ScaleWindow.ScaleUnit.Percent Then
                    newWidth = CInt(rawImage.Width * (widh / 100))
                    newHeight = CInt(rawImage.Height * (hei / 100))
                    newCursorCoods.X = CInt(newCursorCoods.X * (hei / 100))
                    newCursorCoods.Y = CInt(newCursorCoods.Y * (widh / 100))
                    newCursorWidth = CInt(newCursorWidth * (widh / 100))
                    newCursorHeight = CInt(newCursorHeight * (hei / 100))
                Else
                    newWidth = CInt(widh)
                    newHeight = CInt(hei)
                End If

                Dim newRawImage As New Bitmap(newWidth, newHeight)
                Using g As Graphics = Graphics.FromImage(newRawImage)
                    g.DrawImage(rawImage, 0, 0, newWidth, newHeight)
                    If sender.DrawCursor Then
                        g.DrawImage(My.Resources.windowsCursorMedium, newCursorCoods.X, newCursorCoods.Y, newCursorWidth, newCursorHeight)
                    End If
                End Using

                rawImage = newRawImage
                sender.RawBox.Image = newRawImage
            End Using
        End Sub

        Public Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderPreview
            ' Nothing to do here
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
