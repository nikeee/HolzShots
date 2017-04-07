Imports System.ComponentModel
Imports System.Windows.Forms.Layout

Namespace UI.Controls

    Public Class StackPanel
        Inherits Panel
        Public Sub New()
            MyBase.AutoScroll = True
        End Sub

        <DefaultValue(True)>
        Public Shadows Property AutoScroll As Boolean
            Get
                Return MyBase.AutoScroll
            End Get
            Set(value As Boolean)
                MyBase.AutoScroll = value
            End Set
        End Property

        Public Overrides ReadOnly Property LayoutEngine As LayoutEngine
            Get
                Return StackLayout.Instance
            End Get
        End Property

        Private Class StackLayout
            Inherits LayoutEngine
            Friend Shared ReadOnly Instance As StackLayout

            Shared Sub New()
                Instance = New StackLayout()
            End Sub

            Public Overrides Function Layout(container As Object, layoutEventArgs As LayoutEventArgs) As Boolean
                Dim stackPanel As StackPanel = TryCast(container, StackPanel)
                If stackPanel Is Nothing Then
                    Return False
                End If

                ' Use DisplayRectangle so that parent.Padding is honored.
                Dim displayRectangle As Rectangle = stackPanel.DisplayRectangle
                Dim nextControlLocation As Point = displayRectangle.Location

                For Each control As Control In stackPanel.Controls
                    ' Only apply layout to visible controls
                    If control.Visible = False Then
                        Continue For
                    End If

                    ' Respect the margin of the control: shift over the left and the top.
                    nextControlLocation.Offset(control.Margin.Left, control.Margin.Top)

                    ' Adjust control's Location and Size
                    control.Location = nextControlLocation
                    Dim size As Size = control.GetPreferredSize(displayRectangle.Size)
                    If Not control.AutoSize Then
                        size.Width = displayRectangle.Width - control.Margin.Left - control.Margin.Right
                    End If
                    control.Size = size

                    ' Move X back to the display rectangle origin.
                    nextControlLocation.X = displayRectangle.X

                    ' Increment Y by the height of the control and the bottom margin.
                    nextControlLocation.Y += control.Height + control.Margin.Bottom
                Next

                ' Adjust width of control to accomodate vertical scrollbar if necessary
                If stackPanel.AutoScroll AndAlso nextControlLocation.Y > displayRectangle.Height Then
                    displayRectangle.Width -= SystemInformation.VerticalScrollBarWidth
                    For Each control As Control In stackPanel.Controls
                        If control.Visible Then
                            Dim size As Size = control.GetPreferredSize(displayRectangle.Size)
                            If Not control.AutoSize Then
                                size.Width = displayRectangle.Width - control.Margin.Left - control.Margin.Right
                            End If
                            control.Size = size
                        End If
                    Next
                End If

                Return False
            End Function
        End Class
    End Class
End Namespace
