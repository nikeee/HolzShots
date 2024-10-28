namespace HolzShots.Drawing.Tools;

public class ArrowSettings(int width, Color color) : ToolSettingsBase
{
    public const int MinimumWidth = 0;
    public const int MaximumWidth = 100;

    public int Width { get; set; } = width;
    public Color Color { get; set; } = color;
}
