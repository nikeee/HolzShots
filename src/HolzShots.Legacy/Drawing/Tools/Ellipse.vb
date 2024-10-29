Imports System.Drawing.Drawing2D
Imports System.Numerics
Imports HolzShots.Drawing.Tools.UI

Namespace Drawing.Tools
    Friend Class Ellipse
        Implements ITool(Of EllipseSettings)
        Public Property BeginCoordinates As Vector2 Implements ITool(Of EllipseSettings).BeginCoordinates
        Public Property EndCoordinates As Vector2 Implements ITool(Of EllipseSettings).EndCoordinates

        Private Shared ReadOnly CursorInstance As New Cursor(My.Resources.crossMedium.GetHicon())
        Public ReadOnly Property Cursor As Cursor = CursorInstance Implements ITool(Of EllipseSettings).Cursor
        Public ReadOnly Property ToolType As ShotEditorTool = ShotEditorTool.Ellipse Implements ITool(Of EllipseSettings).ToolType

        Public ReadOnly Property SettingsControl As ISettingsControl(Of EllipseSettings) Implements ITool(Of EllipseSettings).SettingsControl

        Sub New()
            SettingsControl = New EllipseSettingsControl(EllipseSettings.Default)
        End Sub

        Public Sub LoadInitialSettings() Implements ITool(Of EllipseSettings).LoadInitialSettings
            If My.Settings.EllipseWidth > EllipseSettings.MaximumWidth OrElse My.Settings.EllipseWidth < EllipseSettings.MinimumWidth Then
                My.Settings.EllipseWidth = EllipseSettings.Default.Width
                My.Settings.Save()
            End If

            With SettingsControl.Settings
                .Color = My.Settings.EllipseColor
                .Width = My.Settings.EllipseWidth
                .Mode = If(My.Settings.UseBoxInsteadOfCirlce, EllipseMode.Rectangle, EllipseMode.Ellipse)
            End With
        End Sub

        Public Sub PersistSettings() Implements ITool(Of EllipseSettings).PersistSettings
            With SettingsControl.Settings
                .Color = My.Settings.EllipseColor
                .Width = My.Settings.EllipseWidth
                My.Settings.UseBoxInsteadOfCirlce = .Mode = EllipseMode.Rectangle
            End With
        End Sub

        Private Shared Function CreatePen(settings As EllipseSettings) As Pen
            Return New Pen(settings.Color, settings.Width) With {
                .DashStyle = DashStyle.Solid
            }
        End Function

        Public Sub RenderFinalImage(ByRef rawImage As Image) Implements ITool(Of EllipseSettings).RenderFinalImage
            Dim settings = SettingsControl.Settings

            Dim rect = Rectangle.Round(New RectangleF(
                Math.Min(EndCoordinates.X, BeginCoordinates.X),
                Math.Min(EndCoordinates.Y, BeginCoordinates.Y),
                Math.Max(Math.Abs(BeginCoordinates.X - EndCoordinates.X), settings.Width),
                Math.Max(Math.Abs(BeginCoordinates.Y - EndCoordinates.Y), settings.Width)
            ))

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

        Public Sub RenderPreview(rawImage As Image, g As Graphics) Implements ITool(Of EllipseSettings).RenderPreview
            Dim settings = SettingsControl.Settings

            Dim rect = Rectangle.Round(New RectangleF(
                Math.Min(EndCoordinates.X, BeginCoordinates.X),
                Math.Min(EndCoordinates.Y, BeginCoordinates.Y),
                Math.Max(Math.Abs(BeginCoordinates.X - EndCoordinates.X), settings.Width),
                Math.Max(Math.Abs(BeginCoordinates.Y - EndCoordinates.Y), settings.Width)
            ))

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
        Public Sub MouseClicked(rawImage As Image, e As Vector2, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of EllipseSettings).MouseClicked
            ' Nothing to do here
        End Sub
        Public Sub Dispose() Implements ITool(Of EllipseSettings).Dispose
        End Sub
    End Class
End Namespace
