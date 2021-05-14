using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace HolzShots.Input.Selection.Animation
{
    /// <summary>
    /// Ref on some of them:
    /// - https://www.febucci.com/2018/08/easing-functions/
    /// - https://easings.net
    /// </summary>
    static class EasingMath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOut(float amount, float source, float destination)
        {
            var o = 1.0f - amount;
            return Lerp(1 - (o * o), source, destination);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 EaseOut(float amount, Vector4 source, Vector4 destination)
        {
            var o = 1.0f - amount;
            return Lerp(1 - (o * o), source, destination);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle EaseOut(float amount, Rectangle source, Rectangle destination) => EaseOut(amount, source.AsVector(), destination.AsVector()).ToRectangle();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseIn(float amount, float source, float destination) => Lerp(amount * amount, source, destination);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 EaseIn(float amount, Vector4 source, Vector4 destination) => Lerp(amount * amount, source, destination);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle EaseIn(float amount, Rectangle source, Rectangle destination) => Lerp(amount * amount, source.AsVector(), destination.AsVector()).ToRectangle();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(float amount, float source, float destination) => ((destination - source) * amount) + source;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Lerp(float amount, Vector4 source, Vector4 destination) => (source - destination) * amount + source;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Lerp(float amount, Rectangle source, Rectangle destination) => Lerp(amount, source.AsVector(), destination.AsVector()).ToRectangle();
    }
}
