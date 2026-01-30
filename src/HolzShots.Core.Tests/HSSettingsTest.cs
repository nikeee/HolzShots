using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace HolzShots.Tests;

public class HSSettingsTest
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        IncludeFields = true,
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true,
    };

    [Fact]
    public void SerializeDeserialize_DefaultSettings_PreservesValues()
    {
        var settings = new HSSettings();

        var json = JsonSerializer.Serialize(settings, JsonOptions);
        var deserialized = JsonSerializer.Deserialize<HSSettings>(json, JsonOptions);

        Assert.NotNull(deserialized);
        Assert.Equal(settings.SaveToLocalDisk, deserialized.SaveToLocalDisk);
        Assert.Equal(settings.SavePath, deserialized.SavePath);
        Assert.Equal(settings.SaveImageFileNamePattern, deserialized.SaveImageFileNamePattern);
        Assert.Equal(settings.SaveVideoFileNamePattern, deserialized.SaveVideoFileNamePattern);
        Assert.Equal(settings.EnableSmartFormatForSaving, deserialized.EnableSmartFormatForSaving);
        Assert.Equal(settings.EnableSmartFormatForUpload, deserialized.EnableSmartFormatForUpload);
        Assert.Equal(settings.TargetImageHoster, deserialized.TargetImageHoster);
        Assert.Equal(settings.ShowUploadProgress, deserialized.ShowUploadProgress);
        Assert.Equal(settings.CloseAfterUpload, deserialized.CloseAfterUpload);
        Assert.Equal(settings.CloseAfterSave, deserialized.CloseAfterSave);
        Assert.Equal(settings.ShotEditorTitle, deserialized.ShotEditorTitle);
        Assert.Equal(settings.ActionAfterUpload, deserialized.ActionAfterUpload);
        Assert.Equal(settings.ActionAfterImageCapture, deserialized.ActionAfterImageCapture);
        Assert.Equal(settings.ActionAfterVideoCapture, deserialized.ActionAfterVideoCapture);
        Assert.Equal(settings.VideoOutputFormat, deserialized.VideoOutputFormat);
        Assert.Equal(settings.VideoFrameRate, deserialized.VideoFrameRate);
        Assert.Equal(settings.VideoPixelFormat, deserialized.VideoPixelFormat);
        Assert.Equal(settings.AreaSelectorDimmingOpacity, deserialized.AreaSelectorDimmingOpacity);
        Assert.Equal(settings.CaptureDelay, deserialized.CaptureDelay);
        Assert.Equal(settings.CaptureCursor, deserialized.CaptureCursor);
        Assert.Equal(settings.EnableHotkeysDuringFullscreen, deserialized.EnableHotkeysDuringFullscreen);
    }

    [Fact]
    public void Deserialize_JsonWithCustomValues_ReturnsCorrectSettings()
    {
        var json = """
        {
            "save.enabled": false,
            "save.path": "C:\\Screenshots",
            "image.save.pattern": "Image-<date>",
            "image.save.autoDetectBestImageFormat": true,
            "video.save.pattern": "Video-<date>",
            "video.save.pixelFormat": "yuv420p",
            "video.format": "mp4",
            "video.framesPerSecond": 25,
            "editor.closeAfterUpload": true,
            "editor.closeAfterSave": true,
            "editor.title": "Custom Editor",
            "upload.showProgress": false,
            "upload.actionAfterUpload": "copyLink",
            "upload.actionAfterUpload.copy.showConfirmation": false,
            "upload.actionAfterUpload.flyout.closeOnCopy": false,
            "upload.image.autoDetectBestImageFormat": true,
            "upload.service": "imgur",
            "capture.image.actionAfterCapture": "upload",
            "capture.video.actionAfterCapture": "openInDefaultApp",
            "capture.selection.dimmingOpacity": 0.5,
            "capture.delayInSeconds": 2.5,
            "capture.cursor": true,
            "key.enabledDuringFullscreen": false
        }
        """;

        var settings = JsonSerializer.Deserialize<HSSettings>(json, JsonOptions);

        Assert.NotNull(settings);
        Assert.False(settings.SaveToLocalDisk);
        Assert.Equal("C:\\Screenshots", settings.SavePath);
        Assert.Equal("Image-<date>", settings.SaveImageFileNamePattern);
        Assert.True(settings.EnableSmartFormatForSaving);
        Assert.Equal("Video-<date>", settings.SaveVideoFileNamePattern);
        Assert.Equal("yuv420p", settings.VideoPixelFormat);
        Assert.Equal(VideoCaptureFormat.Mp4, settings.VideoOutputFormat);
        Assert.Equal(25, settings.VideoFrameRate);
        Assert.True(settings.CloseAfterUpload);
        Assert.True(settings.CloseAfterSave);
        Assert.Equal("Custom Editor", settings.ShotEditorTitle);
        Assert.False(settings.ShowUploadProgress);
        Assert.Equal(UploadHandlingAction.CopyToClipboard, settings.ActionAfterUpload);
        Assert.False(settings.ShowCopyConfirmation);
        Assert.False(settings.AutoCloseLinkViewer);
        Assert.True(settings.EnableSmartFormatForUpload);
        Assert.Equal("imgur", settings.TargetImageHoster);
        Assert.Equal(ImageCaptureHandlingAction.Upload, settings.ActionAfterImageCapture);
        Assert.Equal(VideoCaptureHandlingAction.OpenInDefaultApp, settings.ActionAfterVideoCapture);
        Assert.Equal(0.5f, settings.AreaSelectorDimmingOpacity);
        Assert.Equal(2.5f, settings.CaptureDelay);
        Assert.True(settings.CaptureCursor);
        Assert.False(settings.EnableHotkeysDuringFullscreen);
    }

    [Fact]
    public void Deserialize_JsonWithEnumStrings_ParsesEnumsCorrectly()
    {
        var json = """
        {
            "upload.actionAfterUpload": "flyout",
            "capture.image.actionAfterCapture": "openEditor",
            "capture.video.actionAfterCapture": "copyFile",
            "video.format": "ask"
        }
        """;

        var settings = JsonSerializer.Deserialize<HSSettings>(json, JsonOptions);

        Assert.NotNull(settings);
        Assert.Equal(UploadHandlingAction.Flyout, settings.ActionAfterUpload);
        Assert.Equal(ImageCaptureHandlingAction.OpenEditor, settings.ActionAfterImageCapture);
        Assert.Equal(VideoCaptureHandlingAction.CopyFile, settings.ActionAfterVideoCapture);
        Assert.Equal(VideoCaptureFormat.AskBeforeRecording, settings.VideoOutputFormat);
    }

    [Fact]
    public void Serialize_UsesCorrectJsonPropertyNames()
    {
        var settings = new HSSettings();

        var json = JsonSerializer.Serialize(settings, JsonOptions);

        Assert.Contains("\"$schema\":", json);
        Assert.Contains("\"save.enabled\":", json);
        Assert.Contains("\"save.path\":", json);
        Assert.Contains("\"image.save.pattern\":", json);
        Assert.Contains("\"video.save.pattern\":", json);
        Assert.Contains("\"editor.closeAfterUpload\":", json);
        Assert.Contains("\"upload.showProgress\":", json);
        Assert.Contains("\"capture.cursor\":", json);
    }

    [Theory]
    [InlineData(35, 30)]
    [InlineData(0, 1)]
    [InlineData(-5, 1)]
    [InlineData(15, 15)]
    public void VideoFrameRate_ClampsToValidRange(int input, int expected)
    {
        var json = $"{{\"video.framesPerSecond\": {input}}}";

        var settings = JsonSerializer.Deserialize<HSSettings>(json, JsonOptions);

        Assert.NotNull(settings);
        Assert.Equal(expected, settings.VideoFrameRate);
    }

    [Theory]
    [InlineData(1.5f, 1.0f)]
    [InlineData(-0.5f, 0.0f)]
    [InlineData(0.5f, 0.5f)]
    public void AreaSelectorDimmingOpacity_ClampsToValidRange(float input, float expected)
    {
        var json = $"{{\"capture.selection.dimmingOpacity\": {input.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}";

        var settings = JsonSerializer.Deserialize<HSSettings>(json, JsonOptions);

        Assert.NotNull(settings);
        Assert.Equal(expected, settings.AreaSelectorDimmingOpacity);
    }

    [Theory]
    [InlineData(-5.0f, 0.0f)]
    [InlineData(0.0f, 0.0f)]
    [InlineData(5.0f, 5.0f)]
    public void CaptureDelay_ClampsToValidRange(float input, float expected)
    {
        var json = $"{{\"capture.delayInSeconds\": {input.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}";

        var settings = JsonSerializer.Deserialize<HSSettings>(json, JsonOptions);

        Assert.NotNull(settings);
        Assert.Equal(expected, settings.CaptureDelay);
    }
}
