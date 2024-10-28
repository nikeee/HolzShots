Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Interface ITool(Of Out TSettings As ToolSettingsBase)
        Inherits IDisposable

        ReadOnly Property ToolType As PaintPanel.ShotEditorTool
        ReadOnly Property Cursor As Cursor
        ReadOnly Property SettingsControl As ISettingsControl(Of TSettings)

        Property BeginCoordinates As Point
        Property EndCoordinates As Point

        Sub RenderFinalImage(ByRef rawImage As Image)
        Sub RenderPreview(rawImage As Image, g As Graphics)
        Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs)
        Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control)

        Sub LoadInitialSettings()
        Sub PersistSettings()
    End Interface
End Namespace
