namespace HolzShots.Drawing.Tools;

public class MarkerSettings(int width, Color color) : ToolSettingsBase
{
    public const int MinimumWidth = 1;
    public const int MaximumWidth = 100;
    public int Width { get; set; } = width;
    public Color Color { get; set; } = color;

    public static MarkerSettings Default => new(20, Color.Yellow);
}
