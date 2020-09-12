using System.Runtime.InteropServices;

namespace HolzShots.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Margin
    {
        public static readonly Margin DefaultMargin = new Margin(-1);

        public readonly int cxLeftWidth;
        public readonly int cxRightWidth;
        public readonly int cyTopHeight;
        public readonly int cyBottomheight;

        public Margin(int all)
        {
            cxLeftWidth = all;
            cxRightWidth = all;
            cyTopHeight = all;
            cyBottomheight = all;
        }

        public Margin(int leftWidth, int topHeight, int rightWidth, int bottomHeight)
        {
            cxLeftWidth = leftWidth;
            cxRightWidth = rightWidth;
            cyTopHeight = topHeight;
            cyBottomheight = bottomHeight;
        }

        /*
        public static implicit operator System.Windows.Forms.Padding(Margin mrg)
        {
            return new Padding(mrg.cxLeftWidth, mrg.cyTopHeight, mrg.cxRightWidth, mrg.cyBottomheight);
        }
        public static implicit operator Margin(System.Windows.Forms.Padding fwPadding)
        {
            return new Margin(fwPadding.Left, fwPadding.Top, fwPadding.Right, fwPadding.Bottom);
        }
        */
    }
}
