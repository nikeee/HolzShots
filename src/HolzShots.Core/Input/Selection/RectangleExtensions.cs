using System.Drawing;

namespace HolzShots.Input.Selection
{
    public static class RectangleExtensions
    {
        public static bool HasArea(this Rectangle value) => value.Width > 0 && value.Height > 0;
    }
}
