Imports System.Configuration
Imports System.Drawing.Drawing2D
Imports HolzShots.Drawing
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend NotInheritable Class Marker
        Implements ITool(Of ToolSettingsBase)

        Private _beginCoordinates As Point
        Public Property BeginCoordinates As Point Implements ITool(Of ToolSettingsBase).BeginCoordinates
            Get
                Return _beginCoordinates
            End Get
            Set(value As Point)
                _beginCoordinates = value
                _pointList = New List(Of Point) From {_beginCoordinates}
            End Set
        End Property
        Public Property EndCoordinates As Point Implements ITool(Of ToolSettingsBase).EndCoordinates

        Private _pointList As List(Of Point)
        Private _markerWidth As Integer
        Private ReadOnly _markerColor As Color
        Private ReadOnly _markerPen As NativePen

        Public ReadOnly Property Cursor As Cursor Implements ITool(Of ToolSettingsBase).Cursor
            Get
                _markerWidth = If(_markerWidth < 5, 5, _markerWidth)
                Dim bmp As New Bitmap(Convert.ToInt32(0.2 * _markerWidth), _markerWidth)
                bmp.MakeTransparent()
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.Clear(Color.FromArgb(200, 255, 0, 0))
                End Using
                Cursor = New Cursor(bmp.GetHicon)
            End Get
        End Property
        Public ReadOnly Property SettingsControl As ISettingsControl(Of ToolSettingsBase) = Nothing Implements ITool(Of ToolSettingsBase).SettingsControl
        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Marker Implements ITool(Of ToolSettingsBase).ToolType

        Public Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderFinalImage
            Debug.Assert(TypeOf rawImage Is Bitmap)

            _pointList.Add(EndCoordinates)
            Using g As Graphics = Graphics.FromImage(rawImage)
                g.SmoothingMode = SmoothingMode.AntiAlias
                g.DrawHighlight(DirectCast(rawImage, Bitmap), _pointList.ToArray(), _markerPen)
            End Using
            _pointList.Clear()
        End Sub

        Public Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderPreview
            Debug.Assert(TypeOf rawImage Is Bitmap)

            _pointList.Add(EndCoordinates)
            g.SmoothingMode = SmoothingMode.AntiAlias
            If _pointList.Count > 0 Then
                g.DrawHighlight(DirectCast(rawImage, Bitmap), _pointList.ToArray(), _markerPen)
            End If
        End Sub

        Public Sub New(markerWidth As Integer, markerColor As Color)
            _markerWidth = markerWidth
            _markerColor = markerColor
            _markerPen = New NativePen(_markerColor, _markerWidth)
            _pointList = New List(Of Point)
        End Sub

        Protected Sub Dispose() Implements ITool(Of ToolSettingsBase).Dispose
            _markerPen.Dispose()
        End Sub

        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of ToolSettingsBase).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of ToolSettingsBase).MouseClicked
            ' Nothing to do here
        End Sub
    End Class
End Namespace
