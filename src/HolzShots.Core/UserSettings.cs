using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using HolzShots.Input;
using HolzShots.IO;

namespace HolzShots
{
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
                var defaultSettingsStr = Manager.SerializeSettings(CreateDefaultSettings());
                var defaultSettings = System.Text.Encoding.UTF8.GetBytes(defaultSettingsStr);
                await writer.WriteAsync(defaultSettings, 0, defaultSettings.Length);
            }
        }

        public static void OpenSettingsInDefaultEditor() => Process.Start(HolzShotsPaths.UserSettingsFilePath);

        public static Task ForceReload() => Manager.ForceReload();


        private static HSSettings CreateDefaultSettings()
        {
            var s = new HSSettings
            {
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
}
