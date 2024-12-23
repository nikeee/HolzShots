using System.Drawing;
using System.Numerics;
using unvell.D2DLib;

namespace HolzShots;

static class D2DRectExtensions
{
    /// <summary>Like System.Drawing.Rectangle.Inflate(). Maintains rectangle's center. </summary>
    public static void Inflate(this ref D2DRect rectangle, Vector2 size) => rectangle.Inflate(size.X, size.Y);
    /// <summary>Like System.Drawing.Rectangle.Inflate(). Maintains rectangle's center. </summary>
    public static void Inflate(this ref D2DRect rectangle, float width, float height)
    {
        rectangle.X -= width;
        rectangle.Y -= height;
        rectangle.Width += width * 2;
        rectangle.Height += height * 2;
    }
    /// <summary>Like System.Drawing.Rectangle.Inflate(). Maintains rectangle's center. </summary>
    public static void Inflate(this ref D2DRect rectangle, Size size) => rectangle.Inflate(size.Width, size.Height);
    /// <summary>Like System.Drawing.Rectangle.Inflate(). Maintains rectangle's center. </summary>
    public static void Inflate(this ref D2DRect rectangle, int width, int height)
    {
        rectangle.X -= width;
        rectangle.Y -= height;
        rectangle.Width += width * 2;
        rectangle.Height += height * 2;
    }
}
