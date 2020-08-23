using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HolzShots.Windows.Forms
{
    /// <summary>
    /// Idea based on: https://stackoverflow.com/a/32788174
    /// This was necessary because the previously used ContextMenu is removed in .NET Core's WinForms.
    /// ContextMenuStrip is the replacement. However, it does not look anywhere close to what is used in the rest of windows.
    /// See: https://github.com/dotnet/winforms/issues/2543
    /// -> So we try to mimic that look.
    /// </summary>
    public class HolzShotsToolStripRenderer : ToolStripProfessionalRenderer
    {
        public HolzShotsToolStripRenderer() : base(new LightColorTable()) { }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new Rectangle(e.ArrowRectangle.Location, e.ArrowRectangle.Size);
            r.Inflate(-2, -6);

            e.Graphics.DrawLines(Pens.Black, new Point[]{
        new Point(r.Left, r.Top),
        new Point(r.Right, r.Top + r.Height /2),
        new Point(r.Left, r.Top+ r.Height)});
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new Rectangle(e.ImageRectangle.Location, e.ImageRectangle.Size);
            r.Inflate(-4, -6);
            e.Graphics.DrawLines(Pens.Black, new Point[]{
        new Point(r.Left, r.Bottom - r.Height /2),
        new Point(r.Left + r.Width /3,  r.Bottom),
        new Point(r.Right, r.Top)});
        }
    }

    public class LightColorTable : ProfessionalColorTable
    {
        public override Color MenuItemBorder => Color.WhiteSmoke;
        public override Color MenuItemSelected => Color.WhiteSmoke;
        public override Color ToolStripDropDownBackground => Color.White;
        public override Color ImageMarginGradientBegin => Color.White;
        public override Color ImageMarginGradientMiddle => Color.White;
        public override Color ImageMarginGradientEnd => Color.White;
    }
}
