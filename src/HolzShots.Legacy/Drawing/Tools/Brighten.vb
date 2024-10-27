Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Brighten
        Implements ITool(Of ToolSettingsBase)

        Private Shared ReadOnly CursorInstance As New Cursor(My.Resources.crossMedium.GetHicon())
        Public ReadOnly Property Cursor As Cursor = CursorInstance Implements ITool(Of ToolSettingsBase).Cursor
        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Brighten Implements ITool(Of ToolSettingsBase).ToolType
        Public ReadOnly Property SettingsControl As ISettingsControl(Of ToolSettingsBase) = Nothing Implements ITool(Of ToolSettingsBase).SettingsControl

        Public Property BeginCoordinates As Point Implements ITool(Of ToolSettingsBase).BeginCoordinates
        Public Property EndCoordinates As Point Implements ITool(Of ToolSettingsBase).EndCoordinates

        Public Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderFinalImage
            Dim rct As New Rectangle(
                Math.Min(BeginCoordinates.X, EndCoordinates.X),
                Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
                Math.Abs(BeginCoordinates.X - EndCoordinates.X),
                Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
            )

            Using gr = Graphics.FromImage(rawImage)
                gr.FillRectangle(New SolidBrush(sender.BrightenColor), rct)
            End Using
        End Sub

        Public Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderPreview
            Dim rct As New Rectangle(
                Math.Min(BeginCoordinates.X, EndCoordinates.X),
                Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
                Math.Abs(BeginCoordinates.X - EndCoordinates.X),
                Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
            )

            Using s As New SolidBrush(sender.BrightenColor)
                g.FillRectangle(s, rct)
            End Using
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
