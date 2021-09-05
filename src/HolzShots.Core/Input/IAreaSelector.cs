using System.Drawing;

namespace HolzShots.Input
{
    public interface IAreaSelector : IDisposable
    {
        Task<Rectangle> PromptSelectionAsync();
    }
}
