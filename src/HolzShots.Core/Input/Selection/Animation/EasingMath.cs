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
        #region EaseOutSquare

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutSquare(float amount, float source, float destination)
        {
            var o = 1.0f - amount;
            return Lerp(1 - (o * o), source, destination);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 EaseOutSquare(float amount, Vector2 source, Vector2 destination)
        {
            var o = 1.0f - amount;
            return Lerp(1 - (o * o), source, destination);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 EaseOutSquare(float amount, Vector4 source, Vector4 destination)
        {
            var o = 1.0f - amount;
            return Lerp(1 - (o * o), source, destination);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 EaseOutSquare(float amount, Vector3 source, Vector3 destination)
        {
            var o = 1.0f - amount;
            return Lerp(1 - (o * o), source, destination);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle EaseOutSquare(float amount, Rectangle source, Rectangle destination) => EaseOutSquare(amount, source.AsVector(), destination.AsVector()).ToRectangle();

        #endregion
        #region EaseOutCubic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutCubic(float amount, float source, float destination)
        {
            var o = 1.0f - amount;
            return Lerp(1 - (o * o * o), source, destination);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 EaseOutCubic(float amount, Vector2 source, Vector2 destination)
        {
            var o = 1.0f - amount;
            return Lerp(1 - (o * o * o), source, destination);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 EaseOutCubic(float amount, Vector3 source, Vector3 destination)
        {
            var o = 1.0f - amount;
            return Lerp(1 - (o * o * o), source, destination);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 EaseOutCubic(float amount, Vector4 source, Vector4 destination)
        {
            var o = 1.0f - amount;
            return Lerp(1 - (o * o * o), source, destination);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle EaseOutCubic(float amount, Rectangle source, Rectangle destination) => EaseOutCubic(amount, source.AsVector(), destination.AsVector()).ToRectangle();

        #endregion
        #region EaseInSquare

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInSquare(float amount, float source, float destination) => Lerp(amount * amount, source, destination);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 EaseInSquare(float amount, Vector2 source, Vector2 destination) => Lerp(amount * amount, source, destination);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 EaseInSquare(float amount, Vector3 source, Vector3 destination) => Lerp(amount * amount, source, destination);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 EaseInSquare(float amount, Vector4 source, Vector4 destination) => Lerp(amount * amount, source, destination);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle EaseInSquare(float amount, Rectangle source, Rectangle destination) => Lerp(amount * amount, source.AsVector(), destination.AsVector()).ToRectangle();

        #endregion
        #region EaseInCubic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInCubic(float amount, float source, float destination) => Lerp(amount * amount * amount, source, destination);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 EaseInCubic(float amount, Vector2 source, Vector2 destination) => Lerp(amount * amount * amount, source, destination);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 EaseInCubic(float amount, Vector3 source, Vector3 destination) => Lerp(amount * amount * amount, source, destination);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 EaseInCubic(float amount, Vector4 source, Vector4 destination) => Lerp(amount * amount * amount, source, destination);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle EaseInCubic(float amount, Rectangle source, Rectangle destination) => Lerp(amount * amount * amount, source.AsVector(), destination.AsVector()).ToRectangle();

        #endregion
        #region Lerp

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(float amount, float source, float destination) => (destination - source) * amount + source;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Lerp(float amount, Vector2 source, Vector2 destination) => (destination - source) * amount + source;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(float amount, Vector3 source, Vector3 destination) => (destination - source) * amount + source;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Lerp(float amount, Vector4 source, Vector4 destination) => (destination - source) * amount + source;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Lerp(float amount, Rectangle source, Rectangle destination) => Lerp(amount, source.AsVector(), destination.AsVector()).ToRectangle();

        #endregion
    }
}
