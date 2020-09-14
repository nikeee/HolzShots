using System.ComponentModel;
using System.Drawing;

namespace HolzShots.Windows.Forms
{
    public class CopyCodeLinkLabel : ExplorerLinkLabel
    {
        private static readonly Font _font = new Font("Consolas", 9.75f, FontStyle.Regular, GraphicsUnit.Point); // Consolas; 9,75pt


        [ReadOnly(true)]
        public new Font Font
        {
            get => _font;
            set { }
        }

        public CopyCodeLinkLabel()
        {
            LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
        }
    }
}
