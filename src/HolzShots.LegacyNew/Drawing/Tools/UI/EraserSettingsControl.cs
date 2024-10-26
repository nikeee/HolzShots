using System.ComponentModel;
using System.Runtime;

namespace HolzShots.Drawing.Tools.UI;

[DefaultBindingProperty("Settings")]
public partial class EraserSettingsControl : SettingsControl<EraserSettings>
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

    public EraserSettingsControl(EraserSettings initialSettings) : base(initialSettings)
    {
        InitializeComponent();
        EraserDiameterTrackBar.ValueChanged += (_, _) =>
        {
            OnSettingsUpdated?.Invoke(this, new(EraserDiameterTrackBar.Value));
        };
    }

    public override event EventHandler<EraserSettings>? OnSettingsUpdated;
}


public abstract class SettingsControl<TSettings>(TSettings InitialSettings) : UserControl() where TSettings : struct
{
    public abstract event EventHandler<TSettings>? OnSettingsUpdated;
}

public readonly record struct EraserSettings(int Diameter);
