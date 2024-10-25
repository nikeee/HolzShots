using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;

namespace HolzShots.Windows.Forms.Controls;

public class NumericTextBox : TextBox
{
    private static readonly NumberFormatInfo _numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
    private static readonly string _decimalSeparator = _numberFormatInfo.NumberDecimalSeparator;
    private static readonly string _groupSeparator = _numberFormatInfo.NumberGroupSeparator;
    private static readonly string _negativeSign = _numberFormatInfo.NegativeSign;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowSpace { get; set; }
    public int IntValue => Text.Trim() == string.Empty ? 0 : int.Parse(Text);
    public decimal DecimalValue => Text.Trim() == string.Empty ? 0 : decimal.Parse(Text);

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        base.OnKeyPress(e);

        var keyInput = e.KeyChar.ToString();
        if (keyInput == null)
            return;

        if (char.IsDigit(e.KeyChar)) { }
        else if (keyInput.Equals(_decimalSeparator) || keyInput.Equals(_groupSeparator) || keyInput.Equals(_negativeSign))
        {
            e.Handled = true;
        }
        else if (char.IsControl(e.KeyChar)) { }
        else if (AllowSpace && e.KeyChar == ' ')
        {
            e.Handled = true;
        }
    }
}
