Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend NotInheritable Class Censor
        Implements ITool(Of CensorSettings)

        Private _pointList As List(Of Point)

        Private _beginCoordinates As Point
        Public Property BeginCoordinates As Point Implements ITool(Of CensorSettings).BeginCoordinates
            Get
                Return _beginCoordinates
            End Get
            Set(value As Point)
                _beginCoordinates = value
                _pointList = New List(Of Point) From {_beginCoordinates}
            End Set
        End Property

        Public Property EndCoordinates As Point Implements ITool(Of CensorSettings).EndCoordinates

        Public ReadOnly Property Cursor As Cursor Implements ITool(Of CensorSettings).Cursor
            Get
                Dim censorWidth = Math.Max(5, _settingsControl.Settings.Width)
                Dim bmp As New Bitmap(Convert.ToInt32(0.2 * censorWidth), censorWidth)
                bmp.MakeTransparent()
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.Clear(Color.FromArgb(200, 255, 0, 0))
                End Using
                Cursor = New Cursor(bmp.GetHicon())
            End Get
        End Property

        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Censor Implements ITool(Of CensorSettings).ToolType

        Private ReadOnly _settingsControl As ISettingsControl(Of CensorSettings)
        Public ReadOnly Property SettingsControl As ISettingsControl(Of CensorSettings) Implements ITool(Of CensorSettings).SettingsControl
            Get
                Return _settingsControl
            End Get
        End Property

        Sub New()
            _settingsControl = New CensorSettingsControl(CensorSettings.Default)
            _pointList = New List(Of Point)
        End Sub

        Public Sub LoadInitialSettings() Implements ITool(Of CensorSettings).LoadInitialSettings
            If My.Settings.ZensursulaWidth > CensorSettings.MaximumWidth OrElse My.Settings.ZensursulaWidth < CensorSettings.MinimumWidth Then
                My.Settings.ZensursulaWidth = CensorSettings.Default.Width
                My.Settings.Save()
            End If

            With _settingsControl.Settings
                .Width = My.Settings.ZensursulaWidth
                .Color = My.Settings.ZensursulaColor
            End With
        End Sub

        Public Sub PersistSettings() Implements ITool(Of CensorSettings).PersistSettings
            With _settingsControl.Settings
                My.Settings.ZensursulaWidth = .Width
                My.Settings.ZensursulaColor = .Color
            End With
        End Sub

        Private Shared Function CreatePen(settings As CensorSettings) As Pen
            Return New Pen(Color.FromArgb(255, settings.Color), settings.Width) With {
                .LineJoin = LineJoin.Round
            }
        End Function

        Public Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel) Implements ITool(Of CensorSettings).RenderFinalImage
            _pointList.Add(EndCoordinates)
            Using g = Graphics.FromImage(rawImage)
                Using censorPen = CreatePen(_settingsControl.Settings)
                    With g
                        .SmoothingMode = SmoothingMode.AntiAlias
                        .TextRenderingHint = TextRenderingHint.AntiAlias
                        If _pointList.Count > 0 AndAlso (_pointList.Count - 1) Mod 3 = 0 Then
                            .DrawBeziers(censorPen, _pointList.ToArray())
                        Else
                            .DrawBeziers(censorPen, _pointList.Take(_pointList.Count - (_pointList.Count - 1) Mod 3).ToArray())
                        End If
                    End With
                End Using
            End Using
            _pointList.Clear()
        End Sub

        Public Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel) Implements ITool(Of CensorSettings).RenderPreview
            _pointList.Add(EndCoordinates)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.TextRenderingHint = TextRenderingHint.AntiAlias

            If _pointList.Count <= 0 Then
                Return
            End If

            Using censorPen = CreatePen(_settingsControl.Settings)
                Dim bs As Byte() = New Byte(_pointList.Count - 1) {}
                bs(0) = CByte(PathPointType.Start)
                For a = 1 To _pointList.Count - 1
                    bs(a) = CByte(PathPointType.Line)
                    g.DrawPath(censorPen, New GraphicsPath(_pointList.ToArray, bs))
                Next
            End Using
        End Sub

        Protected Sub Dispose() Implements ITool(Of CensorSettings).Dispose
            ' Nothing to do here
        End Sub

        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of CensorSettings).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of CensorSettings).MouseClicked
            ' Nothing to do here
        End Sub
    End Class
End Namespace
