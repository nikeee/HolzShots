using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class EraserSettingsControl : UserControl, ISettingsControl<EraserSettings>
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public EraserSettings Settings
    {
        get => new(EraserDiameterTrackBar.Value);
        set
        {
            EraserDiameterTrackBar.Value = value.Diameter;
        }
    }

    public EraserSettingsControl(EraserSettings initialSettings)
    {
        InitializeComponent();
        Settings = initialSettings;
        EraserDiameterTrackBar.ValueChanged += (_, _) =>
        {
            EraserDiameterTrackBarLabel.Text = $"{EraserDiameterTrackBar.Value}px";
        };
    }
}
public class EraserSettings(int diameter) : ToolSettingsBase
{
    public int Diameter { get; init; } = diameter;
}
