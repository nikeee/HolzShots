Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend MustInherit Class Tool
        MustOverride ReadOnly Property ToolType As PaintPanel.ShotEditorTool

        Overridable ReadOnly Property Cursor As Cursor = Cursors.Default

        Protected InternalBeginCoords As Point
        Overridable Property BeginCoords As Point
            Get
                Return InternalBeginCoords
            End Get
            Set(value As Point)
                If value <> InternalBeginCoords Then
                    InternalBeginCoords = value
                End If
            End Set
        End Property

        Protected InternalEndCoords As Point
        Overridable Property EndCoords As Point
            Get
                Return InternalEndCoords
            End Get
            Set(value As Point)
                If value <> InternalEndCoords Then
                    InternalEndCoords = value
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
