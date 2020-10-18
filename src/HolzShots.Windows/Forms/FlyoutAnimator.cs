using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HolzShots.Threading;
using Forms.Transitions;
using Forms.Transitions.TransitionTypes;

namespace HolzShots.Windows.Forms
{
    public class FlyoutAnimator
    {
        private static readonly List<FlyoutForm> _currentVisibleFlyouts = new List<FlyoutForm>();

        private readonly FlyoutForm _target;

        public FlyoutAnimator(FlyoutForm target)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _target.StartPosition = FormStartPosition.Manual;
        }

        private bool _taskbarIsOnTopOrBottom;
        private Rectangle _screenRectangle;
        private int _instanceOffsetY;

        public Task AnimateIn(int durationMs)
        {
            _taskbarIsOnTopOrBottom = TaskbarWindow.Instance.Position == Native.Shell32.TaskbarPosition.Bottom
                                        || TaskbarWindow.Instance.Position == Native.Shell32.TaskbarPosition.Top;

            _screenRectangle = Screen.PrimaryScreen.WorkingArea;

            var startX = _screenRectangle.X + _screenRectangle.Width - _target.Width - 10;
            var startY = _screenRectangle.Y + _screenRectangle.Height - _target.Height + (_taskbarIsOnTopOrBottom ? (TaskbarWindow.Instance.Rectangle.Height / 2) : 15);

            if (_currentVisibleFlyouts.Count == 0 || _currentVisibleFlyouts.Count > 3)
            {
                _instanceOffsetY = _screenRectangle.Y + _screenRectangle.Height - _target.Height;
            }
            else
            {
                _instanceOffsetY = _currentVisibleFlyouts.Min(s => s.Location.Y) - _target.Height;
            }

            _currentVisibleFlyouts.Add(_target);

            const int offfsetY = 8;
            var destY = _instanceOffsetY - offfsetY;

            _target.Location = new Point(startX, startY);
            _target.Opacity = 0.0;
            _target.TopMost = true;

            var transition = new Transition(new Deceleration(durationMs));
            transition.Add(this, nameof(TargetY), destY);
            transition.Add(this, nameof(TargetOpacity), 1.0);

            // TODO: Fix this in .NET 5:
            // https://stackoverflow.com/a/63372604
            var cts = new TaskCompletionSource<bool>();
            transition.TransitionCompletedEvent += (s, e) => _target.InvokeIfNeeded(() => cts.SetResult(true));

            transition.Run();

            return cts.Task;
        }

        public Task AnimateOut(int durationMs)
        {
            _currentVisibleFlyouts.Remove(_target);

            var destY = _instanceOffsetY + 20;

            _target.Opacity = 1.0;
            _target.TopMost = true;

            var transition = new Transition(new Deceleration(durationMs));
            transition.Add(this, nameof(TargetY), destY);
            transition.Add(this, nameof(TargetOpacity), 0.0);

            // TODO: Fix this in .NET 5:
            // https://stackoverflow.com/a/63372604
            var cts = new TaskCompletionSource<bool>();
            transition.TransitionCompletedEvent += (s, e) => _target.InvokeIfNeeded(() => cts.SetResult(true));

            transition.Run();

            return cts.Task;
        }

        public int TargetX
        {
            get => _target.Location.X;
            set
            {
                if (!_target.IsDisposed && _target.Location.X != value)
                    _target.InvokeIfNeeded(() => _target.Location = new Point(value, _target.Location.Y));
            }
        }

        public int TargetY
        {
            get => _target.Location.Y;
            set
            {
                if (!_target.IsDisposed && _target.Location.Y != value)
                    _target.InvokeIfNeeded(() => _target.Location = new Point(_target.Location.X, value));
            }
        }

        public double TargetOpacity
        {
            get => _target.Opacity;
            set
            {
                if (!_target.IsDisposed && _target.Opacity != value)
                    _target.InvokeIfNeeded(() => _target.Opacity = value);
            }
        }
    }
}
