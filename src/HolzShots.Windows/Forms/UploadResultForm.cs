using HolzShots.Net;

namespace HolzShots.Windows.Forms
{
    public partial class UploadResultForm : FlyoutForm
    {
        private readonly UploadResult _result;
        private readonly HSSettings _settingsContext;
        private readonly FlyoutAnimator _animator;
        // private readonly string _displayUrl;

        public UploadResultForm(UploadResult result, HSSettings settingsContext)
        {
            _result = result ?? throw new ArgumentNullException(nameof(result));
            _settingsContext = settingsContext ?? throw new ArgumentNullException(nameof(settingsContext));

            InitializeComponent();
            StartPosition = FormStartPosition.Manual;

            _animator = new FlyoutAnimator(this);

            /*
            if (!string.IsNullOrEmpty(result.Url))
                _displayUrl = EnvironmentEx.ShortenViaEllipsisIfNeeded(result.Url, 26);
            */

        }

        private void FormLoad(object sender, EventArgs e)
        {
            MaximumSize = Size;
            MinimumSize = Size;

            _animator.AnimateIn(300);
        }

        void MaybeClose()
        {
            if (_settingsContext.AutoCloseLinkViewer)
                _ = CloseFlyout();
        }

        async Task CloseFlyout()
        {
            await _animator.AnimateOut(150).ConfigureAwait(true);
            Close();
        }

        private void CopyAndMaybeClose(string value)
        {
            if (SetText(value))
                MaybeClose();
        }

        private void CopyMarkdownClick(object sender, EventArgs e) => CopyAndMaybeClose($"![Screenshot]({_result.Url})");
        private void CopyHTMLClick(object sender, EventArgs e) => CopyAndMaybeClose($"<img src=\"{_result.Url}\">");
        private void CopyDirectLinkClick(object sender, EventArgs e) => CopyAndMaybeClose(_result.Url);

        // TODO: Replace with ClipboardEx once it's available
        private static bool SetText(string value)
        {
            try
            {
                Clipboard.SetText(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void CloseLabel_Click(object sender, EventArgs e) => _ = CloseFlyout();
    }
}
