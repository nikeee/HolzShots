using System.Drawing;
using nud2dlib;

namespace HolzShots.Input.Selection.Decoration;

interface IStateDecoration<T> : IDisposable
    where T : SelectionState
{
    void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image, T state);
}
