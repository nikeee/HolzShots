using System.Drawing.Drawing2D;
using System.Numerics;
using HolzShots.Drawing.Tools.UI;

namespace HolzShots.Drawing.Tools;

public class Arrow : ITool<ArrowSettings>
{
    private readonly Point[] _arrowDrawPoints = new Point[4];

    const float ArrowRotationConstant = 2.2f * MathF.PI / 1.2f;
    private Vector2 _arrowBetween2;

    public Vector2 BeginCoordinates { get; set; }
    public Vector2 EndCoordinates { get; set; }

    private static readonly Cursor _cursor = new(Properties.Resources.crossMedium.GetHicon());
    public ShotEditorTool ToolType { get; } = ShotEditorTool.Arrow;
    public Cursor Cursor { get; } = _cursor;
    public ISettingsControl<ArrowSettings> SettingsControl { get; } = new ArrowSettingsControl(ArrowSettings.Default);

    public void LoadInitialSettings()
    {
        var arrowWidth = Math.Clamp(Properties.Settings.Default.ArrowWidth, ArrowSettings.MinimumWidth, ArrowSettings.MaximumWidth);
        if (arrowWidth != Properties.Settings.Default.ArrowWidth)
        {
            Properties.Settings.Default.ArrowWidth = arrowWidth;
            Properties.Settings.Default.Save();
        }

        var settings = SettingsControl.Settings;
        settings.Width = Properties.Settings.Default.ArrowWidth;
        settings.Color = Properties.Settings.Default.ArrowColor;
    }

    public void PersistSettings()
    {
        var settings = SettingsControl.Settings;
        Properties.Settings.Default.ArrowWidth = settings.Width;
        Properties.Settings.Default.ArrowColor = settings.Color;
    }

    private Pen CreatePen(ArrowSettings settings, LineCap endCap)
    {
        return new Pen(settings.Color)
        {
            Width = settings.Width <= 0 ? _arrowBetween2.Length() / 90 : settings.Width,
            EndCap = endCap,
            StartCap = LineCap.Round
        };
    }

    public void RenderFinalImage(ref Image rawImage)
    {
        if (BeginCoordinates == Vector2.Zero || EndCoordinates == Vector2.Zero)
            return;

        using var g = Graphics.FromImage(rawImage);
        g.SmoothingMode = SmoothingMode.AntiAlias;

        using var pen = CreatePen(SettingsControl.Settings, LineCap.Triangle);
        g.DrawLine(pen, _arrowDrawPoints[0], _arrowDrawPoints[1]);
        pen.EndCap = LineCap.Round;
        g.DrawLine(pen, _arrowDrawPoints[1], _arrowDrawPoints[2]);
        g.DrawLine(pen, _arrowDrawPoints[1], _arrowDrawPoints[3]);
    }

    public void RenderPreview(Image rawImage, Graphics g)
    {
        EndCoordinates = new Vector2(EndCoordinates.X, EndCoordinates.Y);

        if (BeginCoordinates == Vector2.Zero || EndCoordinates == Vector2.Zero)
            return;

        if (BeginCoordinates != EndCoordinates)
        {
            _arrowBetween2 = EndCoordinates - BeginCoordinates;

            var between = Vector2.Normalize(_arrowBetween2) * _arrowBetween2.Length() / 5;
            var c = between.Rotate(ArrowRotationConstant) + BeginCoordinates;
            var d = between.Rotate(-ArrowRotationConstant) + BeginCoordinates;
            _arrowDrawPoints[0] = EndCoordinates.ToPoint2D();
            _arrowDrawPoints[1] = BeginCoordinates.ToPoint2D();
            _arrowDrawPoints[2] = c.ToPoint2D();
            _arrowDrawPoints[3] = d.ToPoint2D();
        }

        if (EndCoordinates == Vector2.Zero)
            return;

        g.SmoothingMode = SmoothingMode.AntiAlias;

        using var pen = CreatePen(SettingsControl.Settings, LineCap.Round);
        g.DrawLine(pen, _arrowDrawPoints[0], _arrowDrawPoints[1]);
        g.DrawLine(pen, _arrowDrawPoints[1], _arrowDrawPoints[2]);
        g.DrawLine(pen, _arrowDrawPoints[1], _arrowDrawPoints[3]);
    }

    public void MouseOnlyMoved(Image rawImage, ref Cursor currentCursor, MouseEventArgs e) { }
    public void MouseClicked(Image rawImage, Vector2 e, ref Cursor currentCursor, Control trigger) { }
    public void Dispose() { }
}
