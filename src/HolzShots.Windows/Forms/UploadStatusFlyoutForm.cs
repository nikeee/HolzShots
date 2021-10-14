using System.Diagnostics;
using System.Windows.Forms;
using HolzShots.Net;
using HolzShots.Threading;

namespace HolzShots.Windows.Forms
{
    public partial class UploadStatusFlyoutForm : NoFocusedFlyoutForm, ITransferProgressReporter
    {
        private readonly FlyoutAnimator _animator;
        public UploadStatusFlyoutForm()
        {
            InitializeComponent();
            uploadedBytesLabel.Text = string.Empty;
            _animator = new FlyoutAnimator(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _animator.AnimateIn(300);
        }

        async Task CloseDialog()
        {
            if (Visible)
            {
                await _animator.AnimateOut(150).ConfigureAwait(true);
                Close();
            }
        }

        private void SetSpeed(string value)
        {
            Debug.Assert(!speedLabel.InvokeRequired);
            speedLabel.Text = value;
        }

        private void SetUploadedBytesLabel(string value)
        {
            Debug.Assert(!uploadedBytesLabel.InvokeRequired);
            uploadedBytesLabel.Text = value;
        }

        private void SetProgressBarStyleLabel(ProgressBarStyle value)
        {
            Debug.Assert(!stuffUploadedBar.InvokeRequired);
            stuffUploadedBar.Style = value;
        }

        private void SetProgressBarValueLabel(uint value)
        {
            stuffUploadedBar.InvokeIfNeeded(() => stuffUploadedBar.Value = (int)value);
        }

        public void UpdateProgress(TransferProgress progress, Speed<MemSize> speed)
        {
            switch (progress.State)
            {
                case UploadState.NotStarted:
                    SetProgressBarStyleLabel(ProgressBarStyle.Marquee);

                    SetUploadedBytesLabel("Starting Upload...");
                    SetSpeed(string.Empty);
                    break;
                case UploadState.Processing:
                    SetProgressBarStyleLabel(ProgressBarStyle.Continuous);

                    SetSpeed(speed.ToString());
                    SetProgressBarValueLabel(progress.ProgressPercentage);
                    SetUploadedBytesLabel($"{progress.Current}/{progress.Total}");
                    break;
                case UploadState.Paused:
                    // TODO / Not needed
                    break;
                case UploadState.Finished:
                    SetProgressBarStyleLabel(ProgressBarStyle.Marquee);

                    SetUploadedBytesLabel("Waiting for server reply...");
                    SetSpeed(string.Empty);
                    break;
                default:
                    Debug.Fail($"Unhandled {nameof(UploadState)}");
                    break;
            }
        }

        public void ShowProgress() => Show();
        public void CloseProgress() => _ = CloseDialog();
    }
}
