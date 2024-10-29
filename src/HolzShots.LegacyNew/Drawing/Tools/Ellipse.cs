using System.Drawing.Drawing2D;
using System.Numerics;
using HolzShots.Drawing.Tools.UI;

namespace HolzShots.Drawing.Tools;

public class Ellipse : ITool<EllipseSettings>
{
    public Vector2 BeginCoordinates { get; set; }
    public Vector2 EndCoordinates { get; set; }

    private static readonly Cursor _cursor = new(Properties.Resources.crossMedium.GetHicon());
    public Cursor Cursor { get; } = _cursor;
    public ShotEditorTool ToolType { get; } = ShotEditorTool.Ellipse;

    public ISettingsControl<EllipseSettings> SettingsControl { get; }

    public Ellipse()
    {
        SettingsControl = new EllipseSettingsControl(EllipseSettings.Default);
    }

    public void LoadInitialSettings()
    {
        if (Properties.Settings.Default.EllipseWidth > EllipseSettings.MaximumWidth || Properties.Settings.Default.EllipseWidth < EllipseSettings.MinimumWidth)
        {
            Properties.Settings.Default.EllipseWidth = EllipseSettings.Default.Width;
            Properties.Settings.Default.Save();
        }

        var settings = SettingsControl.Settings;
        settings.Color = Properties.Settings.Default.EllipseColor;
        settings.Width = Properties.Settings.Default.EllipseWidth;
        settings.Mode = (EllipseMode)Properties.Settings.Default.EllipseMode;
    }

    public void PersistSettings()
    {
        var settings = SettingsControl.Settings;
        settings.Color = Properties.Settings.Default.EllipseColor;
        settings.Width = Properties.Settings.Default.EllipseWidth;
        Properties.Settings.Default.EllipseMode = (int)settings.Mode;
    }

    private static Pen CreatePen(EllipseSettings settings)
    {
        return new Pen(settings.Color, settings.Width)
        {
            DashStyle = DashStyle.Solid
        };
    }

    public void RenderFinalImage(ref Image rawImage)
    {
        var settings = SettingsControl.Settings;

        var rect = Rectangle.Round(new RectangleF(
            Math.Min(EndCoordinates.X, BeginCoordinates.X),
            Math.Min(EndCoordinates.Y, BeginCoordinates.Y),
            Math.Max(Math.Abs(BeginCoordinates.X - EndCoordinates.X), settings.Width),
            Math.Max(Math.Abs(BeginCoordinates.Y - EndCoordinates.Y), settings.Width)
        ));

        using var g = Graphics.FromImage(rawImage);
        using var pen = CreatePen(settings);

        if (settings.Mode == EllipseMode.Rectangle)
        {
            g.DrawRectangle(pen, rect);
        }
        else
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawEllipse(pen, rect);
        }
    }

    public void RenderPreview(Image rawImage, Graphics g)
    {
        var settings = SettingsControl.Settings;

        var rect = Rectangle.Round(new RectangleF(
            Math.Min(EndCoordinates.X, BeginCoordinates.X),
            Math.Min(EndCoordinates.Y, BeginCoordinates.Y),
            Math.Max(Math.Abs(BeginCoordinates.X - EndCoordinates.X), settings.Width),
            Math.Max(Math.Abs(BeginCoordinates.Y - EndCoordinates.Y), settings.Width)
        ));

        using var pen = CreatePen(settings);
        if (settings.Mode == EllipseMode.Rectangle)
        {
            g.DrawRectangle(pen, rect);
        }
        else
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawEllipse(pen, rect);
        }
    }

    public void MouseOnlyMoved(Image rawImage, ref Cursor currentCursor, MouseEventArgs e) { }
    public void MouseClicked(Image rawImage, Vector2 e, ref Cursor currentCursor, Control trigger) { }
    public void Dispose() { }
}
