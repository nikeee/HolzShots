Imports System.Configuration
Imports System.Drawing.Drawing2D
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Ellipse
        Implements ITool(Of EllipseSettings)
        Public Property BeginCoordinates As Point Implements ITool(Of EllipseSettings).BeginCoordinates
        Public Property EndCoordinates As Point Implements ITool(Of EllipseSettings).EndCoordinates

        Private Shared ReadOnly CursorInstance As New Cursor(My.Resources.crossMedium.GetHicon())
        Public ReadOnly Property Cursor As Cursor = CursorInstance Implements ITool(Of EllipseSettings).Cursor
        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Ellipse Implements ITool(Of EllipseSettings).ToolType

        Private ReadOnly _settingsControl As EllipseSettingsControl
        Public ReadOnly Property SettingsControl As ISettingsControl(Of EllipseSettings) Implements ITool(Of EllipseSettings).SettingsControl
            Get
                Return _settingsControl
            End Get
        End Property

        Sub New()
            ' TODO: Move
            If My.Settings.EllipseWidth > 100 OrElse My.Settings.EllipseWidth <= 0 Then
                My.Settings.EllipseWidth = 20
                My.Settings.Save()
            End If

            _settingsControl = New EllipseSettingsControl(
                New EllipseSettings(
                    My.Settings.EllipseWidth,
                    My.Settings.EllipseColor,
                    If(My.Settings.UseBoxInsteadOfCirlce, EllipseMode.Rectangle, EllipseMode.Ellipse)
                )
            )
        End Sub

        Private Shared Function CreatePen(settings As EllipseSettings) As Pen
            Return New Pen(settings.Color, settings.Width) With {
                .DashStyle = DashStyle.Solid
            }
        End Function

        Public Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel) Implements ITool(Of EllipseSettings).RenderFinalImage
            Dim settings = _settingsControl.Settings

            Dim rect = New Rectangle(
                Math.Min(EndCoordinates.X, BeginCoordinates.X),
                Math.Min(EndCoordinates.Y, BeginCoordinates.Y),
                Math.Max(Math.Abs(BeginCoordinates.X - EndCoordinates.X), settings.Width),
                Math.Max(Math.Abs(BeginCoordinates.Y - EndCoordinates.Y), settings.Width)
            )

            Using g = Graphics.FromImage(rawImage)
                Using pen = CreatePen(settings)
                    If settings.Mode = EllipseMode.Rectangle Then
                        g.DrawRectangle(pen, rect)
                    Else
                        g.SmoothingMode = SmoothingMode.AntiAlias
                        g.DrawEllipse(pen, rect)
                    End If
                End Using
            End Using
        End Sub

        Public Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel) Implements ITool(Of EllipseSettings).RenderPreview
            Dim settings = _settingsControl.Settings

            Dim rect = New Rectangle(
                Math.Min(EndCoordinates.X, BeginCoordinates.X),
                Math.Min(EndCoordinates.Y, BeginCoordinates.Y),
                Math.Max(Math.Abs(BeginCoordinates.X - EndCoordinates.X), settings.Width),
                Math.Max(Math.Abs(BeginCoordinates.Y - EndCoordinates.Y), settings.Width)
            )

            Using pen = CreatePen(settings)
                If settings.Mode = EllipseMode.Rectangle Then
                    g.DrawRectangle(pen, rect)
                Else
                    g.SmoothingMode = SmoothingMode.AntiAlias
                    g.DrawEllipse(pen, rect)
                End If
            End Using
        End Sub

        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of EllipseSettings).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of EllipseSettings).MouseClicked
            ' Nothing to do here
        End Sub
        Public Sub Dispose() Implements ITool(Of EllipseSettings).Dispose
        End Sub
    End Class
End Namespace
