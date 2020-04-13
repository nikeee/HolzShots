Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Linq
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Censor
        Inherits Tool
        Implements IDisposable

        Public Overrides Property BeginCoords As Point
            Get
                Return InternalBeginCoords
            End Get
            Set(ByVal value As Point)
                InternalBeginCoords = value
                _plist = New List(Of Point) From {InternalBeginCoords}
            End Set
        End Property

        Private _plist As List(Of Point)
        Private _zensurWidth As Integer
        Private ReadOnly _zensurColor As Color
        Private ReadOnly _zensurPen As Pen

        Public Overrides ReadOnly Property Cursor As Cursor
            Get
                _zensurWidth = If(_zensurWidth < 5, 5, _zensurWidth)
                Dim bmp As New Bitmap(Convert.ToInt32(0.2 * _zensurWidth), _zensurWidth)
                bmp.MakeTransparent()
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.Clear(Color.FromArgb(200, 255, 0, 0))
                End Using
                Cursor = New Cursor(bmp.GetHicon())
            End Get
        End Property

        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Censor

        Public Overrides Sub RenderFinalImage(ByRef rawImage As Image, ByVal sender As PaintPanel)
            _plist.Add(EndCoords)
            Using g As Graphics = Graphics.FromImage(rawImage)
                With g
                    .SmoothingMode = SmoothingMode.AntiAlias
                    .TextRenderingHint = TextRenderingHint.AntiAlias
                    If _plist.Count > 0 AndAlso (_plist.Count - 1) Mod 3 = 0 Then
                        .DrawBeziers(_zensurPen, _plist.ToArray())
                    Else
                        g.DrawBeziers(_zensurPen, _plist.Take(_plist.Count - (_plist.Count - 1) Mod 3).ToArray())
                    End If
                End With
            End Using
            _plist.Clear()
        End Sub

        Public Overrides Sub RenderPreview(ByVal rawImage As Image, ByVal g As Graphics, ByVal sender As PaintPanel)
            _plist.Add(EndCoords)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.TextRenderingHint = TextRenderingHint.AntiAlias
            If _plist.Count > 0 Then
                Dim bs As Byte() = New Byte(_plist.Count - 1) {}
                bs(0) = CByte(PathPointType.Start)
                For a = 1 To _plist.Count - 1
                    bs(a) = CByte(PathPointType.Line)
                    g.DrawPath(_zensurPen, New GraphicsPath(_plist.ToArray, bs))
                Next
            End If
        End Sub

        Public Sub New(ByVal zensurWidth As Integer, ByVal zensurColor As Color)
            _zensurWidth = zensurWidth
            _zensurColor = zensurColor
            _zensurPen = New Pen(Color.FromArgb(255, _zensurColor), _zensurWidth) With {
                .LineJoin = LineJoin.Round
            }
            _plist = New List(Of Point)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            _zensurPen.Dispose()
        End Sub
    End Class
End Namespace
