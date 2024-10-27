Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.IO
Imports System.Runtime.InteropServices
Imports HolzShots.Composition
Imports HolzShots.Drawing
Imports HolzShots.Net
Imports HolzShots.UI.Controls
Imports HolzShots.Windows.Forms
Imports HolzShots.Windows.Net
Imports Microsoft.WindowsAPICodePack.Taskbar

Namespace UI
    Public Class ShotEditor
        Inherits System.Windows.Forms.Form
        Implements IDisposable

        Private _activator As PanelActivator

        Private ReadOnly _defaultUploader As UploaderEntry ' ?
        Private ReadOnly _settingsContext As HSSettings
        Private ReadOnly _screenshot As Screenshot
        Private ReadOnly _uploaders As UploaderManager

#Region "Win7/8-Thumbnails"

        Private _uploadThumbnailButton As ThumbnailToolBarButton
        Private _saveThumbnailButton As ThumbnailToolBarButton
        Private _copyThumbnailButton As ThumbnailToolBarButton

        Private Sub InitializeThumbnailToolbar()

            If TaskbarManager.IsPlatformSupported Then

                Dim uploadTooltip As String = String.Empty
                If _defaultUploader?.Metadata IsNot Nothing Then
                    uploadTooltip = UploadToHoster.ToolTipText.Remove(UploadToHoster.ToolTipText.IndexOf(" (", StringComparison.Ordinal))
                    uploadTooltip = String.Format(CurrentCulture, uploadTooltip, _defaultUploader.Metadata.Name)
                End If

                _uploadThumbnailButton = New ThumbnailToolBarButton(Icon.FromHandle(My.Resources.uploadMedium.GetHicon()), uploadTooltip)
                AddHandler _uploadThumbnailButton.Click, Sub() UploadCurrentImageToDefaultProvider()
                _uploadThumbnailButton.Enabled = _defaultUploader?.Metadata IsNot Nothing

                _saveThumbnailButton = New ThumbnailToolBarButton(Icon.FromHandle(My.Resources.saveMedium.GetHicon()), "Save image")
                _copyThumbnailButton = New ThumbnailToolBarButton(Icon.FromHandle(My.Resources.clipboardMedium.GetHicon()), "Copy image")

                AddHandler _saveThumbnailButton.Click, Sub() SaveImage()
                AddHandler _copyThumbnailButton.Click, Sub() CopyImage()

                TaskbarManager.Instance.ThumbnailToolBars.AddButtons(Handle, _uploadThumbnailButton, _saveThumbnailButton, _copyThumbnailButton)
            End If
        End Sub

#End Region

