using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using HolzShots.Input;
using HolzShots.IO;
using Newtonsoft.Json;

namespace HolzShots
{
    public class HSSettings
    {
        #region meta

        [JsonProperty("$schema")]
        public string SchemaUrl { get; } = "https://holzshots.net/schema/settings.json";
        public string Version { get; } = "1.0.0";

        #endregion
        #region save.*

        [SettingsDoc(
            "When enabled, every screenshot captured with HolzShots will be saved at the location specified under the setting \"save.path\".",
            Default = "true"
        )]
        [JsonProperty("save.enable")]
        public bool SaveImagesToLocalDisk { get; private set; } = true;

        /// <summary> Note: Use <see cref="ExpandedSavePath" /> internally when actually saving something. </summary>
        [SettingsDoc(
            "The path where screenshots will be saved.\n" +
            "Feel free to use environment variables like %USERPROFILE%, %ONEDRIVE% or %TMP%."
        )]
        [JsonProperty("save.path")]
        public string SavePath { get; private set; } = HolzShotsPaths.DefaultScreenshotSavePath;

        /// <remarks> Use this instead of <see cref="SavePath" /> when actually saving a file. </remarks>
        [JsonIgnore]
        public string ExpandedSavePath => Environment.ExpandEnvironmentVariables(SavePath);

        /// <summary>
        /// TODO: Docs for available patterns
        /// </summary>
        [JsonProperty("save.pattern")]
        public string SaveFileNamePattern { get; private set; } = "Screenshot-<Date>";

        [SettingsDoc(
            "When enabled, HolzShots decides whether a screenshot should be saved as a JPEG or a PNG.\n" +
            "Some screenshots are better saved as JPGs, for example if they consist of a large photo.\n" +
            "Saving it as a PNG is better suited for pictures of programs.\n" +
            "If JPG is used, there may be a loss in quality. PNG does not reduce the image quality, but uses more space when photos are saved."
        )]
        [JsonProperty("save.autoDetectBestImageFormat")]
        public bool EnableSmartFormatForSaving { get; private set; } = false;

        #endregion
        #region editor.*

        [SettingsDoc(
            "Close the shot editor once the upload button is clicked.",
            Default = "false"
        )]
        [JsonProperty("editor.closeAfterUpload")]
        public bool CloseAfterUpload { get; private set; } = false;

        [SettingsDoc(
            "Close the shot editor once the image was saved.",
            Default = "false"
        )]
        [JsonProperty("editor.closeAfterSave")]
        public bool CloseAfterSave { get; private set; } = false;

        [SettingsDoc(
            "The window title of the shot editor. Feel free to override the title on your key bindings.",
            Default = "Shot Editor"
        )]
        [JsonProperty("editor.title")]
        public string ShotEditorTitle { get; private set; } = "Shot Editor";

        #endregion
        #region upload.*

        [SettingsDoc(
            "When set to true, HolzShots will show a progress flyout during upload.",
            Default = "true"
        )]
        [JsonProperty("upload.showProgress")]
        public bool ShowUploadProgress { get; private set; } = true;

        [SettingsDoc(
            "What will be done with the link that you get from your upload. Possible options are:\n" +
            "    flyout: A popup-window in the corner that shows some options for copying the link\n" +
            "    copy: Copy the link to the clipboard\n" +
            "    none: Do nothing",
            Default = "flyout"
        )]
        [JsonProperty("upload.actionAfterUpload")]
        public UploadHandlingAction ActionAfterUpload { get; private set; } = UploadHandlingAction.Flyout;

        [SettingsDoc(
            "Show a confirmation message as soon as the URL was copied and \"upload.actionAfterUpload\" is set to \"copy\".",
            Default = "true"
        )]
        [JsonProperty("upload.actionAfterUpload.copy.showConfirmation")]
        public bool ShowCopyConfirmation { get; private set; } = true;

        [SettingsDoc(
            "Automatically close the flyout containing the URL to the image as soon as some button is pressed and \"upload.actionAfterUpload\" is set to \"flyout\".",
            Default = "true"
        )]
        [JsonProperty("upload.actionAfterUpload.flyout.closeOnCopy")]
        public bool AutoCloseLinkViewer { get; private set; } = true;

        /// <summary>
        /// TODO: Maybe use a different name for that.
        /// </summary>
        [SettingsDoc(
            "When enabled, HolzShots decides whether a screenshot should be uploaded as a JPEG or a PNG.\n" +
            "Some screenshots are better uploaded as JPGs, for example if they consist of a large photo.\n" +
            "Uploading it as a PNG is better suited for pictures of programs.\n" +
            "If JPG is used, there may be a loss in quality. PNG does not reduce the image quality, but uses more space when photos are saved and therefore takes longer to upload.",
            Default = "false"
        )]
        [JsonProperty("upload.autoDetectBestImageFormat")]
        public bool EnableSmartFormatForUpload { get; private set; } = false;

        [SettingsDoc(
            "Name of the service HolzShots is goind to upload the image.",
            Default = "directupload.net"
        )]
        [JsonProperty("upload.service")]
        public string TargetImageHoster { get; private set; } = "directupload.net";

