Imports System.Drawing.Drawing2D
Imports System.Numerics
Imports HolzShots.Drawing.Tools.UI

Namespace Drawing.Tools
    Friend Class Arrow
        Implements ITool(Of ArrowSettings)

        Private ReadOnly _arrowDrawPoints(3) As Point

        Const ArrowRotationConstant As Single = 2.2 * Math.PI / 1.2
        Private _arrowBetween2 As Vector2

        Public Property BeginCoordinates As Vector2 Implements ITool(Of ArrowSettings).BeginCoordinates
        Public Property EndCoordinates As Vector2 Implements ITool(Of ArrowSettings).EndCoordinates

        Private Shared ReadOnly TheCursor As New Cursor(My.Resources.crossMedium.GetHicon())
        Public ReadOnly Property ToolType As ShotEditorTool = ShotEditorTool.Arrow Implements ITool(Of ArrowSettings).ToolType
        Public ReadOnly Property Cursor As Cursor = TheCursor Implements ITool(Of ArrowSettings).Cursor
        Private ReadOnly _settingsControl As ISettingsControl(Of ArrowSettings)
        Public ReadOnly Property SettingsControl As ISettingsControl(Of ArrowSettings) Implements ITool(Of ArrowSettings).SettingsControl
            Get
                Return _settingsControl
            End Get
        End Property

        Sub New()
            _settingsControl = New ArrowSettingsControl(ArrowSettings.Default)
        End Sub

        Public Sub LoadInitialSettings() Implements ITool(Of ArrowSettings).LoadInitialSettings
            Dim arrowWidth = Math.Clamp(My.Settings.ArrowWidth, ArrowSettings.MinimumWidth, ArrowSettings.MaximumWidth)
            If arrowWidth <> My.Settings.ArrowWidth Then
                My.Settings.ArrowWidth = arrowWidth
                My.Settings.Save()
            End If

            With _settingsControl.Settings
                .Width = My.Settings.ArrowWidth
                .Color = My.Settings.ArrowColor
            End With
        End Sub

        Public Sub PersistSettings() Implements ITool(Of ArrowSettings).PersistSettings
            With _settingsControl.Settings
                My.Settings.ArrowWidth = .Width
                My.Settings.ArrowColor = .Color
            End With
        End Sub

        Private Function CreatePen(settings As ArrowSettings, endCap As LineCap) As Pen
            Return New Pen(settings.Color) With {
                    .Width = If(settings.Width <= 0, _arrowBetween2.Length / 90, settings.Width),
                    .EndCap = endCap,
                    .StartCap = LineCap.Round
                }
        End Function

        Public Sub RenderFinalImage(ByRef rawImage As Image) Implements ITool(Of ArrowSettings).RenderFinalImage
            If BeginCoordinates = Vector2.Zero OrElse EndCoordinates = Vector2.Zero Then
                Return
            End If

            Using g = Graphics.FromImage(rawImage)
                g.SmoothingMode = SmoothingMode.AntiAlias

                Using pen As Pen = CreatePen(_settingsControl.Settings, LineCap.Triangle)
                    g.DrawLine(pen, _arrowDrawPoints(0), _arrowDrawPoints(1))
                    pen.EndCap = LineCap.Round
                    g.DrawLine(pen, _arrowDrawPoints(1), _arrowDrawPoints(2))
                    g.DrawLine(pen, _arrowDrawPoints(1), _arrowDrawPoints(3))
                End Using
            End Using
        End Sub

        Public Sub RenderPreview(rawImage As Image, g As Graphics) Implements ITool(Of ArrowSettings).RenderPreview

            EndCoordinates = New Vector2(EndCoordinates.X, EndCoordinates.Y)

            If BeginCoordinates = Vector2.Zero OrElse EndCoordinates = Vector2.Zero Then
                Return
            End If

            If BeginCoordinates <> EndCoordinates Then
                _arrowBetween2 = EndCoordinates - BeginCoordinates

                Dim between = Vector2.Normalize(_arrowBetween2) * _arrowBetween2.Length / 5
                Dim c = between.Rotate(ArrowRotationConstant) + BeginCoordinates
                Dim d = between.Rotate(-ArrowRotationConstant) + BeginCoordinates
                _arrowDrawPoints(0) = EndCoordinates.ToPoint2D()
                _arrowDrawPoints(1) = BeginCoordinates.ToPoint2D()
                _arrowDrawPoints(2) = c.ToPoint2D()
                _arrowDrawPoints(3) = d.ToPoint2D()

            End If

            If EndCoordinates = Vector2.Zero Then
                Return
            End If

            g.SmoothingMode = SmoothingMode.AntiAlias
            Using pen As Pen = CreatePen(_settingsControl.Settings, LineCap.Round)
                g.DrawLine(pen, _arrowDrawPoints(0), _arrowDrawPoints(1))
                g.DrawLine(pen, _arrowDrawPoints(1), _arrowDrawPoints(2))
                g.DrawLine(pen, _arrowDrawPoints(1), _arrowDrawPoints(3))
            End Using
        End Sub

        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of ArrowSettings).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Vector2, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of ArrowSettings).MouseClicked
            ' Nothing to do here
        End Sub
        Public Sub Dispose() Implements ITool(Of ArrowSettings).Dispose
        End Sub
    End Class
End Namespace
