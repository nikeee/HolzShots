using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HolzShots.Input;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace HolzShots.Input.Selection
{
    public partial class AreaSelector2 : D2DForm, IAreaSelector
    {
        private static TaskCompletionSource<Rectangle> _tcs;
        private Bitmap _image;
        private D2DRect _imageRectangle;
        private readonly D2DBrush _blackOverlayBrush;

        public AreaSelector2()
        {
            InitializeComponent();

            BackColor = Color.Black;

            StartPosition = FormStartPosition.Manual;
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            // DesktopLocation = new Point(0, 0);

            Bounds = SystemInformation.VirtualScreen;
#if !DEBUG
            TopMost = true;
#endif

            _blackOverlayBrush = Device.CreateSolidColorBrush(new D2DColor(0.5f, D2DColor.Black));
        }

        public Task<Rectangle> PromptSelectionAsync(Bitmap image)
        {
            Debug.Assert(image != null);
            Debug.Assert(_image == null);
            Debug.Assert(_tcs == null);

            if (_tcs != null)
                return _tcs.Task;

            _imageRectangle = new D2DRect(0, 0, image.Width, image.Height);
            _image = image;
            _tcs = new TaskCompletionSource<Rectangle>();

            Visible = true;

            return _tcs.Task;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.E:
                    _tcs.TrySetResult(new Rectangle(0, 0, 100, 200));
                    Close();
                    break;
            }
        }

        abstract class SelectionState { }
        class InitialState : SelectionState { }
        class ResizingRectangleState : SelectionState
        {
            public Point UserSelectionStart { get; }
            public Point CursorPosition { get; set; }
            public ResizingRectangleState(Point userSelectionStart) => UserSelectionStart = userSelectionStart;
        }
        class MovingRectangleState : SelectionState
        {
            public Point UserSelectionStart { get; set; }
            public Point CursorPosition { get; set; }
            public MovingRectangleState(Point userSelectionStart, Point cursorPosition)
            {
                UserSelectionStart = userSelectionStart;
                CursorPosition = cursorPosition;
            }
        }
        class FinalState : SelectionState { }

        private SelectionState _state = new InitialState();

        protected override void OnMouseDown(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:

                    switch (_state)
                    {
                        case InitialState initial:
                            _state = new ResizingRectangleState(e.Location);
                            break;
                        case ResizingRectangleState resizing: break; // Pressing the left mouse button without leaving first is not possible
                        case MovingRectangleState moving: break; // This should do nothing
                        case FinalState _: Debug.Fail("Unhandled State"); break; // Not possible
                        default: Debug.Fail("Unhandled State"); break;
                    }
                    break;
                case MouseButtons.Right:
                    switch (_state)
                    {
                        case InitialState initial: break; // Pressing the right mouse button without a selected rectangle is not possible
                        case ResizingRectangleState resizing:
                            _state = new MovingRectangleState(e.Location, resizing.CursorPosition);
                            break;
                        case MovingRectangleState moving: break;  // Pressing the right mouse button without leaving first is not possible
                        case FinalState _: Debug.Fail("Unhandled State"); break; // Not possible
                        default: Debug.Fail("Unhandled State"); break;
                    }
                    break;
                default:
                    break; // Ignore all other mouse buttons
            }

            this.Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:

                    switch (_state)
                    {
                        case InitialState initial: break; // Releasing the left mouse button without being in some state should not be possible
                        case ResizingRectangleState resizing:
                            _state = new FinalState();
                            // TODO: User finished selecting
                            break;
                        case MovingRectangleState moving:
                            _state = new FinalState();
                            // TODO: User finished selecting
                            break;
                        case FinalState _: Debug.Fail("Unhandled State"); break; // Not possible
                        default: Debug.Fail("Unhandled State"); break;
                    }
                    break;
                case MouseButtons.Right:
                    switch (_state)
                    {
                        case InitialState initial: break; // Releasing the left mouse button without being in some state should not be possible
                        case ResizingRectangleState resizing: break;// Releasing the right mouse button without leaving first is not possible
                        case MovingRectangleState moving:
                            _state = new ResizingRectangleState(moving.UserSelectionStart) { CursorPosition = moving.CursorPosition };
                            break;
                        case FinalState _: Debug.Fail("Unhandled State"); break; // Not possible
                        default: Debug.Fail("Unhandled State"); break;
                    }
                    break;
                default:
                    break; // Ignore all other mouse buttons
            }

            this.Invalidate();
        }

        protected override void OnRender(D2DGraphics g)
        {
            Debug.Assert(_blackOverlayBrush != null);

            g.DrawBitmap(_image, _imageRectangle);
            g.FillRectangle(_imageRectangle, _blackOverlayBrush);

            // g.DrawTextCenter

            g.DrawTextCenter(_state.GetType().Name, D2DColor.White, SystemFonts.DefaultFont.Name, 36, ClientRectangle);
            // g.DrawTextCenter("Draw something...", D2DColor.Goldenrod, SystemFonts.DefaultFont.Name, 36, ClientRectangle);
        }

        /*
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor.Hide();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Cursor.Show();
        }
        */

        protected override void OnClosed(EventArgs e)
        {
            OnFinishSelection();
            base.OnClosed(e);
        }

        private void OnFinishSelection()
        {
            _image = null;
            _tcs = null;
            _blackOverlayBrush.Dispose(); // TODO: Dispose method
        }

    }
}