#Region "Ctors"

        Public Sub New(screenshot As Screenshot, uploaders As UploaderManager, settingsContext As HSSettings)
            Debug.Assert(screenshot IsNot Nothing)
            Debug.Assert(screenshot.Image IsNot Nothing)
            Debug.Assert(uploaders IsNot Nothing)
            Debug.Assert(settingsContext IsNot Nothing)

            _settingsContext = settingsContext
            _screenshot = screenshot
            _uploaders = uploaders

            InitializeComponent()

            AutoCloseShotEditor.Checked = settingsContext.CloseAfterUpload
            AutoCloseShotEditor.Enabled = False ' We only support reading that setting for now

            If settingsContext.ShotEditorTitle IsNot Nothing Then
                Text = settingsContext.ShotEditorTitle
            End If

            _defaultUploader = UploadDispatcher.GetUploadServiceForSettingsContext(settingsContext, _uploaders)

            SuspendLayout()

            InitializeThumbnailToolbar()

            If Not settingsContext.EnableHotkeysDuringFullscreen AndAlso EnvironmentEx.IsFullscreenAppRunning() Then
                WindowState = FormWindowState.Minimized
            ElseIf screenshot.Size = SystemInformation.VirtualScreen.Size Then
                WindowState = FormWindowState.Maximized
            ElseIf screenshot.Image.ShouldMaximizeEditorWindowForImage() Then
                WindowState = FormWindowState.Maximized
            Else
                Width = screenshot.Image.Width
                Height = screenshot.Image.Height + ThePanel.Location.Y + 140
                WindowState = FormWindowState.Normal
            End If

            AddSettingsPanels()

            CensorSettingsPanel.BackColor = Color.Transparent
            MarkerSettingsPanel.BackColor = Color.Transparent
            EraserSettingsPanel.BackColor = Color.Transparent
            EllipseSettingsPanel.BackColor = Color.Transparent
            BrightenSettingsPanel.BackColor = Color.Transparent
            ArrowSettingsPanel.BackColor = Color.Transparent
            BlurSettingsPanel.BackColor = Color.Transparent

            Dim focusColor As Color = BackColor

            BlurnessBar.BackColor = focusColor
            ZensursulaBar.BackColor = focusColor
            MarkerBar.BackColor = focusColor
            BlackWhiteTracker.BackColor = focusColor
            ArrowWidthSlider.BackColor = focusColor

            EllipseBar.BackColor = focusColor
            EllipseOrRectangle.BackColor = focusColor

            EraserBar.BackColor = focusColor

            ShareStrip.BackColor = Color.Transparent
            EditStrip.BackColor = Color.Transparent
            ToolStrip1.BackColor = Color.Transparent
            CopyPrintToolStrip.BackColor = Color.Transparent

            DrawCursor.Visible = screenshot.Source <> ScreenshotSource.Selected AndAlso screenshot.Source <> ScreenshotSource.Unknown

            UploadToHoster.Enabled = _defaultUploader?.Metadata IsNot Nothing
            UploadToHoster.ToolTipText = If(
                            _defaultUploader?.Metadata IsNot Nothing,
                            String.Format(CurrentCulture, UploadToHoster.ToolTipText, _defaultUploader?.Metadata.Name),
                            String.Empty
                        )

            Dim renderer = HolzShots.Windows.Forms.EnvironmentEx.ToolStripRendererForCurrentTheme()
            ShareStrip.Renderer = renderer
            ToolStrip1.Renderer = renderer
            EditStrip.Renderer = renderer
            CopyPrintToolStrip.Renderer = renderer

            ResumeLayout(True)
        End Sub

#End Region

#Region "Form Events"

        Private Sub ShotShowerLoad() Handles MyBase.Load
            ThePanel.Initialize(_screenshot)
            LoadToolSettings()
        End Sub

