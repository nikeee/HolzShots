using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class BlurSettingsControl : UserControl, ISettingsControl<BlurSettings>
{
    private readonly BlurSettings _settings;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BlurSettings Settings => _settings;

    public BlurSettingsControl(BlurSettings initialSettings)
    {
        InitializeComponent();
        _settings = initialSettings;

        BlurDiameterTrackBar.Minimum = BlurSettings.MinimumDiameter;
        BlurDiameterTrackBar.Maximum = BlurSettings.MaximumDiameter;

        BlurDiameterTrackBar.ValueChanged += (_, _) =>
        {
            var v = BlurDiameterTrackBar.Value;
            _settings.Diameter = v;
            BlurDiameterTrackBarLabel.Text = $"Factor: {v}px";
        };

        BlurDiameterTrackBar.Value = initialSettings.Diameter;
    }
}
