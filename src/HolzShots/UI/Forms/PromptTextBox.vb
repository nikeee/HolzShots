Imports System.ComponentModel
Imports System.Windows.Forms

Namespace UI.Windows.Forms
    Public Class PromptTextBox
        Inherits TextBox

        Private _description As String

        <Browsable(True), Description("Plceholder text"), Category("Appearance")>
        Public Property Prompt As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
                UpdateMessage()
            End Set
        End Property

        Public Sub New()
            MyBase.New()
            UpdateMessage()
        End Sub

        Private Sub UpdateMessage()
            If Not DesignMode Then
                Native.User32.SendMessage(Me.Handle, Native.WindowMessage.EM_SetCueBanner, IntPtr.Zero, _description)
            End If
        End Sub
    End Class
End Namespace
