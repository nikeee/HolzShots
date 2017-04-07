Imports System.Globalization

Namespace UI.Controls
    Friend Class NumericTextBox
        Inherits TextBox
        ReadOnly _numberFormatInfo As NumberFormatInfo = CultureInfo.CurrentCulture.NumberFormat
        ReadOnly _decimalSeparator As String = _numberFormatInfo.NumberDecimalSeparator
        ReadOnly _groupSeparator As String = _numberFormatInfo.NumberGroupSeparator
        ReadOnly _negativeSign As String = _numberFormatInfo.NegativeSign

        Protected Overrides Sub OnKeyPress(ByVal e As KeyPressEventArgs)
            MyBase.OnKeyPress(e)

            Dim keyInput As String = e.KeyChar.ToString()

            If Char.IsDigit(e.KeyChar) Then
            ElseIf _
                keyInput.Equals(_decimalSeparator) OrElse keyInput.Equals(_groupSeparator) OrElse
                keyInput.Equals(_negativeSign) Then
                e.Handled = True
            ElseIf Char.IsControl(e.KeyChar) Then ' ElseIf e.KeyChar = vbBack Then

            ElseIf AllowSpace AndAlso e.KeyChar = " "c Then
            Else
                e.Handled = True
            End If
        End Sub


        Public ReadOnly Property IntValue() As Integer
            Get
                Return If(Text.Trim = String.Empty, 0, Integer.Parse(Text))
            End Get
        End Property
        Public ReadOnly Property DecimalValue() As Decimal
            Get
                Return If(Text.Trim = String.Empty, 0, Decimal.Parse(Text))
            End Get
        End Property

        Public Property AllowSpace As Boolean = False
    End Class
End Namespace
