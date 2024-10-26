Imports System.Drawing.Drawing2D
Imports HolzShots.Drawing
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend NotInheritable Class Marker
        Inherits Tool
        Implements IDisposable

        Public Overrides Property BeginCoordinates As Point
            Get
                Return InternalBeginCoordinates
            End Get
            Set(value As Point)
                InternalBeginCoordinates = value
                _plist = New List(Of Point) From {InternalBeginCoordinates}
            End Set
        End Property

        Private _plist As List(Of Point)
        Private _markerWidth As Integer
        Private ReadOnly _markerColor As Color
        Private ReadOnly _markerPen As NativePen

        Public Overrides ReadOnly Property Cursor As Cursor
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

        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Marker

        Public Overrides Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel)
            Debug.Assert(TypeOf rawImage Is Bitmap)

            _plist.Add(EndCoordinates)
            Using g As Graphics = Graphics.FromImage(rawImage)
                g.SmoothingMode = SmoothingMode.AntiAlias
                g.DrawHighlight(DirectCast(rawImage, Bitmap), _plist.ToArray(), _markerPen)
            End Using
            _plist.Clear()
        End Sub

        Public Overrides Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel)
            Debug.Assert(TypeOf rawImage Is Bitmap)

            _plist.Add(EndCoordinates)
            g.SmoothingMode = SmoothingMode.AntiAlias
            If _plist.Count > 0 Then
                g.DrawHighlight(DirectCast(rawImage, Bitmap), _plist.ToArray(), _markerPen)
            End If
        End Sub

        Public Sub New(markerWidth As Integer, markerColor As Color)
            _markerWidth = markerWidth
            _markerColor = markerColor
            _markerPen = New NativePen(_markerColor, _markerWidth)
            _plist = New List(Of Point)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            _markerPen.Dispose()
        End Sub
    End Class
End Namespace
