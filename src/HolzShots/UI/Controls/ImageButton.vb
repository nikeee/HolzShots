Imports System.ComponentModel

Namespace UI.Controls
    <DefaultEvent("Click")>
    Friend Class ImageButton
        Inherits PictureBox

        <Category("Appearance")>
        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property HoverImage As Image

        <Category("Appearance")>
        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property DisabledImage As Image

        <Category("Appearance")>
        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property PressedImage As Image

        <Category("Appearance")>
        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property TheImage As Image

        <Category("Appearance")>
        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property InfoText As String

        Public Event UpdateInfoText(ByVal sender As Object, ByVal txt As String)
        Public Shadows Event Click(ByVal sender As Object)

        Private Sub ImageButtonEnabledChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.EnabledChanged
            If Enabled = False Then
                Image = DisabledImage
            Else
                Image = TheImage
            End If
        End Sub

        Private Sub ImagButtonMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseDown
            If Enabled = True AndAlso e.Button = MouseButtons.Left AndAlso PressedImage IsNot Nothing Then
                Image = PressedImage
            End If
        End Sub

        Private Sub ImagButtonMouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.MouseEnter
            If Enabled = True AndAlso HoverImage IsNot Nothing Then
                Image = HoverImage
            End If
            RaiseEvent UpdateInfoText(Me, InfoText)
        End Sub

        Private Sub ImagButtonMouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Me.MouseLeave
            If Enabled = True AndAlso TheImage IsNot Nothing Then
                Image = TheImage
            End If
            RaiseEvent UpdateInfoText(Me, String.Empty)
        End Sub

        Private Sub ImagButtonMouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseUp
            If Enabled = True AndAlso HoverImage IsNot Nothing Then
                Image = HoverImage
            End If
            If e.Button = MouseButtons.Left Then RaiseEvent Click(Me)
        End Sub

        Private Sub ImageButton_VisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.VisibleChanged
            If Enabled = True AndAlso TheImage IsNot Nothing Then
                Image = TheImage
            ElseIf Enabled = False AndAlso DisabledImage IsNot Nothing Then
                Image = DisabledImage
            End If
        End Sub
    End Class
End Namespace
