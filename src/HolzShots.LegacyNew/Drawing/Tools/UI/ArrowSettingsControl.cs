using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class ArrowSettingsControl : UserControl, ISettingsControl<ArrowSettings>
{
    private ArrowSettings _settings;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ArrowSettings Settings => _settings;

    public ArrowSettingsControl(ArrowSettings initialSettings)
    {
        InitializeComponent();
        _settings = initialSettings;

        ArrowDiameterTrackBar.Maximum = ArrowSettings.MaximumWidth;
        ArrowDiameterTrackBar.Minimum = ArrowSettings.MinimumWidth;

        ArrowDiameterTrackBar.ValueChanged += (_, _) =>
        {
            var v = ArrowDiameterTrackBar.Value;
            _settings.Width = v;
            ArrowDiameterTrackBarLabel.Text = $"Width: {(v == 0 ? "Auto" : v)}px";
        };
        ArrowColorSelector.ColorChanged += (_, _) => _settings.Color = ArrowColorSelector.Color;

        ArrowDiameterTrackBar.Value = initialSettings.Width;
        ArrowColorSelector.Color = initialSettings.Color;
    }
}

public class ArrowSettings(int width, Color color) : ToolSettingsBase
{
    public const int MinimumWidth = 0;
    public const int MaximumWidth = 100;

    public int Width { get; set; } = width;
    public Color Color { get; set; } = color;
}
