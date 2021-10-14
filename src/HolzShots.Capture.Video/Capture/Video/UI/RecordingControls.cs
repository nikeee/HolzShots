using System.Drawing;
using System.Windows.Forms;

namespace HolzShots.Capture.Video.UI
{
    /// <summary> TODO: Make these controls prettier </summary>
    public partial class RecordingControls : Form
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly Rectangle _capturedRegion;
        public RecordingControls(Rectangle capturedRegion, CancellationTokenSource cancellationTokenSource)
        {
            _capturedRegion = capturedRegion;
            _cancellationTokenSource = cancellationTokenSource;

            InitializeComponent();
            BackColor = Color.FromArgb(44, 44, 44);

            // Also close this window if the recording is aborted
            cancellationTokenSource.Token.Register(() =>
            {
                Close();
            });

            // TODO: Test multiple locations and take the one that's not hidden / out of screen
            // We only use this one for now
            Location = new Point(
                capturedRegion.Location.X + capturedRegion.Size.Width - Width,
                capturedRegion.Location.Y + capturedRegion.Size.Height + 10
            );
        }

        private void StopRecordingButton_Click(object sender, EventArgs e) => _cancellationTokenSource.Cancel();
    }
}
