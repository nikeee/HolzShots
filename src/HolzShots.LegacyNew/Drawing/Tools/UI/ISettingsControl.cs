namespace HolzShots.Drawing.Tools.UI;

public interface ISettingsControl<out TSettings> : IDisposable where TSettings : ToolSettingsBase
{
    TSettings Settings { get; }
}
