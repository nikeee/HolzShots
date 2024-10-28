using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class MarkerSettingsControl : UserControl, ISettingsControl<MarkerSettings>
{
    private MarkerSettings _settings;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public MarkerSettings Settings => _settings;

    public MarkerSettingsControl(MarkerSettings initialSettings)
    {
        InitializeComponent();
        _settings = initialSettings;

        MarkerDiameterTrackBar.ValueChanged += (_, _) =>
        {
            _settings.Width = MarkerDiameterTrackBar.Value;
            MarkerDiameterTrackBarLabel.Text = $"Width: {MarkerDiameterTrackBar.Value}px";
        };
        MarkerColorSelector.ColorChanged += (_, _) => _settings.Color = MarkerColorSelector.Color;

        MarkerDiameterTrackBar.Value = initialSettings.Width;
        MarkerColorSelector.Color = initialSettings.Color;
    }
}

public class MarkerSettings(int width, Color color) : ToolSettingsBase
{
    public int Width { get; set; } = width;
    public Color Color { get; set; } = color;
}
