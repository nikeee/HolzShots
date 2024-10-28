
namespace HolzShots.Drawing.Tools;

public interface IDialogTool : IDisposable
{
    void ShowToolDialog(ref Image rawImage, Screenshot screenshot, IWin32Window? parent);
    void LoadInitialSettings();
    void PersistSettings();
}
