using System.Drawing;

namespace HolzShots.Windows.Forms
{
    public class CopyCodeLinkLabel : ExplorerLinkLabel
    {
        private static readonly Font _font = new Font("Consolas", 9.75f, FontStyle.Regular, GraphicsUnit.Point); // Consolas; 9,75pt

        public CopyCodeLinkLabel()
        {
            Font = _font;
        }
    }
}
