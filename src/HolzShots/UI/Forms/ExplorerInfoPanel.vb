Imports System.Drawing
Imports System.Windows.Forms

Namespace UI.Forms

    Public Class ExplorerInfoPanel
        Inherits Panel

        Private ReadOnly _upperpen As New Pen(Color.FromArgb(204, 217, 234))
        Private ReadOnly _secondpen As New Pen(Color.FromArgb(217, 227, 240))
        Private ReadOnly _thirdpen As New Pen(Color.FromArgb(232, 238, 247))

        Private ReadOnly _gradientColor1 As Color = Color.FromArgb(237, 242, 249)
        Private ReadOnly _gradientColor2 As Color = Color.FromArgb(241, 245, 251)
        Private ReadOnly _upperBrushPoint As New Point(0, 3)

        Public Overrides Property BackColor As Color = Color.Transparent

        Public Sub New()
            MyBase.New()
            Font = New Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point)
            ForeColor = Color.FromArgb(255, 30, 57, 91)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            If Enabled Then
                If Width > 0 AndAlso Height > 4 Then
                    Select Case Dock
                        Case DockStyle.Bottom
                            DrawBottom(e.Graphics)
                        Case DockStyle.Top
                            DrawTop(e.Graphics)
                        Case DockStyle.Fill
                            DrawFill(e.Graphics)
                        Case DockStyle.Left
                            DrawLeft(e.Graphics)
                        Case DockStyle.Right
                            DrawRight(e.Graphics)
                        Case DockStyle.None
                            DrawFill(e.Graphics)
                    End Select
                End If
            Else
                e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(240, 240, 240)), DisplayRectangle)
            End If
            MyBase.OnPaint(e)
        End Sub

#Region "DrawStuff"

        Private Function CreateBrushTopDock() As Brush
            Return New Drawing2D.LinearGradientBrush(_upperBrushPoint, New Point(0, MyBase.Height - 3), _gradientColor2, _gradientColor1)
        End Function
        Private Function CreateBrushBottomDock() As Brush
            Return New Drawing2D.LinearGradientBrush(_upperBrushPoint, New Point(0, MyBase.Height - 3), _gradientColor1, _gradientColor2)
        End Function
        Private Function CreateBrushFillDock() As Brush
            Return New Drawing2D.LinearGradientBrush(_upperBrushPoint, New Point(MyBase.Width, MyBase.Height), _gradientColor1, _gradientColor2)
        End Function
        Private Function CreateBrushLeftDock() As Brush
            Return New Drawing2D.LinearGradientBrush(_upperBrushPoint, New Point(MyBase.Width, MyBase.Height), _gradientColor1, _gradientColor2)
        End Function
        Private Function CreateBrushRightDock() As Brush
            Return New Drawing2D.LinearGradientBrush(_upperBrushPoint, New Point(MyBase.Width, MyBase.Height), _gradientColor2, _gradientColor1)
        End Function

        Private Sub DrawBottom(ByVal g As Graphics)
            With g
                If MyBase.Height > 6 Then
                    .FillRectangle(CreateBrushTopDock, 0, 3, MyBase.Width, MyBase.Height - 3)
                End If
                .DrawLine(_upperpen, 0, 0, MyBase.Width, 0)
                .DrawLine(_secondpen, 0, 1, MyBase.Width, 1)
                .DrawLine(_thirdpen, 0, 2, MyBase.Width, 2)
            End With
        End Sub
        Private Sub DrawTop(ByVal g As Graphics)
            With g
                .FillRectangle(CreateBrushBottomDock, 0, 0, MyBase.Width, MyBase.Height - 2)
                .DrawLine(_thirdpen, 0, MyBase.Height - 3, MyBase.Width, MyBase.Height - 3)
                .DrawLine(_secondpen, 0, MyBase.Height - 2, MyBase.Width, MyBase.Height - 2)
                .DrawLine(_upperpen, 0, MyBase.Height - 1, MyBase.Width, MyBase.Height - 1)
            End With
        End Sub
        Private Sub DrawFill(ByVal g As Graphics)
            With g
                .FillRectangle(CreateBrushFillDock, 0, 0, MyBase.Width, MyBase.Height)
                .DrawRectangle(_upperpen, 0, 0, MyBase.Width - 1, MyBase.Height - 1)
                .DrawRectangle(_secondpen, 1, 1, MyBase.Width - 3, MyBase.Height - 3)
                .DrawRectangle(_thirdpen, 2, 2, MyBase.Width - 5, MyBase.Height - 5)
            End With
        End Sub
        Private Sub DrawLeft(ByVal g As Graphics)
            With g
                .FillRectangle(CreateBrushLeftDock, 0, 0, MyBase.Width - 3, MyBase.Height - 1)
                .DrawLine(_upperpen, MyBase.Width - 1, 0, MyBase.Width - 1, MyBase.Height - 1)
                .DrawLine(_secondpen, MyBase.Width - 2, 0, MyBase.Width - 2, MyBase.Height - 1)
                .DrawLine(_thirdpen, MyBase.Width - 3, 0, MyBase.Width - 3, MyBase.Height - 1)
            End With
        End Sub
        Private Sub DrawRight(ByVal g As Graphics)
            With g
                .FillRectangle(CreateBrushRightDock, 0, 0, MyBase.Width, MyBase.Height)
                .DrawLine(_upperpen, 0, 0, 0, MyBase.Height)
                .DrawLine(_secondpen, 1, 0, 1, MyBase.Height)
                .DrawLine(_thirdpen, 2, 0, 2, MyBase.Height)
            End With
        End Sub

#End Region
    End Class
End Namespace