#Region "Settings and stuff"

        Private Sub LoadToolSettings()
            LoadZensursula()
            LoadArrow()
            LoadMarker()
            LoadEraser()
            LoadEllipse()
            LoadBrighten()
            LoadPixelator()
            EnlistUploaderPlugins()
        End Sub

        Private Sub EnlistUploaderPlugins()

            UploadToHoster.DropDown.ImageScalingSize = New Size(16, 16)
            UploadToHoster.DropDown.AutoSize = True
            If _uploaders.Loaded Then
                UploadToHoster.DropDown.Renderer = HolzShots.Windows.Forms.EnvironmentEx.ToolStripRendererForCurrentTheme()
                Dim pls = _uploaders.GetUploaderNames()
                For Each uploaderName In pls
                    Dim item As ToolStripItem = UploadToHoster.DropDown.Items.Add(String.Format(Localization.UploadTo, uploaderName))
                    item.Tag = uploaderName
                    item.ImageScaling = ToolStripItemImageScaling.None
                Next
            End If
        End Sub

        Private Sub LoadPixelator()
            If HolzShots.My.Settings.BlurFactor > 30 OrElse HolzShots.My.Settings.BlurFactor <= 5 Then
                HolzShots.My.Settings.BlurFactor = 7
                HolzShots.My.Settings.Save()
            End If
            ThePanel.BlurFactor = HolzShots.My.Settings.BlurFactor
            BlurnessBar.Value = ThePanel.BlurFactor
        End Sub

        Private Sub LoadZensursula()
            ZensursulaColorSelector.Color = HolzShots.My.Settings.ZensursulaColor
            If HolzShots.My.Settings.ZensursulaWidth > 100 OrElse HolzShots.My.Settings.ZensursulaWidth <= 0 Then
                HolzShots.My.Settings.ZensursulaWidth = 20
                HolzShots.My.Settings.Save()
            End If
            ZensursulaBar.Value = HolzShots.My.Settings.ZensursulaWidth
            Pinsel_Width_Zensursula.Text = $"{ZensursulaBar.Value}px"
            ThePanel.ZensursulaColor = ZensursulaColorSelector.Color
            ThePanel.ZensursulaWidth = ZensursulaBar.Value
        End Sub

        Private Sub LoadArrow()
            ArrowColorviewer.Color = My.Settings.ArrowColor
            ThePanel.ArrowColor = ArrowColorviewer.Color
            ArrowWidthSlider.Value = Math.Clamp(My.Settings.ArrowWidth, ArrowWidthSlider.Minimum, ArrowWidthSlider.Maximum)
            ArrowWidthSliderScroll()
        End Sub

        Private Sub LoadMarker()
            MarkerColorSelector.Color = HolzShots.My.Settings.MarkerColor
            If HolzShots.My.Settings.MarkerWidth > 100 OrElse HolzShots.My.Settings.MarkerWidth <= 0 Then
                HolzShots.My.Settings.MarkerWidth = 20
                HolzShots.My.Settings.Save()
            End If
            MarkerBar.Value = HolzShots.My.Settings.MarkerWidth
            Pinsel_Width_Marker.Text = $"{MarkerBar.Value}px"
            ThePanel.MarkerColor = MarkerColorSelector.Color
            ThePanel.MarkerWidth = MarkerBar.Value
        End Sub

        Private Sub LoadEllipse()
            EllipseColorSelector.Color = HolzShots.My.Settings.EllipseColor
            If HolzShots.My.Settings.EllipseWidth > 100 OrElse HolzShots.My.Settings.EllipseWidth <= 0 Then
                HolzShots.My.Settings.EllipseWidth = 20
                HolzShots.My.Settings.Save()
            End If
            ThePanel.UseBoxInsteadOfCircle = HolzShots.My.Settings.UseBoxInsteadOfCirlce
            EllipseOrRectangle.Value = If(ThePanel.UseBoxInsteadOfCircle, 1, 0)
            EllipseBar.Value = HolzShots.My.Settings.EllipseWidth
            Ellipse_Width.Text = $"{EllipseBar.Value}px"
            ThePanel.EllipseColor = EllipseColorSelector.Color
            ThePanel.EllipseWidth = EllipseBar.Value
            'Ellips_style.UseCompatibleTextRendering = True
        End Sub

        Private Sub LoadEraser()
            If HolzShots.My.Settings.EraserDiameter > 100 OrElse HolzShots.My.Settings.EraserDiameter <= 0 Then
                HolzShots.My.Settings.EraserDiameter = 20
                HolzShots.My.Settings.Save()
            End If
            EraserBar.Value = HolzShots.My.Settings.EraserDiameter
            EraserDiameterLabel.Text = $"{ EraserBar.Value}px"
            ThePanel.EraserDiameter = EraserBar.Value
        End Sub

        Private Sub LoadBrighten()
            Dim v As Integer = HolzShots.My.Settings.BrightenColor.A
            If HolzShots.My.Settings.BrightenColor.R = HolzShots.My.Settings.BrightenColor.G AndAlso
                HolzShots.My.Settings.BrightenColor.R = HolzShots.My.Settings.BrightenColor.B Then
                If HolzShots.My.Settings.BrightenColor.R = 255 Then
                    v += 255
                ElseIf HolzShots.My.Settings.BrightenColor.R = 0 Then
                    v = 255 - v
                End If
            End If
            BlackWhiteTracker.Value = v
            BlackWhiteTrackerScroll()
        End Sub

#End Region

#End Region

#Region "Image Actions"

        Private Sub SaveImage() Handles SaveButton.Click
            Using sfd As New SaveFileDialog()
                sfd.Filter = $"{Localization.PngImage}|*.png|{Localization.JpgImage}|*.jpg"
                sfd.DefaultExt = ".png"
                sfd.CheckPathExists = True
                sfd.Title = Localization.ChooseDestinationFileName
                Dim res = sfd.ShowDialog()
                If res = DialogResult.OK Then
                    Dim f = sfd.FileName
                    If String.IsNullOrWhiteSpace(f) Then Return
                    SaveImage(f)
                End If
            End Using
        End Sub

        Private Sub CopyImage() Handles CopyToClipboard.Click
            Dim bmp = ThePanel.CombinedImage
            Try
                Clipboard.SetImage(bmp)
            Catch ex As Exception When _
                    TypeOf ex Is ExternalException _
                    OrElse TypeOf ex Is System.Threading.ThreadStateException _
                    OrElse TypeOf ex Is ArgumentNullException
                NotificationManager.CopyImageFailed(ex)
            End Try
        End Sub

