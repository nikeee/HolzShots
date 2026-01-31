using System.Drawing.Drawing2D;
using System.Numerics;
using HolzShots.Drawing.Tools.UI;
using System.Diagnostics;

namespace HolzShots.Drawing.Tools;

public class Marker : ITool<MarkerSettings>
{
    private List<Point> _pointList = [];

    private Vector2 _beginCoordinates;
    public Vector2 BeginCoordinates
    {
        get => _beginCoordinates;
        set
        {
            _beginCoordinates = value;
            _pointList = [_beginCoordinates.ToPoint2D()];
        }
    }
    public Vector2 EndCoordinates { get; set; }

    public Cursor Cursor
    {
        get
        {
            var markerWidth = Math.Max(5, SettingsControl.Settings.Width);
            var bmp = new Bitmap(Convert.ToInt32(0.2 * markerWidth), markerWidth);
            bmp.MakeTransparent();
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(200, 255, 0, 0));
            }
            return new Cursor(bmp.GetHicon());
        }
    }

    public ISettingsControl<MarkerSettings> SettingsControl { get; } = new MarkerSettingsControl(MarkerSettings.Default);

    public ShotEditorTool ToolType => ShotEditorTool.Marker;

    public void LoadInitialSettings()
    {
        if (Properties.Settings.Default.MarkerWidth > MarkerSettings.MaximumWidth || Properties.Settings.Default.MarkerWidth < MarkerSettings.MinimumWidth)
        {
            Properties.Settings.Default.MarkerWidth = MarkerSettings.Default.Width;
            Properties.Settings.Default.Save();
        }
        var settings = SettingsControl.Settings;
        settings.Width = Properties.Settings.Default.MarkerWidth;
        settings.Color = Properties.Settings.Default.MarkerColor;
    }

    public void PersistSettings()
    {
        var settings = SettingsControl.Settings;
        Properties.Settings.Default.MarkerWidth = settings.Width;
        Properties.Settings.Default.MarkerColor = settings.Color;
    }

    private static NativePen CreatePen(MarkerSettings settings) => new NativePen(settings.Color, settings.Width);

    public void RenderFinalImage(ref Image rawImage)
    {
        Debug.Assert(rawImage is Bitmap);

        _pointList.Add(EndCoordinates.ToPoint2D());

        if (_pointList.Count <= 1)
            return;

        using var g = Graphics.FromImage(rawImage);
        g.SmoothingMode = SmoothingMode.AntiAlias;

        using var markerPen = CreatePen(SettingsControl.Settings);
        g.DrawHighlight((Bitmap)rawImage, _pointList.ToArray(), markerPen);

        _pointList.Clear();
    }

    public void RenderPreview(Image rawImage, Graphics g)
    {
        Debug.Assert(rawImage is Bitmap);

        _pointList.Add(EndCoordinates.ToPoint2D());
        g.SmoothingMode = SmoothingMode.AntiAlias;
        if (_pointList.Count <= 0)
            return;

        using var markerPen = CreatePen(SettingsControl.Settings);
        g.DrawHighlight((Bitmap)rawImage, _pointList.ToArray(), markerPen);
    }

    public void Dispose() { }
    public void MouseOnlyMoved(Image rawImage, ref Cursor currentCursor, MouseEventArgs e) { }
    public void MouseClicked(Image rawImage, Vector2 e, ref Cursor currentCursor, Control trigger) { }
}
