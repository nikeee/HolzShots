using System.Drawing.Drawing2D;
using System.Numerics;
using HolzShots.Drawing.Tools.UI;

namespace HolzShots.Drawing.Tools;

public class Crop : ITool<ToolSettingsBase>
{
    private readonly SolidBrush _alphaBrush = new (Color.FromArgb(128, 0, 0, 0));
    private readonly Pen _redCornerPen = new (Color.FromArgb(255, 255, 0, 0)) { DashStyle = DashStyle.Dash };

    private static readonly Cursor CursorInstance = new (Properties.Resources.cropperCursor.Handle);
    public Cursor Cursor { get; } = CursorInstance;
    public ShotEditorTool ToolType { get; } = ShotEditorTool.Crop;
    public ISettingsControl<ToolSettingsBase>? SettingsControl { get; } = null; // TODO Change to default(_) if this is not a reference type

    public Vector2 BeginCoordinates { get; set; }
    public Vector2 EndCoordinates { get; set; }

    public void LoadInitialSettings() { }
    public void PersistSettings() { }

    public void RenderFinalImage(ref Image rawImage)
    {
        var rect = Rectangle.Round(new RectangleF(
            Math.Min(EndCoordinates.X, BeginCoordinates.X),
            Math.Min(EndCoordinates.Y, BeginCoordinates.Y),
            Math.Abs(BeginCoordinates.X - EndCoordinates.X),
            Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
        ));

        if (rect.X + rect.Width > rawImage.Width)
            rect.Width = Math.Max(1, Math.Abs(rawImage.Width - rect.X));
        if (rect.Y + rect.Height > rawImage.Height)
            rect.Height = Math.Max(1, Math.Abs(rawImage.Height - rect.Y));

        if (rect.X < 0)
        {
            rect.Width += rect.X;
            rect.X = 0;
        }
        if (rect.Y < 0)
        {
            rect.Height += rect.Y;
            rect.Y = 0;
        }

        var newBmp = new Bitmap(rect.Width, rect.Height);

        using var g = Graphics.FromImage(newBmp);
        g.InterpolationMode = InterpolationMode.HighQualityBilinear;
        g.DrawImage(rawImage, new Rectangle(0, 0, newBmp.Width, newBmp.Height), rect, GraphicsUnit.Pixel);

        rawImage = newBmp;
    }

    public void RenderPreview(Image rawImage, Graphics g)
    {
        var rect = Rectangle.Round(new RectangleF(
            Math.Min(EndCoordinates.X, BeginCoordinates.X),
            Math.Min(EndCoordinates.Y, BeginCoordinates.Y),
            Math.Abs(BeginCoordinates.X - EndCoordinates.X),
            Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
        ));

        if (rect.X + rect.Width > rawImage.Width)
        {
            rect.Width = rawImage.Width - rect.X;
            rect.Width = Math.Max(1, Math.Abs(rect.Width));
        }
        if (rect.Y + rect.Height > rawImage.Height)
        {
            rect.Height = rawImage.Height - rect.Y;
            rect.Height = Math.Max(1, Math.Abs(rect.Height));
        }

        if (rect.X < 0)
        {
            rect.Width += rect.X;
            rect.X = 0;
        }
        if (rect.Y < 0)
        {
            rect.Height += rect.Y;
            rect.Y = 0;
        }

        var unit = GraphicsUnit.Pixel;
        g.FillRectangle(_alphaBrush, rawImage.GetBounds(ref unit));
        g.DrawImage(rawImage, rect, rect, GraphicsUnit.Pixel);
        g.DrawRectangle(_redCornerPen, rect);
    }

    public void Dispose()
    {
        _alphaBrush.Dispose();
        _redCornerPen.Dispose();
    }
    public void MouseOnlyMoved(Image rawImage, ref Cursor currentCursor, MouseEventArgs e) { }
    public void MouseClicked(Image rawImage, Vector2 e, ref Cursor currentCursor, Control trigger) { }
}
