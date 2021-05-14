using System;
using System.Drawing;

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

            Current = EasingMath.EaseOut(percentageCompleted, Source, Destination);
        }
    }
}
