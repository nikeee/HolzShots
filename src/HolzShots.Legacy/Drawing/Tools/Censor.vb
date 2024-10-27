Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend NotInheritable Class Censor
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
        Private _censorWidth As Integer
        Private ReadOnly _censorColor As Color
        Private ReadOnly _censorPen As Pen

        Public ReadOnly Property Cursor As Cursor Implements ITool(Of ToolSettingsBase).Cursor
            Get
                _censorWidth = Math.Max(5, _censorWidth)
                Dim bmp As New Bitmap(Convert.ToInt32(0.2 * _censorWidth), _censorWidth)
                bmp.MakeTransparent()
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.Clear(Color.FromArgb(200, 255, 0, 0))
                End Using
                Cursor = New Cursor(bmp.GetHicon())
            End Get
        End Property

        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Censor Implements ITool(Of ToolSettingsBase).ToolType
        Public ReadOnly Property SettingsControl As ISettingsControl(Of ToolSettingsBase) = Nothing Implements ITool(Of ToolSettingsBase).SettingsControl

        Public Sub RenderFinalImage(ByRef rawImage As Image, ByVal sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderFinalImage
            _pointList.Add(EndCoordinates)
            Using g As Graphics = Graphics.FromImage(rawImage)
                With g
                    .SmoothingMode = SmoothingMode.AntiAlias
                    .TextRenderingHint = TextRenderingHint.AntiAlias
                    If _pointList.Count > 0 AndAlso (_pointList.Count - 1) Mod 3 = 0 Then
                        .DrawBeziers(_censorPen, _pointList.ToArray())
                    Else
                        g.DrawBeziers(_censorPen, _pointList.Take(_pointList.Count - (_pointList.Count - 1) Mod 3).ToArray())
                    End If
                End With
            End Using
            _pointList.Clear()
        End Sub

        Public Sub RenderPreview(ByVal rawImage As Image, ByVal g As Graphics, ByVal sender As PaintPanel) Implements ITool(Of ToolSettingsBase).RenderPreview
            _pointList.Add(EndCoordinates)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.TextRenderingHint = TextRenderingHint.AntiAlias
            If _pointList.Count > 0 Then
                Dim bs As Byte() = New Byte(_pointList.Count - 1) {}
                bs(0) = CByte(PathPointType.Start)
                For a = 1 To _pointList.Count - 1
                    bs(a) = CByte(PathPointType.Line)
                    g.DrawPath(_censorPen, New GraphicsPath(_pointList.ToArray, bs))
                Next
            End If
        End Sub

        Public Sub New(ByVal zensurWidth As Integer, ByVal zensurColor As Color)
            _censorWidth = zensurWidth
            _censorColor = zensurColor
            _censorPen = New Pen(Color.FromArgb(255, _censorColor), _censorWidth) With {
                .LineJoin = LineJoin.Round
            }
            _pointList = New List(Of Point)
        End Sub

        Protected Sub Dispose() Implements ITool(Of ToolSettingsBase).Dispose
            _censorPen.Dispose()
        End Sub

        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of ToolSettingsBase).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of ToolSettingsBase).MouseClicked
            ' Nothing to do here
        End Sub
    End Class
End Namespace
