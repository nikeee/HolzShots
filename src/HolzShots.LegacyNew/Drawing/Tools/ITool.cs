using HolzShots.Drawing.Tools.UI;

namespace HolzShots.Drawing.Tools;

public interface ITool<out TSettings> : IDisposable where TSettings : ToolSettingsBase
{
    ShotEditorTool ToolType { get; }
    Cursor Cursor { get; }
    ISettingsControl<TSettings> SettingsControl { get; }

    Point BeginCoordinates { get; set; }
    Point EndCoordinates { get; set; }

    void RenderFinalImage(ref Image rawImage);
    void RenderPreview(Image rawImage, Graphics g);
    void MouseOnlyMoved(Image rawImage, ref Cursor currentCursor, MouseEventArgs e);
    void MouseClicked(Image rawImage, Point e, ref Cursor currentCursor, Control trigger);

    void LoadInitialSettings();
    void PersistSettings();
}
