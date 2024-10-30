using System.Drawing;
using System.Numerics;
using HolzShots.Input.Selection.Animation;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Decoration;

internal class MagnifierDecoration : IStateDecoration<SelectionState>
{
    private static readonly D2DColor OutlineColor = new(1f, 0.9f, 0.9f, 0.9f);
    private static readonly TimeSpan FadeDuration = TimeSpan.FromMilliseconds(150);
    private const int CircleDiameter = 75;

    private bool _draw = false;
    private Vector2Animation? _ellipseAnimation;

    public void Toggle()
    {
        _draw = !_draw;
        if (!_draw)
            _ellipseAnimation = null;
    }

    public void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image, SelectionState state)
    {
        if (!_draw)
            return;

        if (_ellipseAnimation == null)
        {
            _ellipseAnimation = new Vector2Animation(now, FadeDuration, Vector2.Zero, new Vector2(CircleDiameter));
        }

        _ellipseAnimation.Update(now);

        var cursorPos = state.CursorPosition;

        var center = new Vector2(cursorPos.X, cursorPos.Y);
        var size = new Vector2(_ellipseAnimation.Current.X, _ellipseAnimation.Current.Y);
        var ellipse = new D2DEllipse(center, size);

        using (var ellipseGeometry = g.Device.CreateEllipseGeometry(center, size))
        {
            // TODO: This is bug in corners of the screen. However, we don't care at the moment

            g.PushLayer(ellipseGeometry);

            var sourceRectangle = new Rectangle(cursorPos, new Size(CircleDiameter, CircleDiameter));
            sourceRectangle.Offset(-CircleDiameter / 2, -CircleDiameter / 2);

            var destinationRectangle = new Rectangle(
              cursorPos,
              new Size(CircleDiameter * 2, CircleDiameter * 2)
            );
            destinationRectangle.Offset(-CircleDiameter, -CircleDiameter);

            g.DrawBitmap(
                image,
                destinationRectangle,
                sourceRectangle,
                interpolationMode: D2DBitmapInterpolationMode.NearestNeighbor
            );

            g.PopLayer();
        }

        var prevAntiAlias = g.Antialias;
        g.Antialias = true;

        var percentageCompleted = _ellipseAnimation.Completed;
        g.DrawEllipse(ellipse, new D2DColor(percentageCompleted, OutlineColor));

        g.Antialias = prevAntiAlias;

    }
    public void Dispose() { }
}
