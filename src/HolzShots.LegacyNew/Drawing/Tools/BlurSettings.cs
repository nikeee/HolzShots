namespace HolzShots.Drawing.Tools;

public class BlurSettings(int diameter) : ToolSettingsBase
{
    public const int MinimumDiameter = 5;
    public const int MaximumDiameter = 30;
    public int Diameter { get; set; } = diameter;

    public static BlurSettings Default => new(7);
}
