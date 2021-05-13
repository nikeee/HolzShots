using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var elasped = now - _start;
            if (elasped > Duration)
                return;

            var percentageCompleted = (float)elasped.TotalMilliseconds / (float)Duration.TotalMilliseconds;

            Current = new Rectangle(
                (int)MathEx.Lerp(percentageCompleted, Source.X, Destination.X),
                (int)MathEx.Lerp(percentageCompleted, Source.Y, Destination.Y),
                (int)MathEx.Lerp(percentageCompleted, Source.Width, Destination.Width),
                (int)MathEx.Lerp(percentageCompleted, Source.Height, Destination.Height)
            );
        }
    }
}
