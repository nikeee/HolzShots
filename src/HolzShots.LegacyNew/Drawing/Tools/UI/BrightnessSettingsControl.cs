using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class BrightnessSettingsControl : UserControl, ISettingsControl<BrightnessSettings>
{
    private readonly BrightnessSettings _settings;
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
