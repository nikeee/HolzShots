using System;
using System.Drawing;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Decoration
{
    // TODO: Draw selection dimensions ("Consolas", 9pt)
    class SelectionOutlineDecoration : IStateDecoration<RectangleState>
    {
        private const string FontName = "Consolas";
        private const float FontSize = 14f;
        private static readonly D2DColor LabelFontColor = new(1f, 0.9f, 0.9f, 0.9f);
        private static readonly D2DColor RulerColor = new(0.5f, LabelFontColor);
        private const float AxisDistance = 5f;

        private static readonly D2DColor OutlineColor = D2DColor.White;
        private static readonly float[] CustomDashStyle = new[] { 3f };

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
                OutlineColor,
                D2DDashStyle.Custom,
                CustomDashStyle,
                currentDashOffset
            );

            g.DrawRectangle(selectionOutline, selectionOutlinePen, 1.0f);

            var heightLabelText = outline.Height.ToString() + "px";
            var heightLabelRect = DrawHeightRuler(g, outline, heightLabelText);

            var widthLabelText = outline.Width.ToString() + "px";
            var widthLabelRect = DrawWidthRuler(g, outline, widthLabelText);

            var prevAntiAliasing = g.Antialias;
            g.Antialias = true;

            g.DrawText(heightLabelText, LabelFontColor, FontName, FontSize, heightLabelRect);
            g.DrawText(widthLabelText, LabelFontColor, FontName, FontSize, widthLabelRect);

            g.Antialias = prevAntiAliasing;
        }

        /// <summary> This can be mate prettier (visual rendering appearance as well as the code itself). It works for now. </summary>
        private D2DRect DrawWidthRuler(D2DGraphics g, D2DRect outline, string text)
        {
            var placeSize = new D2DSize(1000, 1000);

            var widthLabelSize = g.MeasureText(text, FontName, FontSize, placeSize);

            var widthLabelRect = new D2DRect(
                outline.X + (outline.Width / 2f) - (widthLabelSize.width / 2f),
                outline.Y,
                widthLabelSize.width,
                widthLabelSize.height
            );

            if (widthLabelRect.Y - widthLabelSize.height - AxisDistance >= 0f)
                widthLabelRect.Offset(0, -widthLabelSize.height - AxisDistance);

            var leftRulerLineStart = new D2DPoint(
                outline.X,
                widthLabelRect.Y + AxisDistance * 2f
            );
            var leftRulerLineEnd = new D2DPoint(
                widthLabelRect.X - AxisDistance,
                widthLabelRect.Y + AxisDistance * 2f
            );
            if (leftRulerLineEnd.x > leftRulerLineStart.x)
            {
                g.DrawLine(leftRulerLineStart, leftRulerLineEnd, RulerColor);
                g.DrawLine(
                    new D2DPoint(leftRulerLineStart.x, leftRulerLineStart.y - 4f),
                    new D2DPoint(leftRulerLineStart.x, leftRulerLineStart.y + 3f),
                    RulerColor
                );
            }

            var rightRulerLineStart = new D2DPoint(
                widthLabelRect.X + widthLabelRect.Width + AxisDistance,
                widthLabelRect.Y + AxisDistance * 2f
            );
            var rightRulerLineEnd = new D2DPoint(
                outline.X + outline.Width,
                widthLabelRect.Y + AxisDistance * 2f
            );
            if (rightRulerLineEnd.x > rightRulerLineStart.x)
            {
                g.DrawLine(rightRulerLineStart, rightRulerLineEnd, RulerColor);
                g.DrawLine(
                    new D2DPoint(rightRulerLineEnd.x + 1f, rightRulerLineEnd.y - 4f),
                    new D2DPoint(rightRulerLineEnd.x + 1f, rightRulerLineEnd.y + 3f),
                    RulerColor
                );
            }

            return widthLabelRect;
        }

        /// <summary> This can be mate prettier (visual rendering appearance as well as the code itself). It works for now. </summary>
        private D2DRect DrawHeightRuler(D2DGraphics g, D2DRect outline, string text)
        {
            var placeSize = new D2DSize(1000, 1000);

            var heightLabelSize = g.MeasureText(text, FontName, FontSize, placeSize);

            var heightLabelRect = new D2DRect(
                outline.X,
                outline.Y + (outline.Height / 2f) - (heightLabelSize.height / 2f),
                heightLabelSize.width,
                heightLabelSize.height
            );

            D2DPoint rulerOffset = D2DPoint.Zero;
            if (heightLabelRect.X - heightLabelSize.width - AxisDistance >= 0f)
            {
                heightLabelRect.Offset(-heightLabelSize.width - AxisDistance, 0);

                rulerOffset = new D2DPoint(-AxisDistance * 2f, 0);
            }


            var upperRulerLineStart = new D2DPoint(
                rulerOffset.x * 2f + outline.X + AxisDistance * 2f,
                rulerOffset.y + outline.Y
            );
            var upperRulerLineEnd = new D2DPoint(
                rulerOffset.x * 2f + outline.X + AxisDistance * 2f,
                rulerOffset.y + heightLabelRect.Y - AxisDistance + 1f
            );
            if (upperRulerLineEnd.y > upperRulerLineStart.y)
            {
                g.DrawLine(upperRulerLineStart, upperRulerLineEnd, RulerColor);
                g.DrawLine(
                    new D2DPoint(upperRulerLineStart.x - 4f, upperRulerLineStart.y),
                    new D2DPoint(upperRulerLineStart.x + 3f, upperRulerLineStart.y),
                    RulerColor
                );
            }

            var lowerRulerLineStart = new D2DPoint(
                rulerOffset.x * 2f + outline.X + AxisDistance * 2f,
                rulerOffset.y + heightLabelRect.Y + heightLabelRect.Height + AxisDistance
            );
            var lowerRulerLineEnd = new D2DPoint(
                rulerOffset.x * 2f + outline.X + AxisDistance * 2f,
                rulerOffset.y + outline.Y + outline.Height
            );
            if (lowerRulerLineEnd.y > lowerRulerLineStart.y)
            {
                g.DrawLine(lowerRulerLineStart, lowerRulerLineEnd, RulerColor);
                g.DrawLine(
                    new D2DPoint(lowerRulerLineEnd.x - 4f, lowerRulerLineEnd.y + 1f),
                    new D2DPoint(lowerRulerLineEnd.x + 3f, lowerRulerLineEnd.y + 1f),
                    RulerColor
                );
            }

            return heightLabelRect;
        }

        public void Dispose() { }
    }
}
