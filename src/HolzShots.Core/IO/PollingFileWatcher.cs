using System.ComponentModel;
using System.IO;
using HolzShots.Threading;

namespace HolzShots.IO;

/// <summary>
/// Based on:
/// https://stackoverflow.com/a/26265937
/// </summary>
class PollingFileWatcher
{
    public string FilePath { get; }
    public TimeSpan PollingInterval { get; }

    public static readonly TimeSpan MinimumPollingInterval = TimeSpan.FromMilliseconds(100);

    private readonly ISynchronizeInvoke? _synchronizingObject;
    private FileInfo? _info;
    private DateTime? _lastWriteTime;

    internal PollingFileWatcher(string filePath, TimeSpan pollingInterval, ISynchronizeInvoke? synchronizingObject = null)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        ArgumentOutOfRangeException.ThrowIfLessThan(pollingInterval, MinimumPollingInterval);
        FilePath = filePath;
        PollingInterval = pollingInterval;
        _synchronizingObject = synchronizingObject;
    }

    public async Task Start(CancellationToken cancellationToken)
    {
        _info = new FileInfo(FilePath);
        _lastWriteTime = _info.LastWriteTime;

        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(PollingInterval, cancellationToken).ConfigureAwait(false);

            if (!cancellationToken.IsCancellationRequested)
                PerformCheck();
        }
    }

    void PerformCheck()
    {
        if (_info is null)
            throw new InvalidOperationException($"{nameof(PerformCheck)} called with {nameof(_info)} being null");

        _info.Refresh();
        if (!_info.Exists)
            return;

        if (_lastWriteTime < _info.LastWriteTime)
        {
            _lastWriteTime = _info.LastWriteTime;
            InvokeEvent();
        }
    }

    void InvokeEvent()
    {
        var e = OnFileWritten;
        if (e is null)
            return;

        // We pass the original FileInfo object to the event handlers
        // We don't want to use it for further operations.
        // Instead, we create a new one so we don't interfere with other operations in the event handler.

        var oldInfo = _info ?? throw new InvalidOperationException($"{nameof(_info)} was null in {nameof(InvokeEvent)}");
        _info = new FileInfo(FilePath);

        if (_synchronizingObject is not null)
            _synchronizingObject.InvokeIfNeeded(() => e(this, oldInfo));
        else
            e(this, oldInfo);
    }

    public event EventHandler<FileInfo>? OnFileWritten;
}
