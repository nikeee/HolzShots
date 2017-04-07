Namespace UI
    Friend Class BorderPanelWindow
        Inherits Form

        Protected Sub New()
        End Sub

        Private Shared ReadOnly MenuBarBorderPen As Pen = New Pen(Color.FromArgb(255, 223, 223, 223))
        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            With e.Graphics
                .FillRectangle(SystemBrushes.MenuBar, New Rectangle(0, ClientRectangle.Height - 40, ClientRectangle.Width, 40))
                .DrawLine(MenuBarBorderPen, 0, ClientRectangle.Height - 40, ClientRectangle.Width, ClientRectangle.Height - 40)
                If DesignMode Then
                    .DrawLine(Pens.Green, 0, ClientRectangle.Height - 30, ClientRectangle.Width, ClientRectangle.Height - 30)
                    .DrawLine(Pens.Green, 0, ClientRectangle.Height - 10, ClientRectangle.Width, ClientRectangle.Height - 10)
                End If
            End With
        End Sub
    End Class
End Namespace