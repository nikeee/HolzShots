using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace HolzShots.Input.Selection
{
    public partial class AreaSelector2 : AnimatedForm, IAreaSelector
    {
        private static readonly D2DColor _overlayColor = new(0.8f, D2DColor.Black);
        private static readonly D2DColor _selectionBorder = new(0.6f, D2DColor.White);
        private static readonly Cursor _cursor = new Cursor(Properties.Resources.CrossCursor.Handle);

        private static TaskCompletionSource<Rectangle> _tcs;

        private readonly D2DBrush _blackOverlayBrush;

        private D2DBitmap _image;
        private D2DBitmapGraphics _dimmedImage;
        private D2DBitmap _background;
        private Rectangle _imageBounds;
        private SelectionState _state = new InitialState();

        private ISet<WindowRectangle>? availableWindowsForOutline = null;

        public AreaSelector2(HSSettings settingsContext)
        {
            InitializeComponent();

            BackColor = Color.Black;

            DrawFPS = true;
            StartPosition = FormStartPosition.Manual;
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            // DesktopLocation = new Point(0, 0);

            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);
            WindowFinder.GetCurrentWindowRectanglesAsync(Handle, cts.Token).ContinueWith(t => availableWindowsForOutline = t.Result);

            Bounds = SystemInformation.VirtualScreen;
#if !DEBUG
            TopMost = true;
            DrawFPS = false;
#endif
            _blackOverlayBrush = Device.CreateSolidColorBrush(_overlayColor);
        }

        public Task<Rectangle> PromptSelectionAsync(Bitmap image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            Debug.Assert(_image == null);
            Debug.Assert(_tcs == null);

            if (_tcs != null)
                return _tcs.Task;

            _imageBounds = new Rectangle(0, 0, image.Width, image.Height);
            _image = Device.CreateBitmapFromGDIBitmap(image);
            _dimmedImage = CreateDimemdImage(image.Width, image.Height);
            _background = _dimmedImage.GetBitmap();

            _tcs = new TaskCompletionSource<Rectangle>();

            Visible = true;
            Cursor = _cursor;

            return _tcs.Task;
        }

        private D2DBitmapGraphics CreateDimemdImage(int width, int height)
        {
            var res = Device.CreateBitmapGraphics(width, height);
            res.BeginRender();
            res.DrawBitmap(_image, _imageBounds);
            res.FillRectangle(_imageBounds, _blackOverlayBrush);
            res.EndRender();
            return res;
        }

        #region Mouse and Keyboard Stuff

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_state is FinalState)
                Debug.Fail("OnMouseDown after final state");

            Debug.Assert(e != null);
            var currentPos = e.Location;
            switch (e.Button)
            {
                case MouseButtons.Left:

                    switch (_state)
                    {
                        case InitialState _:
                            _state = new ResizingRectangleState(currentPos, currentPos);
                            break;
                        case ResizingRectangleState _: break; // Pressing the left mouse button without leaving first is not possible
                        case MovingRectangleState _: break; // This should do nothing
                        case FinalState _: Debug.Fail("Unhandled State"); break; // Not possible
                        default: Debug.Fail("Unhandled State"); break;
                    }
                    break;
                case MouseButtons.Right:
                    switch (_state)
                    {
                        case InitialState _: break; // Pressing the right mouse button without a selected rectangle is not possible
                        case ResizingRectangleState resizing:
                            _state = new MovingRectangleState(resizing.UserSelectionStart, resizing.CursorPosition);
                            break;
                        case MovingRectangleState _: break;  // Pressing the right mouse button without leaving first is not possible
                        case FinalState _: Debug.Fail("Unhandled State"); break; // Not possible
                        default: Debug.Fail("Unhandled State"); break;
                    }
                    break;
                default:
                    break; // Ignore all other mouse buttons
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_state is FinalState)
                Debug.Fail("OnMouseUp after final state");

            Debug.Assert(e != null);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    switch (_state)
                    {
                        case InitialState _: break; // Releasing the left mouse button without being in some state should not be possible
                        case RectangleState availableSelection:
                            var res = availableSelection.GetSelectedOutline(_imageBounds);

                            if (res.HasArea())
                            {
                                var finalState = new FinalState(new Rectangle(res.X, res.Y, res.Width, res.Height));
                                _state = finalState;
                                FinishSelection(finalState);
                            }
                            else
                            {
                                // The user most likely just clicked
                                // In this case, he propably wantet to select a window that was outlined (if there was one)
                                // TODO: This is a hack, we need to make this more pretty (put this in the FinalState)
                                var i = new InitialState();
                                i.UpdateOutlinedWindow(availableWindowsForOutline, e.Location);
                                FinishSelectionByWindowOutlineClick(i);
                            }
                            break;
                        case FinalState _: Debug.Fail("Unhandled State"); break; // Not possible
                        default: Debug.Fail("Unhandled State"); break;
                    }
                    break;
                case MouseButtons.Right:
                    switch (_state)
                    {
                        case InitialState _: break; // Releasing the left mouse button without being in some state should not be possible
                        case ResizingRectangleState _: break;// Releasing the right mouse button without leaving first is not possible
                        case MovingRectangleState moving:
                            _state = new ResizingRectangleState(moving.UserSelectionStart, moving.CursorPosition);
                            break;
                        case FinalState _: Debug.Fail("Unhandled State"); break; // Not possible
                        default: Debug.Fail("Unhandled State"); break;
                    }
                    break;
                default:
                    break; // Ignore all other mouse buttons
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_state is FinalState)
                Debug.Fail("OnMouseMove after final state");

            Debug.Assert(e != null);
            var currentPos = e.Location;

            switch (_state)
            {
                case InitialState initial:
                    initial.UpdateOutlinedWindow(availableWindowsForOutline, currentPos);
                    break;
                case ResizingRectangleState resizing:
                    resizing.UpdateCursorPosition(currentPos);
                    break;
                case MovingRectangleState moving:
                    moving.MoveByNewCursorPosition(currentPos);
                    break;
                case FinalState _: break; // Nothing to be updated
                default: Debug.Fail("Unhandled State"); break;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    CancelSelection();
                    break;
                case Keys.Space:
                    // TODO: toggle magnifier
                    break;
                default: break;
            }
            base.OnKeyUp(e);
        }

        #endregion

        protected override void Draw(DateTime now, TimeSpan elapsed, D2DGraphics g)
        {
            if (_state is FinalState)
                return;

            Debug.Assert(_blackOverlayBrush != null);

            g.Antialias = false;

            g.BeginRender(_background);

            switch (_state)
            {
                case InitialState initial:
                    {
                        // foreach (var d in initial.Decorations)
                        //     d.Render(g, initial, _imageBounds);

                        var outlineAnimation = initial.CurrentOutline;
                        if (outlineAnimation != null)
                        {
                            Debug.Assert(initial.Title != null);

                            outlineAnimation.Update(now);

                            var rect = outlineAnimation.Current;
                            var selectionOutline = new D2DRect(
                                rect.X + 0.5f,
                                rect.Y + 0.5f,
                                rect.Width - 1f,
                                rect.Height - 1f
                            );

                            // TODO: Animate
                            // TODO: Make this pretty
                            g.DrawRectangle(selectionOutline, D2DColor.CornflowerBlue, 1.0f);
                            g.DrawTextCenter(initial.Title, D2DColor.CornflowerBlue, "Consolas", 14.0f, selectionOutline);
                        }
                        break;
                    }
                case RectangleState availableSelection:
                    {
                        var outline = availableSelection.GetSelectedOutline(_imageBounds);
                        D2DRect rect = outline; // Caution: implicit conversion which we don't want to do twice

                        g.DrawBitmap(_image, rect, rect);

                        // We need to widen the rectangle by 0.5px so that the result will be exactly 1px wide.
                        // Otherwise, it will be 2px and darker.
                        var selectionOutline = new D2DRect(
                            outline.X - 0.5f,
                            outline.Y - 0.5f,
                            outline.Width + 1f,
                            outline.Height + 1f
                        );
                        g.DrawRectangle(selectionOutline, _selectionBorder, 1.0f, D2DDashStyle.Dash);

                        break;
                    }
                case FinalState _: break; // Nothing to be updated
                default: Debug.Fail("Unhandled State"); break;
            }
#if DEBUG
            g.DrawTextCenter(_state.GetType().Name, D2DColor.White, SystemFonts.DefaultFont.Name, 36, ClientRectangle);
#endif
        }

        private void CancelSelection()
        {
            Debug.Assert(_tcs != null);
            _tcs.SetCanceled();
            CloseInternal();
        }
        private void FinishSelectionByWindowOutlineClick(InitialState state)
        {
            var outline = state.CurrentOutline;
            if (outline == null)
            {
                CancelSelection();
                return;
            }

            Debug.Assert(_tcs != null);
            _tcs.SetResult(outline.Destination);
            CloseInternal();
        }
        private void FinishSelection(FinalState state)
        {
            if (!state.Result.HasArea())
            {
                CancelSelection();
                return;
            }

            Debug.Assert(_tcs != null);
            _tcs.SetResult(state.Result);
            CloseInternal();
        }

        void CloseInternal()
        {
            Close();
            Visible = false;
            CleanUp();
        }

        private void CleanUp()
        {
            _tcs = null;
            _state = null;
            // TODO: Maybe move this to some dispose method
            _image?.Dispose();
            _dimmedImage?.Dispose();
            _blackOverlayBrush?.Dispose();
        }


        interface IInitialStateDecoration
        {
            void Render(D2DGraphics g, InitialState state, Rectangle bounds);
        }


        class HelpTextDecoration : IInitialStateDecoration
        {
            // TODO

            private static readonly string[] HelpText = new[] {
                "Left Mouse: Select area",
                "Right Mouse: Move selected area",
                // "Space Bar: Toggle magnifier",
                "Escape: Cancel",
            };

            private readonly Rectangle _infoBounds;

            public HelpTextDecoration()
            {
                //var x = (Screen.PrimaryScreen.Bounds.X - SystemInformation.VirtualScreen.X + Screen.PrimaryScreen.Bounds.Width / 2); // - (_richInfoTextSize.Width / 2);
                //var y = (Screen.PrimaryScreen.Bounds.Y - SystemInformation.VirtualScreen.Y + Screen.PrimaryScreen.Bounds.Height / 2); // - (_richInfoTextSize.Width / 2);
                _infoBounds = Screen.PrimaryScreen.Bounds;
            }

            public void Render(D2DGraphics g, InitialState state, Rectangle bounds)
            {
                g.DrawTextCenter(string.Join(Environment.NewLine, HelpText), D2DColor.White, "Consolas", 32, _infoBounds);
            }
        }
    }


    public abstract class AnimatedForm : Form
    {

        private int _currentFps = 0;
        private int _lastFps = 0;
        public bool DrawFPS { get; set; }
        private DateTime _lastFpsUpdate = DateTime.Now;
        private D2DGraphics _graphics;

        private D2DDevice? _device;
        public D2DDevice Device => _device ??= D2DDevice.FromHwnd(Handle);

        private DateTime _prevUpdate;


        protected override void CreateHandle()
        {
            base.CreateHandle();

            DoubleBuffered = false;

            _graphics = new D2DGraphics(Device);
            _graphics.SetDPI(96, 96);
        }

        protected abstract void Draw(DateTime now, TimeSpan elapsed, D2DGraphics g);

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode)
            {
                e.Graphics.Clear(Color.Black);
                e.Graphics.DrawString("D2DLib windows form cannot render in design time.", Font, Brushes.White, 10, 10);
                return;
            }
            var now = DateTime.Now;

            Draw(now, now - _prevUpdate, _graphics);

            _prevUpdate = now;

            if (DrawFPS)
            {
                if (_lastFpsUpdate.Second != DateTime.Now.Second)
                {
                    _lastFps = _currentFps;
                    _currentFps = 0;
                    _lastFpsUpdate = DateTime.Now;
                }
                else
                {
                    ++_currentFps;
                }

                var fpsInfo = $"{_lastFps} fps";
                var size = e.Graphics.MeasureString(fpsInfo, Font, Width);
                _graphics.DrawText(fpsInfo, D2DColor.Silver, "Consolas", 26.0f, ClientRectangle.Right - size.Width - 10, 5);
            }

            _graphics.EndRender();

            Invalidate(false);
        }

        protected override void OnPaintBackground(PaintEventArgs e) { /* prevent the .NET windows form to paint the original background */ }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)Win32.WMessages.WM_ERASEBKGND:
                    break;

                case (int)Win32.WMessages.WM_SIZE:
                    base.WndProc(ref m);
                    if (Device != null)
                    {
                        Device.Resize();
                        Invalidate(false);
                    }
                    break;
                case (int)Win32.WMessages.WM_DESTROY:
                    if (Device != null)
                        Device.Dispose();

                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
