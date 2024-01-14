using System.Diagnostics;

namespace HolzShots.Threading;

// TODO: Is this good or a hack?
public sealed class ProcessPriorityRequest : IDisposable
{
    private static volatile bool _instanceExists;

    private readonly Process _currentProcess;
    private readonly ThreadPriority _initialThreadPriority;
    private readonly ProcessPriorityClass _initialProcessPriority;
    private readonly bool _instanceValid;

    public ProcessPriorityRequest()
    {
        _instanceValid = !_instanceExists;
        _instanceExists = true;

        Debug.Assert(_instanceValid);

        _currentProcess = Process.GetCurrentProcess();
        _initialThreadPriority = Thread.CurrentThread.Priority;
        _initialProcessPriority = _currentProcess.PriorityClass;
        Debug.WriteLine("Priority raised.");
    }

    private void ResetPriority()
    {
        Debug.Assert(_instanceValid);
        if (_instanceValid)
        {
            Thread.CurrentThread.Priority = _initialThreadPriority;
            _currentProcess.PriorityClass = _initialProcessPriority;
            Debug.WriteLine("Priority reset.");
        }
    }

    #region IDisposable Support

    private bool disposedValue;
    void Dispose(bool _disposing)
    {
        if (disposedValue)
            return;

        try
        {
            ResetPriority();
        }
        catch
        {
            throw;
        }
        _instanceExists = false;
        disposedValue = true;
    }

    ~ProcessPriorityRequest() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
