Namespace ScreenshotRelated.Selection
    Friend MustInherit Class QuickInfoDecoration
        Implements ISelectionDecoration

        Protected ReadOnly QuickInfoTextBrushLight As Brush = Brushes.White
        Protected ReadOnly QuickInfoTextBrushDark As Brush = Brushes.Black

        Protected Shared ReadOnly QuickInfoBrush As Brush = New SolidBrush(Color.FromArgb(70, 255, 255, 255))
        Protected Shared ReadOnly QuickInfoTextFont As New Font("Consolas", 9, FontStyle.Regular, GraphicsUnit.Point)

        Protected CurrentSelection As Rectangle

        Public Property InvalidationRectangle As Rectangle Implements ISelectionDecoration.InvalidationRectangle

        Public WriteOnly Property Selection As Rectangle Implements ISelectionDecoration.Selection
            Set(ByVal value As Rectangle)
                CurrentSelection = value
                OnUpdateCoordinates()
            End Set
        End Property

        Protected CurrentWholeScreen As Rectangle
        Public WriteOnly Property WholeScreen As Rectangle Implements ISelectionDecoration.WholeScreen
            Set(ByVal value As Rectangle)
                CurrentWholeScreen = value
            End Set
        End Property

        Public MustOverride Sub DrawSelection(ByRef g As Graphics, ByRef selectionBorderPen As Pen) Implements ISelectionDecoration.DrawSelection
        Protected MustOverride Sub OnUpdateCoordinates()

    End Class
End Namespace
