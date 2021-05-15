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
        // private static readonly D2DColor LabelBackgroundColor = new(0.2f, 1f, 1f, 1f);
        // private static readonly D2DSize LabelPadding = new(4f, 4f);

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

            var placeSize = new D2DSize(1000, 1000);

            //var widthLabelText = outline.Width.ToString() + "px";
            // var widthLabelSize = g.MeasureText(widthLabelText, FontName, FontSize, placeSize);


            #region Width Ruler

            var widthLabelText = outline.Width.ToString() + "px";
            var widthLabelSize = g.MeasureText(widthLabelText, FontName, FontSize, placeSize);

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
                widthLabelRect.Y + widthLabelRect.Height / 2f
            );
            var leftRulerLineEnd = new D2DPoint(
                widthLabelRect.X,
                widthLabelRect.Y + widthLabelRect.Height / 2f
            );
            if (leftRulerLineEnd.x - leftRulerLineStart.x > 0)
            {
                g.DrawLine(leftRulerLineStart, leftRulerLineEnd, RulerColor);
                g.DrawLine(
                    new D2DPoint(leftRulerLineStart.x, leftRulerLineStart.y - 4f),
                    new D2DPoint(leftRulerLineStart.x, leftRulerLineStart.y + 3f),
                    RulerColor
                );
            }

            var rightRulerLineStart = new D2DPoint(
                widthLabelRect.X + widthLabelRect.Width,
                widthLabelRect.Y + widthLabelRect.Height / 2f
            );
            var rightRulerLineEnd = new D2DPoint(
                outline.X + outline.Width,
                widthLabelRect.Y + widthLabelRect.Height / 2f
            );
            if (rightRulerLineEnd.x - rightRulerLineStart.x > 0)
            {
                g.DrawLine(rightRulerLineStart, rightRulerLineEnd, RulerColor);
                g.DrawLine(
                    new D2DPoint(rightRulerLineEnd.x + 1f, rightRulerLineEnd.y - 4f),
                    new D2DPoint(rightRulerLineEnd.x + 1f, rightRulerLineEnd.y + 3f),
                    RulerColor
                );
            }

            #endregion

            var prevAntiAliasing = g.Antialias;
            g.Antialias = true;
            // g.PushTransform();
            // g.RotateTransform(90f);
            // g.FillRectangle(widthLabelRect, D2DColor.Green);
            // g.DrawText(widthLabelText, LabelFontColor, FontName, FontSize, widthLabelRect);
            // g.PopTransform();

            // g.FillRectangle(paddedHeightLabelRect, D2DColor.Green);
            g.DrawText(widthLabelText, LabelFontColor, FontName, FontSize, widthLabelRect);

            g.Antialias = prevAntiAliasing;
        }

        public void Dispose() { }
    }

    /*
    class Label
    {
        private string _text;
        private string _fontName;
        private float _fontSize;
        private D2DColor _fontColor;
        private D2DColor _backgroundColor;
        public Label(string text, string fontName, float fontSize, D2DColor fontColor, D2DColor backgroundColor)
        {
            _text = text;
            _fontName = fontName;
            _fontSize = fontSize;
            _fontColor = fontColor;
            _backgroundColor = backgroundColor;
        }
        void Draw()
        {
            g.DrawText(widthLabelText, LabelFontColor, FontName, FontSize, widthLabelRect);
            g.FillRectangle(widthLabelRect);
        }
    }
    */
}
