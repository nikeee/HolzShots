Namespace ScreenshotRelated.Selection
    Friend Interface ISelectionDecoration

        Property InvalidationRectangle As Rectangle

        WriteOnly Property Selection As Rectangle
        WriteOnly Property WholeScreen As Rectangle

        Sub DrawSelection(ByRef g As Graphics, ByRef selectionBorderPen As Pen)

    End Interface
End Namespace
