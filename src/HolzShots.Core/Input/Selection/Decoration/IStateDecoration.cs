using System;
using System.Drawing;
using unvell.D2DLib;

namespace HolzShots.Input.Selection.Decoration
{
    internal interface IStateDecoration<T> : IDisposable
        where T : SelectionState
    {
        void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, T state);
    }

    internal class AggregateStateDecoration<T> : IStateDecoration<T>
        where T : SelectionState
    {
        public bool IsInitialized { get; private set; }

        private readonly IStateDecoration<T>[] _decorations;
        public AggregateStateDecoration(params IStateDecoration<T>[] decorations)
        {
            _decorations = decorations ?? throw new ArgumentNullException(nameof(decorations));
        }

        public void Dispose()
        {
            foreach (var d in _decorations)
                d.Dispose();
            IsInitialized = false;
        }

        public void UpdateAndDraw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, T state)
        {
            foreach (var d in _decorations)
                d.UpdateAndDraw(g, now, elapsed, bounds, state);
        }
    }
}
