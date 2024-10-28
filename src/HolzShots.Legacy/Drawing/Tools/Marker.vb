Imports System.Drawing.Drawing2D
Imports HolzShots.Drawing
Imports HolzShots.Drawing.Tools.UI

Namespace Drawing.Tools
    Friend NotInheritable Class Marker
        Implements ITool(Of MarkerSettings)
        Private _pointList As List(Of Point)

        Private _beginCoordinates As Point
        Public Property BeginCoordinates As Point Implements ITool(Of MarkerSettings).BeginCoordinates
            Get
                Return _beginCoordinates
            End Get
            Set(value As Point)
                _beginCoordinates = value
                _pointList = New List(Of Point) From {_beginCoordinates}
            End Set
        End Property
        Public Property EndCoordinates As Point Implements ITool(Of MarkerSettings).EndCoordinates

        Public ReadOnly Property Cursor As Cursor Implements ITool(Of MarkerSettings).Cursor
            Get
                Dim markerWidth = Math.Max(5, _settingsControl.Settings.Width)
                Dim bmp As New Bitmap(Convert.ToInt32(0.2 * markerWidth), markerWidth)
                bmp.MakeTransparent()
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.Clear(Color.FromArgb(200, 255, 0, 0))
                End Using
                Cursor = New Cursor(bmp.GetHicon)
            End Get
        End Property

        Private ReadOnly _settingsControl As ISettingsControl(Of MarkerSettings)
        Public ReadOnly Property SettingsControl As ISettingsControl(Of MarkerSettings) Implements ITool(Of MarkerSettings).SettingsControl
            Get
                Return _settingsControl
            End Get
        End Property

        Public ReadOnly Property ToolType As ShotEditorTool = ShotEditorTool.Marker Implements ITool(Of MarkerSettings).ToolType

        Sub New()
            _settingsControl = New MarkerSettingsControl(MarkerSettings.Default)
            _pointList = New List(Of Point)
        End Sub

        Public Sub LoadInitialSettings() Implements ITool(Of MarkerSettings).LoadInitialSettings
            If My.Settings.MarkerWidth > MarkerSettings.MaximumWidth OrElse My.Settings.MarkerWidth < MarkerSettings.MinimumWidth Then
                My.Settings.MarkerWidth = MarkerSettings.Default.Width
                My.Settings.Save()
            End If

            With _settingsControl.Settings
                .Width = My.Settings.MarkerWidth
                .Color = My.Settings.MarkerColor
            End With
        End Sub

        Public Sub PersistSettings() Implements ITool(Of MarkerSettings).PersistSettings
            With _settingsControl.Settings
                My.Settings.MarkerWidth = .Width
                My.Settings.MarkerColor = .Color
            End With
        End Sub

        Private Shared Function CreatePen(settings As MarkerSettings) As NativePen
            Return New NativePen(settings.Color, settings.Width)
        End Function


        Public Sub RenderFinalImage(ByRef rawImage As Image) Implements ITool(Of MarkerSettings).RenderFinalImage
            Debug.Assert(TypeOf rawImage Is Bitmap)

            _pointList.Add(EndCoordinates)

            If _pointList.Count <= 1 Then
                Return
            End If

            Using g = Graphics.FromImage(rawImage)
                g.SmoothingMode = SmoothingMode.AntiAlias
                Using markerPen = CreatePen(_settingsControl.Settings)
                    g.DrawHighlight(DirectCast(rawImage, Bitmap), _pointList.ToArray(), markerPen)
                End Using
            End Using
            _pointList.Clear()
        End Sub

        Public Sub RenderPreview(rawImage As Image, g As Graphics) Implements ITool(Of MarkerSettings).RenderPreview
            Debug.Assert(TypeOf rawImage Is Bitmap)

            _pointList.Add(EndCoordinates)
            g.SmoothingMode = SmoothingMode.AntiAlias
            If _pointList.Count <= 0 Then
                Return
            End If

            Using markerPen = CreatePen(_settingsControl.Settings)
                g.DrawHighlight(DirectCast(rawImage, Bitmap), _pointList.ToArray(), markerPen)
            End Using
        End Sub

        Protected Sub Dispose() Implements ITool(Of MarkerSettings).Dispose
            ' Nothing to do here
        End Sub

        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of MarkerSettings).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of MarkerSettings).MouseClicked
            ' Nothing to do here
        End Sub
    End Class
End Namespace
