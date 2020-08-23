using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using HolzShots.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using HolzShots.Threading;

namespace HolzShots
{
    /// <summary>
    /// TODO: Migrate to System.Text.Json some day:
    /// https://docs.microsoft.com/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to
    ///
    /// TODO: Maybe we want another type that can be transformed to T
    /// </summary>
    public class SettingsManager<T> : IDisposable, INotifyPropertyChanged
        where T : new()
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new NonPublicPropertiesResolver(),
            Formatting = Formatting.Indented,
        };

        public T CurrentSettings { get; private set; } = new T();
        public string SettingsFilePath { get; }

        private static readonly TimeSpan _pollingInterval = TimeSpan.FromSeconds(1);

        private readonly ISynchronizeInvoke _synchronizingObject;
        private readonly PollingFileWatcher _watcher;
        private CancellationTokenSource _watcherCancellation = null;

        public SettingsManager(string settingsFilePath, ISynchronizeInvoke synchronizingObject = null)
        {
            Debug.Assert(!string.IsNullOrEmpty(settingsFilePath));

            SettingsFilePath = settingsFilePath ?? throw new ArgumentNullException(nameof(settingsFilePath));

            _watcher = new PollingFileWatcher(settingsFilePath, _pollingInterval, synchronizingObject);
            _synchronizingObject = synchronizingObject;
        }

        public Task InitializeSettings()
        {
            _watcher.OnFileWritten += OnSettingsFileChanged;

            if (_watcherCancellation != null)
                _watcherCancellation.Cancel();

            _watcherCancellation = new CancellationTokenSource();

            _ = _watcher.Start(_watcherCancellation.Token);

            return ForceReload();
        }

        public Task ForceReload() => UpdateSettings(new FileInfo(SettingsFilePath));
        private void OnSettingsFileChanged(object sender, FileInfo e) => _ = UpdateSettings(e);

        private async Task UpdateSettings(FileInfo info)
        {
            if (!info.Exists)
            {
                SetCurrentSettings(new T());
                return;
            }

            // If we'd use a FileSystemWatcher, we should use de-bouncing here.
            // However, we use a polling implementation that will fire at most at _pollingInterval.
            // -> We don't need de-bouncing.

            var (success, newSettings) = await DeserializeSettings(SettingsFilePath).ConfigureAwait(false);
            if (!success || newSettings == null)
                return;

            var validationErrors = IsValidSettingsCandidate(newSettings);
            if (validationErrors.Count > 0)
            {
                InvokeWithSynchronizingObjectIfNeeded(() => OnValidationError?.Invoke(this, validationErrors));
                return;
            }

            InvokeWithSynchronizingObjectIfNeeded(() => SetCurrentSettings(newSettings));
        }

        private void SetCurrentSettings(T newSettings)
        {
            CurrentSettings = newSettings;
            OnSettingsUpdated?.Invoke(this, newSettings);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSettings)));
        }

        private static async Task<(bool, T)> DeserializeSettings(string path)
        {
            try
            {
                // TODO: Make this use some async overload of File.ReadAllText as soon as we're on .NET Core
                // https://stackoverflow.com/questions/13167934

                using (var reader = File.OpenText(path))
                {
                    // No check for File.Exists because we'll get an exception anyways and avoid race conditions
                    var settingsContent = await reader.ReadToEndAsync().ConfigureAwait(false);
                    var newSettings = JsonConvert.DeserializeObject<T>(settingsContent, _jsonSerializerSettings);

                    return (true, newSettings);
                }
            }
            catch
            {
                return (false, default);
            }
        }

        public string SerializeSettings(T settings) => JsonConvert.SerializeObject(settings, _jsonSerializerSettings);
        private void InvokeWithSynchronizingObjectIfNeeded(Action action)
        {
            if (_synchronizingObject == null)
                action();
            else
                _synchronizingObject.InvokeIfNeeded(action);
        }

        public event EventHandler<T> OnSettingsUpdated;
        public event EventHandler<IReadOnlyList<ValidationError>> OnValidationError;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual IReadOnlyList<ValidationError> IsValidSettingsCandidate(T candidate) => ImmutableList<ValidationError>.Empty;


        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _watcherCancellation?.Cancel();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
