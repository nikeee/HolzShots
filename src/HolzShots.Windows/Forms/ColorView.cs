using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HolzShots.Drawing;

namespace HolzShots.Windows.Forms
{
    public class ColorView : Control
    {
        private readonly CheckerboardBrushWrapper _checkerboardBrushWrapper = CheckerboardBrushWrapper.CreateDefault(10);

        private readonly SolidBrush _brush = new SolidBrush(Color.CornflowerBlue);
        public Color Color
        {
            get => _brush.Color;
            set
            {
                if (_brush.Color != value)
                {
                    _brush.Color = value;
                    // Invalidate(new Rectangle(2, 2, Width - 4, Height - 4), false);
                    Invalidate();
                }
            }
        }

        public bool ShowBorder => true;

        protected static readonly Pen DefaultOuterBorderPen = Pens.Gray;
        protected static readonly Pen DefaultInnerBorderPen = SystemPens.Window;
        protected Pen outerBorderPen = DefaultOuterBorderPen;
        protected Pen innerBorderPen = DefaultInnerBorderPen;

        protected override void OnPaintBackground(PaintEventArgs e) { }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            if (ShowBorder)
            {
                g.DrawRectangle(outerBorderPen, 0, 0, Width - 1, Height - 1);
                g.DrawRectangle(innerBorderPen, 1, 1, Width - 3, Height - 3);
            }

            if (_brush.Color.A < 255)
            {
                g.FillRectangle(_checkerboardBrushWrapper.Brush, 2, 2, Width - 4, Height - 4);
            }

            g.FillRectangle(_brush, 2, 2, Width - 4, Height - 4);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _checkerboardBrushWrapper.Dispose();
        }
    }

    public class ColorSelector : ColorView
    {
        protected static readonly Pen HoverOuterBorderPen = new Pen(Color.FromArgb(255, 100, 165, 231));
        protected static readonly Pen HoverInnerBorderPen = new Pen(Color.FromArgb(255, 203, 228, 253));

        public ColorSelector() => Cursor = Cursors.Hand;

        private void ShowColorDialog()
        {
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.FullOpen = true;
                colorDialog.ShowHelp = false;
                colorDialog.Color = Color;

                var res = colorDialog.ShowDialog();
                if (res == DialogResult.OK)
                {
                    OnColorChanged(colorDialog.Color);
                }
            }
        }

        #region Mouse Handling

        protected override void OnClick(EventArgs e)
        {
            if (Enabled)
                ShowColorDialog();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            innerBorderPen = HoverInnerBorderPen;
            outerBorderPen = HoverOuterBorderPen;
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            innerBorderPen = DefaultInnerBorderPen;
            outerBorderPen = DefaultOuterBorderPen;
            Invalidate();
        }

        #endregion

        public event EventHandler<Color> ColorChanged;
        private void OnColorChanged(Color newColor)
        {
            Color = newColor;
            ColorChanged?.Invoke(this, newColor);
        }
    }
}
