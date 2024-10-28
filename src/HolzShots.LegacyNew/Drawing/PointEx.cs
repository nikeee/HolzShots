using System.Numerics;

namespace HolzShots.Drawing;

public static class PointEx
{
    public static Vector2 ToVector2(this Point value) => new(value.X, value.Y);
}
