using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class ArrowSettingsControl : UserControl, ISettingsControl<ArrowSettings>
{
    private readonly ArrowSettings _settings;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ArrowSettings Settings => _settings;

    public ArrowSettingsControl(ArrowSettings initialSettings)
    {
        InitializeComponent();
        _settings = initialSettings;

        ArrowDiameterTrackBar.Minimum = ArrowSettings.MinimumWidth;
        ArrowDiameterTrackBar.Maximum = ArrowSettings.MaximumWidth;

        ArrowDiameterTrackBar.ValueChanged += (_, _) =>
        {
            var v = ArrowDiameterTrackBar.Value;
            _settings.Width = v;
            ArrowDiameterTrackBarLabel.Text = $"Width: " + (v == 0 ? "Auto" : $"{v}px");
        };
        ArrowColorSelector.ColorChanged += (_, _) => _settings.Color = ArrowColorSelector.Color;

        ArrowDiameterTrackBar.Value = initialSettings.Width;
        ArrowColorSelector.Color = initialSettings.Color;
    }
}
