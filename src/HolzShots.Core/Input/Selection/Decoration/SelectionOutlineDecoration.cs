using System.Drawing;
using System.Numerics;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Decoration;

class SelectionOutlineDecoration : IStateDecoration<RectangleState>
{
    private const string FontName = "Consolas";
    private const float FontSize = 14f;
    private static readonly D2DColor LabelFontColor = new(1f, 0.9f, 0.9f, 0.9f);
    private static readonly D2DColor RulerColor = new(0.5f, LabelFontColor);
    private const float AxisDistance = 5f;

    private static readonly D2DColor OutlineColor = D2DColor.White;
    private static readonly float[] CustomDashStyle = [3f];

    private readonly DateTime _selectionStarted = DateTime.Now;

    public static SelectionOutlineDecoration ForContext(D2DGraphics g, DateTime now) => new();

    public void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image, RectangleState state)
    {
        var device = g.Device;
        if (device == null)
            return;

        var outline = state.GetSelectedOutline(bounds);
        D2DRect rect = outline; // Caution: implicit conversion which we don't want to do twice

        g.DrawBitmap(image, rect, rect);

        // We need to widen the rectangle by 0.5px so that the result will be exactly 1px wide.
        // Otherwise, it will be 2px and darker.
        var selectionOutline = outline.AsD2DRect();

        var currentDashOffset = (float)(now - _selectionStarted).TotalMilliseconds / 40;

        using var selectionOutlinePen = device.CreatePen(
            OutlineColor,
            D2DDashStyle.Custom,
            CustomDashStyle,
            currentDashOffset
        );

        if (selectionOutlinePen == null)
            return;

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
    private static D2DRect DrawWidthRuler(D2DGraphics g, D2DRect outline, string text)
    {
        var placeSize = new Vector2(1000, 1000);

        var widthLabelSize = g.MeasureText(text, FontName, FontSize, placeSize);

        var widthLabelRect = new D2DRect(
            outline.X + (outline.Width / 2f) - (widthLabelSize.width / 2f),
            outline.Y,
            widthLabelSize.width,
            widthLabelSize.height
        );

        if (widthLabelRect.Y - widthLabelSize.height - AxisDistance >= 0f)
            widthLabelRect.Offset(0, -widthLabelSize.height - AxisDistance);

        var leftRulerLineStart = new Vector2(
            outline.X,
            widthLabelRect.Y + AxisDistance * 2f
        );
        var leftRulerLineEnd = new Vector2(
            widthLabelRect.X - AxisDistance,
            widthLabelRect.Y + AxisDistance * 2f
        );

        if (leftRulerLineEnd.X > leftRulerLineStart.X)
        {
            g.DrawLine(leftRulerLineStart, leftRulerLineEnd, RulerColor);
            g.DrawLine(
                new Vector2(leftRulerLineStart.X, leftRulerLineStart.Y - 4f),
                new Vector2(leftRulerLineStart.X, leftRulerLineStart.Y + 3f),
                RulerColor
            );
        }

        var rightRulerLineStart = new Vector2(
            widthLabelRect.X + widthLabelRect.Width + AxisDistance,
            widthLabelRect.Y + AxisDistance * 2f
        );
        var rightRulerLineEnd = new Vector2(
            outline.X + outline.Width,
            widthLabelRect.Y + AxisDistance * 2f
        );

        if (rightRulerLineEnd.X > rightRulerLineStart.X)
        {
            g.DrawLine(rightRulerLineStart, rightRulerLineEnd, RulerColor);
            g.DrawLine(
                new Vector2(rightRulerLineEnd.X + 1f, rightRulerLineEnd.Y - 4f),
                new Vector2(rightRulerLineEnd.X + 1f, rightRulerLineEnd.Y + 3f),
                RulerColor
            );
        }

        return widthLabelRect;
    }

    /// <summary> This can be mate prettier (visual rendering appearance as well as the code itself). It works for now. </summary>
    private static D2DRect DrawHeightRuler(D2DGraphics g, D2DRect outline, string text)
    {
        var placeSize = new Vector2(1000, 1000);

        var heightLabelSize = g.MeasureText(text, FontName, FontSize, placeSize);

        var heightLabelRect = new D2DRect(
            outline.X,
            outline.Y + (outline.Height / 2f) - (heightLabelSize.height / 2f),
            heightLabelSize.width,
            heightLabelSize.height
        );

        var rulerOffset = Vector2.Zero;
        if (heightLabelRect.X - heightLabelSize.width - AxisDistance >= 0f)
        {
            heightLabelRect.Offset(-heightLabelSize.width - AxisDistance, 0);

            rulerOffset = new Vector2(-AxisDistance * 2f * 2f, 0);
        }


        var upperRulerLineStart = rulerOffset + new Vector2(
             outline.X + AxisDistance * 2f,
             outline.Y
        );
        var upperRulerLineEnd = rulerOffset + new Vector2(
            outline.X + AxisDistance * 2f,
            heightLabelRect.Y - AxisDistance + 1f
        );

        if (upperRulerLineEnd.Y > upperRulerLineStart.Y)
        {
            g.DrawLine(upperRulerLineStart, upperRulerLineEnd, RulerColor);
            g.DrawLine(
                new Vector2(upperRulerLineStart.X - 4f, upperRulerLineStart.Y),
                new Vector2(upperRulerLineStart.X + 3f, upperRulerLineStart.Y),
                RulerColor
            );
        }

        var lowerRulerLineStart = rulerOffset + new Vector2(
            outline.X + AxisDistance * 2f,
            heightLabelRect.Y + heightLabelRect.Height + AxisDistance
        );
        var lowerRulerLineEnd = rulerOffset + new Vector2(
            outline.X + AxisDistance * 2f,
            outline.Y + outline.Height
        );

        if (lowerRulerLineEnd.Y > lowerRulerLineStart.Y)
        {
            g.DrawLine(lowerRulerLineStart, lowerRulerLineEnd, RulerColor);
            g.DrawLine(
                new Vector2(lowerRulerLineEnd.X - 4f, lowerRulerLineEnd.Y + 1f),
                new Vector2(lowerRulerLineEnd.X + 3f, lowerRulerLineEnd.Y + 1f),
                RulerColor
            );
        }

        return heightLabelRect;
    }

    public void Dispose() { }
}
