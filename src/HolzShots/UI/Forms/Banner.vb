Imports System.ComponentModel

Namespace UI.Forms

    Public Class Banner

        Private Const BannerHeight As Integer = 25
        Private Shared ReadOnly BackgroundBrush As SolidBrush = New SolidBrush(Color.FromArgb(255, 255, 255, 198))
        Private Shared ReadOnly BorderPen As Pen = New Pen(Color.FromArgb(171, 175, 218))

        <Browsable(True)>
        Public Overrides Property Text As String
            Get
                Return textLabel.Text
            End Get
            Set(value As String)
                textLabel.Text = value
            End Set
        End Property

        <Browsable(True)>
        Public Property LockHeight As Boolean

        <Browsable(True)>
        Public Overloads Property Height As Integer
            Get
                Return MyBase.Height
            End Get
            Set(value As Integer)
                MyBase.Height = If(LockHeight, BannerHeight, value)
            End Set
        End Property

        Private _image As Bitmap

        <Browsable(True)>
        Public Property Image As Bitmap
            Get
                Return _image
            End Get
            Set(value As Bitmap)
                If value Is Nothing Then Return
                If _image Is Nothing OrElse _image IsNot value Then
                    _image = value
                    Invalidate()
                End If
            End Set
        End Property

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            With e.Graphics
                .FillRectangle(BackgroundBrush, New Rectangle(0, 0, ClientSize.Width, ClientSize.Height))
                .DrawRectangle(BorderPen, New Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1))
                If _image IsNot Nothing Then
                    .DrawImage(_image, 5, 5, 16, 16)
                End If
            End With
        End Sub
    End Class
End Namespace
