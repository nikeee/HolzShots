namespace HolzShots.Net
{
    public interface IUploadProgressReporter : IDisposable
    {
        void UpdateProgress(UploadProgress progress, Speed<MemSize> speed);
        void ShowProgress();
        void CloseProgress();
    }
}
