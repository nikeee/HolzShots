using System.Diagnostics;
using HolzShots.Net;

namespace HolzShots.Windows.Forms
{
    public sealed class TaskbarItemProgressReporter : IUploadProgressReporter
    {
        private readonly bool _hasValidHandle;
        private readonly TaskbarProgressManager _progressManager;
        private TaskbarProgressBarState _state;

        private static readonly HashSet<TaskbarProgressManager> _currentManagers = new();

        public TaskbarItemProgressReporter(IntPtr targetWindowHandle)
        {
            _progressManager = Taskbar.CreateProgressManagerForWindow(targetWindowHandle);
            _hasValidHandle = targetWindowHandle != IntPtr.Zero
                                && Taskbar.IsPlatformSupported
                                && !_currentManagers.Contains(_progressManager);

            if (_hasValidHandle)
                _currentManagers.Add(_progressManager);
        }
        public void UpdateProgress(UploadProgress progress, Speed<MemSize> speed)
        {
            if (!_hasValidHandle)
                return;

            switch (progress.State)
            {
                case UploadState.NotStarted:
                    SetProgressState(TaskbarProgressBarState.Indeterminate);
                    break;
                case UploadState.Paused:
                    SetProgressState(TaskbarProgressBarState.Paused);
                    break;
                case UploadState.Finished:
                    SetProgressState(TaskbarProgressBarState.Indeterminate);
                    break;
                case UploadState.Processing:
                    if (progress.ProgressPercentage >= 0)
                    {
                        if (_state != TaskbarProgressBarState.Normal)
                            SetProgressState(TaskbarProgressBarState.Normal);
                        SetProgress(progress.ProgressPercentage);
                    }
                    break;
                default:
                    Debug.Fail($"Unhandled UploadState: {progress.State}");
                    break;
            }
        }

        private void SetProgress(uint percentage) => _progressManager.SetProgressValue(percentage, 100);
        private void SetProgressState(TaskbarProgressBarState newState)
        {
            _state = newState;
            _progressManager.SetProgressState(newState);
        }

        public void CloseProgress()
        {
            _currentManagers.Remove(_progressManager);
            SetProgress(0);
            _progressManager.SetProgressState(TaskbarProgressBarState.NoProgress);
        }

        public void ShowProgress()
        {
            _progressManager.SetProgressState(TaskbarProgressBarState.Indeterminate);
        }

        public void Dispose() { /* Nothing to do here */ }
    }
}
