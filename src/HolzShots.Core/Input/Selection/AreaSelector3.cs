using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HolzShots.Input.Selection
{
    public class AreaSelector3 : Form, IAreaSelector
    {
        private static readonly Cursor _cursor = new Cursor(Properties.Resources.CrossCursor.Handle);

        private static TaskCompletionSource<Rectangle> _tcs;

        private readonly Stopwatch _timeSinceStart = new Stopwatch();
        private readonly Pen _outlinePen = new Pen(Color.FromArgb((int)(0.6f * 255), 255, 255, 255))
        {
            DashPattern = new[] { 5.0f, 5.0f },
        };

        private Bitmap _image;
        // private Bitmap _dimmedImage;
        private TextureBrush _dimmedImageBrush;
        private TextureBrush _imageBrush;
        private Rectangle _imageBounds;

        private Point _mousePosition;
        private SelectionState _state = new InitialState();

#if DEBUG
        private readonly FpsStopWatch _fpsWatch = new FpsStopWatch();
        private bool _drawFps = true;
        private static readonly Brush _debugTextBrush = new SolidBrush(Color.White);
        private static readonly Font _fpsFont = new Font("Consolas", 14.0f);
        private static readonly Font _stateFont = new Font("Consolas", 24.0f);
#endif

        public AreaSelector3()
            : base()
        {

            SuspendLayout();

            InitializeComponents();

            ShowInTaskbar = false;
            BackColor = Color.Black;
            StartPosition = FormStartPosition.Manual;
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            Bounds = SystemInformation.VirtualScreen;
            AutoScaleMode = AutoScaleMode.None;

#if !DEBUG
            TopMost = true;
#endif
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            ResumeLayout(false);
        }

        private void InitializeComponents()
        {
        }

        public Task<Rectangle> PromptSelectionAsync(Bitmap image)
        {
            Debug.Assert(_image == null);
            Debug.Assert(_tcs == null);

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            if (_tcs != null)
                return _tcs.Task;

            _tcs = new TaskCompletionSource<Rectangle>();

            Visible = true;
            Cursor = _cursor;
#if DEBUG
            _fpsWatch.Start();
#endif
            _timeSinceStart.Start();

            _image = image;
            _imageBounds = new Rectangle(0, 0, image.Width, image.Height);
            _imageBrush = CreateImageBrush(image);
            _dimmedImageBrush = CreateDimmedImageBrush(image);

            return _tcs.Task;
        }

        private static TextureBrush CreateImageBrush(Bitmap originalImage) => new TextureBrush(originalImage) { WrapMode = WrapMode.Clamp };
        private static TextureBrush CreateDimmedImageBrush(Bitmap originalImage)
        {
            var dimmedImage = originalImage.Clone() as Bitmap;

            using (var g = Graphics.FromImage(dimmedImage))
            using (var brush = new SolidBrush(Color.FromArgb((int)(255 * 0.8), Color.Black)))
            {
                g.FillRectangle(brush, 0, 0, originalImage.Width, originalImage.Height);

                return new TextureBrush(dimmedImage)
                {
                    WrapMode = WrapMode.Clamp,
                };
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // We have no background. So we won't paint one.
            // base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            UpdateData();

            var g = e.Graphics;
            g.CompositingMode = CompositingMode.SourceCopy;
            g.FillRectangle(_dimmedImageBrush, _imageBounds);
            g.CompositingMode = CompositingMode.SourceOver;

            // g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            DrawState(g);

#if DEBUG
            if (_drawFps)
            {
                _fpsWatch.Update();
                DrawFps(g);
            }
#endif

            Invalidate(false);

            // Not calling the base paint
            // base.OnPaint(e);
        }

        private void UpdateData()
        {
            _outlinePen.DashOffset = (float)_timeSinceStart.Elapsed.TotalSeconds * -10.0f;

            var currentPositionOnScreen = Control.MousePosition;
            _mousePosition = PointToClient(currentPositionOnScreen);

            switch (_state)
            {
                case InitialState _: break; // Nothing to be updated
                case ResizingRectangleState resizing:
                    resizing.UpdateCursorPosition(_mousePosition);
                    break;
                case MovingRectangleState moving:
                    moving.MoveByNewCursorPosition(_mousePosition);
                    break;
                case FinalState _: break; // Nothing to be updated
                default: Debug.Fail("Unhandled State"); break;
            }
        }

        private void DrawState(Graphics g)
        {
            switch (_state)
            {
                case InitialState initial:
                    // foreach (var d in initial.Decorations)
                    //     d.Render(g, initial, _imageBounds);

                    break; // Nothing to be updated
                case RectangleState availableSelection:

                    var outline = availableSelection.GetSelectedOutline(_imageBounds);
                    g.FillRectangle(_imageBrush, outline);
                    g.DrawRectangle(_outlinePen, outline);

                    break;
                case FinalState _: break; // Nothing to be updated
                default: Debug.Fail("Unhandled State"); break;
            }

#if DEBUG
            // g.MeasureString(text, infoFont).ToSize();
            g.DrawString(_state.GetType().Name, _stateFont, _debugTextBrush, 10.0f, 24.0f);
#endif
        }


#if DEBUG
        private void DrawFps(Graphics g)
        {
            g.DrawString(_fpsWatch.FramesPerSecond.ToString(), _fpsFont, _debugTextBrush, 10.0f, 10.0f);
        }
#endif

        #region Result Management

        private void CancelSelection()
        {
            Debug.Assert(_tcs != null);
            _tcs.SetCanceled();
            CloseInternal();
        }
        private void FinishSelection(FinalState state)
        {
            if (state.Result.Width < 1 || state.Result.Height < 1)
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
            _imageBrush?.Dispose();
            _dimmedImageBrush?.Dispose();
            _timeSinceStart?.Stop();
            _outlinePen?.Dispose();
#if DEBUG
            _fpsWatch?.Dispose();
#endif
        }

        #endregion

        #region Keyboard and Mouse Events

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

            // Not calling base
            // base.OnMouseUp(e);
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
                            var finalState = new FinalState(new Rectangle(res.X, res.Y, res.Width, res.Height));
                            _state = finalState;
                            FinishSelection(finalState);
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

            // Not calling base
            // base.OnMouseUp(e);
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
    }
}
