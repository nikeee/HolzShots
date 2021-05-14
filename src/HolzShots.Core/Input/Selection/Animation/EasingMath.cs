using System.Drawing;

namespace HolzShots.Input.Selection.Animation
{
    /// <summary>
    /// Ref on some of them:
    /// - https://www.febucci.com/2018/08/easing-functions/
    /// - https://easings.net
    /// </summary>
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
        public static Rectangle EaseIn(float amount, Rectangle source, Rectangle destination)
        {
            var factor = amount * amount;

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

        public static float Lerp(float x, float source, float destination) => ((destination - source) * x) + source;
        public static float EaseIn(float x, float source, float destination) => Lerp(x * x, source, destination);
    }
}
