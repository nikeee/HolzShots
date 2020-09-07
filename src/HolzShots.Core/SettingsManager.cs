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
using System.Reflection;
using System.CodeDom;

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

        public T DeriveContextEffectiveSettings(T input, IReadOnlyDictionary<string, dynamic> overrides)
        {
            if (overrides == null || overrides.Count == 0)
                return input;

            var settingsCopy = input.Copy();

            var settingsType = typeof(T);
            var properties = settingsType.GetProperties();

            foreach (var prop in properties)
            {
                var jsonAttr = prop.GetCustomAttribute<JsonPropertyAttribute>();
                var jsonPropertyName = jsonAttr?.PropertyName;

                if (jsonPropertyName == null)
                    continue;

                if (!overrides.TryGetValue(jsonPropertyName, out var overriddenValue))
                    continue;

                OverrideProperty(settingsCopy, prop, overriddenValue);
            }

            return settingsCopy;
        }

        private static void OverrideProperty(T targetObject, PropertyInfo property, dynamic value)
        {
            Debug.Assert(targetObject != null);
            Debug.Assert(property != null);

            var propType = property.PropertyType;

            if (propType.IsEnum && value is string jsonEnumMember)
            {
                foreach (var enumMemberName in Enum.GetNames(propType))
                {
                    var enumMember = propType.GetField(enumMemberName);
                    var enumMemberAttr = enumMember.GetCustomAttribute<System.Runtime.Serialization.EnumMemberAttribute>();
                    if (enumMemberAttr == null)
                        continue;
                    if (enumMemberAttr.Value == jsonEnumMember)
                    {
                        var enumValue = Enum.Parse(propType, enumMemberName);
                        property.SetValue(targetObject, enumValue);
                        return;
                    }
                }
                Trace.WriteLine($"No match found for enum member of property {property.Name}");
            }

            if (propType.IsPrimitive)
            {
                if (value == null)
                {
                    // Ignore primitive values that are null (they cannot be)
                    // Maybe we want to change this behaviour later so that this property gets set to default()
                    return;
                }

                // What is this
                if (propType == typeof(bool) && value is bool b)
                {
                    property.SetValue(targetObject, b);
                    return;
                }
                if (propType == typeof(double))
                {
                    if (HandleFloatNumber<double>(targetObject, property, value))
                        return;
                }
                if (propType == typeof(float))
                {
                    if (HandleFloatNumber<float>(targetObject, property, value))
                        return;
                }
                if (propType == typeof(int))
                {
                    if (HandleIntegerNumber<int>(targetObject, property, value))
                        return;
                }
                if (propType == typeof(long))
                {
                    if (HandleIntegerNumber<long>(targetObject, property, value))
                        return;
                }
                if (propType == typeof(short))
                {
                    if (HandleIntegerNumber<short>(targetObject, property, value))
                        return;
                }
                if (propType == typeof(byte))
                {
                    if (HandleIntegerNumber<byte>(targetObject, property, value))
                        return;
                }
                if (propType == typeof(char) && value is char c)
                {
                    property.SetValue(targetObject, c);
                    return;
                }
                if (propType == typeof(uint) && value is uint)
                {
                    if (HandleIntegerNumber<uint>(targetObject, property, value))
                        return;
                }
                if (propType == typeof(ulong) && value is ulong)
                {
                    if (HandleIntegerNumber<ulong>(targetObject, property, value))
                        return;
                }
                if (propType == typeof(ushort) && value is ushort)
                {
                    if (HandleIntegerNumber<ushort>(targetObject, property, value))
                        return;
                }
                if (propType == typeof(sbyte) && value is sbyte)
                {
                    if (HandleIntegerNumber<sbyte>(targetObject, property, value))
                        return;
                }
                if (propType == typeof(decimal) && value is decimal)
                {
                    if (HandleFloatNumber<decimal>(targetObject, property, value))
                        return;
                }
            }

            if (propType == typeof(string) && value is string s)
            {
                property.SetValue(targetObject, s);
                return;
            }

            Trace.WriteLine($"Unsupported override of property {property.Name}, doing nothing");
        }
        private static bool HandleFloatNumber<TProp>(T targetObject, PropertyInfo property, dynamic value)
        {
            if (
                value is float
                || value is double
                || value is decimal
                || value is int
                || value is short
                || value is long
                || value is uint
                || value is ushort
                || value is ulong
                )
            {
                property.SetValue(targetObject, (TProp)value);
                return true;
            }
            return false;
        }

        private static bool HandleIntegerNumber<TProp>(T targetObject, PropertyInfo property, dynamic value)
        {
            if (
                value is int
                || value is short
                || value is long
                || value is uint
                || value is ushort
                || value is ulong
                )
            {
                property.SetValue(targetObject, (TProp)value);
                return true;
            }
            return false;
        }


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
