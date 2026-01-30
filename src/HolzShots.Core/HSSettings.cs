using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
using HolzShots.Input.Keyboard;
using HolzShots.IO;

namespace HolzShots;

public class HSSettings
{
    #region meta

    [SettingsDoc(
        "Schema URL to get some auto completion in some editors.",
        Overridable = false
    )]
    [JsonPropertyName("$schema")]
    public string SchemaUrl { get; } = "https://holzshots.net/schema/settings.json";

    [SettingsDoc(
        "version of the settings file.",
        Overridable = false
    )]
    public string Version { get; } = "1.0.0";

    #endregion
    #region save.*

    [SettingsDoc(
        "When enabled, every screenshot and screen recording captured with HolzShots will be saved at the location specified under the setting \"save.path\".",
        Default = "true",
        Overridable = true
    )]
    [JsonPropertyName("save.enabled")]
    public bool SaveToLocalDisk { get; private set; } = true;

    /// <summary> Note: Use <see cref="ExpandedSavePath" /> internally when actually saving something. </summary>
    [SettingsDoc(
        """
        The path where screenshots and screen recordings will be saved.
        Feel free to use environment variables like %USERPROFILE%, %ONEDRIVE% or %TMP%.

        If you want to save videos to a different path, override this setting in the respective command.
        """,
        Overridable = true
    )]
    [JsonPropertyName("save.path")]
    public string SavePath { get; private set; } = HolzShotsPaths.DefaultScreenshotSavePath;

    /// <remarks> Use this instead of <see cref="SavePath" /> when actually saving a file. </remarks>
    [JsonIgnore]
    public string ExpandedSavePath => Environment.ExpandEnvironmentVariables(SavePath);

    #endregion
    #region image.save.*

    [SettingsDoc(
        """
        The pattern to use for saving images to the local disk. You can use several placeholders:
            <date>: The date when the screenshot was created. Defaults to ISO 8601 (sortable) timestamps.
                    You can use string formats that .NET supports: https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring
                    For example, you can use <date:s>

            <size:width>, <size:height>: The width or the height of the image.
        """,
        Overridable = true
    )]
    [JsonPropertyName("image.save.pattern")]
    public string SaveImageFileNamePattern { get; private set; } = "Screenshot-<Date>";

    [SettingsDoc(
        """
        When enabled, HolzShots decides whether a screenshot should be saved as a JPEG or a PNG.
        Some screenshots are better saved as JPGs, for example if they consist of a large photo.
        Saving it as a PNG is better suited for pictures of programs.
        If JPG is used, there may be a loss in quality. PNG does not reduce the image quality, but uses more space when photos are saved.
        """,
        Overridable = true
    )]
    [JsonPropertyName("image.save.autoDetectBestImageFormat")]
    public bool EnableSmartFormatForSaving { get; private set; } = false;

    #endregion
    #region video.*

    [SettingsDoc(
        """
        The pattern to use for saving videos to the local disk. You can use several placeholders:
            <date>: The date when the video recording was started. Defaults to ISO 8601 (sortable) timestamps.
                    You can use string formats that .NET supports: https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring
                    For example, you can use <date:s>

            <size:width>, <size:height>: The width or the height of the video.
        """,
        Overridable = true
    )]
    [JsonPropertyName("video.save.pattern")]
    public string SaveVideoFileNamePattern { get; private set; } = "Recording-<Date>";

    [SettingsDoc(
        "The pixel format for videos to use. Look up FFmpeg's pix_fmt parameter for valid values. Leave unconfigured if you don't know what this means.",
        Overridable = true
    )]
    [JsonPropertyName("video.save.pixelFormat")]
    public string? VideoPixelFormat { get; private set; } = null;

    [SettingsDoc(
        "File format that recorded screen captures will be saved as. Use \"ask\" to select the format before the recording.",
        Default = "mp4",
        Overridable = true
    )]
    [JsonPropertyName("video.format")]
    public VideoCaptureFormat VideoOutputFormat { get; private set; } = VideoCaptureFormat.Mp4; // Not changing default to "ask" before GH#110

    // TODO: This is pretty buggy right now. FPS > 30 seem to result in a glitchy video.
    [SettingsDoc(
        "Frame rate (FPS) for screen recordings. Min: 1; Max: 30",
        Default = "30",
        Overridable = true
    )]
    [JsonPropertyName("video.framesPerSecond")]
    public int VideoFrameRate
    {
        get;
        private set => field = Math.Clamp(value, 1, 30);
    } = 30;

    #endregion
    #region editor.*

    [SettingsDoc(
        "Close the shot editor once the upload button is clicked.",
        Default = "false",
        Overridable = true
    )]
    [JsonPropertyName("editor.closeAfterUpload")]
    public bool CloseAfterUpload { get; private set; } = false;

