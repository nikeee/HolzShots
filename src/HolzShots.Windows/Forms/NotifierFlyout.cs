using HolzShots.Native;

namespace HolzShots.Windows.Forms
{
    public partial class NotifierFlyout : NoFocusedFlyoutForm
    {
        private readonly FlyoutAnimator _animator;

        public NotifierFlyout(string title, string body, TimeSpan timeout)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;

            _animator = new FlyoutAnimator(this);

            titleLabel.Text = title ?? throw new ArgumentNullException(nameof(title));
            bodyLabel.Text = body ?? throw new ArgumentNullException(nameof(body));

            closeTimer.Interval = (int)timeout.TotalMilliseconds;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            MaximumSize = Size;
            MinimumSize = Size;
            base.OnVisibleChanged(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _animator.AnimateIn(300);
        }

        async Task CloseNotification()
        {
            if (Visible)
            {
                await _animator.AnimateOut(150).ConfigureAwait(true);
                Close();
            }
        }

        public Task ShowNotification()
        {
            Visible = true;
            var cts = new TaskCompletionSource<bool>();

            closeTimer.Tick += async (s, e) =>
            {
                await CloseNotification();
                cts.SetResult(true);
            };

            closeTimer.Start();

            return cts.Task;
        }


        public static Task ShowNotification(string title, string body) => ShowNotification(title, body, TimeSpan.FromSeconds(3));
        public static async Task ShowNotification(string title, string body, TimeSpan timeout)
        {
            using (var notifierWindow = new NotifierFlyout(title, body, timeout))
                await notifierWindow.ShowNotification();
        }
    }

    public class NoFocusedFlyoutForm : FlyoutForm
    {
        protected override bool ShowWithoutActivation => true;

        /// <summary>
        /// We don't want this form to be focused when shown. This disrupts the user experience.
        /// Ref: https://stackoverflow.com/a/62739625
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= (int)ExtendedWindowStyleFlags.WS_EX_NOACTIVATE;
                return cp;
            }
        }
    }
}
