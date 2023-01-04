using System.Drawing;
using System.Windows.Forms;

namespace HolzShots.Windows.Forms;

/// <summary>
/// We inherit from Label instead of LinkLabel because LinkLabel is way to over-powered for our use-case.
/// Also, LinkLabel is buggy on HighDPI (font size changes on hover).
/// </summary>
public class ExplorerLinkLabel : Label
{
    private Font? _defaultFont;
    private Font? _hoverFont;

    public ExplorerLinkLabel() : base()
    {
        Cursor = Cursors.Hand;
        ForeColor = SystemColors.HotTrack;
    }

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        _defaultFont = Font;
        _hoverFont = new Font(_defaultFont, FontStyle.Underline);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        ForeColor = SystemColors.Highlight;
        Font = _hoverFont!;
    }
    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        ForeColor = SystemColors.HotTrack;
        Font = _defaultFont!;
    }
}
