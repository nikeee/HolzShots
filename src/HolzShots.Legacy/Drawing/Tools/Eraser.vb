Imports System.Drawing.Drawing2D
Imports System.Numerics
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Eraser
        Implements ITool(Of EraserSettings)

        Private ReadOnly _parent As PaintPanel

        Public ReadOnly Property SettingsControl As ISettingsControl(Of EraserSettings) Implements ITool(Of EraserSettings).SettingsControl

        Private _beginCoordinates As Vector2
        Public Property BeginCoordinates As Vector2 Implements ITool(Of EraserSettings).BeginCoordinates
            Get
                Return _beginCoordinates
            End Get
            Set(value As Vector2)
                _beginCoordinates = value
                RenderPreview(_parent.RawBox.Image, Nothing)
            End Set
        End Property
        Public Property EndCoordinates As Vector2 Implements ITool(Of EraserSettings).EndCoordinates

        Public ReadOnly Property Cursor As Cursor Implements ITool(Of EraserSettings).Cursor
            Get
                Dim settings = SettingsControl.Settings

                Dim bmp As New Bitmap(settings.Diameter + 8, settings.Diameter + 8)
                bmp.MakeTransparent()
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.SmoothingMode = SmoothingMode.AntiAlias
                    g.FillEllipse(Brushes.LightGray, 4, 4, settings.Diameter, settings.Diameter)
                    g.DrawEllipse(Pens.Black, 4, 4, settings.Diameter, settings.Diameter)
                End Using
                Return New Cursor(bmp.GetHicon)
            End Get
        End Property

        Public ReadOnly Property ToolType As ShotEditorTool = ShotEditorTool.Eraser Implements ITool(Of EraserSettings).ToolType

        Private Shared ReadOnly ClearBrush As Brush = New SolidBrush(Color.FromArgb(0, Color.White))

        Private _isFirstClick As Boolean = True

        Public Sub RenderPreview(rawImage As Image, ga As Graphics) Implements ITool(Of EraserSettings).RenderPreview
            Using g As Graphics = Graphics.FromImage(rawImage)
                g.CompositingMode = CompositingMode.SourceCopy
                g.SmoothingMode = SmoothingMode.AntiAlias

                Dim settings = SettingsControl.Settings

                If _isFirstClick Then
                    g.FillEllipse(
                        ClearBrush,
                        _beginCoordinates.X - CInt(settings.Diameter / 2),
                        _beginCoordinates.Y - CInt(settings.Diameter / 2),
                        settings.Diameter,
                        settings.Diameter
                    )
                    _isFirstClick = False
                Else
                    g.FillEllipse(
                        ClearBrush,
                        EndCoordinates.X - CInt(settings.Diameter / 2),
                        EndCoordinates.Y - CInt(settings.Diameter / 2),
                        settings.Diameter,
                        settings.Diameter
                    )
                End If
            End Using
        End Sub

        Public Sub New(parent As PaintPanel)
            ArgumentNullException.ThrowIfNull(parent)
            _parent = parent

            SettingsControl = New EraserSettingsControl(EraserSettings.Default)
        End Sub

        Public Sub LoadInitialSettings() Implements ITool(Of EraserSettings).LoadInitialSettings
            If My.Settings.EraserDiameter > EraserSettings.MaximumDiameter OrElse My.Settings.EraserDiameter <= EraserSettings.MinimumDiameter Then
                My.Settings.EraserDiameter = EraserSettings.Default.Diameter
                My.Settings.Save()
            End If

            With SettingsControl.Settings
                .Diameter = My.Settings.EraserDiameter
            End With
        End Sub

        Public Sub PersistSettings() Implements ITool(Of EraserSettings).PersistSettings
            With SettingsControl.Settings
                My.Settings.EraserDiameter = .Diameter
            End With
        End Sub

        Public Sub Dispose() Implements ITool(Of EraserSettings).Dispose
            SettingsControl.Dispose()
        End Sub

        Public Sub RenderFinalImage(ByRef rawImage As Image) Implements ITool(Of EraserSettings).RenderFinalImage
            ' Nothing to do here
        End Sub
        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of EraserSettings).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Vector2, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of EraserSettings).MouseClicked
            ' Nothing to do here
        End Sub
    End Class
End Namespace
