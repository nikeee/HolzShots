Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend MustInherit Class Tool
        MustOverride ReadOnly Property ToolType As PaintPanel.ShotEditorTool

        Overridable ReadOnly Property Cursor As Cursor = Cursors.Default

        Protected InternalBeginCoordinates As Point
        Overridable Property BeginCoordinates As Point
            Get
                Return InternalBeginCoordinates
            End Get
            Set(value As Point)
                If value <> InternalBeginCoordinates Then
                    InternalBeginCoordinates = value
                End If
            End Set
        End Property

        Protected InternalEndCoordinates As Point
        Overridable Property EndCoordinates As Point
            Get
                Return InternalEndCoordinates
            End Get
            Set(value As Point)
                If value <> InternalEndCoordinates Then
                    InternalEndCoordinates = value
                End If
            End Set
        End Property

        Overridable Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel)
        End Sub

        Overridable Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel)
        End Sub

        Overridable Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs)
        End Sub

        Overridable Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control)
        End Sub

    End Class
End Namespace
