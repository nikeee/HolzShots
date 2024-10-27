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
        EraserDiameterTrackBar.ValueChanged += (_, _) =>
        {
            OnSettingsUpdated?.Invoke(this, new(EraserDiameterTrackBar.Value));
        };
    }

    public event EventHandler<EraserSettings>? OnSettingsUpdated;
    // public static ISettingsControl<EraserSettings> Create(EraserSettings initialSettings) => new EraserSettingsControl(initialSettings);
}


public interface ISettingsControl<out TSettings> : IDisposable where TSettings : ToolSettingsBase
{
    TSettings Settings { get; }
    // public abstract event EventHandler<TSettings>? OnSettingsUpdated;
    // static abstract ISettingsControl<TSettings> Create(TSettings initialSettings);
}

public abstract record ToolSettingsBase;
public record EraserSettings(int Diameter) : ToolSettingsBase;