    [SettingsDoc(
        "Close the shot editor once the image was saved.",
        Default = "false",
        Overridable = true
    )]
    [JsonPropertyName("editor.closeAfterSave")]
    public bool CloseAfterSave { get; private set; } = false;

    [SettingsDoc(
        "The window title of the shot editor. Feel free to override the title on your key bindings.",
        Default = "Shot Editor",
        Overridable = true
    )]
    [JsonPropertyName("editor.title")]
    public string ShotEditorTitle { get; private set; } = "Shot Editor";

    #endregion
    #region upload.*

    [SettingsDoc(
        "When set to true, HolzShots will show a progress flyout during upload.",
        Default = "true",
        Overridable = true
    )]
    [JsonPropertyName("upload.showProgress")]
    public bool ShowUploadProgress { get; private set; } = true;

    [SettingsDoc(
        """
        What will be done with the link that you get from your upload. Possible options are:
            flyout: A popup-window in the corner that shows some options for copying the link
            copy: Copy the link to the clipboard
            none: Do nothing
        """,
        Default = "flyout",
        Overridable = true
    )]
    [JsonPropertyName("upload.actionAfterUpload")]
    public UploadHandlingAction ActionAfterUpload { get; private set; } = UploadHandlingAction.Flyout;

    [SettingsDoc(
        "Show a confirmation message as soon as the URL was copied and \"upload.actionAfterUpload\" is set to \"copy\".",
        Default = "true",
        Overridable = true
    )]
    [JsonPropertyName("upload.actionAfterUpload.copy.showConfirmation")]
    public bool ShowCopyConfirmation { get; private set; } = true;

    [SettingsDoc(
        "Automatically close the flyout containing the URL to the image as soon as some button is pressed and \"upload.actionAfterUpload\" is set to \"flyout\".",
        Default = "true",
        Overridable = true
    )]
    [JsonPropertyName("upload.actionAfterUpload.flyout.closeOnCopy")]
    public bool AutoCloseLinkViewer { get; private set; } = true;

    /// <summary>
    /// TODO: Maybe use a different name for that.
    /// </summary>
    [SettingsDoc(
        """
        When enabled, HolzShots decides whether a screenshot should be uploaded as a JPEG or a PNG.
        Some screenshots are better uploaded as JPGs, for example if they consist of a large photo.
        Uploading it as a PNG is better suited for pictures of programs.
        If JPG is used, there may be a loss in quality. PNG does not reduce the image quality, but uses more space when photos are saved and therefore takes longer to upload.
        """,
        Default = "false",
        Overridable = true
    )]
    [JsonPropertyName("upload.image.autoDetectBestImageFormat")]
    public bool EnableSmartFormatForUpload { get; private set; } = false;

    [SettingsDoc(
        "Name of the service HolzShots is goind to upload the image.",
        Default = "directupload.net",
        Overridable = true
    )]
    [JsonPropertyName("upload.service")]
    public string TargetImageHoster { get; private set; } = "directupload.net";

    #endregion
    #region capture.*

    /// <summary>
    /// TODO: Maybe we can replace this as well as AutoCloseLinkViewer with something command-based.
    /// To do that, we need to be able to pass objects as command parameters.
    /// For now, this should work.
    /// </summary>
    [SettingsDoc(
        """
        What to do after an image got captured. Possible options are:
            openEditor: Open the shot editor with the captured image
            upload: Upload the image to the specified default image service
            saveAs: Show a dialog and choose where you want to save the image
            copy: Copy the image data to clipboard; useful for pasting the image to popular messengers etc.
            none: Do nothing (this would only trigger saving the image to disk if this is enabled)
        """,
        Default = "openEditor",
        Overridable = true
    )]
    [JsonPropertyName("capture.image.actionAfterCapture")]
    public ImageCaptureHandlingAction ActionAfterImageCapture { get; private set; } = ImageCaptureHandlingAction.OpenEditor;

    [SettingsDoc(
        """
        What to do after capturing a screen recording. Possible options are:
            upload: Upload the image to the specified default service
            copyFile: Copy the file to the clipboard; useful for pasting the video to popular messengers etc.
            copyFilePath: Copy the path to file to the clipboard.
            showInExplorer: Opens an explorer window in the path of the saved video.
            openInDefaultApp: Opens the video in the default application for that file type.
            none: Do nothing (this would only trigger saving the video to disk if this is enabled)
        """,
        Default = "showInExplorer",
        Overridable = true
    )]
    [JsonPropertyName("capture.video.actionAfterCapture")]
    public VideoCaptureHandlingAction ActionAfterVideoCapture { get; private set; } = VideoCaptureHandlingAction.ShowInExplorer;

