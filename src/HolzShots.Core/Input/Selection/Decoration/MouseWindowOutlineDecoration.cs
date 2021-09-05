using System.Drawing;
using nud2dlib;

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
            var outlineAnimation = state.CurrentOutlineAnimation;
            if (outlineAnimation == null)
                return;

            outlineAnimation.Update(now);

            var rect = outlineAnimation.Current;
            g.DrawRectangle(rect, OutlineColor, 1.0f);

            var windowTitle = state.Title;
            if (windowTitle != null)
                g.DrawTextCenter(windowTitle, FontColor, FontName, FontSize, rect);
        }

        public void Dispose() { }
    }
}
