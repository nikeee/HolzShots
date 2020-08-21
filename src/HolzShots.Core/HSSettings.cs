using System;
using HolzShots;
using HolzShots.IO;
using HolzShots.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.Serialization;

namespace HolzShots
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings")]
    public class HSSettings
    {
        #region meta

        [JsonProperty("$schema")]
        public string SchemaUrl { get; } = "";
        public string Version { get; } = "0.1.0";

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
            "The path where screenshots will be saved." +
            "Feed free to use environment variables like %USERPROFILE%, %ONEDRIVE% or %TMP%."
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

        [JsonProperty("editor.closeAfterUpload")]
        public bool CloseAfterUpload { get; private set; } = false;

        [JsonProperty("editor.closeAfterSave")]
        public bool CloseAfterSave { get; private set; } = false;

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
            "    flyout: A popup-window in the corner that shows some optiosn for copying the link\n" +
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

        [SettingsDoc(
            "Automatically close the flyout containing the URL to the image as soon as some button is pressed.\n" +
            "Needs <see cref=\"CopyUploadedLinkToClipboard\"/> to be set to true. Will do nothing otherwise.",
            // TODO: Set property ref
            Default = "true"
        )]
        public bool AutoCloseLinkViewer { get; private set; } = true;

        #region key.*

        [SettingsDoc(
            "Enable or disable hotkeys whan a full screen application is running.",
            Default = "false"
        )]
        [JsonProperty("key.enabledDuringFullscreen")]
        public bool EnableHotkeysDuringFullscreen { get; private set; } = false;

        #endregion

        /// <summary>
        /// TODO: Maybe use a different name for that.
        /// </summary>
        [SettingsDoc(
            "When enabled, HolzShots decides whether a screenshot should be uploaded as a JPEG or a PNG.\n" +
            "Some screenshots are better uploaded as JPGs, for example if they consist of a large photo.\n" +
            "Uploading it as a PNG is better suited for pictures of programs.\n" +
            "If JPG is used, there may be a loss in quality. PNG does not reduce the image quality, but uses more space when photos are saved and tehrefore takes longer to upload.",
            Default = "false"
        )]
        [JsonProperty("upload.autoDetectBestImageFormat")]
        public bool EnableSmartFormatForUpload { get; private set; } = false;

        [SettingsDoc(
            "The command to execute when the tray icon is double-clicked.",
            Default = "null"
        )]
        [JsonProperty("tray.doubleClickCommand")]
        public CommandDeclaration TrayIconDoubleClickCommand { get; set; } = null;

        // TODO: Fix visibility
        [JsonProperty("key.bindings")]
        public IReadOnlyList<KeyBinding> KeyBindings { get; set; } = ImmutableList<KeyBinding>.Empty;
    }


    // TODO: When we're on C# 9, we may want to use a data class / record for this.
    public class KeyBinding
    {
        // TODO: Fix visibility
        public bool Enabled { get; set; } = false;
        public CommandDeclaration Command { get; set; } = null;
        public Hotkey Keys { get; set; } = null;
    }

    /// <summary>
    /// TODO: Some custom converter that converts a command that does not have any parameters to a plain string.
    /// </summary>
    public class CommandDeclaration
    {
        [JsonProperty("name")]
        public string CommandName { get; set; }

        /// <summary>
        /// TODO: Maybe we want to create somethign that every setting can be overwritten in the params.
        /// This would create the need for a context-sepcific settings instance that is merged from the global user settings and the params of that command.
        /// </summary>
        [JsonProperty("params")]
        public IReadOnlyDictionary<string, string> Parameters { get; set; } = ImmutableDictionary<string, string>.Empty;

        public static implicit operator CommandDeclaration(string commandName) => ToCommandDeclaration(commandName);
        public static CommandDeclaration ToCommandDeclaration(string commandName)
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
        [EnumMember(Value = "copy")]
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
        [EnumMember(Value = "none")]
        None,
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    class SettingsDocAttribute : Attribute
    {
        public string Default { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; }
        public SettingsDocAttribute(string description) => Description = description ?? throw new ArgumentNullException(nameof(description));
    }
}
