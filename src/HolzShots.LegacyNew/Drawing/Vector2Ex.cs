using System.Numerics;

namespace HolzShots.Drawing;

public static class Vector2Ex
{
    public static Vector2 Rotate(this Vector2 value, float angle) => new(
        (float)(Math.Cos(angle) * value.X - Math.Sin(angle) * value.Y),
        (float)(Math.Sin(angle) * value.X + Math.Cos(angle) * value.Y)
    );

    public static Point ToPoint2D(this Vector2 value) => new((int)value.X, (int)value.Y);
}
