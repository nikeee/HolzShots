#nullable enable
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HolzShots.Drawing;

namespace HolzShots.Net
{
    public interface IUploadPayload : IDisposable
    {
        string MimeType { get; }
        string Extension { get; }
        Stream GetStream();
        string GetSuggestedFileName();
    }

    // TODO: refactor to use Bitmap
    public record ImageUploadPayload : IUploadPayload
    {
        public string MimeType { get; init; }
        public string Extension { get; init; }

        private readonly Image _image;
        private readonly ImageFormat _format;

        public ImageUploadPayload(Image image, ImageFormat format)
        {
            _format = format ?? throw new ArgumentNullException(nameof(format));
            _image = image.CloneGifBug(_format);

            (Extension, MimeType) = _format.GetExtensionAndMimeType();
            Debug.Assert(!string.IsNullOrWhiteSpace(MimeType));
            Debug.Assert(!string.IsNullOrWhiteSpace(Extension));
        }

        public Stream GetStream() => _image.GetImageStream(_format);

        public string GetSuggestedFileName()
        {
            Debug.Assert(Extension != null);
            Debug.Assert(MimeType != null);

            return ImageFormatInformation.DefaultUploadFileNameWithoutExtension + Extension;
        }

        public void Dispose() => _image.Dispose();
    }

    public sealed class UploadUI : IDisposable
    {
        private readonly ImageUploadPayload _payload;
        private readonly Uploader _uploader;
        private readonly ITransferProgressReporter? _progressReporter;

        private readonly SpeedCalculatorProgress _speedCalculator = new SpeedCalculatorProgress();

        public UploadUI(ImageUploadPayload payload, Uploader uploader, ITransferProgressReporter? progressReporter)
        {
            _payload = payload ?? throw new ArgumentNullException(nameof(payload));
            _uploader = uploader ?? throw new ArgumentNullException(nameof(uploader));
            _progressReporter = progressReporter;
        }

        public async Task<UploadResult> InvokeUploadAsync()
        {
            Debug.Assert(!_speedCalculator.HasStarted);

            using var payloadStream = _payload.GetStream();
            Debug.Assert(payloadStream != null);

            var cts = new CancellationTokenSource();

            var speed = _speedCalculator;

            if (_progressReporter != null)
                speed.ProgressChanged += ProgressChanged;

            speed.Start();

            try
            {
                return await _uploader.InvokeAsync(payloadStream, _payload.GetSuggestedFileName(), _payload.MimeType, speed, cts.Token).ConfigureAwait(false);
            }
            finally
            {
                speed.Stop();
                if (_progressReporter != null)
                    speed.ProgressChanged -= ProgressChanged;
            }
        }

        public void ShowUI() => _progressReporter?.ShowProgress();
        public void HideUI() => _progressReporter?.CloseProgress();
        private void ProgressChanged(object? sender, TransferProgress progress) => _progressReporter?.UpdateProgress(progress, _speedCalculator.CurrentSpeed);

        public void Dispose() => _progressReporter?.Dispose();
    }
}
