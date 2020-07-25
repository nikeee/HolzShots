using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HolzShots.Settings
{
    class SettingsManager<T> : IDisposable, INotifyPropertyChanged
        where T : new()
    {
        public T CurrentSettings { get; private set; } = default;
        public string SettingsFilePath { get; }

        private readonly FileSystemWatcher _fsw;

        public SettingsManager(string settingsFilePath)
        {
            SettingsFilePath = settingsFilePath ?? throw new ArgumentNullException(nameof(settingsFilePath));

            _fsw = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(settingsFilePath),
                Filter = Path.GetFileName(settingsFilePath),
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime,
                IncludeSubdirectories = false,
                // TODO SynchronizingObject?
            };
        }

        public Task InitializeSettings()
        {
            _fsw.Changed += OnSettingsFileChanged;
            _fsw.Deleted += OnSettingsFileChanged;
            _fsw.EnableRaisingEvents = true;

            return UpdateSettings(File.Exists(SettingsFilePath));
        }

        private void OnSettingsFileChanged(object sender, FileSystemEventArgs e) => UpdateSettings(false);
        private void OnSettingsFileDeleted(object sender, FileSystemEventArgs e) => UpdateSettings(true);

        private async Task UpdateSettings(bool deleted)
        {
            if (deleted)
            {
                SetCurrentSettings(new T());
                return;
            }

            var (success, newSettings) = await DeserializeSettings(SettingsFilePath);
            if (!success)
                return;

            var settingsAreValid = ValidateSettingsCandidate(newSettings);
            if (!settingsAreValid)
                return;

            SetCurrentSettings(newSettings);
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

                using var reader = File.OpenText(path);
                // No check for File.Exists because we'll get an exception anyways and avoid race conditions
                var settingsContent = await reader.ReadToEndAsync();
                var newSettings = JsonConvert.DeserializeObject<T>(settingsContent);

                return (true, newSettings);
            }
            catch
            {
                return (false, default);
            }
        }

        public event EventHandler<T> OnSettingsUpdated;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool ValidateSettingsCandidate(T candidate) => true;

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _fsw.Changed -= OnSettingsFileChanged;
                    _fsw.Deleted -= OnSettingsFileChanged;
                    _fsw.Dispose();
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

    class Test
    {
        class HSSettings
        {
            public int Test { get; }
        }

        async Task TestTest()
        {

            var sm = new SettingsManager<HSSettings>("settings.json");

            sm.OnSettingsUpdated += (_, newSettings) =>
            {
                var a = newSettings.Test;
            };

            await sm.InitializeSettings();
        }
    }
}
