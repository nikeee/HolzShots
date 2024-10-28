namespace HolzShots.Drawing.Tools;

public class EraserSettings(int diameter) : ToolSettingsBase
{
    public const int MinimumDiameter = 5;
    public const int MaximumDiameter = 30;

    public int Diameter { get; set; } = diameter;

    public static EraserSettings Default => new(20);
}
