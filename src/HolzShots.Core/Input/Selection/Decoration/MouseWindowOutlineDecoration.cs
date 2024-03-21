using System.Drawing;
using nud2dlib;

namespace HolzShots.Input.Selection.Decoration;

class MouseWindowOutlineDecoration : IStateDecoration<InitialState>
{
    private const string FontName = "Consolas";
    private const float FontSize = 14.0f;
    private D2DColor FontColor = D2DColor.WhiteSmoke;
    private D2DColor BackgroundColor = new(1f, 0.2f, 0.2f, 0.2f);
    private D2DColor OutlineColor = D2DColor.White;
    private readonly D2DSize _placeSize = new(1000, 1000);

    public static MouseWindowOutlineDecoration ForContext(D2DGraphics g, DateTime now) => new();

    public void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image, InitialState state)
    {
        var outlineAnimation = state.CurrentOutlineAnimation;
        if (outlineAnimation == null)
            return;

        outlineAnimation.Update(now);

        var rect = outlineAnimation.Current;
        g.DrawRectangle(rect, OutlineColor, 1.0f);

        var windowTitle = state.Title;
        if (windowTitle is not null)
        {
            using var backgroundBush = g.Device.CreateSolidColorBrush(BackgroundColor);

            var textSize = g.MeasureText(windowTitle, FontName, FontSize, _placeSize);

            var padding = new Size(10, 4);

            var backgroundRectangle = new D2DRect(
                rect.X + (int)(rect.Width / 2) - (int)(textSize.Width / 2),
                rect.Y + (int)(rect.Height / 2) - (int)(textSize.Height / 2),
                textSize.Width,
                textSize.Height
            );
            backgroundRectangle.Inflate(padding);

            g.FillRectangle(backgroundRectangle, backgroundBush);
            g.DrawTextCenter(windowTitle, FontColor, FontName, FontSize, backgroundRectangle);
        }
    }

    public void Dispose() { }
}


static class D2DRectExtensions
{
    /// <summary>Like System.Drawing.Rectangle.Inflate(). Maintains rectangle's center. </summary>
    public static void Inflate(this ref D2DRect rectangle, D2DSize size) => Inflate(ref rectangle, size.Width, size.Height);
    /// <summary>Like System.Drawing.Rectangle.Inflate(). Maintains rectangle's center. </summary>
    public static void Inflate(this ref D2DRect rectangle, float width, float height)
    {
        rectangle.X -= width;
        rectangle.Y -= height;
        rectangle.Width += width * 2;
        rectangle.Height += height * 2;
    }
    /// <summary>Like System.Drawing.Rectangle.Inflate(). Maintains rectangle's center. </summary>
    public static void Inflate(this ref D2DRect rectangle, Size size) => Inflate(ref rectangle, size.Width, size.Height);
    /// <summary>Like System.Drawing.Rectangle.Inflate(). Maintains rectangle's center. </summary>
    public static void Inflate(this ref D2DRect rectangle, int width, int height)
    {
        rectangle.X -= width;
        rectangle.Y -= height;
        rectangle.Width += width * 2;
        rectangle.Height += height * 2;
    }
}
