using System.Drawing;
using System.Windows.Forms;

namespace HolzShots.Input;

public interface IAreaSelector : IDisposable
{
    Task<SelectionResult> PromptSelectionAsync();
}

public record SelectionResult(
    Rectangle SelectedRectangle,
    WindowRectangle? SelectedWindowInfo = null
);

public record WindowRectangle(IntPtr Handle, Rectangle Rectangle, string? Title) : IWin32Window
{
    public override int GetHashCode() => Handle.GetHashCode();
}
