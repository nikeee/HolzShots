using System;
using System.Collections.Generic;

namespace HolzShots.Net
{
    public sealed class AggregateProgressReporter : IUploadProgressReporter
    {
        private readonly IReadOnlyCollection<IUploadProgressReporter> _reporters;
        public AggregateProgressReporter(IReadOnlyCollection<IUploadProgressReporter> reporters)
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

        public void UpdateProgress(UploadProgress progress, Speed<MemSize> speed)
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
}
