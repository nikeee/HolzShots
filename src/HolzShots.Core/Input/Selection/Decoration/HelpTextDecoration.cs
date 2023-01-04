using System.Drawing;
using System.Linq;
using HolzShots.Input.Selection.Animation;
using nud2dlib;

namespace HolzShots.Input.Selection.Decoration;

class HelpTextDecoration : IStateDecoration<InitialState>
{
    private const string FontName = "Consolas";
    private const float FontSize = 24.0f;
    private const float SmallFontSize = 14.0f;

    private const int HelpTextsToDisplay = 3;

    private static readonly (string, float)[] HelpTextCandidates = new[] {
        ("Left Mouse: Select area", FontSize),
        ("Right Mouse: Move selected area", FontSize),
        ("Space: Toggle magnifier", FontSize),
        ("Tab: Cycle through windows", FontSize),
        ("Return: Confirm current selection", FontSize),
        ("Escape: Cancel", FontSize),
    };


    private static readonly D2DSize Margin = new(10, 5);
    private static readonly D2DColor BackgroundColor = new(0.2f, 1f, 1f, 1f);
    private static readonly D2DColor FontColor = new(1f, 0.9f, 0.9f, 0.9f);
    private static readonly TimeSpan FadeStart = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan FadeDuration = TimeSpan.FromSeconds(3);

    private readonly BoxedTextAnimation[] _animations;
    private DateTime? _firstUpdate = null;
    private DateTime? _fadeOutStarted = null;

    public HelpTextDecoration(BoxedTextAnimation[] animations) => _animations = animations;

    public static HelpTextDecoration ForContext(D2DGraphics g, DateTime now) => new(BuildAnimations(now, g));

    private static BoxedTextAnimation[] BuildAnimations(DateTime now, D2DGraphics g)
    {
        var res = new BoxedTextAnimation[HelpTextsToDisplay];

        var random = new Random();
        var randomHelpEntries = HelpTextCandidates
            .OrderBy(_ => random.Next())
            .Take(HelpTextsToDisplay)
            .OrderByDescending(t => t.Item1.Length)
            .ToArray();

        int lastX = 100;
        int lastY = 100;

        var someRandomSize = new D2DSize(1000, 1000);

        for (int i = 0; i < res.Length; ++i)
        {
            var (text, fontSize) = randomHelpEntries[i];
            var textSize = g.MeasureText(text, FontName, fontSize, someRandomSize);
            var destination = new Rectangle(
                lastX,
                lastY,
                (int)(textSize.Width + 2 * Margin.Width),
                (int)(textSize.Height + 2 * Margin.Height)
            );

            var start = new Rectangle(
                destination.Location,
                new Size(0, destination.Height)
            );

            var animation = new BoxedTextAnimation(
                now,
                TimeSpan.FromMilliseconds(175 * i + 75),
                start,
                destination,
                text,
                fontSize
            );

            res[i] = animation;

            lastY = destination.Y + destination.Height;
        }

        return res;
    }

    public void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image, InitialState state)
    {
        _firstUpdate ??= now;

        var opacityElapsed = now - _firstUpdate;
        if (_fadeOutStarted == null && opacityElapsed > FadeStart)
            _fadeOutStarted = now;

        var opacity = 1f;
        if (_fadeOutStarted is not null)
            opacity = EasingMath.EaseInSquare((float)(now - _fadeOutStarted.Value).TotalMilliseconds / (float)FadeDuration.TotalMilliseconds, 1, 0);

        if (opacity <= 0)
            return;

        for (int i = 0; i < _animations.Length; ++i)
        {
            var animation = _animations[i];
            animation.Update(now);
            var rect = animation.Current;

            var textLocation = new D2DRect(
                animation.Destination.X + Margin.Width,
                animation.Destination.Y + Margin.Height,
                animation.Destination.Width,
                animation.Destination.Height
            );

            using var maskRectangle = g.Device.CreateRectangleGeometry(rect);
            using var layer = g.PushLayer(maskRectangle);
            g.FillRectangle(rect, new D2DColor(opacity * BackgroundColor.A, BackgroundColor));
            g.DrawText(animation.Text, new D2DColor(opacity * FontColor.A, FontColor), FontName, animation.FontSize, textLocation);
            g.PopLayer();
        }
    }
    public void Dispose() { }
}
