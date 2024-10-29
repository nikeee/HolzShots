Imports System.Numerics
Imports HolzShots.Drawing.Tools.UI

Namespace Drawing.Tools
    Friend Class Brighten
        Implements ITool(Of BrightnessSettings)

        Private Shared ReadOnly CursorInstance As New Cursor(My.Resources.crossMedium.GetHicon())
        Public ReadOnly Property Cursor As Cursor = CursorInstance Implements ITool(Of BrightnessSettings).Cursor
        Public ReadOnly Property ToolType As ShotEditorTool = ShotEditorTool.Brighten Implements ITool(Of BrightnessSettings).ToolType
        Public ReadOnly Property SettingsControl As ISettingsControl(Of BrightnessSettings) Implements ITool(Of BrightnessSettings).SettingsControl

        Public Property BeginCoordinates As Vector2 Implements ITool(Of BrightnessSettings).BeginCoordinates
        Public Property EndCoordinates As Vector2 Implements ITool(Of BrightnessSettings).EndCoordinates

        Sub New()
            SettingsControl = New BrightnessSettingsControl(BrightnessSettings.Default)
        End Sub

        Public Sub LoadInitialSettings() Implements ITool(Of BrightnessSettings).LoadInitialSettings
            Dim v As Integer = My.Settings.BrightenColor.A
            If My.Settings.BrightenColor.R = My.Settings.BrightenColor.G AndAlso
                My.Settings.BrightenColor.R = My.Settings.BrightenColor.B Then
                If My.Settings.BrightenColor.R = 255 Then
                    v += 255
                ElseIf My.Settings.BrightenColor.R = 0 Then
                    v = 255 - v
                End If
            End If

            With SettingsControl.Settings
                .Brightness = v
            End With
        End Sub

        Public Sub PersistSettings() Implements ITool(Of BrightnessSettings).PersistSettings
            With SettingsControl.Settings
                My.Settings.BrightenColor = .BrightnessColor
            End With
        End Sub


        Private Shared Function CreateBrush(settings As BrightnessSettings) As Brush
            Return New SolidBrush(settings.BrightnessColor)
        End Function

        Public Sub RenderFinalImage(ByRef rawImage As Image) Implements ITool(Of BrightnessSettings).RenderFinalImage
            Dim rct = Rectangle.Round(New RectangleF(
                Math.Min(BeginCoordinates.X, EndCoordinates.X),
                Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
                Math.Abs(BeginCoordinates.X - EndCoordinates.X),
                Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
            ))

            Using gr = Graphics.FromImage(rawImage)
                Using brush = CreateBrush(SettingsControl.Settings)
                    gr.FillRectangle(brush, rct)
                End Using
            End Using
        End Sub

        Public Sub RenderPreview(rawImage As Image, g As Graphics) Implements ITool(Of BrightnessSettings).RenderPreview
            Dim rct = Rectangle.Round(New RectangleF(
                Math.Min(BeginCoordinates.X, EndCoordinates.X),
                Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
                Math.Abs(BeginCoordinates.X - EndCoordinates.X),
                Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
            ))

            Using brush = CreateBrush(SettingsControl.Settings)
                g.FillRectangle(brush, rct)
            End Using
        End Sub

        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of BrightnessSettings).MouseOnlyMoved
            ' Nothing to do here
        End Sub

        Public Sub MouseClicked(rawImage As Image, e As Vector2, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of BrightnessSettings).MouseClicked
            ' Nothing to do here
        End Sub
        Public Sub Dispose() Implements ITool(Of BrightnessSettings).Dispose
        End Sub
    End Class
End Namespace
