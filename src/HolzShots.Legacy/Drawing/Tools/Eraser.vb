Imports System.Drawing.Drawing2D
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Eraser
        Inherits Tool

        Private _parent As PaintPanel

        Public Sub SetParent(p As PaintPanel)
            _parent = p
        End Sub

        Public Overrides Property BeginCoordinates As Point
            Get
                Return InternalBeginCoords
            End Get
            Set(ByVal value As Point)
                InternalBeginCoords = value
                RenderPreview(_parent.RawBox.Image, Nothing, _parent)
            End Set
        End Property

        Public Overrides ReadOnly Property Cursor As Cursor
            Get
                Dim bmp As New Bitmap(_parent.EraserDiameter + 8, _parent.EraserDiameter + 8)
                bmp.MakeTransparent()
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.SmoothingMode = SmoothingMode.AntiAlias
                    g.FillEllipse(Brushes.LightGray, 4, 4, _parent.EraserDiameter, _parent.EraserDiameter)
                    g.DrawEllipse(Pens.Black, 4, 4, _parent.EraserDiameter, _parent.EraserDiameter)
                End Using
                Return New Cursor(bmp.GetHicon)
            End Get
        End Property

        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Eraser

        Private Shared ReadOnly ClearBrush As Brush = New SolidBrush(Color.FromArgb(0, Color.White))
        Private _isFirstClick As Boolean = True

        Public Overrides Sub RenderPreview(rawImage As Image, ga As Graphics, sender As PaintPanel)
            Using g As Graphics = Graphics.FromImage(rawImage)
                g.CompositingMode = CompositingMode.SourceCopy
                g.SmoothingMode = SmoothingMode.AntiAlias
                If _isFirstClick Then
                    g.FillEllipse(ClearBrush, InternalBeginCoords.X - CInt(_parent.EraserDiameter / 2), InternalBeginCoords.Y - CInt(_parent.EraserDiameter / 2), _parent.EraserDiameter, _parent.EraserDiameter)
                    _isFirstClick = False
                Else
                    g.FillEllipse(ClearBrush, EndCoordinates.X - CInt(_parent.EraserDiameter / 2), EndCoordinates.Y - CInt(_parent.EraserDiameter / 2), _parent.EraserDiameter, _parent.EraserDiameter)
                End If
            End Using
        End Sub

        Public Sub New(parent As PaintPanel)
            ArgumentNullException.ThrowIfNull(parent)
            _parent = parent
        End Sub
    End Class
End Namespace
