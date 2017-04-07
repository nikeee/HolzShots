using System;
using System.Diagnostics;
using System.Threading;

namespace HolzShots.Threading
{
    // TODO: Is this good or a hack?
    public sealed class ProcessPriorityRequest : IDisposable
    {
        private static volatile bool _instanceExists;

        private readonly Process _currentProcess;
        private ThreadPriority _initialThreadPriority;
        private ProcessPriorityClass _initialProcessPriority;
        private readonly bool _instanceValid;

        public ProcessPriorityRequest()
        {
            _instanceValid = !_instanceExists;
            _instanceExists = true;

            Debug.Assert(_instanceValid);

            _currentProcess = Process.GetCurrentProcess();
            _initialThreadPriority = Thread.CurrentThread.Priority;
            _initialProcessPriority = _currentProcess.PriorityClass;
            Debug.WriteLine("Priortiy raised.");
        }

        private void ResetPriority()
        {
            Debug.Assert(_instanceValid);
            if (_instanceValid)
            {
                Thread.CurrentThread.Priority = _initialThreadPriority;
                _currentProcess.PriorityClass = _initialProcessPriority;
                Debug.WriteLine("Priortiy reset.");
            }
        }

        #region IDisposable Support

        private bool disposedValue;
        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                try
                {
                    ResetPriority();
                }
                catch
                {
                    Debug.Assert(false);
                }
                _instanceExists = false;
                disposedValue = true;
            }
        }

        ~ProcessPriorityRequest() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
