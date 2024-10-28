using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class EraserSettingsControl : UserControl, ISettingsControl<EraserSettings>
{
    private readonly EraserSettings _settings;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public EraserSettings Settings => _settings;

    public EraserSettingsControl(EraserSettings initialSettings)
    {
        InitializeComponent();
        _settings = initialSettings;
        EraserDiameterTrackBar.ValueChanged += (_, _) =>
        {
            var v = EraserDiameterTrackBar.Value;
            _settings.Diameter = v;
            EraserDiameterTrackBarLabel.Text = $"Diameter: {v}px";
        };

        EraserDiameterTrackBar.Value = initialSettings.Diameter;
    }
}
