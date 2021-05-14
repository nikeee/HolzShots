using System.Drawing;

namespace HolzShots.Input.Selection.Animation
{
    static class EasingMath
    {
        public static Rectangle EaseOut(float amount, Rectangle source, Rectangle destination)
        {
            var o = 1.0f - amount;
            var factor = 1 - (o * o);

            var s = source.AsVector();
            var d = destination.AsVector();
            var res = (d - s) * factor + s;
            return res.ToRectangle();
        }

        public static Rectangle Lerp(float amount, Rectangle source, Rectangle destination)
        {
            var s = source.AsVector();
            var d = destination.AsVector();

            var res = (d - s) * amount + s;
            return res.ToRectangle();
        }
    }
}