    [SettingsDoc(
        "Opacity of the dimming effect when selection a region to capture. Must be between 0.0 and 1.0.",
        Default = "0.8",
        Overridable = true
    )]
    [JsonPropertyName("capture.selection.dimmingOpacity")]
    public float AreaSelectorDimmingOpacity
    {
        get;
        private set => field = Math.Clamp(value, 0.0f, 1.0f);
    } = 0.8f;

    [SettingsDoc(
        "Seconds to wait before invoking the capture. Must be >=0.",
        Default = "0.0",
        Overridable = true
    )]
    [JsonPropertyName("capture.delayInSeconds")]
    public float CaptureDelay
    {
        get;
        private set => field = MathF.Max(0.0f, value);
    } = 0.0f;

    [SettingsDoc(
        """
        Capture the mouse cursor.

        When recording videos and you want to have a cursor visible, make sure you set this option to "true" in the command overrides.
        """,
        Default = "false",
        Overridable = true
    )]
    [JsonPropertyName("capture.cursor")]
    public bool CaptureCursor { get; private set; } = false;

    #endregion
    #region tray.*

    [SettingsDoc(
        "The command to execute when the tray icon is double-clicked.",
        Default = "null",
        Overridable = false
    )]
    [JsonPropertyName("tray.doubleClickCommand")]
    [field: LeaveUntouchedInObjectDeepCopy] // Support for this doesn't make any sense
    public CommandDeclaration? TrayIconDoubleClickCommand { get; set; } = null;

    #endregion
    #region key.*

    [SettingsDoc(
        "Enable or disable hotkeys when a full screen application is running.",
        Default = "true",
        Overridable = true // TODO: Check if we check this on hotkey invocation with the appropriate settings context
    )]
    [JsonPropertyName("key.enabledDuringFullscreen")]
    public bool EnableHotkeysDuringFullscreen { get; private set; } = true;

    // TODO: Fix visibility
    [SettingsDoc(
        "List of commands that get triggered by hotkeys.",
        Overridable = false
    )]
    [JsonPropertyName("key.bindings")]
    [field: LeaveUntouchedInObjectDeepCopy] // Support for this doesn't make any sense
    public IReadOnlyList<KeyBinding> KeyBindings { get; set; } = [];

    #endregion
}


public record KeyBinding(Hotkey Keys, CommandDeclaration Command, bool Enabled = false);

/// <summary>
/// TODO: Some custom converter that converts a command that does not have any parameters to a plain string.
/// </summary>
public class CommandDeclaration
{
    [JsonPropertyName("name")]
    public string CommandName { get; set; } = null!;

    /// <summary>
    /// TODO: Maybe we want to create something that every setting can be overwritten in the parameters.
    /// This would create the need for a context-specific settings instance that is merged from the global user settings and the parameters of that command.
    /// </summary>
    [JsonPropertyName("params")]
    public IReadOnlyDictionary<string, string> Parameters { get; set; } = ImmutableDictionary<string, string>.Empty;

    /// <summary>
    ///
    /// </summary>
    [JsonPropertyName("overrides")]
    public IReadOnlyDictionary<string, object> Overrides { get; set; } = ImmutableDictionary<string, object>.Empty;

