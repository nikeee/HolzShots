using System.Numerics;
using HolzShots.Drawing.Tools.UI;

namespace HolzShots.Drawing.Tools;

public class NoTool : ITool<ToolSettingsBase>
{
    public ShotEditorTool ToolType => ShotEditorTool.None;
    public Cursor Cursor => Cursors.Default;
    public ISettingsControl<ToolSettingsBase>? SettingsControl => null;
    public Vector2 BeginCoordinates { get; set; }
    public Vector2 EndCoordinates { get; set; }
    public void Dispose() { }
    public void LoadInitialSettings() { }
    public void MouseClicked(Image rawImage, Vector2 e, ref Cursor currentCursor, Control trigger) { }
    public void MouseOnlyMoved(Image rawImage, ref Cursor currentCursor, MouseEventArgs e) { }
    public void PersistSettings() { }
    public void RenderFinalImage(ref Image rawImage) { }
    public void RenderPreview(Image rawImage, Graphics g) { }
}
