using System;
using System.Drawing;
using HolzShots.Input.Selection.Animation;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Decoration
{
    internal class HelpTextDecoration : IStateDecoration<InitialState>
    {
        private const string FontName = "Consolas";
        private const float FontSize = 24.0f;
        private const float SmallFontSize = 14.0f;

        private static readonly (string, float)[] HelpText = new[] {
            ("Left Mouse: Select area", FontSize),
            ("Right Mouse: Move selected area", FontSize),
            // "Space Bar: Toggle magnifier", FontSize),
            ("Escape: Cancel", SmallFontSize),
        };

        private static readonly D2DSize Margin = new(10, 5);
        private static readonly D2DColor BackgroundColor = new(0.2f, 1f, 1f, 1f);
        private static readonly D2DColor FontColor = new(1f, 0.9f, 0.9f, 0.9f);
        private static readonly TimeSpan FadeStart = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan FadeDuration = TimeSpan.FromSeconds(3);

        private readonly RectangleAnimation[] _animations;
        private DateTime? _firstUpdate = null;
        private DateTime? _fadeOutStarted = null;

        public HelpTextDecoration(RectangleAnimation[] animations) => _animations = animations;

        public static HelpTextDecoration ForContext(D2DGraphics g, DateTime now) => new(BuildAnimations(now, g));

        private static RectangleAnimation[] BuildAnimations(DateTime now, D2DGraphics g)
        {
            var res = new RectangleAnimation[HelpText.Length];

            int lastX = 100;
            int lastY = 100;

            var someRandomSize = new D2DSize(1000, 1000);

            for (int i = 0; i < res.Length; ++i)
            {
                var (text, fontSize) = HelpText[i];
                var textSize = g.MeasureText(text, FontName, fontSize, someRandomSize);
                var destination = new Rectangle(
                    lastX,
                    lastY,
                    (int)(textSize.width + 2 * Margin.width),
                    (int)(textSize.height + 2 * Margin.height)
                );

                var start = new Rectangle(
                    destination.Location,
                    new Size(0, destination.Height)
                );

                var animation = new RectangleAnimation(
                    TimeSpan.FromMilliseconds(150 * i + 100),
                    start,
                    destination
                );

                res[i] = animation;
                animation.Start(now);

                lastY = destination.Y + destination.Height + 1;
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
            if (_fadeOutStarted != null)
                opacity = EasingMath.EaseInSquare((float)(now - _fadeOutStarted.Value).TotalMilliseconds / (float)FadeDuration.TotalMilliseconds, 1, 0);

            for (int i = 0; i < _animations.Length; ++i)
            {
                var animation = _animations[i];
                var (text, helpSize) = HelpText[i];

                animation.Update(now, elapsed);
                var rect = animation.Current.AsD2DRect();

                var textLocation = new Rectangle(
                    animation.Destination.X + (int)Margin.width,
                    animation.Destination.Y + (int)Margin.height,
                    animation.Destination.Width,
                    animation.Destination.Height
                );

                g.FillRectangle(rect, new D2DColor(opacity * BackgroundColor.a, BackgroundColor));
                g.DrawText(text, new D2DColor(opacity * FontColor.a, FontColor), FontName, helpSize, textLocation);
            }
        }
        public void Dispose() { }
    }
}
