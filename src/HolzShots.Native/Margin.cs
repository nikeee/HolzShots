using System.Runtime.InteropServices;

namespace HolzShots.Native;

[StructLayout(LayoutKind.Sequential)]
public readonly struct Margin
{
    public static readonly Margin DefaultMargin = new(-1);

    public readonly int cxLeftWidth;
    public readonly int cxRightWidth;
    public readonly int cyTopHeight;
    public readonly int cyBottomHeight;

    public Margin(int all)
    {
        cxLeftWidth = all;
        cxRightWidth = all;
        cyTopHeight = all;
        cyBottomHeight = all;
    }

    public Margin(int leftWidth, int topHeight, int rightWidth, int bottomHeight)
    {
        cxLeftWidth = leftWidth;
        cxRightWidth = rightWidth;
        cyTopHeight = topHeight;
        cyBottomHeight = bottomHeight;
    }

    public static bool operator ==(Margin left, Margin right) => left.Equals(right);
    public static bool operator !=(Margin left, Margin right) => !(left == right);
    public override readonly bool Equals(object? obj) => obj is not null && obj is Margin m && Equals(m);
    public readonly bool Equals(Margin obj) =>
        cxLeftWidth == obj.cxLeftWidth
        && cxRightWidth == obj.cxRightWidth
        && cyBottomHeight == obj.cyBottomHeight
        && cyTopHeight == obj.cyTopHeight;
    public readonly override int GetHashCode() => HashCode.Combine(cxLeftWidth, cxRightWidth, cyBottomHeight, cyTopHeight);

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
