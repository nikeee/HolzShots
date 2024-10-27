Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Brighten
        Inherits Tool
        Private Shared ReadOnly CursorInstance As New Cursor(My.Resources.crossMedium.GetHicon())
        Public Overrides ReadOnly Property Cursor As Cursor = CursorInstance
        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Brighten

        Public Overrides Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel)
            Dim rct As New Rectangle(
                Math.Min(BeginCoordinates.X, EndCoordinates.X),
                Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
                Math.Abs(BeginCoordinates.X - EndCoordinates.X),
                Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
            )

            Using gr = Graphics.FromImage(rawImage)
                gr.FillRectangle(New SolidBrush(sender.BrightenColor), rct)
            End Using
        End Sub

        Public Overrides Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel)
            Dim rct As New Rectangle(
                Math.Min(BeginCoordinates.X, EndCoordinates.X),
                Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
                Math.Abs(BeginCoordinates.X - EndCoordinates.X),
                Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
            )

            Using s As New SolidBrush(sender.BrightenColor)
                g.FillRectangle(s, rct)
            End Using
        End Sub
    End Class
End Namespace
