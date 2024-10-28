namespace HolzShots.Drawing.Tools;

public class CensorSettings(int width, Color color) : ToolSettingsBase
{
    public const int MinimumWidth = 1;
    public const int MaximumWidth = 100;

    public int Width { get; set; } = width;
    public Color Color { get; set; } = color;

    public static CensorSettings Default => new(20, Color.Red);
}
