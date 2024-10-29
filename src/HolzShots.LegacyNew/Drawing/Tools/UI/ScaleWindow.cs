using System.ComponentModel;
using HolzShots.UI;

namespace HolzShots.Drawing.Tools.UI;

public partial class ScaleWindow : Form
{
    private readonly Image _img;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ScaleUnit CurrentScaleUnit { get; set; } = ScaleUnit.Percent;

    public double WidthBoxV => (double)WidthBox.DecimalValue;
    public double HeightBoxV => (double)HeightBox.DecimalValue;

    public ScaleWindow(Image img)
    {
        _img = img;
        InitializeComponent();
    }

    private void ScaleWindow_Load(object sender, EventArgs e)
    {
        HeightBox.Text = "100";
        WidthBox.Text = "100";
    }

    private void okButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }
    private void cancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void WidthBox_TextChanged(object sender, EventArgs e)
    {
        if (!KeepAspectRatio.Checked || !WidthBox.Focused || HeightBox.IntValue <= 0)
            return;

        if (CurrentScaleUnit == ScaleUnit.Pixel)
        {
            double a = (double)_img.Width / _img.Height;
            int b = (int)(WidthBox.IntValue / a);
            HeightBox.Text = (b > 0 ? (b > 999999 ? 999999 : b) : 1).ToString();
        }
        else
        {
            if (WidthBox.IntValue == HeightBox.IntValue)
                return;
            HeightBox.Text = WidthBox.IntValue.ToString();
        }
    }

    private void HeightBox_TextChanged(object sender, EventArgs e)
    {
        if (!KeepAspectRatio.Checked || !HeightBox.Focused || WidthBox.IntValue <= 0)
            return;

        if (CurrentScaleUnit == ScaleUnit.Pixel)
        {
            double a = (double)_img.Width / _img.Height;
            int b = (int)(HeightBox.IntValue * a);
            WidthBox.Text = (b > 0 ? (b > 999999 ? 999999 : b) : 1).ToString();
        }
        else
        {
            if (WidthBox.IntValue == HeightBox.IntValue)
                return;
            WidthBox.Text = HeightBox.IntValue.ToString();
        }
    }

    private void KeepAspectRatio_CheckedChanged(object sender, EventArgs e)
    {
        if (Pixel.Checked)
        {
            CurrentScaleUnit = ScaleUnit.Pixel;
            WidthBox.Text = _img.Width.ToString();
            HeightBox.Text = _img.Height.ToString();
            UnitLabel1.Text = Localization.PixelUnit;
            UnitLabel2.Text = Localization.PixelUnit;
        }
        else
        {
            CurrentScaleUnit = ScaleUnit.Percent;
            WidthBox.Text = "100";
            HeightBox.Text = "100";
            UnitLabel1.Text = Localization.PercentUnit;
            UnitLabel2.Text = Localization.PercentUnit;
        }
    }
}

public enum ScaleUnit
{
    Percent,
    Pixel
}
