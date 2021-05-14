using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Decoration
{
    class SelectionOutlineDecoration : IStateDecoration<RectangleState>
    {
        public bool IsInitialized { get; private set; }

        public void Initialize(D2DDevice device, D2DGraphics g, DateTime now)
        {
            //throw new NotImplementedException();
        }
        public void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, RectangleState state)
        {
            // throw new NotImplementedException();
        }

        public void Dispose() => IsInitialized = false;
    }
}
