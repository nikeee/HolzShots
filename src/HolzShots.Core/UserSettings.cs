using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using HolzShots.IO;
using Newtonsoft.Json;

namespace HolzShots
{
    public class HSSettings
    {
        public string Version { get; } = "0.1.0";
        // TODO: SchemaURL

        public bool AutoCloseShotEditor { get; private set; } = false;
        /// <summary> Mutually exclusive with EnableLinkViewer </summary>
        public bool AutoCloseLinkViewer { get; private set; } = true;
        public bool EnableUploadProgressToast { get; private set; } = true;
        public bool ShowCopyConfirmation { get; private set; } = false;
    }

    public static class UserSettings
    {
        public static SettingsManager<HSSettings> Manager { get; private set; } = null;
        public static HSSettings Current => Manager.CurrentSettings;

        public static Task Load(ISynchronizeInvoke synchronizingObject)
        {
            Manager = new SettingsManager<HSSettings>(HolzShotsPaths.UserSettingsFilePath, synchronizingObject);
            return Manager.InitializeSettings();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("AsyncUsage", "AsyncFixer01:Unnecessary async/await usage", Justification = "using statement")]
        public static async Task CreateUserSettingsIfNotPresent()
        {
            if (File.Exists(HolzShotsPaths.UserSettingsFilePath))
                return;

            var defaultSettingsStr = @"{
    // 'autoCloseShotEditor': false,
    // 'autoCloseLinkViewer': true,
    // 'enableUploadProgressToast': true,
    // 'showCopyConfirmation': false,
}
";
            using (var writer = File.OpenWrite(HolzShotsPaths.UserSettingsFilePath))
            {
                var defaultSettings = System.Text.Encoding.UTF8.GetBytes(defaultSettingsStr.Replace("'", "\""));
                await writer.WriteAsync(defaultSettings, 0, defaultSettings.Length);
            }
        }

        public static void OpenSettingsInDefaultEditor() => Process.Start(HolzShotsPaths.UserSettingsFilePath);

        public static Task ForceReload() => Manager.ForceReload();
    }
}
