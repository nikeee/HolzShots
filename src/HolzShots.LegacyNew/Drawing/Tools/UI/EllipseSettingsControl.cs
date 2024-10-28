using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class EllipseSettingsControl : UserControl, ISettingsControl<EllipseSettings>
{
    private readonly EllipseSettings _settings;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public EllipseSettings Settings => _settings;

    public EllipseSettingsControl(EllipseSettings initialSettings)
    {
        InitializeComponent();
        _settings = initialSettings;

        EllipseDiameterTrackBar.Minimum = EllipseSettings.MinimumWidth;
        EllipseDiameterTrackBar.Maximum = EllipseSettings.MaximumWidth;

        EllipseDiameterTrackBar.ValueChanged += (_, _) =>
        {
            var v = EllipseDiameterTrackBar.Value;

            _settings.Width = v;

            EllipseDiameterTrackBarLabel.Text = $"Width: {(v == 0 ? "Auto" : v)}px";
        };

        ModeSelector.Minimum = EllipseSettings.EllipseValue;
        ModeSelector.Maximum = EllipseSettings.RectangleValue;
        ModeSelector.ValueChanged += (_, _) =>
        {
            var v = ModeSelector.Value == EllipseSettings.EllipseValue
                ? EllipseMode.Ellipse
                : EllipseMode.Rectangle;

            _settings.Mode = v;

            ModeSelectorLabel.Text = $"Mode: {(v == EllipseMode.Rectangle ? "Rectangle" : "Ellipse")}";
        };

        EllipseColorSelector.ColorChanged += (_, _) => _settings.Color = EllipseColorSelector.Color;

        EllipseDiameterTrackBar.Value = initialSettings.Width;
        EllipseColorSelector.Color = initialSettings.Color;
    }
}
