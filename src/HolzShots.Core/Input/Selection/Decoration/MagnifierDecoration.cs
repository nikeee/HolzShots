using System;
using System.Drawing;
using System.Numerics;
using HolzShots.Input.Selection.Animation;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Decoration
{
    internal class MagnifierDecoration : IStateDecoration<SelectionState>
    {
        // private static readonly D2DSize Margin = new(10, 5);
        // private static readonly D2DColor BackgroundColor = new(0.2f, 1f, 1f, 1f);
        // private static readonly D2DColor FontColor = new(1f, 0.9f, 0.9f, 0.9f);
        // private static readonly TimeSpan FadeStart = TimeSpan.FromSeconds(5);
        private static readonly D2DColor OutlineColor = new(1f, 0.9f, 0.9f, 0.9f);
        private static readonly TimeSpan FadeDuration = TimeSpan.FromMilliseconds(200);
        private const float CircleDiameter = 100f;

        private bool _draw = false;
        private Vector2Animation? _ellipseAnimation;

        public void Toggle()
        {
            _draw = !_draw;
            if (!_draw)
                _ellipseAnimation = null;
        }

        public void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, SelectionState state)
        {
            if (!_draw)
                return;

            if (_ellipseAnimation == null)
            {
                _ellipseAnimation = new Vector2Animation(FadeDuration, Vector2.Zero, new Vector2(CircleDiameter));
                _ellipseAnimation.Start(now);
            }

            _ellipseAnimation.Update(now, elapsed);

            var center = new Vector2(state.CursorPosition.X, state.CursorPosition.Y);

            var ellipse = new D2DEllipse(
                center.X,
                center.Y,
                _ellipseAnimation.Current.X,
                _ellipseAnimation.Current.Y
            );

            g.FillEllipse(ellipse, D2DColor.WhiteSmoke);
            g.DrawEllipse(ellipse, OutlineColor);

        }
        public void Dispose() { }
    }
}
