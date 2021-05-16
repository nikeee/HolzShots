using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using HolzShots.Input.Selection.Animation;
using HolzShots.Input.Selection.Decoration;
using nud2dlib;

namespace HolzShots.Input.Selection
{
    abstract class SelectionState : IDisposable
    {
        public Point CursorPosition { get; protected set; }

        public virtual void EnsureDecorationInitialization(D2DGraphics g, DateTime now) { }
        public abstract void Draw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image);
        public virtual void UpdateCursorPosition(Point newCursorPosition) => CursorPosition = newCursorPosition;
        public abstract void Dispose();
    }

    class InitialState : SelectionState
    {
        private WindowRectangle? _currentSelectedWindow;

        public Rectangle? CurrentOutline { get; private set; }
        public RectangleAnimation? CurrentOutlineAnimation { get; private set; }
        public string? Title { get; private set; }

        public IStateDecoration<InitialState>[] Decorations { get; private set; } = null!;

        public override void Draw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image)
        {
            Debug.Assert(Decorations != null);
            foreach (var d in Decorations)
                d.UpdateAndDraw(g, now, elapsed, bounds, image, this);
        }

        public override void EnsureDecorationInitialization(D2DGraphics g, DateTime now)
        {
            Decorations ??= new IStateDecoration<InitialState>[] {
                HelpTextDecoration.ForContext(g, now),
                MouseWindowOutlineDecoration.ForContext(g, now),
            };
        }

        public void SelectWindowBasedOnMouseMove(IReadOnlyList<WindowRectangle>? windows, Point cursorPosition)
        {
            if (windows == null)
            {
                ResetWindowHighlight();
                return;
            }

            var previousWindow = _currentSelectedWindow;
            foreach (var candidate in windows)
            {
                if (candidate.Rectangle.Contains(cursorPosition))
                {
                    if (previousWindow != candidate)
                        HighlightWindow(candidate);
                    return;
                }
            }

            ResetWindowHighlight();
        }

        private void HighlightWindow(WindowRectangle candidate)
        {
            var source = CurrentOutline ?? candidate.Rectangle;

            _currentSelectedWindow = candidate;

            Title = candidate.Title;
            CurrentOutline = candidate.Rectangle;
            CurrentOutlineAnimation = new RectangleAnimation(
                DateTime.Now,
                TimeSpan.FromMilliseconds(100),
                source,
                candidate.Rectangle
            );
        }
        private void ResetWindowHighlight()
        {
            CurrentOutline = null;
            CurrentOutlineAnimation = null;
            Title = null;
        }

        public override void Dispose()
        {
            var ds = Decorations;
            foreach (var d in ds)
                d.Dispose();
        }
    }

    abstract class RectangleState : SelectionState
    {
        public Point UserSelectionStart { get; protected set; }
        public IStateDecoration<RectangleState>[] Decorations { get; private set; } = null!;

        protected RectangleState(Point userSelectionStart, Point cursorPosition)
        {
            UserSelectionStart = userSelectionStart;
            CursorPosition = cursorPosition;
        }

        public Rectangle GetSelectedOutline(Rectangle canvasBounds)
        {
            var start = UserSelectionStart;
            var cursor = CursorPosition;
            var x = Math.Min(start.X, cursor.X);
            var y = Math.Min(start.Y, cursor.Y);
            var width = Math.Abs(start.X - cursor.X);
            var height = Math.Abs(start.Y - cursor.Y);

            var unconstrainedSelection = new Rectangle(x, y, width, height);

            return Rectangle.Intersect(unconstrainedSelection, canvasBounds);
        }

        public override void EnsureDecorationInitialization(D2DGraphics g, DateTime now)
        {
            Decorations ??= new IStateDecoration<RectangleState>[] {
                SelectionOutlineDecoration.ForContext(g, now),
            };
        }

        public override void Draw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image)
        {
            Debug.Assert(Decorations != null);
            foreach (var d in Decorations)
                d.UpdateAndDraw(g, now, elapsed, bounds, image, this);
        }
        public override void Dispose()
        {
            var ds = Decorations;
            foreach (var d in ds)
                d.Dispose();
        }
    }

    class ResizingRectangleState : RectangleState
    {
        public ResizingRectangleState(Point userSelectionStart, Point cursorPosition) : base(userSelectionStart, cursorPosition) { }
    }

    class MovingRectangleState : RectangleState
    {
        public MovingRectangleState(Point userSelectionStart, Point cursorPosition) : base(userSelectionStart, cursorPosition) { }

        public override void UpdateCursorPosition(Point newCursorPosition)
        {
            var prevStart = UserSelectionStart;
            var prev = CursorPosition;
            var offset = new Point(
                newCursorPosition.X - prev.X,
                newCursorPosition.Y - prev.Y
            );
            UserSelectionStart = new Point(
                prevStart.X + offset.X,
                prevStart.Y + offset.Y
            );
            CursorPosition = newCursorPosition;
        }
    }

    class FinalState : SelectionState
    {
        public Rectangle Result { get; }
        public FinalState(Rectangle result) => Result = result;

        public override void Draw(D2DGraphics g, DateTime now, TimeSpan elapsed, Rectangle bounds, D2DBitmap image)
        {
            // nothing to draw here
        }

        public override void Dispose() { }
    }
}
