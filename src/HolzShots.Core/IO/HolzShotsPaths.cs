using System;
using System.IO;

namespace HolzShots.IO
{
    public static class HolzShotsPaths
    {
        private static readonly string SystemAppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string AppDataDirectory = Path.Combine(SystemAppDataDirectory, LibraryInformation.Name);

        public static string PluginDirectory { get; } = Path.Combine(AppDataDirectory, "Plugin");
        public static string UserSettingsFilePath { get; } = Path.Combine(AppDataDirectory, "settings.json");

        // TODO: Paths for JSON upload configs
    }
}