#End Region

#Region "Filesystem"

        Private Sub SaveImage(fileName As String)
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName)
            Try
                Dim bmp = ThePanel.CombinedImage()
                Debug.Assert(bmp IsNot Nothing)

                Dim format = ImageFormatInformation.GetImageFormatFromFileName(fileName)
                Debug.Assert(format IsNot Nothing)

                Using fileStream = File.OpenWrite(fileName)
                    bmp.SaveExtended(fileStream, format)
                End Using

                If _settingsContext.CloseAfterSave Then
                    Close()
                End If

            Catch ex As PathTooLongException
                NotificationManager.PathIsTooLong(fileName, Me)
            Catch ex As Exception
                NotificationManager.ErrorSavingImage(ex, Me)
            End Try
        End Sub

#End Region

#Region "Updater"

        Private Sub UpdateSettings() Handles MyBase.FormClosing
            HolzShots.My.Settings.ZensursulaColor = ZensursulaColorSelector.Color
            HolzShots.My.Settings.ZensursulaWidth = ZensursulaBar.Value

            HolzShots.My.Settings.MarkerColor = MarkerColorSelector.Color
            HolzShots.My.Settings.MarkerWidth = MarkerBar.Value

            HolzShots.My.Settings.EraserDiameter = EraserBar.Value

            HolzShots.My.Settings.EllipseColor = EllipseColorSelector.Color
            HolzShots.My.Settings.EllipseWidth = EllipseBar.Value

            HolzShots.My.Settings.BrightenColor = BigColorViewer1.Color

            HolzShots.My.Settings.ArrowColor = ArrowColorviewer.Color
            HolzShots.My.Settings.ArrowWidth = ArrowWidthSlider.Value
            HolzShots.My.Settings.UseBoxInsteadOfCirlce = ThePanel.UseBoxInsteadOfCircle

            HolzShots.My.Settings.BlurFactor = ThePanel.BlurFactor

            HolzShots.My.Settings.Save()
        End Sub

        Private Sub ResetTools()
            EnableTool(PaintPanel.ShotEditorTool.None)
        End Sub

#End Region

