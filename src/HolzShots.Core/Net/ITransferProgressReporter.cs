using System;

namespace HolzShots.Net
{
    public interface ITransferProgressReporter : IDisposable
    {
        void UpdateProgress(TransferProgress progress, Speed<MemSize> speed);
        void ShowProgress();
        void CloseProgress();
    }
}
