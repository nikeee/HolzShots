using System;
using System.Drawing;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Decoration
{
    // TODO: Draw selection dimensions ("Consolas", 9pt)
    class SelectionOutlineDecoration : IStateDecoration<RectangleState>
    {
        public bool IsInitialized { get; private set; }

        public void Initialize(D2DDevice device, D2DGraphics g, DateTime now)
        {
            //throw new NotImplementedException();
        }
        public void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image, RectangleState state)
        {
            // throw new NotImplementedException();
        }

        public void Dispose() => IsInitialized = false;
    }
}
