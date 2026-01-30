using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Xunit;

namespace HolzShots.Tests;

public class SettingsManagerTest
{
    private sealed class TempSettingsFile : IDisposable
    {
        public string Path { get; }

        public TempSettingsFile()
        {
            Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"holzshots-test-{Guid.NewGuid()}.json");
        }

        public void Dispose()
        {
            try
            {
                if (File.Exists(Path))
                    File.Delete(Path);
            }
            catch
            {
                // Ignore cleanup errors
            }
        }
    }

    [Fact]
    public void DeriveContextEffectiveSettings_EmptyOverrides_ReturnsSameInstance()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();
        var overrides = ImmutableDictionary<string, dynamic>.Empty;

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.Same(settings, result);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_NullOverrides_ReturnsSameInstance()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();

        var result = manager.DeriveContextEffectiveSettings(settings, null!);

        Assert.Same(settings, result);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_OverrideBooleanProperty_ReturnsModifiedCopy()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();
        var originalSaveToLocalDisk = settings.SaveToLocalDisk;

        var overrides = new Dictionary<string, dynamic>
        {
            ["save.enabled"] = !originalSaveToLocalDisk
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
        Assert.Equal(originalSaveToLocalDisk, settings.SaveToLocalDisk);
        Assert.Equal(!originalSaveToLocalDisk, result.SaveToLocalDisk);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_OverrideStringProperty_ReturnsModifiedCopy()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();

        var customPath = Path.Combine(Path.GetTempPath(), "CustomPath");
        var overrides = new Dictionary<string, dynamic>
        {
            ["save.path"] = customPath
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
        Assert.NotEqual(customPath, settings.SavePath);
        Assert.Equal(customPath, result.SavePath);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_OverrideIntProperty_ReturnsModifiedCopy()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();

        var overrides = new Dictionary<string, dynamic>
        {
            ["video.framesPerSecond"] = 15
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
        Assert.Equal(30, settings.VideoFrameRate);
        Assert.Equal(15, result.VideoFrameRate);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_OverrideFloatProperty_ReturnsModifiedCopy()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();

        var overrides = new Dictionary<string, dynamic>
        {
            ["capture.selection.dimmingOpacity"] = 0.5
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
        Assert.Equal(0.8f, settings.AreaSelectorDimmingOpacity);
        Assert.Equal(0.5f, result.AreaSelectorDimmingOpacity);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_OverrideEnumProperty_ReturnsModifiedCopy()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();

        var overrides = new Dictionary<string, dynamic>
        {
            ["upload.actionAfterUpload"] = "copyLink"
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
        Assert.Equal(UploadHandlingAction.Flyout, settings.ActionAfterUpload);
        Assert.Equal(UploadHandlingAction.CopyToClipboard, result.ActionAfterUpload);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_OverrideMultipleProperties_ReturnsModifiedCopy()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();

        var videosPath = Path.Combine(Path.GetTempPath(), "Videos");
        var overrides = new Dictionary<string, dynamic>
        {
            ["save.enabled"] = false,
            ["save.path"] = videosPath,
            ["editor.title"] = "Video Editor",
            ["video.framesPerSecond"] = 20,
            ["capture.cursor"] = true,
            ["upload.actionAfterUpload"] = "none"
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
        Assert.True(settings.SaveToLocalDisk);
        Assert.NotEqual(videosPath, settings.SavePath);
        Assert.Equal("Shot Editor", settings.ShotEditorTitle);
        Assert.Equal(30, settings.VideoFrameRate);
        Assert.False(settings.CaptureCursor);
        Assert.Equal(UploadHandlingAction.Flyout, settings.ActionAfterUpload);
        Assert.False(result.SaveToLocalDisk);
        Assert.Equal(videosPath, result.SavePath);
        Assert.Equal("Video Editor", result.ShotEditorTitle);
        Assert.Equal(20, result.VideoFrameRate);
        Assert.True(result.CaptureCursor);
        Assert.Equal(UploadHandlingAction.None, result.ActionAfterUpload);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_OverrideWithInvalidEnumValue_KeepsOriginalValue()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();
        var originalValue = settings.ActionAfterUpload;

        var overrides = new Dictionary<string, dynamic>
        {
            ["upload.actionAfterUpload"] = "invalidValue"
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
        Assert.Equal(originalValue, result.ActionAfterUpload);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_OverrideNonExistentProperty_IgnoresOverride()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();

        var overrides = new Dictionary<string, dynamic>
        {
            ["nonexistent.property"] = "someValue"
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_IntOverrideWithClamp_AppliesClamp()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();

        var overrides = new Dictionary<string, dynamic>
        {
            ["video.framesPerSecond"] = 50
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
        Assert.Equal(30, result.VideoFrameRate);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_FloatOverrideWithClamp_AppliesClamp()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();

        var overrides = new Dictionary<string, dynamic>
        {
            ["capture.selection.dimmingOpacity"] = 2.0
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
        Assert.Equal(1.0f, result.AreaSelectorDimmingOpacity);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_FloatOverrideAsDouble_ConvertsCorrectly()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();

        var overrides = new Dictionary<string, dynamic>
        {
            ["capture.delayInSeconds"] = 3.5
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
        Assert.Equal(3.5, result.CaptureDelay);
    }

    [Fact]
    public void DeriveContextEffectiveSettings_PreservesUnoverriddenProperties()
    {
        using var tempFile = new TempSettingsFile();
        var manager = new SettingsManager<HSSettings>(tempFile.Path);
        var settings = new HSSettings();

        var overrides = new Dictionary<string, dynamic>
        {
            ["save.enabled"] = false
        };

        var result = manager.DeriveContextEffectiveSettings(settings, overrides);

        Assert.NotSame(settings, result);
        Assert.False(result.SaveToLocalDisk);
        Assert.Equal(settings.SavePath, result.SavePath);
        Assert.Equal(settings.SaveImageFileNamePattern, result.SaveImageFileNamePattern);
        Assert.Equal(settings.TargetImageHoster, result.TargetImageHoster);
        Assert.Equal(settings.ShowUploadProgress, result.ShowUploadProgress);
        Assert.Equal(settings.CloseAfterUpload, result.CloseAfterUpload);
        Assert.Equal(settings.VideoFrameRate, result.VideoFrameRate);
    }
}
