using System.Drawing.Drawing2D;
using System.Numerics;
using HolzShots.Drawing.Tools.UI;

namespace HolzShots.Drawing.Tools;

public class Blur : ITool<BlurSettings>
{
    private readonly Pen _thePen = new Pen(Color.Red) { DashStyle = DashStyle.Dash };
    private static readonly Cursor CursorInstance = new Cursor(Properties.Resources.crossMedium.GetHicon());
    public Cursor Cursor { get; } = CursorInstance;
    public ShotEditorTool ToolType { get; } = ShotEditorTool.Blur;

    public ISettingsControl<BlurSettings> SettingsControl { get; } = new BlurSettingsControl(BlurSettings.Default);
    public Vector2 BeginCoordinates { get; set; }
    public Vector2 EndCoordinates { get; set; }

    public void LoadInitialSettings()
    {
        if (Properties.Settings.Default.BlurFactor > BlurSettings.MaximumDiameter || Properties.Settings.Default.BlurFactor <= BlurSettings.MinimumDiameter)
        {
            Properties.Settings.Default.BlurFactor = BlurSettings.Default.Diameter;
            Properties.Settings.Default.Save();
        }

        var settings = SettingsControl.Settings;
        settings.Diameter = Properties.Settings.Default.BlurFactor;
    }

    public void PersistSettings()
    {
        var settings = SettingsControl.Settings;
        Properties.Settings.Default.BlurFactor = settings.Diameter;
    }

    public void RenderFinalImage(ref Image rawImage)
    {
        if (rawImage == null)
            return;

        var rawSrcRect = Rectangle.Round(new RectangleF(
            Math.Min(BeginCoordinates.X, EndCoordinates.X),
            Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
            Math.Abs(BeginCoordinates.X - EndCoordinates.X),
            Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
        ));

        if (rawSrcRect.X < 0)
        {
            rawSrcRect.Width += rawSrcRect.X;
            rawSrcRect.X = 0;
        }

        if (rawSrcRect.Y < 0)
        {
            rawSrcRect.Height += rawSrcRect.Y;
            rawSrcRect.Y = 0;
        }

        if (rawSrcRect.Width == 0 || rawSrcRect.Height == 0)
            return;

        using var g = Graphics.FromImage(rawImage);
        var settings = SettingsControl.Settings;

        using var img = BlurImage(rawImage, settings.Diameter, rawSrcRect);
        if (img == null)
            return;

        g.FillRectangle(Brushes.White, rawSrcRect);
        g.CompositingMode = CompositingMode.SourceOver;
        g.DrawImage(img, rawSrcRect);
    }

    public void RenderPreview(Image rawImage, Graphics g)
    {
        if (rawImage == null)
            return;

        var rawSrcRectangle = Rectangle.Round(new RectangleF(
            Math.Min(BeginCoordinates.X, EndCoordinates.X),
            Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
            Math.Abs(BeginCoordinates.X - EndCoordinates.X),
            Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
        ));

        if (rawSrcRectangle.X < 0)
        {
            rawSrcRectangle.Width += rawSrcRectangle.X;
            rawSrcRectangle.X = 0;
        }

        if (rawSrcRectangle.Y < 0)
        {
            rawSrcRectangle.Height += rawSrcRectangle.Y;
            rawSrcRectangle.Y = 0;
        }

        if (rawSrcRectangle.Width == 0 || rawSrcRectangle.Height == 0)
            return;

        var settings = SettingsControl.Settings;

        using var img = BlurImage(rawImage, settings.Diameter, rawSrcRectangle);
        if (img == null)
            return;

        g.CompositingMode = CompositingMode.SourceOver;
        g.FillRectangle(Brushes.White, rawSrcRectangle);
        g.DrawImage(img, rawSrcRectangle);
        g.DrawRectangle(_thePen, rawSrcRectangle);
    }

    private static Bitmap BlurImage(Image img, int factor, Rectangle rawSrcRect)
    {
        var width = Convert.ToInt32(Math.Round(Math.Ceiling(rawSrcRect.Width / (double)factor)));
        var height = Convert.ToInt32(Math.Round(Math.Ceiling(rawSrcRect.Height / (double)factor)));

        if (width <= 0 || height <= 0)
            return null; // TODO Change to default(_) if this is not a reference type

        using var smallBitmap = new Bitmap(width, height);
        using var g0 = Graphics.FromImage(smallBitmap);

        g0.CompositingMode = CompositingMode.SourceOver;
        g0.InterpolationMode = InterpolationMode.HighQualityBilinear;
        g0.DrawImage(img, new Rectangle(0, 0, smallBitmap.Width, smallBitmap.Height), rawSrcRect, GraphicsUnit.Pixel);

        using var secondBitmap = new Bitmap(rawSrcRect.Width, rawSrcRect.Height);
        using var g1 = Graphics.FromImage(secondBitmap);

        g1.CompositingMode = CompositingMode.SourceOver;
        g1.InterpolationMode = InterpolationMode.HighQualityBilinear;
        g1.DrawImage(
            smallBitmap,
            new Rectangle(0, 0, rawSrcRect.Width + factor, rawSrcRect.Height + factor),
            new Rectangle(0, 0, smallBitmap.Width, smallBitmap.Height),
            GraphicsUnit.Pixel
        );
        return (Bitmap)secondBitmap.Clone();
    }

    public void MouseOnlyMoved(Image rawImage, ref Cursor currentCursor, MouseEventArgs e) { }
    public void MouseClicked(Image rawImage, Vector2 e, ref Cursor currentCursor, Control trigger) { }
    public void Dispose() { }
}
