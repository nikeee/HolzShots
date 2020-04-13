Imports System.Windows.Forms

Namespace UI.Windows.Forms
    Public Class NumericTextBox
        Inherits PromptTextBox
        Public Property AllowSpace() As Boolean

        ReadOnly _numberFormatInfo As Globalization.NumberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat
        ReadOnly _decimalSeparator As String = _numberFormatInfo.NumberDecimalSeparator
        ReadOnly _groupSeparator As String = _numberFormatInfo.NumberGroupSeparator
        ReadOnly _negativeSign As String = _numberFormatInfo.NegativeSign

        Protected Overrides Sub OnKeyPress(ByVal e As KeyPressEventArgs)
            If e Is Nothing Then Throw New ArgumentNullException(NameOf(e))

            MyBase.OnKeyPress(e)

            Dim keyInput As String = e.KeyChar.ToString()

            If Char.IsDigit(e.KeyChar) Then
            ElseIf keyInput.Equals(_decimalSeparator) OrElse keyInput.Equals(_groupSeparator) OrElse keyInput.Equals(_negativeSign) Then
                e.Handled = True
            ElseIf Char.IsControl(e.KeyChar) Then
            ElseIf AllowSpace AndAlso e.KeyChar = " "c Then
            Else
                e.Handled = True
            End If

        End Sub

        Public ReadOnly Property IntValue As Integer
            Get
                Return If(String.IsNullOrEmpty(Text), 0, Integer.Parse(Text))
            End Get
        End Property

        Public ReadOnly Property LongValue As Long
            Get
                Return If(String.IsNullOrEmpty(Text), 0, Long.Parse(Text))
            End Get
        End Property

        Public ReadOnly Property DecimalValue As Decimal
            Get
                Return If(String.IsNullOrEmpty(Text), 0, Decimal.Parse(Text))
            End Get
        End Property
    End Class
End Namespace
