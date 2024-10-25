using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using nud2dlib;
using nud2dlib.Windows.Forms;

namespace HolzShots.Input.Selection;

public abstract class AnimatedForm : Form
{

    private int _currentFps = 0;
    private int _lastFps = 0;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool DrawFPS { get; set; }

    private DateTime _lastFpsUpdate = DateTime.Now;
    private D2DGraphics _graphics = null!;

    private D2DDevice? _device;
    public D2DDevice Device => _device ??= D2DDevice.FromHwnd(Handle);

    private DateTime _prevUpdate;

    public AnimatedForm()
    {
        StartPosition = FormStartPosition.Manual;
        WindowState = FormWindowState.Normal;
        FormBorderStyle = FormBorderStyle.None;
        SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void CreateHandle()
    {
        base.CreateHandle();

        DoubleBuffered = false;

        _graphics = new D2DGraphics(Device);
        _graphics.SetDPI(96, 96);
    }

    protected abstract void Draw(DateTime now, TimeSpan elapsed, D2DGraphics g);

    protected override void OnPaint(PaintEventArgs e)
    {
        if (DesignMode)
        {
            e.Graphics.Clear(Color.Black);
            e.Graphics.DrawString("D2DLib windows form cannot render in design time.", Font, Brushes.White, 10, 10);
            return;
        }

        var now = DateTime.Now;

        Draw(now, now - _prevUpdate, _graphics);

        _prevUpdate = now;

        if (DrawFPS)
        {
            if (_lastFpsUpdate.Second != DateTime.Now.Second)
            {
                _lastFps = _currentFps;
                _currentFps = 0;
                _lastFpsUpdate = DateTime.Now;
            }
            else
            {
                ++_currentFps;
            }

            var fpsInfo = $"{_lastFps} fps";
            var size = e.Graphics.MeasureString(fpsInfo, Font, Width);
            _graphics.DrawText(fpsInfo, D2DColor.Silver, "Consolas", 26.0f, ClientRectangle.Right - size.Width - 10, 5);
        }

        _graphics.EndRender();

        Invalidate(false);
    }

    protected override void OnPaintBackground(PaintEventArgs e) { /* prevent the .NET windows form to paint the original background */ }

    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case (int)Win32.WMessages.WM_ERASEBKGND:
                break;

            case (int)Win32.WMessages.WM_SIZE:
                base.WndProc(ref m);
                if (Device is not null)
                {
                    Device.Resize();
                    Invalidate(false);
                }
                break;
            case (int)Win32.WMessages.WM_DESTROY:
                Device?.Dispose();

                base.WndProc(ref m);
                break;
            default:
                base.WndProc(ref m);
                break;
        }
    }
}
