namespace HolzShots.Drawing.Tools;

public class BrightnessSettings(int brightness) : ToolSettingsBase
{
    public const int MinimumBrightness = 0;
    public const int MaximumBrightness = 255 * 2;

    public int Brightness { get; set; } = brightness;
    public Color BrightnessColor => Brightness <= 255
                ? Color.FromArgb(255 - Brightness, 0, 0, 0)
                : Color.FromArgb(Brightness - 255, 255, 255, 255);
}
