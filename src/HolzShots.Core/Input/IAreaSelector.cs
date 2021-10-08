using System;
using System.Drawing;
using System.Threading.Tasks;
using HolzShots.Input.Selection;

namespace HolzShots.Input
{
    public interface IAreaSelector : IDisposable
    {
        Task<SelectionResult> PromptSelectionAsync();
    }

    public record SelectionResult(
        Rectangle SelectedRectangle,
        WindowRectangle? SelectedWindowInfo = null
    );

    public record WindowRectangle(IntPtr Handle, Rectangle Rectangle, string? Title)
    {
        public override int GetHashCode() => Handle.GetHashCode();
    }
}
