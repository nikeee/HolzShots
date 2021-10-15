using System.Diagnostics;
using System.IO;

namespace HolzShots.IO
{
    public static class HolzShotsPaths
    {
        private static readonly string SystemAppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string UserPicturesDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        public static string AppDataDirectory { get; } = Path.Combine(SystemAppDataDirectory, LibraryInformation.Name);

        public const string CustomUploadersFilePattern = "*.json";

        public static string SystemPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.System);

        public static string PluginDirectory { get; } = Path.Combine(AppDataDirectory, "Plugin");
        public static string CustomUploadersDirectory { get; } = PluginDirectory;
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

        public static void EnsureAppDataDirectories()
        {
            if (!Directory.Exists(AppDataDirectory))
            {
                Directory.CreateDirectory(AppDataDirectory);
                Directory.CreateDirectory(PluginDirectory);
            }
        }

        public static void OpenLink(string url)
        {
            Debug.Assert(url != null);

            try
            {
                OpenFileInDefaultApplication(url);
            }
            catch
            {
                Trace.WriteLine("Could not open link: " + url);
            }
        }
        public static void OpenFileInDefaultApplication(string fileName)
        {
            // Necessary since .NET core, see: https://stackoverflow.com/questions/46808315
            var psi = new ProcessStartInfo(fileName)
            {
                UseShellExecute = true,
            };
            Process.Start(psi);
        }
        public static bool OpenSelectedFileInExplorer(string fileName)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(fileName));
            if (string.IsNullOrEmpty(fileName))
                return false;

            var psi = new ProcessStartInfo(
                "explorer",
                $"/e, /select, \"{fileName}\""
            );
            try
            {
                Process.Start(psi);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool OpenFolderInExplorer(string directoryName)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(directoryName));
            if (string.IsNullOrEmpty(directoryName))
                return false;

            var psi = new ProcessStartInfo("explorer", directoryName)
            {
                Verb = "open",
                UseShellExecute = true,
            };

            try
            {
                Process.Start(psi);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary> TODO: Consider re-adding this functionality </summary>
        public static void AddToRecentDocuments(string path)
        {
            Native.Shell32.SHAddToRecentDocs(Native.Shell32.ShellAddToRecentDocsFlags.Path, path);
        }
    }
}
