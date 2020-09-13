Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Brighten
        Inherits Tool
        Private Shared ReadOnly CursorInstance As Cursor = New Cursor(My.Resources.crossMedium.GetHicon())
        Public Overrides ReadOnly Property Cursor As Cursor = CursorInstance
        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Brighten

        Public Overrides Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel)
            Dim startPoint As New Point(Math.Min(BeginCoords.X, EndCoords.X), Math.Min(BeginCoords.Y, EndCoords.Y))

            Dim rct As New Rectangle(startPoint.X, startPoint.Y, Math.Abs(BeginCoords.X - EndCoords.X), Math.Abs(BeginCoords.Y - EndCoords.Y))
            Using gr = Graphics.FromImage(rawImage)
                gr.FillRectangle(New SolidBrush(sender.BrightenColor), rct)
            End Using
        End Sub

        Public Overrides Sub RenderPreview(rawImage As Image, g As Graphics, sender As PaintPanel)

            Dim thePoint As New Point(If(BeginCoords.X > EndCoords.X, EndCoords.X, BeginCoords.X), If(BeginCoords.Y > EndCoords.Y, EndCoords.Y, BeginCoords.Y))
            Dim rct As New Rectangle(thePoint.X, thePoint.Y, Math.Abs(BeginCoords.X - EndCoords.X), Math.Abs(BeginCoords.Y - EndCoords.Y))
            Using s As New SolidBrush(sender.BrightenColor)
                g.FillRectangle(s, rct)
            End Using
        End Sub
    End Class
End Namespace
