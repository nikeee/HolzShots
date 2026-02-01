using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Windows.Forms;
using HolzShots.Forms.Transitions;
using HolzShots.Forms.Transitions.TransitionTypes;
using HolzShots.Threading;

namespace HolzShots.Windows.Forms;

public class FlyoutAnimator
{
    private static readonly List<FlyoutForm> _currentVisibleFlyouts = [];

    private readonly FlyoutForm _target;

    public FlyoutAnimator(FlyoutForm target)
    {
        ArgumentNullException.ThrowIfNull(target);
        _target = target;
        _target.StartPosition = FormStartPosition.Manual;
    }

    private bool _taskbarIsOnTopOrBottom;
    private Rectangle _screenRectangle;
    private int _instanceOffsetY;

    public Task AnimateIn(int durationMs)
    {
        _taskbarIsOnTopOrBottom = TaskbarWindow.Instance.Position == Native.Shell32.TaskbarPosition.Bottom
                                    || TaskbarWindow.Instance.Position == Native.Shell32.TaskbarPosition.Top;

        _screenRectangle = EnvironmentEx.PrimaryScreen.WorkingArea;

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

        const int offsetY = 8;
        var targetY = _instanceOffsetY - offsetY;

        _target.Location = new Point(startX, startY);
        _target.Opacity = 0.0;
        _target.TopMost = true;

        var transition = new Transition(new Deceleration(durationMs));
        transition.Add(this, nameof(TargetY), targetY);
        transition.Add(this, nameof(TargetOpacity), 1.0);

        var cts = new TaskCompletionSource();
        transition.TransitionCompletedEvent += (s, e) => _target.InvokeIfNeeded(() => cts.SetResult());

        transition.Run();

        return cts.Task;
    }

    public Task AnimateOut(int durationMs)
    {
        _currentVisibleFlyouts.Remove(_target);

        var targetY = _instanceOffsetY + 20;

        _target.Opacity = 1.0;
        _target.TopMost = true;

        var transition = new Transition(new Deceleration(durationMs));
        transition.Add(this, nameof(TargetY), targetY);
        transition.Add(this, nameof(TargetOpacity), 0.0);

        var cts = new TaskCompletionSource();
        transition.TransitionCompletedEvent += (s, e) => _target.InvokeIfNeeded(() => cts.SetResult());

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
