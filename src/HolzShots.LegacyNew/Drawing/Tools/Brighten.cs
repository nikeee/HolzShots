using System.Numerics;
using HolzShots.Drawing.Tools.UI;

namespace HolzShots.Drawing.Tools;

public class Brighten : ITool<BrightnessSettings>
{
    private static readonly Cursor CursorInstance = new Cursor(Properties.Resources.crossMedium.GetHicon());
    public Cursor Cursor { get; } = CursorInstance;
    public ShotEditorTool ToolType { get; } = ShotEditorTool.Brighten;
    public ISettingsControl<BrightnessSettings> SettingsControl { get; } = new BrightnessSettingsControl(BrightnessSettings.Default);

    public Vector2 BeginCoordinates { get; set; }
    public Vector2 EndCoordinates { get; set; }

    public void LoadInitialSettings()
    {
        var settings = SettingsControl.Settings;
        settings.Brightness = Properties.Settings.Default.BrightenFactor;
    }

    public void PersistSettings()
    {
        var settings = SettingsControl.Settings;
        Properties.Settings.Default.BrightenFactor = settings.Brightness;
    }


    private static Brush CreateBrush(BrightnessSettings settings) => new SolidBrush(settings.BrightnessColor);

    public void RenderFinalImage(ref Image rawImage)
    {
        var rct = Rectangle.Round(new RectangleF(
            Math.Min(BeginCoordinates.X, EndCoordinates.X),
            Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
            Math.Abs(BeginCoordinates.X - EndCoordinates.X),
            Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
        ));

        using var gr = Graphics.FromImage(rawImage);
        using var brush = CreateBrush(SettingsControl.Settings);
        gr.FillRectangle(brush, rct);
    }

    public void RenderPreview(Image rawImage, Graphics g)
    {
        var rct = Rectangle.Round(new RectangleF(
            Math.Min(BeginCoordinates.X, EndCoordinates.X),
            Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
            Math.Abs(BeginCoordinates.X - EndCoordinates.X),
            Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
        ));

        using var brush = CreateBrush(SettingsControl.Settings);
        g.FillRectangle(brush, rct);
    }

    public void MouseOnlyMoved(Image rawImage, ref Cursor currentCursor, MouseEventArgs e) { }
    public void MouseClicked(Image rawImage, Vector2 e, ref Cursor currentCursor, Control trigger) { }
    public void Dispose() { }
}
