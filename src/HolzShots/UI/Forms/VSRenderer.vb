Imports System.Drawing.Drawing2D

Namespace UI.Forms

    Public Class VSRenderer
        Inherits ToolStripSystemRenderer

        Private Shared ReadOnly BackgroundBrush1 As New SolidBrush(Color.FromArgb(233, 236, 238))
        Private Shared ReadOnly BackgroundBrush2 As Brush = New SolidBrush(Color.FromArgb(255, 236, 181))
        Private Shared ReadOnly RectanglePen1 As Pen = New Pen(Color.FromArgb(229, 195, 101))
        Private Shared ReadOnly BackgroundColor1 As Color = Color.FromArgb(211, 217, 227)

        Private Sub RenderBackground(ByVal e As ToolStripItemRenderEventArgs)
            Dim bgBrush1 As New LinearGradientBrush(Point.Empty, New Point(0, CInt(e.Item.Size.Height / 2)), Color.FromArgb(255, 251, 240), Color.FromArgb(255, 243, 207))
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            e.Graphics.FillRectangle(bgBrush1, New Rectangle(0, 0, e.Item.Width - 1, CInt(e.Item.Size.Height / 2)))
            e.Graphics.FillRectangle(BackgroundBrush2, New Rectangle(0, CInt(e.Item.Size.Height / 2), e.Item.Width - 1, CInt(e.Item.Size.Height / 2)))
            e.Graphics.DrawRectangle(RectanglePen1, New Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1))
        End Sub

        Protected Overrides Sub OnRenderMenuItemBackground(ByVal e As ToolStripItemRenderEventArgs)
            If e Is Nothing Then Throw New ArgumentNullException(NameOf(e))

            If e.Item.Selected Then
                RenderBackground(e)
            Else
                MyBase.OnRenderMenuItemBackground(e)
            End If
        End Sub
        Protected Overrides Sub OnRenderToolStripBackground(ByVal e As ToolStripRenderEventArgs)
            If e Is Nothing Then Throw New ArgumentNullException(NameOf(e))

            e.Graphics.Clear(BackgroundColor1)
        End Sub
        Protected Overrides Sub OnRenderImageMargin(ByVal e As ToolStripRenderEventArgs)
            If e Is Nothing Then Throw New ArgumentNullException(NameOf(e))

            e.Graphics.FillRectangle(BackgroundBrush1, e.AffectedBounds)
        End Sub
        Protected Overrides Sub OnRenderDropDownButtonBackground(ByVal e As ToolStripItemRenderEventArgs)
            If e Is Nothing Then Throw New ArgumentNullException(NameOf(e))

            If e.Item.Selected Then
                RenderBackground(e)
            Else
                MyBase.OnRenderDropDownButtonBackground(e)
            End If
        End Sub
        Protected Overrides Sub OnRenderItemText(ByVal e As ToolStripItemTextRenderEventArgs)
            If e Is Nothing Then Throw New ArgumentNullException(NameOf(e))

            e.TextColor = Color.Black
            MyBase.OnRenderItemText(e)
        End Sub
    End Class
End Namespace