    public static implicit operator CommandDeclaration?(string commandName) => ToCommandDeclaration(commandName);
    public static CommandDeclaration? ToCommandDeclaration(string commandName)
    {
        return commandName == null
                ? null
                : new CommandDeclaration() { CommandName = commandName, Parameters = ImmutableDictionary<string, string>.Empty };
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UploadHandlingAction
{
    [JsonStringEnumMemberName("flyout")]
    Flyout,
    [JsonStringEnumMemberName("copyLink")]
    CopyToClipboard,
    [JsonStringEnumMemberName("none")]
    None,
}

[JsonConverter(typeof(JsonStringEnumConverter<ImageCaptureHandlingAction>))]
public enum ImageCaptureHandlingAction
{
    [JsonStringEnumMemberName("openEditor")]
    OpenEditor,
    [JsonStringEnumMemberName("upload")]
    Upload,
    [JsonStringEnumMemberName("saveAs")]
    SaveAs,
    [JsonStringEnumMemberName("copyImage")]
    Copy,
    [JsonStringEnumMemberName("none")]
    None,
}

[JsonConverter(typeof(JsonStringEnumConverter<VideoCaptureHandlingAction>))]
public enum VideoCaptureHandlingAction
{
    [JsonStringEnumMemberName("upload")]
    Upload,
    [JsonStringEnumMemberName("copyFile")]
    CopyFile,
    [JsonStringEnumMemberName("copyFilePath")]
    CopyFilePath,
    [JsonStringEnumMemberName("showInExplorer")]
    ShowInExplorer,
    [JsonStringEnumMemberName("openInDefaultApp")]
    OpenInDefaultApp,
    [JsonStringEnumMemberName("none")]
    None,
}

/// <remarks> When adding new entries to this enum, consider adding it in the <see cref="HolzShots.Input.VideoCaptureFormatSelection.PromptFormat" /> of <see cref="HolzShots.Input.VideoCaptureFormatSelection" />. </remarks>
[JsonConverter(typeof(JsonStringEnumConverter<VideoCaptureFormat>))]
public enum VideoCaptureFormat
{
    [JsonStringEnumMemberName("mp4")]
    Mp4,
    // See GH-110
    // [JsonStringEnumMemberName("webm")]
    // Webm,
    // [JsonStringEnumMemberName("gif")]
    // Gif,
    [JsonStringEnumMemberName("ask")]
    AskBeforeRecording,
}

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
class SettingsDocAttribute : Attribute
{
    public string Description { get; }
    public bool Overridable { get; init; } = false;
    public string? Default { get; set; }
    public string? DisplayName { get; set; }
    public SettingsDocAttribute(string description) => Description = description ?? throw new ArgumentNullException(nameof(description));
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class LeaveUntouchedInObjectDeepCopyAttribute : Attribute { }

/// <summary> Based on this solution: https://stackoverflow.com/a/11308879 </summary>
public static class ObjectExtensions
{
    private static readonly MethodInfo CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance)!;

    public static bool IsPrimitive(this Type type) => (type == typeof(string)) || (type.IsValueType && type.IsPrimitive);

    public static object? Copy(this object originalObject) => InternalCopy(originalObject, new Dictionary<object, object>(new ReferenceEqualityComparer()));

    private static object? InternalCopy(object? originalObject, IDictionary<object, object> visited)
    {
        if (originalObject is null)
            return null;

        var typeToReflect = originalObject.GetType();
        if (IsPrimitive(typeToReflect))
            return originalObject;

        if (visited.ContainsKey(originalObject))
            return visited[originalObject];

        if (typeof(Delegate).IsAssignableFrom(typeToReflect))
            return null;

        var cloneObject = CloneMethod.Invoke(originalObject, null)!;
        if (typeToReflect.IsArray)
        {
            var arrayType = typeToReflect.GetElementType();
            if (arrayType is not null && IsPrimitive(arrayType) == false)
            {
                var clonedArray = cloneObject as Array;
                if (clonedArray is not null)
                    ArrayExtensions.ForEach(clonedArray, (array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
            }

        }
        visited.Add(originalObject, cloneObject);
        CopyFields(originalObject, visited, cloneObject, typeToReflect);
        RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
        return cloneObject;
    }

    private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
    {
        if (typeToReflect.BaseType is not null)
        {
            RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
            CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
        }
    }

    private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool>? filter = null)
    {
        foreach (var fieldInfo in typeToReflect.GetFields(bindingFlags))
        {
            if (filter is not null && filter(fieldInfo) == false)
                continue;
            if (IsPrimitive(fieldInfo.FieldType))
                continue;

            var shouldIgnoreThisField = fieldInfo.GetCustomAttribute<LeaveUntouchedInObjectDeepCopyAttribute>();
            if (shouldIgnoreThisField is not null)
                continue;

            var originalFieldValue = fieldInfo.GetValue(originalObject);
            var clonedFieldValue = InternalCopy(originalFieldValue, visited);
            fieldInfo.SetValue(cloneObject, clonedFieldValue);
        }
    }
    public static T? Copy<T>(this T? original) =>
        original is null
            ? default
            : (T)Copy((object)original)!;
}

public class ReferenceEqualityComparer : EqualityComparer<object>
{
    public override bool Equals(object? x, object? y) => ReferenceEquals(x, y);
    public override int GetHashCode(object o) => o == null ? 0 : o.GetHashCode();
}

public static class ArrayExtensions
{
    public static void ForEach(Array array, Action<Array, int[]> action)
    {
        Debug.Assert(array is not null);

        if (array.LongLength == 0)
            return;

        var walker = new ArrayTraverse(array);
        do
        {
            action(array, walker.Position);
        } while (walker.Step());
    }

    private class ArrayTraverse
    {
        public int[] Position { get; }
        private readonly int[] _maxLengths;

        public ArrayTraverse(Array array)
        {
            _maxLengths = new int[array.Rank];
            for (var i = 0; i < array.Rank; ++i)
                _maxLengths[i] = array.GetLength(i) - 1;

            Position = new int[array.Rank];
        }

        public bool Step()
        {
            for (var i = 0; i < Position.Length; ++i)
            {
                if (Position[i] < _maxLengths[i])
                {
                    Position[i]++;
                    for (var j = 0; j < i; j++)
                        Position[j] = 0;

                    return true;
                }
            }
            return false;
        }
    }
}
