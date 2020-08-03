Imports System.Linq
Imports HolzShots.Input
Imports HolzShots.Interop
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
            Public Const NotSet = "<not set>"
            Public Const InvalidFilePattern = "Invalid Pattern"
            Public Const SetHotkeyCurrentWindow = "Set Window Screenshot Hotkey"
            Public Const SetHotkeyAreaSelector = "Set Area Selector Hotkey"
            Public Const SetHotkeyFullscreen = "Set Fullscreen Hotkey"
        End Class

        Public Shared ReadOnly Instance As SettingsWindow = New SettingsWindow()

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

            openImageInExplorerMenu.Checked = ManagedSettings.ShellExtensionOpen
            openImageInExplorerMenu.Enabled = InteropHelper.IsAdministrator()

            disableShotEditorCheckBox.Checked = Not UserSettings.Current.EnableShotEditor
            disableShotEditorCheckBox.Enabled = False ' we only support reading the current setting for now

            deactivateLinkViewerCheckBox.Checked = UserSettings.Current.CopyUploadedLinkToClipboard
            deactivateLinkViewerCheckBox.Enabled = False ' we only support reading the current setting for now

            EnableIngameMode.Checked = Not UserSettings.Current.EnableIngameMode
            EnableIngameMode.Enabled = False ' we only support reading the current setting for now

            start_with_windows.Checked = HolzShotsEnvironment.AutoStart

            enableStatusToaster.Checked = UserSettings.Current.ShowUploadProgress
            enableStatusToaster.Enabled = False ' we only support reading the current setting for now

            AutoCloseLinkViewer.Checked = UserSettings.Current.AutoCloseLinkViewer
            AutoCloseLinkViewer.Enabled = False ' we only support reading the current setting for now

            enableSmartFormatForUpload.Checked = UserSettings.Current.EnableSmartFormatForUpload
            enableSmartFormatForUpload.Enabled = False ' we only support reading the current setting for now

            showCopyConfirmation.Checked = UserSettings.Current.ShowCopyConfirmation
            showCopyConfirmation.Enabled = False ' we only support reading the current setting for now
            ' deactivateLinkViewerCheckBox.Checked

            ' /Screenshot Methods

            ' Local saves
            enableLocalSaveCheckBox.Checked = UserSettings.Current.SaveImagesToLocalDisk

            localSaveSettingsPanel.Enabled = enableLocalSaveCheckBox.Checked

            fileNamingPattern.Text = UserSettings.Current.SaveFileNamePattern
            fileNamingPattern.Enabled = False ' we only support reading the current setting for now

            enableSmartFormatForSaving.Checked = UserSettings.Current.EnableSmartFormatForSaving
            enableSmartFormatForSaving.Enabled = False ' we only support reading the current setting for now

            Dim p = UserSettings.Current.SavePath
            If String.IsNullOrWhiteSpace(p) Then
                p = HolzShotsPaths.DefaultScreenshotSavePath
            End If
            localSavePath.Text = p

            localSavePath.Enabled = True ' enableCustomPaths
            localSavePathBrowseButton.Enabled = True ' enableCustomPaths
        End Sub

        Private Sub SavePolicies()
            If ShellExtensions.ShellExtensionOpen <> openImageInExplorerMenu.Checked AndAlso InteropHelper.IsAdministrator() Then
                ShellExtensions.ShellExtensionOpen = openImageInExplorerMenu.Checked
            End If
            If ShellExtensions.ShellExtensionUpload <> uploadImageInExplorerMenu.Checked AndAlso InteropHelper.IsAdministrator() Then
                ShellExtensions.ShellExtensionUpload = uploadImageInExplorerMenu.Checked
            End If

            With Global.HolzShots.My.Settings
                .DefaultImageHoster = defaultHosterBox.Text
            End With

            HolzShotsEnvironment.AutoStart = start_with_windows.Checked

            Global.HolzShots.My.Settings.Save()
        End Sub

        Private Sub SettingsLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadPolicies()
        End Sub

        Private Sub SavebtnClick(ByVal sender As Object, ByVal e As EventArgs) Handles savebtn.Click
            SavePolicies()
            Close()
        End Sub

        Private Sub AbortClick(ByVal sender As Object, ByVal e As EventArgs) Handles Abort.Click
            Close()
        End Sub

        Private Sub ActivateAreaCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Activate_Area.CheckedChanged
            EnableIngameMode.Enabled = Activate_Area.Checked
        End Sub

        Private Sub ActivateFastUploadCheckBoxCheckedChanged(sender As Object, e As EventArgs) Handles deactivateLinkViewerCheckBox.CheckedChanged
            AutoCloseLinkViewer.Enabled = Not deactivateLinkViewerCheckBox.Checked
            showCopyConfirmation.Enabled = deactivateLinkViewerCheckBox.Checked
        End Sub

        Private Sub SettingsShown(sender As Object, e As EventArgs) Handles MyBase.Shown
            Focus()
        End Sub

        Private Sub EnableLocalSaveCheckBoxCheckedChanged(sender As Object, e As EventArgs) Handles enableLocalSaveCheckBox.CheckedChanged
            localSaveSettingsPanel.Enabled = enableLocalSaveCheckBox.Checked
            UpdateFileNamingPreviewLabel()
        End Sub


        Private Sub FileNamingPatternTextChanged(sender As Object, e As EventArgs) Handles fileNamingPattern.TextChanged
            UpdateFileNamingPreviewLabel()
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

        Private Sub UpdateFileNamingPreviewLabel()
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

        Private Sub PluginsTabPaint(sender As Object, e As PaintEventArgs) Handles PluginsTab.Paint
            e.Graphics.DrawLine(BorderPen, 0, pluginListPanel.Location.Y - 1, Width - 1, pluginListPanel.Location.Y - 1)
        End Sub

        Private Async Sub OpenSettingsJson_Click(sender As Object, e As EventArgs) Handles OpenSettingsJson.Click
            Await UserSettings.CreateUserSettingsIfNotPresent()
            UserSettings.OpenSettingsInDefaultEditor()
        End Sub
    End Class
End Namespace
