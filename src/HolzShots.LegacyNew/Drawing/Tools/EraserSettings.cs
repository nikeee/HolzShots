namespace HolzShots.Drawing.Tools;

public class EraserSettings(int diameter) : ToolSettingsBase
{
    public int Diameter { get; init; } = diameter;
}
