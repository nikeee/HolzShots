using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using HolzShots.Drawing;

namespace HolzShots.Net
{
    public sealed class UploadUI : IDisposable
    {
        private readonly Image _image;
        private readonly Uploader _uploader;
        private readonly ImageFormat _format;
        private readonly IUploadProgressReporter _progressReporter;

        private readonly SpeedCalculatorProgress _speedCalculator = new SpeedCalculatorProgress();

        public UploadUI(Image image, Uploader uploader, ImageFormat format, IUploadProgressReporter /* ? */ progressReporter)
        {
            _image = image.CloneGifBug(format) ?? throw new ArgumentNullException(nameof(image));
            _format = format ?? throw new ArgumentNullException(nameof(format));
            _uploader = uploader ?? throw new ArgumentNullException(nameof(uploader));
            _progressReporter = progressReporter;
        }

        public async Task<UploadResult> InvokeUploadAsync()
        {
            Debug.Assert(!_speedCalculator.HasStarted);

            using (var imageStream = _image.GetImageStream(_format))
            {
                Debug.Assert(imageStream != null);

                var metadata = _format.GetExtensionAndMimeType();
                Debug.Assert(!string.IsNullOrWhiteSpace(metadata.MimeType));
                Debug.Assert(!string.IsNullOrWhiteSpace(metadata.FileExtension));

                var suggestedFileName = ImageFormatInformation.GetSuggestedFileName(metadata);

                var cts = new CancellationTokenSource();

                var speed = _speedCalculator;

                if (_progressReporter != null)
                    speed.ProgressChanged += ProgressChanged;

                speed.Start();

                try
                {
                    return await _uploader.InvokeAsync(imageStream, suggestedFileName, metadata.MimeType, speed, cts.Token).ConfigureAwait(false);
                }
                finally
                {
                    speed.Stop();
                    if (_progressReporter != null)
                        speed.ProgressChanged -= ProgressChanged;
                }
            }
        }

        public void ShowUI() => _progressReporter?.ShowProgress();
        public void HideUI() => _progressReporter?.CloseProgress();
        private void ProgressChanged(object sender, UploadProgress progress) => _progressReporter?.UpdateProgress(progress, _speedCalculator.CurrentSpeed);

        public void Dispose()
        {
            _image.Dispose();
            _progressReporter?.Dispose();
        }
    }
}
