using System.Drawing.Drawing2D;
using System.Numerics;
using HolzShots.Drawing.Tools.UI;

namespace HolzShots.Drawing.Tools;

public class Eraser : ITool<EraserSettings>
{
    public ISettingsControl<EraserSettings> SettingsControl { get; } = new EraserSettingsControl(EraserSettings.Default);
    public Vector2 BeginCoordinates { get; set; }
    public Vector2 EndCoordinates { get; set; }

    public Cursor Cursor
    {
        get
        {
            var settings = SettingsControl.Settings;

            var bmp = new Bitmap(settings.Diameter + 8, settings.Diameter + 8);
            bmp.MakeTransparent();

            using var g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillEllipse(Brushes.LightGray, 4, 4, settings.Diameter, settings.Diameter);
            g.DrawEllipse(Pens.Black, 4, 4, settings.Diameter, settings.Diameter);
            return new Cursor(bmp.GetHicon());
        }
    }

    public ShotEditorTool ToolType { get; } = ShotEditorTool.Eraser;

    private static readonly Brush ClearBrush = new SolidBrush(Color.FromArgb(0, Color.White));

    private bool _isFirstClick = true;

    public void RenderPreview(Image rawImage, Graphics ga)
    {
        using var g = Graphics.FromImage(rawImage);
        g.CompositingMode = CompositingMode.SourceCopy;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        var settings = SettingsControl.Settings;

        if (_isFirstClick)
        {
            g.FillEllipse(
                ClearBrush,
                BeginCoordinates.X - Convert.ToSingle(settings.Diameter / 2.0),
                BeginCoordinates.Y - Convert.ToSingle(settings.Diameter / 2.0),
                settings.Diameter,
                settings.Diameter
            );
            _isFirstClick = false;
        }
        else
        {
            g.FillEllipse(
                ClearBrush,
                EndCoordinates.X - Convert.ToSingle(settings.Diameter / 2.0),
                EndCoordinates.Y - Convert.ToSingle(settings.Diameter / 2.0),
                settings.Diameter,
                settings.Diameter
            );
        }
    }

    public void LoadInitialSettings()
    {
        if (Properties.Settings.Default.EraserDiameter > EraserSettings.MaximumDiameter || Properties.Settings.Default.EraserDiameter <= EraserSettings.MinimumDiameter)
        {
            Properties.Settings.Default.EraserDiameter = EraserSettings.Default.Diameter;
            Properties.Settings.Default.Save();
        }
        var settings = SettingsControl.Settings;
        settings.Diameter = Properties.Settings.Default.EraserDiameter;
    }

    public void PersistSettings()
    {
        var settings = SettingsControl.Settings;
        Properties.Settings.Default.EraserDiameter = settings.Diameter;
    }

    public void Dispose() => SettingsControl.Dispose();
    public void RenderFinalImage(ref Image rawImage) { }
    public void MouseOnlyMoved(Image rawImage, ref Cursor currentCursor, MouseEventArgs e) { }
    public void MouseClicked(Image rawImage, Vector2 e, ref Cursor currentCursor, Control trigger) { }
}