        #endregion
        #region capture.*

        /// <summary>
        /// TODO: Maybe we can replace this as well as AutoCloseLinkViewer with something command-based.
        /// To do that, we need to be able to pass objects as command parameters.
        /// For now, this should work.
        /// </summary>
        [SettingsDoc(
            "What to do after an image got captured. Possible options are:\n" +
            "    openEditor: Open the shot editor with the captured image\n" +
            "    upload: Upload the image to the specified default image service\n" +
            "    none: Do nothing (this would only trigger saving the image to disk if this is enabled)",
            Default = "openEditor"
        )]
        [JsonProperty("capture.actionAfterCapture")]
        public CaptureHandlingAction ActionAfterCapture { get; private set; } = CaptureHandlingAction.OpenEditor;

        [SettingsDoc(
            "Opacity of the dimming effect when selection a region to capture. Must be between 0.0 and 1.0.",
            Default = "0.8"
        )]
        [JsonProperty("capture.selection.dimmingOpacity")]
        public float AreaSelectorDimmingOpacity
        {
            get => _areaSelectorDimmingOpacity;
            private set => _areaSelectorDimmingOpacity = MathEx.Clamp(value, 0.0f, 1.0f);
        }
        private float _areaSelectorDimmingOpacity = 0.8f;

        #endregion
        #region tray.*

        [SettingsDoc(
            "The command to execute when the tray icon is double-clicked.",
            Default = "null"
        )]
        [JsonProperty("tray.doubleClickCommand")]
        [field: LeaveUntouchedInObjectDeepCopy] // Support for this doesn't make any sense
        public CommandDeclaration? TrayIconDoubleClickCommand { get; set; } = null;

        #endregion
        #region key.*

        [SettingsDoc(
            "Enable or disable hotkeys when a full screen application is running.",
            Default = "true"
        )]
        [JsonProperty("key.enabledDuringFullscreen")]
        public bool EnableHotkeysDuringFullscreen { get; private set; } = true;

        // TODO: Fix visibility
        [SettingsDoc(
            "List of commands that get triggered by hotkeys."
        )]
        [JsonProperty("key.bindings")]
        [field: LeaveUntouchedInObjectDeepCopy] // Support for this doesn't make any sense
        public IReadOnlyList<KeyBinding> KeyBindings { get; set; } = ImmutableList<KeyBinding>.Empty;

        #endregion
    }


    // TODO: When we're on C# 9, we may want to use a data class / record for this.
    public class KeyBinding
    {
        // TODO: Fix visibility
        public bool Enabled { get; set; } = false;
        public Hotkey Keys { get; set; } = null!;
        public CommandDeclaration Command { get; set; } = null!;
    }

    /// <summary>
    /// TODO: Some custom converter that converts a command that does not have any parameters to a plain string.
    /// </summary>
    public class CommandDeclaration
    {
        [JsonProperty("name")]
        public string CommandName { get; set; } = null!;

        /// <summary>
        /// TODO: Maybe we want to create somethign that every setting can be overwritten in the params.
        /// This would create the need for a context-sepcific settings instance that is merged from the global user settings and the params of that command.
        /// </summary>
        [JsonProperty("params")]
        public IReadOnlyDictionary<string, string> Parameters { get; set; } = ImmutableDictionary<string, string>.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("overrides")]
        public IReadOnlyDictionary<string, dynamic> Overrides { get; set; } = ImmutableDictionary<string, dynamic>.Empty;

        public static implicit operator CommandDeclaration?(string commandName) => ToCommandDeclaration(commandName);
        public static CommandDeclaration? ToCommandDeclaration(string commandName)
        {
            return commandName == null
                    ? null
                    : new CommandDeclaration() { CommandName = commandName, Parameters = ImmutableDictionary<string, string>.Empty };
        }
    }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum UploadHandlingAction
    {
        [EnumMember(Value = "flyout")]
        Flyout,
        [EnumMember(Value = "copyLink")]
        CopyToClipboard,
        [EnumMember(Value = "none")]
        None,
    }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum CaptureHandlingAction
    {
        [EnumMember(Value = "openEditor")]
        OpenEditor,
        [EnumMember(Value = "upload")]
        Upload,
        [EnumMember(Value = "saveAs")]
        SaveAs,
        [EnumMember(Value = "copyImage")]
        Copy,
        [EnumMember(Value = "none")]
        None,
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    class SettingsDocAttribute : Attribute
    {
        public string? Default { get; set; }
        public string? DisplayName { get; set; }
        public string Description { get; }
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
            if (originalObject == null)
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
            if (typeToReflect.BaseType != null)
            {
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool>? filter = null)
        {
            foreach (var fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && filter(fieldInfo) == false)
                    continue;
                if (IsPrimitive(fieldInfo.FieldType))
                    continue;

                var shouldIgnoreThisField = fieldInfo.GetCustomAttribute<LeaveUntouchedInObjectDeepCopyAttribute>();
                if (shouldIgnoreThisField != null)
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
            Debug.Assert(array != null);

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
}
