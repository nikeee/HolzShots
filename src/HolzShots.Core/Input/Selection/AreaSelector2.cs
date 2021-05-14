using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HolzShots.Input.Selection.Animation;
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

        private IInitialStateDecoration? _decoration = null;

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

            _decoration = new HelpTextDecoration();

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

                        if (_decoration != null)
                            _decoration.Draw(now, elapsed, g, initial, _imageBounds);

                        var outlineAnimation = initial.CurrentOutline;
                        if (outlineAnimation != null)
                        {
                            Debug.Assert(initial.Title != null);

                            outlineAnimation.Update(now, elapsed);

                            var rect = outlineAnimation.Current;
                            var selectionOutline = rect.AsD2DRect();

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
                        var selectionOutline = outline.AsD2DRect();

                        // TODO: Make this a decoration
                        using var selectionOutlinePen = Device.CreatePen(
                            D2DColor.White,
                            D2DDashStyle.Custom,
                            _customDashStyle,
                            _currentDashOffset
                        );

                        g.DrawRectangle(selectionOutline, selectionOutlinePen, 1.0f);
                        _currentDashOffset = (float)(now - _selectionStarted).TotalMilliseconds / 40;

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
            // TODO: Maybe move this to some dispose method
            _image?.Dispose();
            _dimmedImage?.Dispose();
            _blackOverlayBrush?.Dispose();
        }
    }

    internal interface IInitialStateDecoration
    {
        void Draw(DateTime now, TimeSpan elapsed, D2DGraphics g, InitialState state, Rectangle bounds);
    }

    internal class HelpTextDecoration : IInitialStateDecoration
    {
        private static readonly string[] HelpText = new[] {
            "Left Mouse: Select area",
            "Right Mouse: Move selected area",
            // "Space Bar: Toggle magnifier",
            "Escape: Cancel",
        };

        private const string FontName = "Consolas";
        private const float FontSize = 24.0f;
        private static readonly D2DSize Margin = new(10, 5);
        private static readonly D2DColor BackgroundColor = new(0.2f, 1f, 1f, 1f);
        private static readonly D2DColor FontColor = new(1f, 0.9f, 0.9f, 0.9f);
        private static readonly TimeSpan FadeStart = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan FadeDuration = TimeSpan.FromSeconds(3);

        private RectangleAnimation[]? _animations = null;
        private DateTime? _firstUpdate = null;
        private DateTime? _fadeOutStarted = null;

        private static RectangleAnimation[] InitializeAnimations(DateTime now, D2DGraphics g)
        {
            var res = new RectangleAnimation[HelpText.Length];

            int lastX = 100;
            int lastY = 100;

            var someRandomSize = new D2DSize(1000, 1000);

            for (int i = 0; i < res.Length; ++i)
            {
                var textSize = g.MeasureText(HelpText[i], FontName, FontSize, someRandomSize);
                var destination = new Rectangle(
                    lastX,
                    lastY,
                    (int)(textSize.width + 2 * Margin.width),
                    (int)(textSize.height + 2 * Margin.height)
                );

                var start = new Rectangle(
                    destination.Location,
                    new Size(0, destination.Height)
                );

                var animation = new RectangleAnimation(
                    TimeSpan.FromMilliseconds(150 * i + 100),
                    start,
                    destination
                );
                res[i] = animation;
                animation.Start(now);

                lastY = destination.Y + destination.Height + 1;
            }

            return res;
        }

        public void Draw(DateTime now, TimeSpan elapsed, D2DGraphics g, InitialState state, Rectangle bounds)
        {
            _animations ??= InitializeAnimations(now, g);
            _firstUpdate ??= now;

            var opacityElapsed = now - _firstUpdate;
            if (_fadeOutStarted == null && opacityElapsed > FadeStart)
                _fadeOutStarted = now;


            var opacity = 1f;
            if (_fadeOutStarted != null)
                opacity = EasingMath.EaseInSquare((float)(now - _fadeOutStarted.Value).TotalMilliseconds / (float)FadeDuration.TotalMilliseconds, 1, 0);

            for (int i = 0; i < _animations.Length; ++i)
            {
                var animation = _animations[i];
                var text = HelpText[i];

                animation.Update(now, elapsed);
                var rect = animation.Current.AsD2DRect();

                var textLocation = new Rectangle(
                    animation.Destination.X + (int)Margin.width,
                    animation.Destination.Y + (int)Margin.height,
                    animation.Destination.Width,
                    animation.Destination.Height
                );

                g.FillRectangle(rect, new D2DColor(opacity * BackgroundColor.a, BackgroundColor));
                g.DrawText(text, new D2DColor(opacity * FontColor.a, FontColor), FontName, FontSize, textLocation);
            }
        }
    }
}
