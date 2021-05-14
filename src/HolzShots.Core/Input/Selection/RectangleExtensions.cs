using System.Drawing;
using System.Numerics;
using unvell.D2DLib;

namespace HolzShots.Input.Selection
{
    public static class RectangleExtensions
    {
        public static bool HasArea(this Rectangle value) => value.Width > 0 && value.Height > 0;
        public static Vector4 AsVector(this Rectangle v) => new(v.X, v.Y, v.Width, v.Height);
        public static D2DRect AsD2DRect(this Rectangle v) => new(v.X + 0.5f, v.Y + 0.5f, v.Width - 0.5f, v.Height - 0.5f);
    }

    public static class Vector4Extensions
    {
        public static Rectangle ToRectangle(this Vector4 v) => new((int)v.X, (int)v.Y, (int)v.Z, (int)v.W);
        public static RectangleF ToRectangleF(this Vector4 v) => new(v.X, v.Y, v.Z, v.W);
    }
}