#Region "Painting Tools"

        Private ReadOnly ToolControlMap As New Dictionary(Of PaintPanel.ShotEditorTool, ToolStripButton)

        Private Sub AddSettingsPanels()
            _activator = New PanelActivator()
            _activator.AddPanel(PaintPanel.ShotEditorTool.Arrow, ArrowSettingsPanel)
            _activator.AddPanel(PaintPanel.ShotEditorTool.Brighten, BrightenSettingsPanel)
            _activator.AddPanel(PaintPanel.ShotEditorTool.Blur, BlurSettingsPanel)
            _activator.AddPanel(PaintPanel.ShotEditorTool.Ellipse, EllipseSettingsPanel)
            _activator.AddPanel(PaintPanel.ShotEditorTool.Eraser, EraserSettingsPanel)
            _activator.AddPanel(PaintPanel.ShotEditorTool.Marker, MarkerSettingsPanel)
            _activator.AddPanel(PaintPanel.ShotEditorTool.Censor, CensorSettingsPanel)

            ToolControlMap.Add(PaintPanel.ShotEditorTool.Pipette, PipettenTool)
            ToolControlMap.Add(PaintPanel.ShotEditorTool.Scale, Nothing)
            ToolControlMap.Add(PaintPanel.ShotEditorTool.Ellipse, EllipseTool)
            ToolControlMap.Add(PaintPanel.ShotEditorTool.Text, TextToolButton)
            ToolControlMap.Add(PaintPanel.ShotEditorTool.Eraser, EraserTool)
            ToolControlMap.Add(PaintPanel.ShotEditorTool.Crop, CroppingTool)
            ToolControlMap.Add(PaintPanel.ShotEditorTool.Arrow, ArrowTool)
            ToolControlMap.Add(PaintPanel.ShotEditorTool.Censor, CensorTool)
            ToolControlMap.Add(PaintPanel.ShotEditorTool.Marker, MarkerTool)
            ToolControlMap.Add(PaintPanel.ShotEditorTool.Blur, BlurTool)
            ToolControlMap.Add(PaintPanel.ShotEditorTool.Brighten, BrightenTool)
        End Sub

        Private Sub EnableTool(tool As PaintPanel.ShotEditorTool)
            ThePanel.CurrentTool = tool
            _activator.ActivateSettingsPanel(tool)
            Dim toolToEnable = ToolControlMap.GetValueOrDefault(tool)
            For Each button In ToolControlMap.Values
                If button IsNot Nothing Then
                    button.Checked = toolToEnable IsNot Nothing AndAlso button Is toolToEnable
                End If
            Next
            _activator.HideAll()
        End Sub

        Private Sub PipettenToolClick() Handles PipettenTool.Click
            EnableTool(PaintPanel.ShotEditorTool.Pipette)
        End Sub
        Private Sub ScaleToolClick() Handles ScaleTool.Click
            EnableTool(PaintPanel.ShotEditorTool.Scale)
        End Sub
        Private Sub CircleToolClick() Handles EllipseTool.Click
            EnableTool(PaintPanel.ShotEditorTool.Ellipse)
        End Sub
        Private Sub TextToolButtonClick() Handles TextToolButton.Click
            EnableTool(PaintPanel.ShotEditorTool.Text)
        End Sub
        Private Sub EraserButtonClick() Handles EraserTool.Click
            EnableTool(PaintPanel.ShotEditorTool.Eraser)
        End Sub
        Private Sub CroppingToolClick() Handles CroppingTool.Click
            EnableTool(PaintPanel.ShotEditorTool.Crop)
        End Sub
        Private Sub ArrowToolClick() Handles ArrowTool.Click
            EnableTool(PaintPanel.ShotEditorTool.Arrow)
        End Sub
        Private Sub ZensursulaClick() Handles CensorTool.Click
            EnableTool(PaintPanel.ShotEditorTool.Censor)
        End Sub
        Private Sub HighlightClick() Handles MarkerTool.Click
            EnableTool(PaintPanel.ShotEditorTool.Marker)
        End Sub
        Private Sub PixelateAreaClick() Handles BlurTool.Click
            EnableTool(PaintPanel.ShotEditorTool.Blur)
        End Sub
        Private Sub BrightenToolClick() Handles BrightenTool.Click
            EnableTool(PaintPanel.ShotEditorTool.Brighten)
        End Sub

        Private Sub UndoStuffClick() Handles UndoStuff.Click
            ThePanel.Undo()
        End Sub

#End Region

#Region "Shortcut Keys"

        Private Sub ZensToolStripMenuItemClick() Handles ZensToolStripMenuItem.Click
            CensorTool.PerformClick()
        End Sub
        Private Sub MarkToolStripMenuItemClick() Handles MarkToolStripMenuItem.Click
            MarkerTool.PerformClick()
        End Sub
        Private Sub TextToolStripMenuItemClick() Handles TextToolStripMenuItem.Click
            TextToolButton.PerformClick()
        End Sub
        Private Sub CropToolStripMenuItemClick() Handles CropToolStripMenuItem.Click
            CroppingTool.PerformClick()
        End Sub
        Private Sub EraseToolStripMenuItemClick() Handles EraseToolStripMenuItem.Click
            EraserTool.PerformClick()
        End Sub
        Private Sub PixelateToolStripMenuItemClick() Handles PixelateToolStripMenuItem.Click
            BlurTool.PerformClick()
        End Sub
        Private Sub ArrowToolStripMenuItemClick() Handles ArrowToolStripMenuItem.Click
            ArrowTool.PerformClick()
        End Sub
        Private Sub ResetToolStripMenuItemClick() Handles ResetToolStripMenuItem.Click
            UndoStuff.PerformClick()
        End Sub
        Private Sub UploadToolStripMenuItemClick() Handles UploadToolStripMenuItem.Click
            UploadToHoster.PerformButtonClick()
        End Sub
        Private Sub SaveToolStripMenuItemClick() Handles SaveToolStripMenuItem.Click
            SaveButton.PerformClick()
        End Sub
        Private Sub ClipboardToolStripMenuItemClick() Handles ClipboardToolStripMenuItem.Click
            CopyToClipboard.PerformClick()
        End Sub
        Private Sub KreisToolStripMenuItemClick() Handles KreisToolStripMenuItem.Click
            EllipseTool.PerformClick()
        End Sub

#End Region

