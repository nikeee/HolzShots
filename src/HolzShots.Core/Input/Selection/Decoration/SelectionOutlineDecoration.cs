using System;
using System.Drawing;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Decoration
{
    // TODO: Draw selection dimensions ("Consolas", 9pt)
    class SelectionOutlineDecoration : IStateDecoration<RectangleState>
    {
        private static readonly D2DColor _outlineColor = D2DColor.White;
        private static readonly float[] _customDashStyle = new[] { 3f };

        private DateTime _selectionStarted = DateTime.Now;

        public static SelectionOutlineDecoration ForContext(D2DGraphics g, DateTime now) => new();

        public void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image, RectangleState state)
        {
            var outline = state.GetSelectedOutline(bounds);
            D2DRect rect = outline; // Caution: implicit conversion which we don't want to do twice

            g.DrawBitmap(image, rect, rect);

            // We need to widen the rectangle by 0.5px so that the result will be exactly 1px wide.
            // Otherwise, it will be 2px and darker.
            var selectionOutline = outline.AsD2DRect();

            var currentDashOffset = (float)(now - _selectionStarted).TotalMilliseconds / 40;

            using var selectionOutlinePen = g.Device.CreatePen(
                _outlineColor,
                D2DDashStyle.Custom,
                _customDashStyle,
                currentDashOffset
            );

            g.DrawRectangle(selectionOutline, selectionOutlinePen, 1.0f);
        }

        public void Dispose() { }
    }
}
