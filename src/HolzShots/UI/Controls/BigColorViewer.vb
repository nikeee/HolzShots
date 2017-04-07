Namespace UI.Controls
    Friend Class BigColorViewer
        Inherits Control

        Private ReadOnly _brush As SolidBrush
        Public Property Color As Color
            Get
                Return _brush.Color
            End Get
            Set(ByVal value As Color)
                If (_brush.Color <> value) Then
                    _brush.Color = value
                    Invalidate(New Rectangle(2, 2, Width - 4, Height - 4), False)
                End If
            End Set
        End Property

        Private ReadOnly _transparentBackgroundBrush As Brush

        Public Sub New()
            _brush = New SolidBrush(Color.FromArgb(128, 0, 0, 0))
            _transparentBackgroundBrush = New TextureBrush(HolzShots.My.Resources.PaintPanelBackground)
        End Sub

        Protected Overrides Sub OnPaint(pe As System.Windows.Forms.PaintEventArgs)
            pe.Graphics.DrawRectangle(Pens.Gray, 0, 0, Width - 1, Height - 1)
            If _brush.Color.A < 255 Then
                pe.Graphics.FillRectangle(_transparentBackgroundBrush, 2, 2, Width - 4, Height - 4)
            End If
            pe.Graphics.FillRectangle(_brush, 2, 2, Width - 4, Height - 4)
        End Sub
    End Class
End Namespace