#Region "Tool Settings"

        Private Sub ZensursulaBarValueChanged() Handles ZensursulaBar.Scroll
            Pinsel_Width_Zensursula.Text = $"{ZensursulaBar.Value}px"
            ThePanel.ZensursulaWidth = ZensursulaBar.Value
        End Sub

        Private Sub ZensursulaViewerColorChanged(sender As Object, c As Color) Handles ZensursulaColorSelector.ColorChanged
            ThePanel.ZensursulaColor = c
        End Sub

        Private Sub MarkerBarValueChanged() Handles MarkerBar.Scroll
            Pinsel_Width_Marker.Text = $"{MarkerBar.Value}px"
            ThePanel.MarkerWidth = MarkerBar.Value
        End Sub

        Private Sub MarkerViewerColorChanged(sender As Object, c As Color) Handles MarkerColorSelector.ColorChanged
            ThePanel.MarkerColor = c
        End Sub

        Private Sub EraserBarScroll() Handles EraserBar.ValueChanged
            EraserDiameterLabel.Text = $"{EraserBar.Value}px"
            ThePanel.EraserDiameter = EraserBar.Value
        End Sub

        Private Sub EllipseBarValueChanged() Handles EllipseBar.ValueChanged
            Ellipse_Width.Text = $"{EllipseBar.Value}px"
            ThePanel.EllipseWidth = EllipseBar.Value
        End Sub

        Private Sub EllipseViewerColorChanged(sender As Object, c As Color) Handles EllipseColorSelector.ColorChanged
            ThePanel.EllipseColor = c
        End Sub

