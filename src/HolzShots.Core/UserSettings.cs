using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using HolzShots.IO;

namespace HolzShots;

public static class UserSettings
{
    public static SettingsManager<HSSettings> Manager { get; private set; } = null!;
    public static HSSettings Current => Manager.CurrentSettings;

    public static async Task Load(ISynchronizeInvoke synchronizingObject)
    {
        Manager = new HolzShotsUserSettings(HolzShotsPaths.UserSettingsFilePath, synchronizingObject);
        await CreateUserSettingsIfNotPresent().ConfigureAwait(false);
        await Manager.InitializeSettings().ConfigureAwait(false);
    }

    /// <returns>true if the default stuff was created.</returns>
    public static async Task CreateUserSettingsIfNotPresent()
    {
        if (File.Exists(HolzShotsPaths.UserSettingsFilePath))
            return;

        var createdAppData = HolzShotsPaths.EnsureAppDataDirectories();
        if (!createdAppData)
        {
            // Since the appdata (and the plugins dir) was just created, there is no demo uploader. So we need to place it there.
            // It's also referenced in the default settings, so it should better be there
            var contents = await HolzShotsResources.ReadResourceAsString("HolzShots.Resources.DirectUpload.net.hs.json");
            await File.WriteAllTextAsync(HolzShotsPaths.DemoCustomUploaderPath, contents);
        }

        using var fs = File.OpenWrite(HolzShotsPaths.UserSettingsFilePath);
        var defaultSettingsStr = await CreateDefaultSettingsJson().ConfigureAwait(false);
        var defaultSettings = Encoding.UTF8.GetBytes(defaultSettingsStr);
        await fs.WriteAsync(defaultSettings).ConfigureAwait(false);
    }

    public static void OpenSettingsInDefaultEditor() => HolzShotsPaths.OpenFileInDefaultApplication(HolzShotsPaths.UserSettingsFilePath);

    public static Task ForceReload() => Manager.ForceReload();

    private async static Task<string> CreateDefaultSettingsJson()
    {
        // TODO: Make this prettier
        var defaultSettings = await HolzShotsResources.ReadResourceAsString("HolzShots.Resources.DefaultSettings.json");
        defaultSettings = defaultSettings
            .Replace("DEFAULT_SAVE_PATH", HolzShotsPaths.DefaultScreenshotSavePath.Replace(@"\", @"\\"))
            .Replace("DEFAULT_UPLOAD_SERVICE", "directupload.net");
        return defaultSettings;
    }
}

class HolzShotsUserSettings : SettingsManager<HSSettings>
{
    const string SupportedVersion = "1.0.0";

    public HolzShotsUserSettings(string settingsFilePath, ISynchronizeInvoke? synchronizingObject = null)
        : base(settingsFilePath, synchronizingObject) { }

    protected override IReadOnlyList<ValidationError> IsValidSettingsCandidate(HSSettings candidate)
    {
        Debug.Assert(candidate is not null);

        // We might want to use SemVer in the future
        if (candidate.Version != SupportedVersion)
            return SingleError($"Version {candidate.Version} is not supported. This version of HolzShots only supports settings version {SupportedVersion}.", "version");

        if (candidate.TargetImageHoster is not null)
        {
            // TODO: Validate that we actually have an image service that is named like that
        }

        // var validationErrors = ImmutableList.CreateBuilder<ValidationError>();

        return ImmutableList<ValidationError>.Empty;
    }

    private static IReadOnlyList<ValidationError> SingleError(string message, string affectedProperty, Exception? exception = null)
    {
        return ImmutableList.Create(new ValidationError(message, affectedProperty, exception));
    }
}
