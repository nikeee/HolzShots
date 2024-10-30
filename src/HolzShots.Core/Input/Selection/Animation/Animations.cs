using System.Numerics;
using unvell.D2DLib;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Animation;

abstract class BaseAnimation
{
    public DateTime Start { get; }
    public TimeSpan Duration { get; }
    public TimeSpan Elapsed { get; private set; }
    public bool IsDone { get; private set; }

    protected BaseAnimation(DateTime startTime, TimeSpan duration)
    {
        Start = startTime;
        Duration = duration;
    }
    public virtual void Update(DateTime now)
    {
        Elapsed = now - Start;
        IsDone = Elapsed > Duration;
    }
}

class RectangleAnimation : BaseAnimation
{
    public D2DRect Source { get; }
    public D2DRect Destination { get; }
    public D2DRect Current { get; private set; }

    public RectangleAnimation(DateTime startTime, TimeSpan duration, D2DRect source, D2DRect destination)
        : base(startTime, duration)
    {
        Source = source;
        Destination = destination;
    }

    public override void Update(DateTime now)
    {
        if (IsDone)
        {
            Current = Destination;
            return;
        }

        base.Update(now);

        if (Elapsed > Duration)
        {
            Current = Destination;
            return;
        }

        var progress = (float)Elapsed.TotalMilliseconds / (float)Duration.TotalMilliseconds;

        Current = EasingMath.EaseOutCubic(progress, Source.AsVector4(), Destination.AsVector4()).ToD2DRect();
    }
}

class BoxedTextAnimation : RectangleAnimation
{
    public string Text { get; }
    public float FontSize { get; }
    public BoxedTextAnimation(DateTime startTime, TimeSpan duration, D2DRect source, D2DRect destination, string text, float fontSize)
        : base(startTime, duration, source, destination)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        FontSize = fontSize;
    }
}

class Vector2Animation : BaseAnimation
{
    public Vector2 Source { get; }
    public Vector2 Destination { get; }
    public Vector2 Current { get; private set; }
    public float Completed { get; private set; }

    private readonly Func<float, Vector2, Vector2, Vector2> _timingFunction;

    public Vector2Animation(DateTime startTime, TimeSpan duration, Vector2 source, Vector2 destination)
        : this(startTime, duration, source, destination, EasingMath.EaseOutCubic) { }
    public Vector2Animation(DateTime startTime, TimeSpan duration, Vector2 source, Vector2 destination, Func<float, Vector2, Vector2, Vector2> timingFunction)
        : base(startTime, duration)
    {
        Source = source;
        Destination = destination;
        _timingFunction = timingFunction ?? throw new ArgumentNullException(nameof(timingFunction));
    }

    public override void Update(DateTime now)
    {
        if (IsDone)
        {
            Current = Destination;
            Completed = 1f;
            return;
        }

        base.Update(now);

        if (Elapsed > Duration)
        {
            Current = Destination;
            Completed = 1f;
            return;
        }

        var percentageCompleted = (float)Elapsed.TotalMilliseconds / (float)Duration.TotalMilliseconds;

        Completed = percentageCompleted;
        Current = _timingFunction(percentageCompleted, Source, Destination);
    }
}
