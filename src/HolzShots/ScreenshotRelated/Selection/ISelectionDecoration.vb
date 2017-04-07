Namespace ScreenshotRelated.Selection
    Friend Interface ISelectionDecoration

        Property InvalidationRectangle As Rectangle

        WriteOnly Property Selection As Rectangle
        WriteOnly Property WholeScreen As Rectangle

        Sub DrawSelection(ByRef g As Graphics, ByRef selectionBorderPen As Pen)

    End Interface

    <Serializable>
    Friend Enum SelectionDecorations
        Nomination1 = 0
        Nomination2 = 1
        Nomination3 = 2
    End Enum
End Namespace
