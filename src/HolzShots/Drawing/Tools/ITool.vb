Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend MustInherit Class Tool
        MustOverride ReadOnly Property ToolType As PaintPanel.Tools

        Overridable ReadOnly Property Cursor As Cursor = Cursors.Default

        Protected InternalBeginCoords As Point
        Overridable Property BeginCoords As Point
            Get
                Return InternalBeginCoords
            End Get
            Set(ByVal value As Point)
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
            Set(ByVal value As Point)
                If value <> InternalEndCoords Then
                    InternalEndCoords = value
                End If
            End Set
        End Property

        Overridable Sub RenderFinalImage(ByRef rawImage As Image, ByVal sender As PaintPanel)
        End Sub

        Overridable Sub RenderPreview(ByVal rawImage As Image, ByVal g As Graphics, ByVal sender As PaintPanel)
        End Sub

        Overridable Sub MouseOnlyMoved(ByVal rawImage As Image, ByRef currentCursor As Cursor, ByVal e As MouseEventArgs)
        End Sub

        Overridable Sub MouseClicked(ByVal rawImage As Image, ByVal e As Point, ByRef currentCursor As Cursor, ByVal trigger As Control)
        End Sub

    End Class
End Namespace
