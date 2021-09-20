using System;
using System.Drawing;
using System.Threading.Tasks;

namespace HolzShots.Input
{
    public interface IAreaSelector : IDisposable
    {
        Task<Rectangle> PromptSelectionAsync();
    }
}
