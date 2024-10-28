using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class CensorSettingsControl : UserControl, ISettingsControl<CensorSettings>
{
    private readonly CensorSettings _settings;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CensorSettings Settings => _settings;

    public CensorSettingsControl(CensorSettings initialSettings)
    {
        InitializeComponent();
        _settings = initialSettings;

        CensorDiameterTrackBar.ValueChanged += (_, _) =>
        {
            _settings.Width = CensorDiameterTrackBar.Value;
            CensorDiameterTrackBarLabel.Text = $"Width: {CensorDiameterTrackBar.Value}px";
        };
        CensorColorSelector.ColorChanged += (_, _) => _settings.Color = CensorColorSelector.Color;

        CensorDiameterTrackBar.Value = initialSettings.Width;
        CensorColorSelector.Color = initialSettings.Color;
    }
}
