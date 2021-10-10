using System;

namespace HolzShots.IO
{
    /// <summary>
    /// Hack to get around a bug in a library.
    /// See: https://github.com/rosenbjerg/FFMpegCore/issues/256#issuecomment-932819872
    /// </summary>
    public class PrefixEnvironmentVariable : IDisposable
    {
        private const EnvironmentVariableTarget VariableTarget = EnvironmentVariableTarget.Process;

        private readonly string? _previousValue;
        private readonly string _envVar;

        public PrefixEnvironmentVariable(string pathToPrefix, string variableName = "PATH")
        {
            _envVar = variableName;
            _previousValue = Environment.GetEnvironmentVariable(variableName, VariableTarget);

            var nextValue = $"{pathToPrefix};{_previousValue ?? string.Empty}";
            Environment.SetEnvironmentVariable(variableName, nextValue, VariableTarget);
        }

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Environment.SetEnvironmentVariable(_envVar, _previousValue, VariableTarget);
                disposedValue = true;
            }
        }

        ~PrefixEnvironmentVariable()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
