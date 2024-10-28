using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class BrightnessSettingsControl : UserControl, ISettingsControl<BrightnessSettings>
{
    private BrightnessSettings _settings;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BrightnessSettings Settings => _settings;

    public BrightnessSettingsControl(BrightnessSettings initialSettings)
    {
        InitializeComponent();
        _settings = initialSettings;

        BrightnessTrackBar.Minimum = BrightnessSettings.MinimumBrightness;
        BrightnessTrackBar.Maximum = BrightnessSettings.MaximumBrightness;

        BrightnessTrackBar.ValueChanged += (_, _) =>
        {
            var v = BrightnessTrackBar.Value;
            _settings.Brightness = v;
            BrightnessPreview.Color = _settings.BrightnessColor;
        };

        BrightnessTrackBar.Value = initialSettings.Brightness;
    }
}

public class BrightnessSettings(int brightness) : ToolSettingsBase
{
    public const int MinimumBrightness = 0;
    public const int MaximumBrightness = 255 * 2;

    public int Brightness { get; set; } = brightness;
    public Color BrightnessColor => Brightness <= 255
                ? Color.FromArgb(255 - Brightness, 0, 0, 0)
                : Color.FromArgb(Brightness - 255, 255, 255, 255);
}
