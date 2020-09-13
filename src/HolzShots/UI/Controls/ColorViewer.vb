Namespace UI.Controls
    Friend Class ColorViewer
        Inherits Control

        Private ReadOnly _bush As SolidBrush

        Sub New()
            _bush = New SolidBrush(Color.Transparent)
            Cursor = Cursors.Hand
        End Sub

        Public Event ColorChanged(ByVal sender As Object, ByVal c As Color)

        Public Property Color As Color
            Get
                Return _bush.Color
            End Get
            Set(ByVal value As Color)
                If _bush.Color <> value Then
                    _bush.Color = value
                    RaiseEvent ColorChanged(Me, _bush.Color)
                    Invalidate()
                End If
            End Set
        End Property

        Private Sub ColorViewerClick() Handles Me.Click
            If Not Enabled Then Exit Sub

            Using cfd As New ColorDialog()
                cfd.FullOpen = True
                cfd.ShowHelp = False
                cfd.Color = _bush.Color
                Dim res As DialogResult = cfd.ShowDialog(Me)
                If res = DialogResult.OK Then
                    Color = cfd.Color
                End If
            End Using
        End Sub

        Protected ContainsMouse As Boolean
        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            ContainsMouse = True
            Invalidate(False)
        End Sub
        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            ContainsMouse = False
            Invalidate(False)
        End Sub

        Protected Shared ReadOnly HoverBorderPen As Pen = New Pen(Color.FromArgb(255, 100, 165, 231))
        Protected Shared ReadOnly HoverInnerBorderPen As Pen = New Pen(Color.FromArgb(203, 228, 253))

        Protected Overrides Sub OnPaint(pe As PaintEventArgs)
            If ContainsMouse Then
                pe.Graphics.DrawRectangle(HoverBorderPen, 0, 0, Width - 1, Height - 1)
                pe.Graphics.DrawRectangle(HoverInnerBorderPen, 1, 1, Width - 3, Height - 3)
            Else
                pe.Graphics.DrawRectangle(Pens.Gray, 0, 0, Width - 1, Height - 1)
                pe.Graphics.DrawRectangle(SystemPens.Window, 1, 1, Width - 3, Height - 3)
            End If
            pe.Graphics.FillRectangle(_bush, 2, 2, Width - 4, Height - 4)
        End Sub
    End Class
End Namespace
