using System;
using System.Diagnostics;
using System.Drawing;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Decoration
{
    class MouseWindowOutlineDecoration : IStateDecoration<InitialState>
    {
        private const string FontName = "Consolas";
        private const float FontSize = 14.0f;
        private D2DColor FontColor = D2DColor.WhiteSmoke;
        private D2DColor OutlineColor = D2DColor.White;

        public static MouseWindowOutlineDecoration ForContext(D2DGraphics g, DateTime now) => new();

        public void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image, InitialState state)
        {
            var outlineAnimation = state.CurrentOutline;
            if (outlineAnimation == null)
                return;

            Debug.Assert(state.Title != null);

            outlineAnimation.Update(now, elapsed);

            var rect = outlineAnimation.Current;
            var selectionOutline = rect.AsD2DRect();

            g.DrawRectangle(selectionOutline, OutlineColor, 1.0f);
            g.DrawTextCenter(state.Title, FontColor, FontName, FontSize, selectionOutline);
        }

        public void Dispose() { }
    }
}
