using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

        private readonly FileSystemWatcher _fsw;
        private readonly Func<T, IReadOnlyList<ValidationError>> _candidateValidator;

        public SettingsManager(string settingsFilePath, Func<T, IReadOnlyList<ValidationError>> candidateValidator = null, ISynchronizeInvoke synchronizingObject = null)
        {
            Debug.Assert(!string.IsNullOrEmpty(settingsFilePath));

            SettingsFilePath = settingsFilePath ?? throw new ArgumentNullException(nameof(settingsFilePath));

            _candidateValidator = candidateValidator;
            _fsw = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(settingsFilePath),
                Filter = Path.GetFileName(settingsFilePath),
                NotifyFilter = NotifyFilters.LastWrite,
                IncludeSubdirectories = false,
                SynchronizingObject = synchronizingObject,
            };
        }

        public Task InitializeSettings()
        {
            _fsw.Changed += OnSettingsFileChanged;
            _fsw.Deleted += OnSettingsFileDeleted;
            _fsw.EnableRaisingEvents = true;

            return ForceReload();
        }

        public Task ForceReload() => UpdateSettings(!File.Exists(SettingsFilePath));
        private void OnSettingsFileChanged(object sender, FileSystemEventArgs e) => _ = UpdateSettings(false);
        private void OnSettingsFileDeleted(object sender, FileSystemEventArgs e) => _ = UpdateSettings(true);

        private async Task UpdateSettings(bool deleted)
        {
            try
            {
                _fsw.EnableRaisingEvents = false;

                if (deleted)
                {
                    SetCurrentSettings(new T());
                    return;
                }

                // TODO: Maybe we want to add a de-bouncer here

                var (success, newSettings) = await DeserializeSettings(SettingsFilePath);
                if (!success || newSettings == null)
                    return;

                var validationErrors = IsValidSettingsCandidate(newSettings);
                if (validationErrors.Count > 0)
                {
                    OnValidationError?.Invoke(this, validationErrors);
                    return;
                }

                SetCurrentSettings(newSettings);
            }
            finally
            {
                _fsw.EnableRaisingEvents = true;
            }
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
                    var settingsContent = await reader.ReadToEndAsync();
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

    public class ValidationError
    {
        public string Message { get; }
        public string AffectedProperty { get; }
        public string InvalidValue { get; }
        public Exception /* ? */ Exception { get; }
        // TODO: Proper class with ctor and stuff
    }

    public class NonPublicPropertiesResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            if (member is PropertyInfo pi)
            {
                prop.Readable = (pi.GetMethod != null);
                prop.Writable = (pi.SetMethod != null);
            }
            return prop;
        }
    }
}
