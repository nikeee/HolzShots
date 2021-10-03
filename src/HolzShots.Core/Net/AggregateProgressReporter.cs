using System;
using System.Collections.Generic;

namespace HolzShots.Net
{
    public sealed class AggregateProgressReporter : ITransferProgressReporter
    {
        private readonly IReadOnlyCollection<ITransferProgressReporter> _reporters;
        public AggregateProgressReporter(IReadOnlyCollection<ITransferProgressReporter> reporters)
        {
            _reporters = reporters ?? throw new ArgumentNullException(nameof(reporters));
        }

        public void CloseProgress()
        {
            foreach (var reporter in _reporters)
                reporter.CloseProgress();
        }

        public void ShowProgress()
        {
            foreach (var reporter in _reporters)
                reporter.ShowProgress();
        }

        public void UpdateProgress(TransferProgress progress, Speed<MemSize> speed)
        {
            foreach (var reporter in _reporters)
                reporter.UpdateProgress(progress, speed);
        }

        public void Dispose()
        {
            foreach (var reporter in _reporters)
                reporter.Dispose();
        }
    }

#if DEBUG
    public sealed class ConsoleProgressReporter : ITransferProgressReporter
    {
        public void CloseProgress() { }
        public void ShowProgress() { }
        public void UpdateProgress(TransferProgress progress, Speed<MemSize> speed)
        {
            Console.WriteLine($"{progress.State}; {progress.Current}/{progress.Total} ({progress.ProgressPercentage}%) with {speed.ItemsPerSecond}");
        }
        public void Dispose() { }
    }

#endif
}
