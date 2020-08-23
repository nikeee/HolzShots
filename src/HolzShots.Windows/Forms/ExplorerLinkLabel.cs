using System;
using System.Drawing;
using System.Windows.Forms;

namespace HolzShots.Windows.Forms
{
    public class ExplorerLinkLabel : LinkLabel
    {
        public ExplorerLinkLabel()
            : base()
        {
            LinkBehavior = LinkBehavior.HoverUnderline;
            Cursor = Cursors.Hand;
            ActiveLinkColor = SystemColors.Highlight;
            LinkColor = SystemColors.HotTrack;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            LinkColor = SystemColors.Highlight;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            LinkColor = SystemColors.HotTrack;
        }
    }
}
