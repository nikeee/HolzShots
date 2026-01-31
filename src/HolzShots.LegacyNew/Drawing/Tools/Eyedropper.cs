using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Numerics;
using HolzShots.Drawing.Tools.UI;
using HolzShots.Windows.Forms;

namespace HolzShots.Drawing.Tools;

public class Eyedropper : ITool<ToolSettingsBase>
{
    public ShotEditorTool ToolType => ShotEditorTool.Eyedropper;

    public Cursor Cursor => Cursors.Cross;
    public ISettingsControl<ToolSettingsBase>? SettingsControl => null; // TODO Change to default(_) if this is not a reference type

    public Vector2 BeginCoordinates { get; set; }
    public Vector2 EndCoordinates { get; set; }

    public void MouseOnlyMoved(Image rawImage, ref Cursor currentCursor, MouseEventArgs e)
    {
        Debug.Assert(rawImage is Bitmap);
        var rawBmp = rawImage is Bitmap ? (Bitmap)rawImage : new Bitmap(rawImage);
        if (new Rectangle(0, 0, rawImage.Width, rawImage.Height).Contains(e.Location))
        {
            // currentCursor.Dispose()
            currentCursor = new Cursor(DrawCursor(rawBmp.GetPixel(e.X, e.Y)).Handle);
        }
    }

    private static readonly Bitmap EyedropperCursor = new(195, 195);
    private static readonly Pen PenWhite = new(Color.FromArgb(180, 255, 255, 255));

    private static readonly Rectangle CursorRectangle = new (10, 10, EyedropperCursor.Width - 20, EyedropperCursor.Height - 20);

    private Bitmap _cursorImage = new(28, 28);
    private readonly Pen _cursorPen = new(Brushes.Black);

    public void LoadInitialSettings() { }
    public void PersistSettings() { }

    private Icon DrawCursor(Color c)
    {
        _cursorImage = new Bitmap(195, 195);
        _cursorImage.MakeTransparent();

        using var cursorGraphics = Graphics.FromImage(_cursorImage);
        cursorGraphics.SmoothingMode = SmoothingMode.AntiAlias;
        _cursorPen.Width = 1;
        _cursorPen.Color = c;

        cursorGraphics.DrawLine(PenWhite, 10, Convert.ToInt32(195 / (double)2) - 1, Convert.ToInt32(195 / (double)2) - 1, Convert.ToInt32(195 / (double)2) - 1);
        cursorGraphics.DrawLine(PenWhite, Convert.ToInt32(195 / (double)2), Convert.ToInt32(195 / (double)2) - 1, 190, Convert.ToInt32(195 / (double)2) - 1);
        cursorGraphics.DrawLine(PenWhite, Convert.ToInt32(195 / (double)2) - 1, 10, Convert.ToInt32(195 / (double)2) - 1, Convert.ToInt32(195 / (double)2) - 2);

        _cursorPen.Width = 18;

        cursorGraphics.DrawEllipse(_cursorPen, CursorRectangle);

        var hIcon = _cursorImage.GetHicon();

        Icon ico;
        using var temp = Icon.FromHandle(hIcon);
        ico = (Icon)temp.Clone();

        Native.User32.DestroyIcon(hIcon);
        return ico;
    }

    public void MouseClicked(Image rawImage, Vector2 e, ref Cursor currentCursor, Control trigger)
    {
        Debug.Assert(rawImage is Bitmap);
        var rawBmp = rawImage is Bitmap ? (Bitmap)rawImage : new Bitmap(rawImage);
        var c = rawBmp.GetPixel((int)e.X, (int)e.Y);
        var viewer = new CopyColorForm(c, trigger.PointToScreen(e.ToPoint2D()));
        viewer.Show();
    }

    public void Dispose()
    {
        _cursorImage.Dispose();
        _cursorPen.Dispose();
    }
    public void RenderFinalImage(ref Image rawImage) { }
    public void RenderPreview(Image rawImage, Graphics g) { }
}
