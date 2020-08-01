Imports System.Linq
Imports HolzShots.Input
Imports HolzShots.Interop
Imports HolzShots.Interop.LocalDisk
Imports HolzShots.IO
Imports HolzShots.IO.Naming
Imports HolzShots.My
Imports HolzShots.ScreenshotRelated.Selection
Imports HolzShots.UI.Controls
Imports Microsoft.WindowsAPICodePack.Dialogs

Namespace UI.Specialized

    Friend Class SettingsWindow
        Inherits Form

        Private Class Localization
            Public Const AdminBannerText = "Some settings are configured by your system administrator."
            Public Const NotSet = "<not set>"
            Public Const InvalidFilePattern = "Invalid Pattern"
            Public Const SetHotkeyCurrentWindow = "Set Window Screenshot Hotkey"
            Public Const SetHotkeyAreaSelector = "Set Area Selector Hotkey"
            Public Const SetHotkeyFullscreen = "Set Fullscreen Hotkey"
        End Class

        Public Shared ReadOnly Instance As SettingsWindow = New SettingsWindow()

        Private Shared ReadOnly Target As MainWindow = MainWindow.Instance

        Private Shared ReadOnly BorderPen As New Pen(Color.FromArgb(255, &HCC, &HCC, &HCC))

        Private ReadOnly _pluginInfoItemList As New List(Of PluginInfoItem)

        Private Sub New()
            InitializeComponent()
            DisplayPlugins()
            InitializeIconResources()
        End Sub

        Private Sub InitializeIconResources()
            Dim shield = MyApplication.SmallStockIcons.Shield.Icon.ToBitmap()

            elevatedRequiredPictureBox1.Image = shield
            elevatedRequiredPictureBox2.Image = shield
            gpoInfoBanner.Image = MyApplication.SmallStockIcons.Info.Icon.ToBitmap()
        End Sub


        Private Sub DisplayPlugins()

            If Global.HolzShots.My.Application.Uploaders.Loaded Then
                Dim uploaders = Global.HolzShots.My.Application.Uploaders.GetUploaderNames()
                defaultHosterBox.Items.Clear()
                defaultHosterBox.Items.AddRange(uploaders.ToArray())

                pluginListPanel.Controls.Clear()
                _pluginInfoItemList.Clear()
                Dim metadata = Global.HolzShots.My.Application.Uploaders.GetMetadata()
                Dim metaArr = metadata.Select(Function(i) New PluginInfoItem(i)).ToArray()
                pluginListPanel.Controls.AddRange(metaArr)
                _pluginInfoItemList.AddRange(metaArr)
            End If

            Dim index = defaultHosterBox.Items.IndexOf(Global.HolzShots.My.Settings.DefaultImageHoster)
            If index > -1 Then
                defaultHosterBox.SelectedItem = defaultHosterBox.Items(index)
            End If
        End Sub

        Private Sub LoadPolicies()
            uploadImageInExplorerMenu.Checked = ManagedSettings.ShellExtensionUpload
            uploadImageInExplorerMenu.Enabled = InteropHelper.IsAdministrator()
            'uploadImageInExplorerMenu.Enabled = Not ManagedSettings.ShellExtensionUploadPolicy.IsSet AndAlso InteropHelper.IsAdministrator()

            openImageInExplorerMenu.Checked = ManagedSettings.ShellExtensionOpen
            openImageInExplorerMenu.Enabled = InteropHelper.IsAdministrator()
            'openImageInExplorerMenu.Enabled = Not ManagedSettings.ShellExtensionOpenPolicy.IsSet AndAlso InteropHelper.IsAdministrator()

            disableShotEditorCheckBox.Checked = Not ManagedSettings.EnableShotEditor
            'disableShotEditorCheckBox.Enabled = Not ManagedSettings.EnableShotEditorPolicy.IsSet

            deactivateLinkViewerCheckBox.Checked = Not ManagedSettings.EnableLinkViewer
            deactivateLinkViewerCheckBox.Enabled = False ' we only support reading the current setting for now

            EnableIngameMode.Checked = Not UserSettings.Current.EnableIngameMode
            EnableIngameMode.Enabled = False ' we only support reading the current setting for now

            start_with_windows.Checked = HolzShotsEnvironment.AutoStart

            enableStatusToaster.Checked = UserSettings.Current.EnableUploadProgressToast
            enableStatusToaster.Enabled = False ' we only support reading the current setting for now

            AutoCloseLinkViewer.Checked = UserSettings.Current.AutoCloseLinkViewer
            AutoCloseLinkViewer.Enabled = False ' we only support reading the current setting for now

            enableSmartFormatForUpload.Checked = UserSettings.Current.EnableSmartFormatForUpload
            enableSmartFormatForUpload.Enabled = False ' we only support reading the current setting for now

            ' Screenshot Methods
            Activate_Area.Checked = ManagedSettings.EnableAreaScreenshot
            'Activate_Area.Enabled = Not ManagedSettings.EnableAreaScreenshotPolicy.IsSet

            Activate_Fullscreen.Checked = ManagedSettings.EnableFullscreenScreenshot
            'Activate_Fullscreen.Enabled = Not ManagedSettings.EnableFullscreenScreenshotPolicy.IsSet

            Activate_Window.Checked = ManagedSettings.EnableWindowScreenshot
            'Activate_Window.Enabled = Not ManagedSettings.EnableWindowScreenshotPolicy.IsSet

            showCopyConfirmation.Checked = UserSettings.Current.ShowCopyConfirmation
            showCopyConfirmation.Enabled = False ' we only support reading the current setting for now
            ' deactivateLinkViewerCheckBox.Checked

            ' /Screenshot Methods

            Dim d As SelectionDecorations = ManagedSettings.SelectionDecoration
            decoration1.Checked = d = SelectionDecorations.Nomination1
            decoration2.Checked = d = SelectionDecorations.Nomination2
            decoration3.Checked = d = SelectionDecorations.Nomination3
            'decorationPanel.Enabled = Not SelectionDecorationPolicy.IsSet

            ' Local saves
            enableLocalSaveCheckBox.Checked = UserSettings.Current.SaveImagesToLocalDisk
            'enableLocalSaveCheckBox.Enabled = Not ManagedSettings.SaveImagesToLocalDiskPolicy.IsSet

            localSaveSettingsPanel.Enabled = enableLocalSaveCheckBox.Checked

            fileNamingPattern.Text = ManagedSettings.SaveImagesPattern
            'fileNamingPattern.Enabled = Not ManagedSettings.SaveImagesPatternPolicy.IsSet

            enableSmartFormatForSaving.Checked = UserSettings.Current.EnableSmartFormatForSaving
            enableSmartFormatForSaving.Enabled = False ' we only support reading the current setting for now

            'Dim enableCustomPaths = Not ManagedSettings.ScreenshotPathPolicy.IsSet
            Dim p = UserSettings.Current.SavePath
            If String.IsNullOrWhiteSpace(p) Then
                p = HolzShotsPaths.DefaultScreenshotSavePath
            End If
            localSavePath.Text = p

            localSavePath.Enabled = True ' enableCustomPaths
            localSavePathBrowseButton.Enabled = True ' enableCustomPaths
            ' /Local saves

            SetGpoBannerVisibility(False)
        End Sub

        Private Sub SavePolicies()
            'If Not ManagedSettings.ShellExtensionOpenPolicy.IsSet AndAlso ShellExtensions.ShellExtensionOpen <> openImageInExplorerMenu.Checked Then
            If ShellExtensions.ShellExtensionOpen <> openImageInExplorerMenu.Checked AndAlso InteropHelper.IsAdministrator() Then
                ShellExtensions.ShellExtensionOpen = openImageInExplorerMenu.Checked
            End If
            'If Not ManagedSettings.ShellExtensionUploadPolicy.IsSet AndAlso ShellExtensions.ShellExtensionUpload <> uploadImageInExplorerMenu.Checked Then
            If ShellExtensions.ShellExtensionUpload <> uploadImageInExplorerMenu.Checked AndAlso InteropHelper.IsAdministrator() Then
                ShellExtensions.ShellExtensionUpload = uploadImageInExplorerMenu.Checked
            End If

            With Global.HolzShots.My.Settings
                .DefaultImageHoster = defaultHosterBox.Text
            End With

            ManagedSettings.EnableWindowScreenshot = Activate_Window.Checked
            ManagedSettings.EnableFullscreenScreenshot = Activate_Fullscreen.Checked
            ManagedSettings.EnableLinkViewer = Not deactivateLinkViewerCheckBox.Checked
            ManagedSettings.EnableShotEditor = Not disableShotEditorCheckBox.Checked
            ManagedSettings.EnableAreaScreenshot = Activate_Area.Checked

            Dim d As SelectionDecorations
            If decoration1.Checked Then
                d = SelectionDecorations.Nomination1
            ElseIf decoration2.Checked Then
                d = SelectionDecorations.Nomination2
            ElseIf decoration3.Checked Then
                d = SelectionDecorations.Nomination3
            End If
            'decorationPanel.Enabled = Not SelectionDecorationPolicy.IsSet
            ManagedSettings.SelectionDecoration = d

            If UserSettings.Current.SaveImagesToLocalDisk Then
                ManagedSettings.SaveImagesPattern = fileNamingPattern.Text
            End If

            HolzShotsEnvironment.AutoStart = start_with_windows.Checked

            SaveHotkeys()

            Global.HolzShots.My.Settings.Save()
        End Sub

        Private Sub SaveHotkeys()
            Dim preSelectorHotkey = Global.HolzShots.My.Settings.SelectorHotkey
            Dim preFullHotkey = Global.HolzShots.My.Settings.FullHotkey
            Dim preWindowHotkey = Global.HolzShots.My.Settings.WindowHotkey

            Global.HolzShots.My.Settings.SelectorHotkey = _selectorKeyStroke
            Global.HolzShots.My.Settings.FullHotkey = _fullKeyStroke
            Global.HolzShots.My.Settings.WindowHotkey = _windowKeyStroke

            Try
                Target.ActionContainer.Refresh()
            Catch ex As AggregateException
                Global.HolzShots.My.Settings.SelectorHotkey = preSelectorHotkey
                Global.HolzShots.My.Settings.FullHotkey = preFullHotkey
                Global.HolzShots.My.Settings.WindowHotkey = preWindowHotkey
                Target.ActionContainer.Refresh()
                HumanInterop.ErrorRegisteringHotkeys()
            Finally
            End Try
            EnsureHotkeySettingsIntegrity()
        End Sub

        Private _hasShrinked As Boolean = False
        Private Sub SetGpoBannerVisibility(v As Boolean)
            If v Then
                If _hasShrinked Then
                    Dim h As Integer = gpoInfoBanner.Height
                    Height += h
                    Tabs.Height -= h
                    _hasShrinked = False
                End If
                gpoInfoBanner.Visible = True
                gpoInfoBanner.Text = Localization.AdminBannerText
            Else
                If Not _hasShrinked Then
                    gpoInfoBanner.Visible = False
                    Dim h As Integer = gpoInfoBanner.Height
                    Height -= h
                    Tabs.Height += h
                    _hasShrinked = True
                End If
            End If
        End Sub

        Private Sub SettingsLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadPolicies()
            LoadHotkeys()
        End Sub

        Private Sub LoadHotkeys()
            EnsureHotkeySettingsIntegrity()

            _selectorKeyStroke = HolzShots.My.Settings.SelectorHotkey
            _windowKeyStroke = HolzShots.My.Settings.WindowHotkey
            _fullKeyStroke = HolzShots.My.Settings.FullHotkey

            UpdateHotkeyLabels()
        End Sub

        Private Sub UpdateHotkeyLabels()
            _selectorStrokeLabel.Text = If(_selectorKeyStroke?.ToString(), Localization.NotSet)
            _fullStrokeLabel.Text = If(_fullKeyStroke?.ToString(), Localization.NotSet)
            _windowStrokeLabel.Text = If(_windowKeyStroke?.ToString(), Localization.NotSet)
        End Sub

        Private Shared ReadOnly Property SelectorDefaultHotkey As Hotkey
            Get
                Return New Hotkey(Input.ModifierKeys.None, Keys.F8)
            End Get
        End Property

        Private Shared ReadOnly Property FullDefaultHotkey As Hotkey
            Get
                Return New Hotkey(Input.ModifierKeys.None, Keys.F9)
            End Get
        End Property

        Private Shared ReadOnly Property WindowDefaultHotkey As Hotkey
            Get
                Return New Hotkey(Input.ModifierKeys.None, Keys.F10)
            End Get
        End Property

        Friend Shared Sub EnsureHotkeySettingsIntegrity()
            Dim changed = False

            If HolzShots.My.Settings.EnableAreaScreenshot Then
                Dim r = IdentityOrDefaultHotkey(HolzShots.My.Settings.SelectorHotkey, SelectorDefaultHotkey)
                HolzShots.My.Settings.SelectorHotkey = r.Item1
                If r.Item2 Then
                    changed = True
                End If
            Else
                HolzShots.My.Settings.SelectorHotkey = Nothing
            End If
            If HolzShots.My.Settings.EnableFullscreenScreenshot Then
                Dim r = IdentityOrDefaultHotkey(HolzShots.My.Settings.FullHotkey, FullDefaultHotkey)
                HolzShots.My.Settings.FullHotkey = r.Item1
                If r.Item2 Then
                    changed = True
                End If
            Else
                HolzShots.My.Settings.FullHotkey = Nothing
            End If
            If HolzShots.My.Settings.EnableWindowScreenshot Then
                Dim r = IdentityOrDefaultHotkey(HolzShots.My.Settings.WindowHotkey, WindowDefaultHotkey)
                HolzShots.My.Settings.WindowHotkey = r.Item1
                If r.Item2 Then
                    changed = True
                End If
            Else
                HolzShots.My.Settings.WindowHotkey = Nothing
            End If

            If changed Then
                HolzShots.My.Settings.Save()
            End If
        End Sub

        Private Shared Function IdentityOrDefaultHotkey(h As Hotkey, [default] As Hotkey) As (Hotkey, Boolean)
            If h Is Nothing OrElse h.IsNone() Then Return ([default], True)
            Return (h, False)
        End Function

        Private Sub SavebtnClick(ByVal sender As Object, ByVal e As EventArgs) Handles savebtn.Click
            SavePolicies()
            Close()
        End Sub

        Private Sub AbortClick(ByVal sender As Object, ByVal e As EventArgs) Handles Abort.Click
            Close()
        End Sub

        Private Sub ActivateAreaCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Activate_Area.CheckedChanged
            'setSelector.Enabled = Not ManagedSettings.EnableAreaScreenshotPolicy.IsSet
            EnableIngameMode.Enabled = Activate_Area.Checked ' AndAlso Not ManagedSettings.EnableAreaScreenshotPolicy.IsSet
        End Sub

        Private Sub ActivateFullscreenCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Activate_Fullscreen.CheckedChanged
            'setFullscreen.Enabled = Not ManagedSettings.EnableFullscreenScreenshotPolicy.IsSet
        End Sub

        Private Sub ActivateWindowCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Activate_Window.CheckedChanged
            'setWindow.Enabled = Not ManagedSettings.EnableWindowScreenshotPolicy.IsSet
        End Sub

        Private Sub ActivateFastUploadCheckBoxCheckedChanged(sender As Object, e As EventArgs) Handles deactivateLinkViewerCheckBox.CheckedChanged
            AutoCloseLinkViewer.Enabled = Not deactivateLinkViewerCheckBox.Checked ' AndAlso Not ManagedSettings.AutoCloseLinkViewerPolicy.IsSet
            showCopyConfirmation.Enabled = deactivateLinkViewerCheckBox.Checked
        End Sub

        Private Sub SettingsShown(sender As Object, e As EventArgs) Handles MyBase.Shown
            Focus()
        End Sub

        Private Sub EnableLocalSaveCheckBoxCheckedChanged(sender As Object, e As EventArgs) Handles enableLocalSaveCheckBox.CheckedChanged
            localSaveSettingsPanel.Enabled = enableLocalSaveCheckBox.Checked ' AndAlso Not ManagedSettings.SaveImagesToLocalDiskPolicy.IsSet
            UpdateFileNameingPreviewLabel()
        End Sub


        Private Sub FileNamingPatternTextChanged(sender As Object, e As EventArgs) Handles fileNamingPattern.TextChanged
            UpdateFileNameingPreviewLabel()
        End Sub

        Private Sub LocalSavePathBrowseButtonClick(sender As Object, e As EventArgs) Handles localSavePathBrowseButton.Click
            Using dlg As New CommonOpenFileDialog()
                dlg.IsFolderPicker = True
                dlg.DefaultDirectory = UserSettings.Current.SavePath
                dlg.InitialDirectory = dlg.DefaultDirectory
                dlg.Multiselect = False

                If dlg.ShowDialog(Me.Handle) = DialogResult.OK Then
                    localSavePath.Text = dlg.FileName
                End If
            End Using
        End Sub

        Private Shared ReadOnly InvalidFilePatternColor As Color = Color.FromArgb(255, 136, 0, 21)
        Private Shared ReadOnly ValidFilePatternColor As Color = SystemColors.ControlText

        Private Sub PatternIsInvalid()
            fileNamingPatternPreview.Text = Localization.InvalidFilePattern
            fileNamingPatternPreview.ForeColor = InvalidFilePatternColor
            savebtn.Enabled = Not enableLocalSaveCheckBox.Checked
        End Sub
        Private Sub PatternIsValid(updateText As String)
            fileNamingPatternPreview.Text = updateText & DefaultFileExtension
            fileNamingPatternPreview.ForeColor = ValidFilePatternColor
            savebtn.Enabled = enableLocalSaveCheckBox.Checked
        End Sub

        Private Sub UpdateFileNameingPreviewLabel()
            If Not enableLocalSaveCheckBox.Checked Then
                savebtn.Enabled = True
                Return
            End If

            Dim patternStr = fileNamingPattern.Text

            If String.IsNullOrWhiteSpace(patternStr) Then
                PatternIsInvalid()
                Return
            End If

            Dim parsedPattern As FileNamePattern
            Try
                parsedPattern = FileNamePattern.Parse(patternStr)
            Catch ex As Exception
                PatternIsInvalid()
                Return
            End Try

            If parsedPattern Is Nothing OrElse parsedPattern.IsEmpty Then
                PatternIsInvalid()
                Return
            End If

            Dim formattedSampleName = parsedPattern.FormatMetadata(FileMetadata.DemoMetadata)
            PatternIsValid(formattedSampleName)
        End Sub

        Private Shared Sub OpenPluginFolderLinkLabelLinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles openPluginFolderLinkLabel.LinkClicked
            Global.HolzShots.My.Application.Uploaders.Plugins.PluginDirectory.OpenFolderInExplorer()
        End Sub

        Private _selectorKeyStroke As Hotkey
        Private Sub SetSelectorHotkeyClick(sender As Object, e As EventArgs) Handles setSelector.Click
            Using s As IHotkeySelector = New SimpleHotkeySelector(Me, _selectorKeyStroke, Localization.SetHotkeyAreaSelector)
                If s.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    _selectorKeyStroke = s.SelectedKeyStroke
                    UpdateHotkeyLabels()
                End If
            End Using
        End Sub

        Private _fullKeyStroke As Hotkey
        Private Sub SetFullscreenClick(sender As Object, e As EventArgs) Handles setFullscreen.Click
            Using s As IHotkeySelector = New SimpleHotkeySelector(Me, _fullKeyStroke, Localization.SetHotkeyFullscreen)
                If s.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    _fullKeyStroke = s.SelectedKeyStroke
                    UpdateHotkeyLabels()
                End If
            End Using
        End Sub

        Private _windowKeyStroke As Hotkey
        Private Sub SetWindowClick(sender As Object, e As EventArgs) Handles setWindow.Click
            Using s As IHotkeySelector = New SimpleHotkeySelector(Me, _windowKeyStroke, Localization.SetHotkeyCurrentWindow)
                If s.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    _windowKeyStroke = s.SelectedKeyStroke
                    UpdateHotkeyLabels()
                End If
            End Using
        End Sub

        Private Sub PluginsTabPaint(sender As Object, e As PaintEventArgs) Handles PluginsTab.Paint
            e.Graphics.DrawLine(BorderPen, 0, pluginListPanel.Location.Y - 1, Width - 1, pluginListPanel.Location.Y - 1)
        End Sub

        Private Async Sub OpenSettingsJson_Click(sender As Object, e As EventArgs) Handles OpenSettingsJson.Click
            Await UserSettings.CreateUserSettingsIfNotPresent()
            UserSettings.OpenSettingsInDefaultEditor()
        End Sub
    End Class
End Namespace
