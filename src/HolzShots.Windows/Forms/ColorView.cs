using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HolzShots.Windows.Forms
{
    public class ColorView : Control
    {
        private static readonly Brush _checkerboardBrush = new HatchBrush(
            HatchStyle.LargeCheckerBoard,
            Color.FromArgb(255, 204, 204, 204),
            Color.White
        );

        private readonly SolidBrush _brush = new SolidBrush(Color.CornflowerBlue);
        public Color Color
        {
            get => _brush.Color;
            set
            {
                if (_brush.Color != value)
                {
                    _brush.Color = value;
                    Invalidate(new Rectangle(2, 2, Width - 4, Height - 4), false);
                }
            }
        }

        public bool ShowBorder => true;

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            var g = e.Graphics;

            if (ShowBorder)
            {
                g.DrawRectangle(Pens.Gray, 0, 0, Width - 1, Height - 1);
            }

            if (_brush.Color.A < 255)
            {
                var originalOrigin = g.RenderingOrigin;

                g.RenderingOrigin = new Point(originalOrigin.X - 2, originalOrigin.Y - 2);
                g.FillRectangle(_checkerboardBrush, 2, 2, Width - 4, Height - 4);
                g.RenderingOrigin = originalOrigin;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            g.FillRectangle(_brush, 2, 2, Width - 4, Height - 4);
        }
    }
}
