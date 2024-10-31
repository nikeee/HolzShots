using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using HolzShots.Composition;
using HolzShots.Drawing;
using HolzShots.Drawing.Tools;
using HolzShots.Net;
using HolzShots.Windows.Forms;
using HolzShots.Windows.Net;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace HolzShots.UI
{
    public partial class ShotEditor : Form
    {
        private readonly UploaderEntry _defaultUploader;
        private readonly HSSettings _settingsContext;
        private readonly Screenshot _screenshot;
        private readonly UploaderManager _uploaders;

        private ThumbnailToolBarButton _uploadThumbnailButton;
        private ThumbnailToolBarButton _saveThumbnailButton;
        private ThumbnailToolBarButton _copyThumbnailButton;

        private readonly PanelActivator _activator;
        private readonly Dictionary<ShotEditorTool, ToolStripButton> _toolControlMap;

        public ShotEditor(Screenshot screenshot, UploaderManager uploaders, HSSettings settingsContext)
        {
            _screenshot = screenshot ?? throw new ArgumentNullException(nameof(screenshot));
            _uploaders = uploaders ?? throw new ArgumentNullException(nameof(uploaders));
            _settingsContext = settingsContext ?? throw new ArgumentNullException(nameof(settingsContext));
            Debug.Assert(screenshot.Image is not null);

            InitializeComponent();

            components = new System.ComponentModel.Container();

            AutoCloseShotEditor.Checked = settingsContext.CloseAfterUpload;
            AutoCloseShotEditor.Enabled = false; // We only support reading that setting for now

            if (settingsContext.ShotEditorTitle != null)
                Text = settingsContext.ShotEditorTitle;

            _defaultUploader = UploadDispatcher.GetUploadServiceForSettingsContext(settingsContext, _uploaders);

            InitializeThumbnailToolbar();

            SuspendLayout();

            if (!settingsContext.EnableHotkeysDuringFullscreen && EnvironmentEx.IsFullscreenAppRunning())
                WindowState = FormWindowState.Minimized;
            else if (screenshot.Size == SystemInformation.VirtualScreen.Size)
                WindowState = FormWindowState.Maximized;
            else if (screenshot.Image.ShouldMaximizeEditorWindowForImage())
                WindowState = FormWindowState.Maximized;
            else
            {
                Width = screenshot.Image.Width;
                Height = screenshot.Image.Height + ThePanel.Location.Y + 140;
                WindowState = FormWindowState.Normal;
            }

            _toolControlMap = new()
            {
                [ShotEditorTool.Eyedropper] = PipettenTool,
                [ShotEditorTool.Ellipse] = EllipseTool,
                [ShotEditorTool.Eraser] = EraserTool,
                [ShotEditorTool.Crop] = CroppingTool,
                [ShotEditorTool.Arrow] = ArrowTool,
                [ShotEditorTool.Censor] = CensorTool,
                [ShotEditorTool.Marker] = MarkerTool,
                [ShotEditorTool.Blur] = BlurTool,
                [ShotEditorTool.Brighten] = BrightenTool,
            };
            _activator = new PanelActivator(CurrentToolSettingsPanel);

            CurrentToolSettingsPanel.BackColor = Color.Transparent;

            ShareStrip.BackColor = Color.Transparent;
            EditStrip.BackColor = Color.Transparent;
            WeirdToolsStrip.BackColor = Color.Transparent;
            CopyPrintToolStrip.BackColor = Color.Transparent;

            DrawCursor.Visible = screenshot.Source != ScreenshotSource.Selected && screenshot.Source != ScreenshotSource.Unknown;

            UploadToHoster.Enabled = _defaultUploader?.Metadata != null;
            UploadToHoster.ToolTipText = _defaultUploader?.Metadata != null ? string.Format(UIConfig.Culture, UploadToHoster.ToolTipText, _defaultUploader?.Metadata.Name) : string.Empty;

            var renderer = EnvironmentEx.ToolStripRendererForCurrentTheme;
            ShareStrip.Renderer = renderer;
            WeirdToolsStrip.Renderer = renderer;
            EditStrip.Renderer = renderer;
            CopyPrintToolStrip.Renderer = renderer;

            ResumeLayout(true);

            ThePanel.Initialized += (_, _) =>
            {
                ImageInfoLabel.Text = ThePanel.SizeInfo;
                ImageInfoLabel.ToolTipText = ThePanel.SizeInfoText;
                MouseInfoLabel.Text = "0, 0px";
            };
            ThePanel.UpdateMousePosition += (_, e) =>
            {
                MouseInfoLabel.Text = $"{e.X}, {e.Y}px";
            };
        }

        private void InitializeThumbnailToolbar()
        {
            if (!TaskbarManager.IsPlatformSupported)
                return;

            string uploadTooltip = string.Empty;
            if (_defaultUploader?.Metadata != null)
            {
                uploadTooltip = UploadToHoster.ToolTipText.Remove(UploadToHoster.ToolTipText.IndexOf(" (", StringComparison.Ordinal));
                uploadTooltip = string.Format(UIConfig.Culture, uploadTooltip, _defaultUploader.Metadata.Name);
            }

            _uploadThumbnailButton = new ThumbnailToolBarButton(Icon.FromHandle(Properties.Resources.uploadMedium.GetHicon()), uploadTooltip);
            _uploadThumbnailButton.Click += (_, _) => UploadCurrentImageToDefaultProvider();
            _uploadThumbnailButton.Enabled = _defaultUploader?.Metadata != null;

            _saveThumbnailButton = new ThumbnailToolBarButton(Icon.FromHandle(Properties.Resources.saveMedium.GetHicon()), "Save image");
            _copyThumbnailButton = new ThumbnailToolBarButton(Icon.FromHandle(Properties.Resources.clipboardMedium.GetHicon()), "Copy image");

            _saveThumbnailButton.Click += SaveImage;
            _copyThumbnailButton.Click += CopyImage;

            components.Add(_uploadThumbnailButton);
            components.Add(_saveThumbnailButton);
            components.Add(_copyThumbnailButton);


            TaskbarManager.Instance.ThumbnailToolBars.AddButtons(Handle, _uploadThumbnailButton, _saveThumbnailButton, _copyThumbnailButton);
        }


        private void ShotEditorLoad(object sender, EventArgs e)
        {
            ThePanel.Initialize(_screenshot);
            EnlistUploaderPlugins();
        }
        private void ShotEditorClosed(object sender, FormClosedEventArgs e) => Properties.Settings.Default.Save();

        private void ShotEditorResize(object sender, EventArgs e)
        {
            ThePanel.VerticalLinealBox.Invalidate();
            ThePanel.HorizontalLinealBox.Invalidate();
        }


        private void EnlistUploaderPlugins()
        {
            UploadToHoster.DropDown.ImageScalingSize = new Size(16, 16);
            UploadToHoster.DropDown.AutoSize = true;
            if (_uploaders.Loaded)
            {
                UploadToHoster.DropDown.Renderer = HolzShots.Windows.Forms.EnvironmentEx.ToolStripRendererForCurrentTheme;
                var pls = _uploaders.GetUploaderNames();
                foreach (var uploaderName in pls)
                {
                    var item = UploadToHoster.DropDown.Items.Add(string.Format(Localization.UploadTo, uploaderName));
                    item.Tag = uploaderName;
                    item.ImageScaling = ToolStripItemImageScaling.None;
                }
            }
        }

        private void ImageInfoLabel_MouseUp(object? sender, MouseEventArgs e)
        {
            var s = ThePanel.Screenshot;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    ClipboardEx.SetText($"{s.Size.Width}x{s.Size.Height}px");
                    break;
                case MouseButtons.Right:
                    ClipboardEx.SetText(s.Timestamp.ToString());
                    break;
                case MouseButtons.Middle:
                    ClipboardEx.SetText($"{s.Timestamp} {s.Size.Width}x{s.Size.Height}px");
                    break;
                default:
                    return;
            }
        }

        private void EnableTool(ShotEditorTool tool)
        {
            var previousTool = ThePanel.CurrentTool;

            if (previousTool == tool)
                return;

            var previousToolObject = ThePanel.CurrentToolObject;
            previousToolObject?.PersistSettings(); // may depend on controls, so we cannot clear the settings panel before

            _activator.ClearSettingsPanel();

            ThePanel.CurrentTool = tool;

            var cto = ThePanel.CurrentToolObject;

            cto.LoadInitialSettings();

            if (cto?.SettingsControl != null)
                _activator.CreateSettingsPanel(cto);

            var toolToEnable = _toolControlMap.GetValueOrDefault(tool);
            foreach (var button in _toolControlMap.Values)
            {
                if (button != null)
                    button.Checked = toolToEnable != null && button == toolToEnable;
            }
        }


        private void CopyImage(object? sender, EventArgs e)
        {
            var bmp = ThePanel.CombinedImage;
            try
            {
                Clipboard.SetImage(bmp);
            }
            catch (Exception ex) when (ex is ExternalException
                   || ex is ThreadStateException
                   || ex is ArgumentNullException)
            {
                NotificationManager.CopyImageFailed(ex);
            }
        }

        private void SaveImage(object? sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog();
            sfd.Filter = $"{Localization.PngImage}|*.png|{Localization.JpgImage}|*.jpg";
            sfd.DefaultExt = ".png";
            sfd.CheckPathExists = true;
            sfd.Title = Localization.ChooseDestinationFileName;
            var res = sfd.ShowDialog();
            if (res == DialogResult.OK)
            {
                var f = sfd.FileName;
                if (string.IsNullOrWhiteSpace(f))
                    return;
                SaveImageInternal(f);
            }
        }

        private void SaveImageInternal(string fileName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
            try
            {
                var bmp = ThePanel.CombinedImage;
                Debug.Assert(bmp != null);

                var format = ImageFormatInformation.GetImageFormatFromFileName(fileName);
                Debug.Assert(format != null);

                using var fileStream = File.OpenWrite(fileName);
                bmp.SaveExtended(fileStream, format);

                if (_settingsContext.CloseAfterSave)
                    Close();
            }
            catch (PathTooLongException ex)
            {
                NotificationManager.PathIsTooLong(fileName, this);
            }
            catch (Exception ex)
            {
                NotificationManager.ErrorSavingImage(ex, this);
            }
        }
        private async void UploadCurrentImageToDefaultProvider()
        {
            var image = ThePanel.CombinedImage;

            var progressReporter = UploadHelper.GetUploadReporterForCurrentSettingsContext(_settingsContext, this);

            try
            {
                var result = await UploadDispatcher.InitiateUploadToDefaultUploader(ThePanel.CombinedImage, _settingsContext, _uploaders, null/* TODO Change to default(_) if this is not a reference type */, progressReporter).ConfigureAwait(true);
                Debug.Assert(result != null);
                UploadHelper.InvokeUploadFinishedUI(result, _settingsContext);
            }
            catch (UploadCanceledException ex)
            {
                NotificationManager.ShowOperationCanceled();
            }
            catch (UploadException ex)
            {
                await NotificationManager.UploadFailed(ex);
                return;
            }
            finally
            {
                HandleAfterUpload();
            }
        }
        private void HandleAfterUpload()
        {
            if (_settingsContext.CloseAfterUpload)
                Close();
        }

        private void EyedropperToolClick(object sender, EventArgs e) => EnableTool(ShotEditorTool.Eyedropper);
        private void ScaleToolClick(object sender, EventArgs e) => ThePanel.RunDialogTool(new Scale());
        private void EllipseToolClick(object sender, EventArgs e) => EnableTool(ShotEditorTool.Ellipse);
        private void EraserToolClick(object sender, EventArgs e) => EnableTool(ShotEditorTool.Eraser);
        private void CropToolClick(object sender, EventArgs e) => EnableTool(ShotEditorTool.Crop);
        private void ArrowToolClick(object sender, EventArgs e) => EnableTool(ShotEditorTool.Arrow);
        private void CensorToolClick(object sender, EventArgs e) => EnableTool(ShotEditorTool.Censor);
        private void MarkerToolClick(object sender, EventArgs e) => EnableTool(ShotEditorTool.Marker);
        private void BlurToolClick(object sender, EventArgs e) => EnableTool(ShotEditorTool.Blur);
        private void BrightenToolClick(object sender, EventArgs e) => EnableTool(ShotEditorTool.Brighten);
        private void UndoClick(object sender, EventArgs e) => ThePanel.Undo();
        private void DrawCursor_Click(object sender, EventArgs e) => ThePanel.DrawCursor = DrawCursor.Checked;
        private void ToolStripPaint(object sender, PaintEventArgs e) => e.Graphics.Clear(BackColor);
    }
}
