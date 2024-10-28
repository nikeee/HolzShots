namespace HolzShots.Drawing.Tools;

public class EllipseSettings(int width, Color color, EllipseMode mode) : ToolSettingsBase
{
    public const int EllipseValue = 0;
    public const int RectangleValue = 1;

    public const int MinimumWidth = 0;
    public const int MaximumWidth = 100;

    public int Width { get; set; } = width;
    public Color Color { get; set; } = color;
    public EllipseMode Mode { get; set; } = mode;
}

public enum EllipseMode
{
    Ellipse,
    Rectangle,
}
