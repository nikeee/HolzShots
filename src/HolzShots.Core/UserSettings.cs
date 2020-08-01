using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using HolzShots.IO;
using Newtonsoft.Json;

namespace HolzShots
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings")]
    public class HSSettings
    {
        [JsonProperty("$schema")]
        public string SchemaUrl { get; } = "";
        public string Version { get; } = "0.1.0";

        public string SavePath { get; private set; } = HolzShotsPaths.DefaultScreenshotSavePath;
        /// <summary> TODO: Change name </summary>
        public string SaveFileNamePattern { get; private set; } = "Screenshot-<Date>";

        public bool AutoCloseShotEditor { get; private set; } = false;
        /// <summary> Mutually exclusive with EnableLinkViewer </summary>
        public bool AutoCloseLinkViewer { get; private set; } = true;
        public bool EnableUploadProgressToast { get; private set; } = true;
        public bool ShowCopyConfirmation { get; private set; } = false;

        public bool SaveImagesToLocalDisk { get; private set; } = true;

        /// <summary> TODO: Change name </summary>
        public bool EnableIngameMode { get; private set; } = false;
        /// <summary> TODO: Maybe use a different name for that. </summary>
        public bool EnableSmartFormatForUpload { get; private set; } = false;
        public bool EnableSmartFormatForSaving { get; private set; } = false;
    }

    public static class UserSettings
    {
        public static SettingsManager<HSSettings> Manager { get; private set; } = null;
        public static HSSettings Current => Manager.CurrentSettings;

        public static async Task Load(ISynchronizeInvoke synchronizingObject)
        {
            Manager = new SettingsManager<HSSettings>(HolzShotsPaths.UserSettingsFilePath, synchronizingObject);
            await CreateUserSettingsIfNotPresent();
            await Manager.InitializeSettings();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("AsyncUsage", "AsyncFixer01:Unnecessary async/await usage", Justification = "using statement")]
        public static async Task CreateUserSettingsIfNotPresent()
        {
            if (File.Exists(HolzShotsPaths.UserSettingsFilePath))
                return;

            using (var writer = File.OpenWrite(HolzShotsPaths.UserSettingsFilePath))
            {
                var defaultSettingsStr = Manager.SerializeSettings(new HSSettings());
                var defaultSettings = System.Text.Encoding.UTF8.GetBytes(defaultSettingsStr);
                await writer.WriteAsync(defaultSettings, 0, defaultSettings.Length);
            }
        }

        public static void OpenSettingsInDefaultEditor() => Process.Start(HolzShotsPaths.UserSettingsFilePath);

        public static Task ForceReload() => Manager.ForceReload();
    }
}
