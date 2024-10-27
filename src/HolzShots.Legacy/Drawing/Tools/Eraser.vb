Imports System.Drawing.Drawing2D
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Eraser
        Implements ITool(Of EraserSettings)

        Private ReadOnly _parent As PaintPanel

        Private ReadOnly _settingsControl As ISettingsControl(Of EraserSettings)

        Public ReadOnly Property SettingsControl As ISettingsControl(Of EraserSettings) Implements ITool(Of EraserSettings).SettingsControl
            Get
                Return _settingsControl
            End Get
        End Property

        Private _beginCoordinates As Point
        Public Property BeginCoordinates As Point Implements ITool(Of EraserSettings).BeginCoordinates
            Get
                Return _beginCoordinates
            End Get
            Set(value As Point)
                _beginCoordinates = value
                RenderPreview(_parent.RawBox.Image, Nothing, _parent)
            End Set
        End Property
        Public Property EndCoordinates As Point Implements ITool(Of EraserSettings).EndCoordinates

        Public ReadOnly Property Cursor As Cursor Implements ITool(Of EraserSettings).Cursor
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

        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Eraser Implements ITool(Of EraserSettings).ToolType

        Private Shared ReadOnly ClearBrush As Brush = New SolidBrush(Color.FromArgb(0, Color.White))

        Private _isFirstClick As Boolean = True

        Public Sub RenderPreview(rawImage As Image, ga As Graphics, sender As PaintPanel) Implements ITool(Of EraserSettings).RenderPreview
            Using g As Graphics = Graphics.FromImage(rawImage)
                g.CompositingMode = CompositingMode.SourceCopy
                g.SmoothingMode = SmoothingMode.AntiAlias
                If _isFirstClick Then
                    g.FillEllipse(
                        ClearBrush,
                        _beginCoordinates.X - CInt(_parent.EraserDiameter / 2),
                        _beginCoordinates.Y - CInt(_parent.EraserDiameter / 2),
                        _parent.EraserDiameter,
                        _parent.EraserDiameter
                    )
                    _isFirstClick = False
                Else
                    g.FillEllipse(
                        ClearBrush,
                        EndCoordinates.X - CInt(_parent.EraserDiameter / 2),
                        EndCoordinates.Y - CInt(_parent.EraserDiameter / 2),
                        _parent.EraserDiameter,
                        _parent.EraserDiameter
                    )
                End If
            End Using
        End Sub

        Public Sub New(parent As PaintPanel)
            ArgumentNullException.ThrowIfNull(parent)
            _parent = parent
            _settingsControl = New EraserSettingsControl(New EraserSettings(10))
            ' AddHandler _settingsControl.OnSettingsUpdated, AddressOf OnSettingsUpdated
        End Sub

        Private Sub OnSettingsUpdated(sender As Object, newSettings As EraserSettings)
            MessageBox.Show("Settings updated: " & newSettings.ToString())
        End Sub

        Public Sub Dispose() Implements ITool(Of EraserSettings).Dispose
            _settingsControl.Dispose()
        End Sub

        Public Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel) Implements ITool(Of EraserSettings).RenderFinalImage
            ' Nothing to do here
        End Sub
        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of EraserSettings).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of EraserSettings).MouseClicked
            ' Nothing to do here
        End Sub
    End Class
End Namespace