#End Region

        Private Sub ImageInfoLabelMouseClick(sender As Object, e As MouseEventArgs) Handles ImageInfoLabel.MouseUp

            Dim s = ThePanel.Screenshot
            If e.Button = MouseButtons.Left Then
                ClipboardEx.SetText($"{s.Size.Width}x{s.Size.Height}px")
            ElseIf e.Button = MouseButtons.Right Then
                ClipboardEx.SetText(s.Timestamp.ToString())
            ElseIf e.Button = MouseButtons.Middle Then
                ClipboardEx.SetText($"{s.Timestamp} {s.Size.Width}x{s.Size.Height}px")
            End If
        End Sub

        Private Sub ThePanelInitialized() Handles ThePanel.Initialized
            ImageInfoLabel.Text = ThePanel.SizeInfo
            ImageInfoLabel.ToolTipText = ThePanel.SizeInfoText
            MouseInfoLabel.Text = "0, 0px"
        End Sub

        Private Sub ThePanelUpdateMousePosition(e As Point) Handles ThePanel.UpdateMousePosition
            MouseInfoLabel.Text = $"{e.X}, {e.Y}px"
        End Sub

        Private Sub BlackWhiteTrackerScroll() Handles BlackWhiteTracker.Scroll
            If BlackWhiteTracker.Value >= 0 AndAlso BlackWhiteTracker.Value <= 255 Then
                ThePanel.BrightenColor = Color.FromArgb(255 - BlackWhiteTracker.Value, 0, 0, 0)
                BigColorViewer1.Color = ThePanel.BrightenColor
            ElseIf BlackWhiteTracker.Value > 255 AndAlso BlackWhiteTracker.Value <= 510 Then
                ThePanel.BrightenColor = Color.FromArgb(BlackWhiteTracker.Value - 255, 255, 255, 255)
                BigColorViewer1.Color = ThePanel.BrightenColor
            End If
        End Sub

        Private Sub ArrowColorviewerColorChanged(sender As Object, c As Color) Handles ArrowColorviewer.ColorChanged
            ThePanel.ArrowColor = c
        End Sub

        Private Sub DrawCursorClick() Handles DrawCursor.Click
            ThePanel.DrawCursor = DrawCursor.Checked
        End Sub

        Private Sub ArrowWidthSliderScroll() Handles ArrowWidthSlider.Scroll
            ArrowWidthLabel.Text = If(ArrowWidthSlider.Value = 0, "Auto", $"{ArrowWidthSlider.Value}px")
            ThePanel.ArrowWidth = ArrowWidthSlider.Value
        End Sub

        Private Sub ShotEditorResize() Handles Me.Resize
            ThePanel.VerticalLinealBox.Invalidate()
            ThePanel.HorizontalLinealBox.Invalidate()
        End Sub

        Private Sub ToolStripsPaint(sender As Object, e As PaintEventArgs) Handles ToolStrip1.Paint, ShareStrip.Paint, EditStrip.Paint, CopyPrintToolStrip.Paint
            e.Graphics.Clear(BackColor)
        End Sub

        Private Async Sub UploadToHosterDropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles UploadToHoster.DropDownItemClicked

            Dim tag = DirectCast(e.ClickedItem.Tag, String)
            ' the tag represents the name of the image hoster here
            If String.IsNullOrWhiteSpace(tag) Then Return

            ' Dirty :>
            Dim info = _uploaders.GetUploaderByName(tag)

            Debug.Assert(Not String.IsNullOrEmpty(tag))
            Debug.Assert(info IsNot Nothing)
            Debug.Assert(info.Metadata IsNot Nothing)
            Debug.Assert(info.Uploader IsNot Nothing)
            Debug.Assert(Uploader.HasEqualName(info.Metadata.Name, tag))

            Dim image = ThePanel.CombinedImage
            Dim progressReporter = UploadHelper.GetUploadReporterForCurrentSettingsContext(_settingsContext, Me)

            Try
                Dim result = Await UploadDispatcher.InitiateUpload(image, _settingsContext, info.Uploader, Nothing, progressReporter).ConfigureAwait(True)
                Debug.Assert(result IsNot Nothing)
                UploadHelper.InvokeUploadFinishedUI(result, _settingsContext)
            Catch ex As UploadCanceledException
                NotificationManager.ShowOperationCanceled()
            Catch ex As UploadException
                Dim ignored = NotificationManager.UploadFailed(ex) ' VB doesn't support await in catch clauses
                Return
            End Try
            HandleAfterUpload()
        End Sub

        Private Sub EllipseOrRectangleValueChanged(sender As Object, e As EventArgs) Handles EllipseOrRectangle.ValueChanged
            ThePanel.UseBoxInsteadOfCircle = EllipseOrRectangle.Value = 1
            EllipseOrRectangleBox.Invalidate()
        End Sub

        Private Sub EllipseOrRectangleBoxClick(sender As Object, e As EventArgs) Handles EllipseOrRectangleBox.Click
            If EllipseOrRectangle.Value = 1 Then EllipseOrRectangle.Value = 0 Else EllipseOrRectangle.Value = 1
        End Sub

        Private Sub EllipseOrRectangleBoxPaint(sender As Object, e As PaintEventArgs) Handles EllipseOrRectangleBox.Paint
            Dim rct As New Rectangle(2, 2, 12, 12)
            Dim pe As New Pen(Brushes.Red) With {.Width = 2}
            If ThePanel.UseBoxInsteadOfCircle Then
                e.Graphics.DrawRectangle(pe, rct)
            Else
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality
                e.Graphics.DrawEllipse(pe, rct)
            End If
        End Sub

        Private Sub HandleAfterUpload()
            If _settingsContext.CloseAfterUpload Then
                Close()
            End If
        End Sub

        Private Sub BlurnessBarValueChanged() Handles BlurnessBar.ValueChanged
            ThePanel.BlurFactor = BlurnessBar.Value
        End Sub

        Private Async Sub UploadCurrentImageToDefaultProvider() Handles UploadToHoster.ButtonClick
            Dim image = ThePanel.CombinedImage

            Dim progressReporter = UploadHelper.GetUploadReporterForCurrentSettingsContext(_settingsContext, Me)

            Try
                Dim result = Await UploadDispatcher.InitiateUploadToDefaultUploader(ThePanel.CombinedImage, _settingsContext, _uploaders, Nothing, progressReporter).ConfigureAwait(True)
                Debug.Assert(result IsNot Nothing)
                UploadHelper.InvokeUploadFinishedUI(result, _settingsContext)
            Catch ex As UploadCanceledException
                NotificationManager.ShowOperationCanceled()
            Catch ex As UploadException
                Dim ignored = NotificationManager.UploadFailed(ex) ' VB doesn't support await in catch clauses
                Return
            Finally
                HandleAfterUpload()
            End Try
        End Sub

        Protected Overrides Sub Dispose(disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components?.Dispose()
                    _uploadThumbnailButton?.Dispose()
                    _saveThumbnailButton?.Dispose()
                    _copyThumbnailButton?.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub
    End Class
End Namespace
