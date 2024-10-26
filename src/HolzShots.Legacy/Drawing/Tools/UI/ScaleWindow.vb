Imports System.ComponentModel
Imports HolzShots.UI

Namespace Drawing.Tools.UI

    Friend Class ScaleWindow
        Private ReadOnly _img As Image

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property CurrentScaleUnit As ScaleUnit = ScaleUnit.Percent
        Public ReadOnly Property WidthBoxV As Double
            Get
                Return WidthBox.DecimalValue
            End Get
        End Property
        Public ReadOnly Property HeightBoxV As Double
            Get
                Return HeightBox.DecimalValue
            End Get
        End Property

        Friend Sub New(img As Image)
            _img = img
            InitializeComponent()
        End Sub

        Private Sub ScaleWindowLoad(sender As Object, e As EventArgs) Handles MyBase.Load
            HeightBox.Text = "100"
            WidthBox.Text = "100"
        End Sub

        Private Sub PixelCheckedChanged(sender As Object, e As EventArgs) Handles Pixel.CheckedChanged
            If Pixel.Checked Then
                CurrentScaleUnit = ScaleUnit.Pixel
                WidthBox.Text = _img.Width.ToString
                HeightBox.Text = _img.Height.ToString
                UnitLabel1.Text = Localization.PixelUnit
                UnitLabel2.Text = Localization.PixelUnit
            Else
                CurrentScaleUnit = ScaleUnit.Percent
                WidthBox.Text = "100"
                HeightBox.Text = "100"
                UnitLabel1.Text = Localization.PercentUnit
                UnitLabel2.Text = Localization.PercentUnit
            End If
        End Sub

        Enum ScaleUnit
            Percent
            Pixel
        End Enum

        Private Sub HeightBoxValueChanged(sender As Object, e As EventArgs) Handles HeightBox.TextChanged
            If KeepAspectRatio.Checked AndAlso HeightBox.Focused AndAlso WidthBox.IntValue > 0 Then
                If CurrentScaleUnit = ScaleUnit.Pixel Then
                    Dim a As Double = _img.Width / _img.Height
                    Dim b As Integer = CInt(HeightBox.IntValue * a)
                    WidthBox.Text = If(b > 0, If(b > 999999, 999999, b), 1).ToString
                Else
                    If WidthBox.IntValue = HeightBox.IntValue Then Exit Sub
                    WidthBox.Text = HeightBox.IntValue.ToString
                End If
            End If
        End Sub

        Private Sub WidthBoxValueChanged(sender As Object, e As EventArgs) Handles WidthBox.TextChanged
            If KeepAspectRatio.Checked AndAlso WidthBox.Focused AndAlso HeightBox.IntValue > 0 Then
                If CurrentScaleUnit = ScaleUnit.Pixel Then
                    Dim a As Double = _img.Width / _img.Height
                    Dim b As Integer = CInt(WidthBox.IntValue / a)
                    HeightBox.Text = If(b > 0, If(b > 999999, 999999, b), 1).ToString
                Else
                    If WidthBox.IntValue = HeightBox.IntValue Then Exit Sub
                    HeightBox.Text = WidthBox.IntValue.ToString
                End If
            End If
        End Sub

        Private Sub okButton_Click(sender As Object, e As EventArgs) Handles okButton.Click
            DialogResult = DialogResult.OK
        End Sub

        Private Sub cancelButton_Click(sender As Object, e As EventArgs) Handles cnclButton.Click
            DialogResult = DialogResult.Cancel
        End Sub
    End Class
End Namespace
