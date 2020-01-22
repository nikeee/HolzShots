Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace UI.Windows.Forms
    Public Class InfoPanelLabel
        Inherits Label
        Private _viewMode As InfoMode

        <Category("Appearance")>
        Public Property ViewMode As InfoMode
            Get
                Return _viewMode
            End Get
            Set(ByVal value As InfoMode)
                _viewMode = value
                UpdateViewMode()
            End Set
        End Property

        Protected Overrides ReadOnly Property DefaultPadding As Padding
            Get
                Return Padding.Empty
            End Get
        End Property

        Public Sub New()
            MyBase.New()
            UpdateViewMode()
        End Sub

        Enum InfoMode
            Label
            Information
            Headline
        End Enum

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
        End Sub

        Private Sub UpdateViewMode()
            Select Case _viewMode
                Case InfoMode.Headline
                    Font = New Font("Segeo UI", 9.5, FontStyle.Regular, GraphicsUnit.Point)
                    ForeColor = Color.FromArgb(250, 0, 0, 0)
                Case InfoMode.Information
                    Font = New Font("Segeo UI", 9, FontStyle.Regular, GraphicsUnit.Point)
                    ForeColor = Color.FromArgb(255, 30, 57, 91)
                Case InfoMode.Label
                    Font = New Font("Segeo UI", 9, FontStyle.Regular, GraphicsUnit.Point)
                    ForeColor = Color.FromArgb(255, 90, 103, 144)
            End Select
        End Sub
    End Class
End Namespace
