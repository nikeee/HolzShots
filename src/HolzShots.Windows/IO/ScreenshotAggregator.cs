using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using HolzShots.Drawing;
using HolzShots.IO.Naming;
using HolzShots.Windows.Forms;

namespace HolzShots.IO
{
    public static class ScreenshotAggregator
    {
        private static string _lastFileName = string.Empty;

        public static void OpenPictureSaveDirectory(HSSettings settingsContext)
        {
            var path = settingsContext.ExpandedSavePath;
            if (string.IsNullOrWhiteSpace(path))
            {
                NotificationManager.NoPathSpecified();
                return;
            }

            var ensuredDestinationDirectory = GetAndEnsureDestinationDirectory(settingsContext);
            if (ensuredDestinationDirectory == null)
            {
                NotificationManager.PathDoesNotExist(path);
                return;
            }

            if (!string.IsNullOrEmpty(_lastFileName) && File.Exists(_lastFileName))
            {
                HolzShotsPaths.OpenSelectedFileInExplorer(_lastFileName);
            }
            else
            {
                HolzShotsPaths.OpenFolderInExplorer(path);
            }
        }

        public static void HandleScreenshot(Screenshot shot, HSSettings settingsContext)
        {
            //  If Not settingsContext.SaveImagesToLocalDisk OrElse Not CheckSavePath(settingsContext) Then Return
            if (!settingsContext.SaveToLocalDisk)
                return;

            var ensuredDestinationDirectory = GetAndEnsureDestinationDirectory(settingsContext);
            if (ensuredDestinationDirectory == null)
                return;

            SaveScreenshot(shot, ensuredDestinationDirectory, settingsContext);
        }

        private static void SaveScreenshot(Screenshot shot, string ensuredDestinationDirectory, HSSettings settingsContext)
        {
            var format = ImageFormat.Png;
            var extensionAndMimeType = ImageFormatInformation.GetExtensionAndMimeType(format);

            Debug.Assert(shot.Image.GetType() == typeof(Bitmap));

            var screenshotImage = shot.Image.GetType() == typeof(Bitmap)
                ? shot.Image
                : new Bitmap(shot.Image);

            if (settingsContext.EnableSmartFormatForSaving && ImageFormatAnalyser.IsOptimizable(screenshotImage))
            {
                format = ImageFormatAnalyser.GetBestFittingFormat(screenshotImage);

                extensionAndMimeType = format.GetExtensionAndMimeType();
                Debug.Assert(!string.IsNullOrWhiteSpace(extensionAndMimeType.FileExtension));
            }

            var pattern = settingsContext.SaveImageFileNamePattern;
            var patternData = shot.GetFileMetadata(format);

            string name;
            try
            {

                name = FileNamePatternFormatter.GetFileNameFromPattern(patternData, pattern);
            }
            catch (PatternSyntaxException)
            {
                NotificationManager.InvalidFilePattern(pattern);
                return;
            }

            var fileName = Path.ChangeExtension(name, extensionAndMimeType.FileExtension);
            var path = GetAbsolutePath(ensuredDestinationDirectory, fileName);

            var freePath = FileEx.GetUnusedFileNameFromCandidate(path);

            screenshotImage.Save(freePath, format);

            _lastFileName = path;
        }

        private static string? GetAndEnsureDestinationDirectory(HSSettings settingsContext)
        {
            var resolvedPath = settingsContext.ExpandedSavePath;
            Debug.Assert(!string.IsNullOrEmpty(resolvedPath));

            try
            {
                HolzShotsPaths.EnsureDirectory(resolvedPath);
                return resolvedPath;
            }
            catch (UnauthorizedAccessException)
            {
                NotificationManager.UnauthorizedAccessExceptionDirectory(resolvedPath);
                return null;
            }
            catch (PathTooLongException)
            {
                NotificationManager.PathIsTooLong(resolvedPath);
                return null;
            }
        }

        private static string GetAbsolutePath(string resolvedSaveDir, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));
            return Path.Combine(resolvedSaveDir, fileName);
        }
    }
}
