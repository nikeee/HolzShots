using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HolzShots.Threading;

namespace HolzShots.IO
{
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
        private FileInfo _info;
        private DateTime _lastWriteTime;

        internal PollingFileWatcher(string filePath, TimeSpan pollingInterval, ISynchronizeInvoke synchronizingObject = null)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            PollingInterval = pollingInterval >= MinimumPollingInterval
                                    ? pollingInterval
                                    : throw new ArgumentException($"pollingInterval should be >= {MinimumPollingInterval}");
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
            if (e == null)
                return;

            // We pass the original FileInfo object to the event handlers
            // We don't want to use it for further operations.
            // Instead, we create a new one so we don't interfere with other operations in the event handler.

            var oldInfo = _info;
            _info = new FileInfo(FilePath);

            if (_synchronizingObject != null)
                _synchronizingObject.InvokeIfNeeded(() => e(this, oldInfo));
            else
                e(this, oldInfo);
        }

        public event EventHandler<FileInfo> OnFileWritten;
    }
}
