using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HolzShots.Input.Selection.Decoration;
using unvell.D2DLib;

namespace HolzShots.Input.Selection
{
    public partial class AreaSelector2 : AnimatedForm, IAreaSelector
    {
        private static readonly D2DColor _overlayColor = new(0.8f, D2DColor.Black);
        private static readonly Cursor _cursor = new(Properties.Resources.CrossCursor.Handle);
        private static readonly float[] _customDashStyle = new[] { 3f };

        private static TaskCompletionSource<Rectangle>? _tcs;

        private readonly D2DBrush _blackOverlayBrush;

        private D2DBitmap _image;
        private D2DBitmapGraphics _dimmedImage;
        private D2DBitmap _background;
        private Rectangle _imageBounds;
        private float _currentDashOffset = 0.0f;
        private DateTime _selectionStarted;
        private SelectionState _state = new InitialState();
        private readonly MagnifierDecoration _magnifier = new();

        private ISet<WindowRectangle>? availableWindowsForOutline = null;

        public AreaSelector2(HSSettings settingsContext)
        {
            BackColor = Color.Black;

            DrawFPS = true;
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

            _selectionStarted = DateTime.Now;

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
                                var finalState = new FinalState(res);
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
                    initial.UpdateCursorPosition(currentPos);
                    initial.UpdateOutlinedWindow(availableWindowsForOutline, currentPos);
                    break;
                case ResizingRectangleState resizing:
                    resizing.UpdateCursorPosition(currentPos);
                    break;
                case MovingRectangleState moving:
                    moving.UpdateCursorPosition(currentPos);
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
                    _magnifier.Toggle();
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

            _state.EnsureDecorationInitialization(g, now);
            _state.Draw(g, now, elapsed, _imageBounds, _image);

            switch (_state)
            {
                case InitialState initial: break;
                case RectangleState availableSelection: break;
                case FinalState _: break; // Nothing to be updated
                default: Debug.Fail("Unhandled State"); break;
            }

            _magnifier.UpdateAndDraw(g, now, elapsed, _imageBounds, _image, _state);
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
            // TODO: Maybe move this to some dispose method
            _image?.Dispose();
            _dimmedImage?.Dispose();
            _blackOverlayBrush?.Dispose();
        }
    }
}
