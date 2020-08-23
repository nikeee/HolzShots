using System;
using System.Diagnostics;
using System.IO;

namespace HolzShots.IO
{
    public static class HolzShotsPaths
    {
        private static readonly string SystemAppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string UserPicturesDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private static readonly string AppDataDirectory = Path.Combine(SystemAppDataDirectory, LibraryInformation.Name);

        public static string SystemPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.System);

        public static string PluginDirectory { get; } = Path.Combine(AppDataDirectory, "Plugin");
        public static string CustomUploadersDirectory { get; } = Path.Combine(AppDataDirectory, "CustomUploaders");
        public static string UserSettingsFilePath { get; } = Path.Combine(AppDataDirectory, "settings.json");

        public static string DefaultScreenshotSavePath { get; } = Path.Combine(UserPicturesDirectory, LibraryInformation.Name);

        /// <summary>
        /// We are doing this synchronously, assuming the application is not located on a network drive.
        /// See: https://stackoverflow.com/a/20596865
        /// </summary>
        /// <exception cref="System.UnauthorizedAccessException" />
        /// <exception cref="System.IO.PathTooLongException" />
        public static void EnsureDirectory(string directory)
        {
            Debug.Assert(directory != null);
            DirectoryEx.EnsureDirectory(directory);
        }
    }
}
