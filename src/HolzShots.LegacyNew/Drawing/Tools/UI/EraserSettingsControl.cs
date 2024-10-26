using System.ComponentModel;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class EraserSettingsControl : UserControl
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public EraserSettings Settings
    {
        get => new(EraserDiameterTrackBar.Value);
        set => EraserDiameterTrackBar.Value = value.Diameter;
    }

    public EraserSettingsControl()
    {
        InitializeComponent();
    }
}
