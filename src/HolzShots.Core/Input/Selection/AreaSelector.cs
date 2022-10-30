using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HolzShots.Input.Selection.Decoration;
using nud2dlib;

namespace HolzShots.Input.Selection
{
    public partial class AreaSelector : AnimatedForm, IAreaSelector
    {
        private static readonly D2DColor OverlayColor = D2DColor.Black;

        private static TaskCompletionSource<SelectionResult>? _tcs;

        private readonly D2DBrush _dimmingOverlayBrush;
        private readonly D2DBitmap _image;
        private readonly Rectangle _imageBounds;
        private readonly D2DBitmapGraphics _dimmedImage;
        private readonly D2DBitmap _background;

        /// <summary> List, because we need them ordered. </summary>
        private IReadOnlyList<WindowRectangle>? _availableWindowsForOutline = System.Collections.Immutable.ImmutableList<WindowRectangle>.Empty;
        private readonly MagnifierDecoration _magnifier = new();

        private SelectionState _state = new InitialState();

        private AreaSelector(Bitmap image, bool allowEntireScreen, HSSettings settingsContext)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            BackColor = Color.Black;
            Cursor = new(Properties.Resources.CrossCursor.Handle);
            Bounds = SystemInformation.VirtualScreen;

#if RELEASE
            TopMost = true;
            DrawFPS = false;
#else
            DrawFPS = true;
#endif

            var windowEnumerationTask = new CancellationTokenSource();
            windowEnumerationTask.CancelAfter(5000);
            WindowFinder.GetCurrentWindowRectanglesAsync(Handle, allowEntireScreen, windowEnumerationTask.Token).ContinueWith(t => SetAvailableWindows(t.Result));

            _dimmingOverlayBrush = Device.CreateSolidColorBrush(new D2DColor(settingsContext.AreaSelectorDimmingOpacity, OverlayColor));
            _image = Device.CreateBitmapFromGDIBitmap(image);
            _imageBounds = new Rectangle(0, 0, image.Width, image.Height);
            _dimmedImage = CreateDimemdImage(image.Width, image.Height);
            _background = _dimmedImage.GetBitmap();
        }

        public static AreaSelector Create(Bitmap image, bool allowEntireScreen, HSSettings settingsContext) => new(image, allowEntireScreen, settingsContext);

        public Task<SelectionResult> PromptSelectionAsync()
        {
            Debug.Assert(_tcs == null);
            Debug.Assert(!SelectionSemaphore.IsInAreaSelection);

            if (_tcs != null)
                return _tcs.Task;

            SelectionSemaphore.IsInAreaSelection = true;

            _tcs = new TaskCompletionSource<SelectionResult>();

            Visible = true;

            return _tcs.Task;
        }

        private D2DBitmapGraphics CreateDimemdImage(int width, int height)
        {
            var res = Device.CreateBitmapGraphics(width, height);
            res.BeginRender();
            res.DrawBitmap(_image, _imageBounds);
            res.FillRectangle(_imageBounds, _dimmingOverlayBrush);
            res.EndRender();
            return res;
        }

        private void SetAvailableWindows(IReadOnlyList<WindowRectangle> windows)
        {
            _availableWindowsForOutline = windows.Select(w => w with
            {
                Rectangle = w.Rectangle.WorldToScreen(SystemInformation.VirtualScreen)
            }).ToList();
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
                                i.SelectWindowBasedOnMouseMove(_availableWindowsForOutline, e.Location);
                                FinishSelectionByWindowOutlineClickOrKeyboardInput(i);
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
                    initial.SelectWindowBasedOnMouseMove(_availableWindowsForOutline, currentPos);
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
                case Keys.Tab when !e.Shift && _state is InitialState s:
                    s.SelectWindowWithOffset(_availableWindowsForOutline, 1);
                    break;
                case Keys.Tab when e.Shift && _state is InitialState s:
                    s.SelectWindowWithOffset(_availableWindowsForOutline, -1);
                    break;
                case Keys.Return when _state is InitialState s && s.CurrentOutline != null:
                    // The user has a window highlighted and pressed enter -> we just take the window outline as a result.
                    FinishSelectionByWindowOutlineClickOrKeyboardInput(s);
                    return;
                default: break;
            }
            base.OnKeyUp(e);
        }

        #endregion

        protected override void Draw(DateTime now, TimeSpan elapsed, D2DGraphics g)
        {
            if (_state is FinalState)
                return;

            Debug.Assert(_dimmingOverlayBrush != null);

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
            Debug.Assert(SelectionSemaphore.IsInAreaSelection);
            SelectionSemaphore.IsInAreaSelection = false;

            _tcs.SetCanceled();
            CloseInternal();
        }
        private void FinishSelectionByWindowOutlineClickOrKeyboardInput(InitialState state)
        {
            var outline = state.CurrentOutline;
            if (outline == null)
            {
                CancelSelection();
                return;
            }

            Debug.Assert(_tcs != null);
            Debug.Assert(SelectionSemaphore.IsInAreaSelection);
            SelectionSemaphore.IsInAreaSelection = false;

            Debug.Assert(outline.HasValue);
            _tcs.SetResult(new SelectionResult(outline.Value, state.SelectedWindowInformation));
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
            Debug.Assert(SelectionSemaphore.IsInAreaSelection);
            SelectionSemaphore.IsInAreaSelection = false;

            _tcs.SetResult(new SelectionResult(state.Result));
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
            _dimmingOverlayBrush?.Dispose();
        }
    }
}
