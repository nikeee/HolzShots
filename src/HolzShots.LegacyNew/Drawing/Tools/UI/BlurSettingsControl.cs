using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class BlurSettingsControl : UserControl, ISettingsControl<BlurSettings>
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BlurSettings Settings
    {
        get => new(BlurDiameterTrackBar.Value);
        set
        {
            BlurDiameterTrackBar.Value = value.Diameter;
        }
    }

    public BlurSettingsControl(BlurSettings initialSettings)
    {
        InitializeComponent();
        Settings = initialSettings;
        BlurDiameterTrackBar.ValueChanged += (_, _) =>
        {
            BlurDiameterTrackBarLabel.Text = $"Factor: {BlurDiameterTrackBar.Value}px";
        };

        BlurDiameterTrackBar.Value = initialSettings.Diameter;
    }
}

public class BlurSettings(int diameter) : ToolSettingsBase
{
    public int Diameter { get; init; } = diameter;
}
