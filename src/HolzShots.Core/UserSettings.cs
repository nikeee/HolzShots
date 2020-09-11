using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HolzShots.Composition;
using HolzShots.Input;
using HolzShots.IO;
using HolzShots.Net;
using Newtonsoft.Json;

namespace HolzShots
{
    public static class UserSettings
    {
        public static SettingsManager<HSSettings> Manager { get; private set; } = null;
        public static HSSettings Current => Manager.CurrentSettings;

        public static async Task Load(ISynchronizeInvoke synchronizingObject)
        {
            Manager = new HolzShotsUserSettings(HolzShotsPaths.UserSettingsFilePath, synchronizingObject);
            await CreateUserSettingsIfNotPresent().ConfigureAwait(false);
            await Manager.InitializeSettings().ConfigureAwait(false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("AsyncUsage", "AsyncFixer01:Unnecessary async/await usage", Justification = "using statement")]
        public static async Task CreateUserSettingsIfNotPresent()
        {
            if (File.Exists(HolzShotsPaths.UserSettingsFilePath))
                return;

            using (var fs = File.OpenWrite(HolzShotsPaths.UserSettingsFilePath))
            {

                var defaultSettingsStr = Manager.SerializeSettings(CreateDefaultSettings());
                var defaultSettings = System.Text.Encoding.UTF8.GetBytes(defaultSettingsStr);
                await fs.WriteAsync(defaultSettings, 0, defaultSettings.Length).ConfigureAwait(false);
            }
        }

        public static void OpenSettingsInDefaultEditor() => HolzShotsPaths.OpenFileInDefaultApplication(HolzShotsPaths.UserSettingsFilePath);

        public static Task ForceReload() => Manager.ForceReload();

        /// <summary> TODO: This look wrong here. We should place this somewhere else. </summary>
        public static UploaderEntry /*?*/ GetImageServiceForSettingsContext(HSSettings context, UploaderManager uploaderManager)
        {
            Debug.Assert(context != null);
            Debug.Assert(uploaderManager != null);
            Debug.Assert(uploaderManager.Loaded);

            return uploaderManager.GetUploaderByName(context.TargetImageHoster);
        }

        private static HSSettings CreateDefaultSettings()
        {
            var s = new HSSettings
            {
                TrayIconDoubleClickCommand = "openSettingsJson",
                KeyBindings = new List<KeyBinding>() {
                    new KeyBinding {
                        Enabled = true,
                        Command = "captureArea",
                        Keys = new Hotkey(ModifierKeys.None, Keys.F8),
                    },
                    new KeyBinding {
                        Enabled = true,
                        Command = "captureEntireScreen",
                        Keys = new Hotkey(ModifierKeys.None, Keys.F10),
                    },
                    new KeyBinding {
                        Enabled = false,
                        Command = "captureWindow",
                        Keys = new Hotkey(ModifierKeys.None, Keys.F9),
                    },
                }
            };
            return s;
        }
    }

    class HolzShotsUserSettings : SettingsManager<HSSettings>
    {
        const string SupportedVersion = "1.0.0";

        public HolzShotsUserSettings(string settingsFilePath, ISynchronizeInvoke synchronizingObject = null)
            : base(settingsFilePath, synchronizingObject) { }

        protected override IReadOnlyList<ValidationError> IsValidSettingsCandidate(HSSettings candidate)
        {
            Debug.Assert(candidate != null);

            // We might want to use SemVer in the future
            if (candidate.Version != SupportedVersion)
                return SingleError($"Version {candidate.Version} is not supported. This version of HolzShots only supports settings version {SupportedVersion}.", "version");

            if (candidate.TargetImageHoster != null)
            {
                // TODO: Validate that we actually have an image service that is named like that
            }

            // var validationErrors = ImmutableList.CreateBuilder<ValidationError>();

            return ImmutableList<ValidationError>.Empty;
        }

        private static IReadOnlyList<ValidationError> SingleError(string message, string affectedProperty, Exception exception = null)
        {
            return ImmutableList.Create(new ValidationError(message, affectedProperty, exception));
        }
    }
}
