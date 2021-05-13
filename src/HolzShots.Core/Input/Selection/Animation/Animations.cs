using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace HolzShots.Input.Selection.Animation
{

    public abstract class Animation
    {
        protected DateTime _start;
        protected bool _isRunning;

        public void Start()
        {
            _start = DateTime.Now;
            _isRunning = true;
        }
        public void Stop()
        {
            _isRunning = false;
        }
    }

    public class RectangleAnimation : Animation
    {
        public Rectangle Source { get; }
        public Rectangle Destination { get; }
        public Rectangle Current { get; private set; }
        public TimeSpan Duration { get; }

        public RectangleAnimation(TimeSpan duration, Rectangle source, Rectangle destination)
        {
            Duration = duration;
            Source = source;
            Destination = destination;
        }

        public void Update(DateTime now)
        {
            if (!_isRunning)
                return;

            // if (_start < now)
            //    return;

            var elasped = now - _start;
            if (elasped > Duration)
                return;

            var percentageCompleted = (float)elasped.TotalMilliseconds / (float)Duration.TotalMilliseconds;

            Current = SimdMathEx.EaseOut(percentageCompleted, Source, Destination);
        }
    }

    static class SimdMathEx
    {
        public static Rectangle EaseOut(float amount, Rectangle source, Rectangle destination)
        {
            var s = new Vector4(
                source.X,
                source.Y,
                source.Width,
                source.Height
            );

            var d = new Vector4(
                destination.X,
                destination.Y,
                destination.Width,
                destination.Height
            );

            var o = 1.0f - amount;
            var factor = 1 - (o * o);
            var res = (d - s) * factor + s;
            return new Rectangle((int)res.X, (int)res.Y, (int)res.Z, (int)res.W);
        }

        public static Rectangle Lerp(float amount, Rectangle source, Rectangle destination)
        {
            var s = new Vector4(
                source.X,
                source.Y,
                source.Width,
                source.Height
            );

            var d = new Vector4(
                destination.X,
                destination.Y,
                destination.Width,
                destination.Height
            );

            var res = (d - s) * amount;
            return new Rectangle((int)res.X, (int)res.Y, (int)res.Z, (int)res.W);
        }
    }
}
