using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HolzShots.Windows.Forms;

public class ExplorerInfoPanel : Panel
{
    private static readonly Pen _upperPen = new(Color.FromArgb(204, 217, 234));
    private static readonly Pen _secondPen = new(Color.FromArgb(217, 227, 240));
    private static readonly Pen _thirdPen = new(Color.FromArgb(232, 238, 247));

    private static readonly Color _gradientColor1 = Color.FromArgb(237, 242, 249);
    private static readonly Color _gradientColor2 = Color.FromArgb(241, 245, 251);
    private static readonly Point _upperBrushPoint = new(0, 3);

    public override Color BackColor { get; set; } = Color.Transparent;

    public ExplorerInfoPanel()
    {
        Font = new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point);
        ForeColor = Color.FromArgb(255, 30, 57, 91);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);

        base.OnPaint(e);

        if (!Enabled)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), DisplayRectangle);
            return;
        }

        if (Width <= 0 || Height <= 4)
            return;

        switch (Dock)
        {
            case DockStyle.Bottom:
                DrawBottom(e.Graphics);
                break;
            case DockStyle.Top:
                DrawTop(e.Graphics);
                break;
            case DockStyle.Fill:
                DrawFill(e.Graphics);
                break;
            case DockStyle.Left:
                DrawLeft(e.Graphics);
                break;
            case DockStyle.Right:
                DrawRight(e.Graphics);
                break;
            case DockStyle.None:
                DrawFill(e.Graphics);
                break;
            default:
                DrawFill(e.Graphics);
                break;
        }
    }


    private Brush CreateBrushTopDock() => new LinearGradientBrush(_upperBrushPoint, new Point(0, Height - 3), _gradientColor2, _gradientColor1);
    private Brush CreateBrushBottomDock() => new LinearGradientBrush(_upperBrushPoint, new Point(0, Height - 3), _gradientColor1, _gradientColor2);
    private Brush CreateBrushFillDock() => new LinearGradientBrush(_upperBrushPoint, new Point(Width, Height), _gradientColor1, _gradientColor2);
    private Brush CreateBrushLeftDock() => new LinearGradientBrush(_upperBrushPoint, new Point(Width, Height), _gradientColor1, _gradientColor2);
    private Brush CreateBrushRightDock() => new LinearGradientBrush(_upperBrushPoint, new Point(Width, Height), _gradientColor2, _gradientColor1);

    private void DrawBottom(Graphics g)
    {
        if (Height > 6)
            g.FillRectangle(CreateBrushTopDock(), 0, 3, Width, Height - 3);
        g.DrawLine(_upperPen, 0, 0, Width, 0);
        g.DrawLine(_secondPen, 0, 1, Width, 1);
        g.DrawLine(_thirdPen, 0, 2, Width, 2);
    }

    private void DrawTop(Graphics g)
    {
        g.FillRectangle(CreateBrushBottomDock(), 0, 0, Width, Height - 2);
        g.DrawLine(_thirdPen, 0, Height - 3, Width, Height - 3);
        g.DrawLine(_secondPen, 0, Height - 2, Width, Height - 2);
        g.DrawLine(_upperPen, 0, Height - 1, Width, Height - 1);
    }
    private void DrawFill(Graphics g)
    {
        g.FillRectangle(CreateBrushFillDock(), 0, 0, Width, Height);
        g.DrawRectangle(_upperPen, 0, 0, Width - 1, Height - 1);
        g.DrawRectangle(_secondPen, 1, 1, Width - 3, Height - 3);
        g.DrawRectangle(_thirdPen, 2, 2, Width - 5, Height - 5);
    }
    private void DrawLeft(Graphics g)
    {
        g.FillRectangle(CreateBrushLeftDock(), 0, 0, Width - 3, Height - 1);
        g.DrawLine(_upperPen, Width - 1, 0, Width - 1, Height - 1);
        g.DrawLine(_secondPen, Width - 2, 0, Width - 2, Height - 1);
        g.DrawLine(_thirdPen, Width - 3, 0, Width - 3, Height - 1);
    }
    private void DrawRight(Graphics g)
    {
        g.FillRectangle(CreateBrushRightDock(), 0, 0, Width, Height);
        g.DrawLine(_upperPen, 0, 0, 0, Height);
        g.DrawLine(_secondPen, 1, 0, 1, Height);
        g.DrawLine(_thirdPen, 2, 0, 2, Height);
    }
}
