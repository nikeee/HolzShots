using System;
using HolzShots;
using HolzShots.IO;
using HolzShots.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Immutable;

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

        [JsonProperty("save.enable")]
        public bool SaveImagesToLocalDisk { get; private set; } = true;
        [JsonProperty("save.path")]
        public string SavePath { get; private set; } = HolzShotsPaths.DefaultScreenshotSavePath;
        /// <summary>
        /// TODO: Docs for available patterns
        /// </summary>
        [JsonProperty("save.pattern")]
        public string SaveFileNamePattern { get; private set; } = "Screenshot-<Date>";
        /// <summary>
        /// When enabled, HolzShots decides whether a screenshot should be saved as a JPEG or a PNG.
        /// Some screenshots are better saved as JPGs, for example if they consist of a large photo.
        /// Saving it as a PNG is better suited for pictures of programs.
        /// If JPG is used, there may be a loss in quality. PNG does not reduce the image quality, but uses more space when photos are saved.
        /// </summary>
        [JsonProperty("save.autoDetectBestImageFormat")]
        public bool EnableSmartFormatForSaving { get; private set; } = false;

        #endregion
        #region editor.*

        [JsonProperty("editor.closeAfterUpload")]
        public bool CloseAfterUpload { get; private set; } = false;
        /// <summary> TODO: Use this property </summary>
        [JsonProperty("editor.closeAfterSave")]
        public bool CloseAfterSave { get; private set; } = false;

        #endregion

        /// <summary> Copy the URL of the uploaded image to clipboard instead of showing a flyout with other options. </summary>
        public bool CopyUploadedLinkToClipboard { get; private set; } = true;
        /// <summary>
        /// Automatically close the flyout containing the URL to the image as soon as some button is pressed.
        /// Needs <see cref="CopyUploadedLinkToClipboard"/> to be set to true. Will do nothing otherwise.
        /// </summary>
        public bool AutoCloseLinkViewer { get; private set; } = true;
        /// <summary>
        /// When set to true, HolzShots will show a progress flyout during upload.
        /// Default: true.
        /// </summary>
        public bool ShowUploadProgress { get; private set; } = true;
        /// <summary> When TODO is set to true, show a flyout as soon as the URL is copied to the clipboard. </summary>
        public bool ShowCopyConfirmation { get; private set; } = true;

        /// <summary>
        /// If disabled, it does not show the Shot Editor but uploads it instead.
        /// TODO: We may just add a parameter to the key bindings to be able to configure this on a key-binding basis.
        /// 
        /// TODO: Find better name.
        /// </summary>
        public bool EnableShotEditor { get; private set; } = true;

        /// <summary> Enable or disable hotkeys whan a full screen application is running. </summary>
        [JsonProperty("key.enabledDuringFullscreen")]
        public bool EnableHotkeysDuringFullscreen { get; private set; } = false;

        /// <summary> Opacity of the dimming effect when selection a region to capture. Must be between 0.0 and 1.0. Default: 80% </summary>
        [JsonProperty("capture.selection.dimmingOpacity")]
        public float AreaSelectorDimmingOpacity
        {
            get => _areaSelectorDimmingOpacity;
            private set => _areaSelectorDimmingOpacity = MathEx.Clamp(value, 0.0f, 1.0f);
        }
        private float _areaSelectorDimmingOpacity = 0.8f;

        /// <summary>
        /// When enabled, HolzShots decides whether a screenshot should be uploaded as a JPEG or a PNG.
        /// Some screenshots are better uploaded as JPGs, for example if they consist of a large photo.
        /// Uploading it as a PNG is better suited for pictures of programs.
        /// If JPG is used, there may be a loss in quality. PNG does not reduce the image quality, but uses more space when photos are saved and tehrefore takes longer to upload.
        /// 
        /// TODO: Maybe use a different name for that.
        /// </summary>
        [JsonProperty("upload.autoDetectBestImageFormat")]
        public bool EnableSmartFormatForUpload { get; private set; } = false;

        [JsonProperty("tray.doubleClickCommand")]
        public string TrayIconDoubleClickCommand { get; set; } = null;

        // TODO: Fix visibility
        [JsonProperty("key.bindings")]
        public IReadOnlyList<KeyBinding> KeyBindings { get; set; } = ImmutableList<KeyBinding>.Empty;
    }

    // TODO: When we're on C# 9, we may want to use a data class / record for this.
    public class KeyBinding
    {
        // TODO: Fix visibility
        public bool Enabled { get; set; } = false;
        public string Command { get; set; } = null;
        public Hotkey Keys { get; set; } = null;
    }
}
