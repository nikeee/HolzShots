using System;
using System.Drawing;
using System.Numerics;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Animation
{
    public abstract class Animation
    {
        protected DateTime _start;
        protected bool _isRunning;

        public void Start(DateTime startTime)
        {
            _start = startTime;
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

        public void Update(DateTime now, TimeSpan _)
        {
            if (!_isRunning)
                return;

            var elapsed = now - _start;
            if (elapsed > Duration)
            {
                Current = Destination;
                return;
            }

            var percentageCompleted = (float)elapsed.TotalMilliseconds / (float)Duration.TotalMilliseconds;

            Current = EasingMath.EaseOutCubic(percentageCompleted, Source, Destination);
        }
    }

    public class Vector2Animation : Animation
    {
        public Vector2 Source { get; }
        public Vector2 Destination { get; }
        public Vector2 Current { get; private set; }
        public float Completed { get; private set; }
        public TimeSpan Duration { get; }

        private readonly Func<float, Vector2, Vector2, Vector2> _timingFunction;

        public Vector2Animation(TimeSpan duration, Vector2 source, Vector2 destination)
            : this(duration, source, destination, EasingMath.EaseOutCubic) { }
        public Vector2Animation(TimeSpan duration, Vector2 source, Vector2 destination, Func<float, Vector2, Vector2, Vector2> timingFunction)
        {
            Duration = duration;
            Source = source;
            Destination = destination;
            _timingFunction = timingFunction ?? throw new ArgumentNullException(nameof(timingFunction));
        }

        public void Update(DateTime now, TimeSpan _)
        {
            if (!_isRunning)
                return;

            var elapsed = now - _start;
            if (elapsed > Duration)
            {
                Current = Destination;
                return;
            }

            var percentageCompleted = (float)elapsed.TotalMilliseconds / (float)Duration.TotalMilliseconds;

            Completed = percentageCompleted;
            Current = _timingFunction(percentageCompleted, Source, Destination);
        }
    }
}
