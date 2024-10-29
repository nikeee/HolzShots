using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Numerics;
using HolzShots.Drawing.Tools.UI;

namespace HolzShots.Drawing.Tools;

public class Censor : ITool<CensorSettings>
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
            var censorWidth = Math.Max(5, SettingsControl.Settings.Width);
            var bmp = new Bitmap(Convert.ToInt32(0.2 * censorWidth), censorWidth);
            bmp.MakeTransparent();
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(200, 255, 0, 0));
            return new Cursor(bmp.GetHicon());
        }
    }

    public ShotEditorTool ToolType { get; } = ShotEditorTool.Censor;

    public ISettingsControl<CensorSettings> SettingsControl { get; } = new CensorSettingsControl(CensorSettings.Default);

    public void LoadInitialSettings()
    {
        if (Properties.Settings.Default.CensorWidth > CensorSettings.MaximumWidth || Properties.Settings.Default.CensorWidth < CensorSettings.MinimumWidth)
        {
            Properties.Settings.Default.CensorWidth = CensorSettings.Default.Width;
            Properties.Settings.Default.Save();
        }
        var settings = SettingsControl.Settings;
        settings.Width = Properties.Settings.Default.CensorWidth;
        settings.Color = Properties.Settings.Default.CensorColor;
    }

    public void PersistSettings()
    {
        var settings = SettingsControl.Settings;
        Properties.Settings.Default.CensorWidth = settings.Width;
        Properties.Settings.Default.CensorColor = settings.Color;
    }

    private static Pen CreatePen(CensorSettings settings)
    {
        return new Pen(Color.FromArgb(255, settings.Color), settings.Width)
        {
            LineJoin = LineJoin.Round
        };
    }

    public void RenderFinalImage(ref Image rawImage)
    {
        _pointList.Add(EndCoordinates.ToPoint2D());
        using var g = Graphics.FromImage(rawImage);

        using var censorPen = CreatePen(SettingsControl.Settings);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.AntiAlias;

        if (_pointList.Count > 0 && (_pointList.Count - 1) % 3 == 0)
            g.DrawBeziers(censorPen, _pointList.ToArray());
        else
            g.DrawBeziers(censorPen, _pointList.Take(_pointList.Count - (_pointList.Count - 1) % 3).ToArray());

        _pointList.Clear();
    }

    public void RenderPreview(Image rawImage, Graphics g)
    {
        _pointList.Add(EndCoordinates.ToPoint2D());

        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.AntiAlias;

        if (_pointList.Count <= 0)
            return;

        using var censorPen = CreatePen(SettingsControl.Settings);

        var bs = new byte[_pointList.Count - 1 + 1];
        bs[0] = Convert.ToByte(PathPointType.Start);
        for (var a = 1; a <= _pointList.Count - 1; a++)
        {
            bs[a] = Convert.ToByte(PathPointType.Line);
            g.DrawPath(censorPen, new GraphicsPath(_pointList.ToArray(), bs));
        }
    }

    public void Dispose() { }
    public void MouseOnlyMoved(Image rawImage, ref Cursor currentCursor, MouseEventArgs e) { }
    public void MouseClicked(Image rawImage, Vector2 e, ref Cursor currentCursor, Control trigger) { }
}
