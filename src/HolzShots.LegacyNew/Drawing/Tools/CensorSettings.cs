﻿namespace HolzShots.Drawing.Tools;

public class CensorSettings(int width, Color color) : ToolSettingsBase
{
    public int Width { get; set; } = width;
    public Color Color { get; set; } = color;
}
