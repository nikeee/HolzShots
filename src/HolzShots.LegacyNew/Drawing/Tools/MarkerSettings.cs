namespace HolzShots.Drawing.Tools;

public class MarkerSettings(int width, Color color) : ToolSettingsBase
{
    public int Width { get; set; } = width;
    public Color Color { get; set; } = color;
}
