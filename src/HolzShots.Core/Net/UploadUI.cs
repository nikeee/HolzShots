using System.Diagnostics;

namespace HolzShots.Net;

public sealed class UploadUI : IDisposable
{
    private readonly IUploadPayload _payload;
    private readonly Uploader _uploader;
    private readonly ITransferProgressReporter? _progressReporter;

    private readonly SpeedCalculatorProgress _speedCalculator = new();

    public UploadUI(IUploadPayload payload, Uploader uploader, ITransferProgressReporter? progressReporter)
    {
        _payload = payload ?? throw new ArgumentNullException(nameof(payload));
        _uploader = uploader ?? throw new ArgumentNullException(nameof(uploader));
        _progressReporter = progressReporter;
    }

    public async Task<UploadResult> InvokeUploadAsync()
    {
        Debug.Assert(!_speedCalculator.HasStarted);

        using var payloadStream = _payload.GetStream();
        Debug.Assert(payloadStream is not null);

        var cts = new CancellationTokenSource();

        var speed = _speedCalculator;

        if (_progressReporter is not null)
            speed.ProgressChanged += ProgressChanged;

        speed.Start();

        try
        {
            return await _uploader.InvokeAsync(payloadStream, _payload.GetSuggestedFileName(), _payload.MimeType, speed, cts.Token).ConfigureAwait(false);
        }
        finally
        {
            speed.Stop();
            if (_progressReporter is not null)
                speed.ProgressChanged -= ProgressChanged;
        }
    }

    public void ShowUI() => _progressReporter?.ShowProgress();
    public void HideUI() => _progressReporter?.CloseProgress();
    private void ProgressChanged(object? sender, TransferProgress progress) => _progressReporter?.UpdateProgress(progress, _speedCalculator.CurrentSpeed);

    public void Dispose()
    {
        _payload.Dispose();
        _progressReporter?.Dispose();
    }
}
