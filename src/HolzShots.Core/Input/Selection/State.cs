using System;
using System.Collections.Generic;
using System.Drawing;
using unvell.D2DLib;

namespace HolzShots.Input.Selection
{
    internal abstract class SelectionState { }
    internal class InitialState : SelectionState
    {
        public WindowRectangle? CurrentOutlinedWindow { get; protected set; }
        // public IInitialStateDecoration[] Decorations => new[] { new HelpTextDecoration() };
        public void UpdateOutlinedWindow(ISet<WindowRectangle> windows, Point cursorPosition)
        {
            if (windows == null)
                CurrentOutlinedWindow = null;

            foreach (var candidate in windows)
            {
                if (candidate.Rectangle.Contains(cursorPosition))
                {
                    CurrentOutlinedWindow = candidate;
                    return;
                }
            }
            CurrentOutlinedWindow = null;
        }
    }

    internal abstract class RectangleState : SelectionState
    {
        public Point UserSelectionStart { get; protected set; }
        public Point CursorPosition { get; protected set; }
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
    }

    internal class ResizingRectangleState : RectangleState
    {
        public ResizingRectangleState(Point userSelectionStart, Point cursorPosition) : base(userSelectionStart, cursorPosition) { }
        public void UpdateCursorPosition(Point newCursorPosition) => CursorPosition = newCursorPosition;
    }

    internal class MovingRectangleState : RectangleState
    {
        public MovingRectangleState(Point userSelectionStart, Point cursorPosition) : base(userSelectionStart, cursorPosition) { }
        public void MoveByNewCursorPosition(Point newCursorPosition)
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

    internal class FinalState : SelectionState
    {
        public Rectangle Result { get; }
        public FinalState(Rectangle result) => Result = result;
    }
}
