Imports HolzShots.UI.Forms

Namespace UI.Specialized
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class SettingsWindow
        Inherits System.Windows.Forms.Form

        'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Wird vom Windows Form-Designer benötigt.
        Private components As System.ComponentModel.IContainer

        'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        'Das Bearbeiten ist mit dem Windows Form-Designer möglich.
        'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingsWindow))
            Me.Activate_Area = New System.Windows.Forms.CheckBox()
            Me.EnableIngameMode = New System.Windows.Forms.CheckBox()
            Me.Activate_Fullscreen = New System.Windows.Forms.CheckBox()
            Me.savebtn = New System.Windows.Forms.Button()
            Me.Tabs = New System.Windows.Forms.TabControl()
            Me.SelectorsTab = New System.Windows.Forms.TabPage()
            Me.Panel3 = New System.Windows.Forms.Panel()
            Me.windowStrokeLabel = New System.Windows.Forms.Label()
            Me.setWindow = New System.Windows.Forms.Button()
            Me.Activate_Window = New System.Windows.Forms.CheckBox()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.fullStrokeLabel = New System.Windows.Forms.Label()
            Me.setFullscreen = New System.Windows.Forms.Button()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.selectorStrokeLabel = New System.Windows.Forms.Label()
            Me.setSelector = New System.Windows.Forms.Button()
            Me.UploadTab = New System.Windows.Forms.TabPage()
            Me.showCopyConfirmation = New System.Windows.Forms.CheckBox()
            Me.enableSmartFormatForUpload = New System.Windows.Forms.CheckBox()
            Me.disableShotEditorCheckBox = New System.Windows.Forms.CheckBox()
            Me.deactivateLinkViewerCheckBox = New System.Windows.Forms.CheckBox()
            Me.AutoCloseLinkViewer = New System.Windows.Forms.CheckBox()
            Me.enableStatusToaster = New System.Windows.Forms.CheckBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.defaultHosterBox = New System.Windows.Forms.ComboBox()
            Me.SaveLocalTab = New System.Windows.Forms.TabPage()
            Me.enableLocalSaveCheckBox = New System.Windows.Forms.CheckBox()
            Me.localSaveSettingsPanel = New System.Windows.Forms.Panel()
            Me.enableSmartFormatForSaving = New System.Windows.Forms.CheckBox()
            Me.fileNamingPatternPreview = New System.Windows.Forms.Label()
            Me.fileNamingPatternPreviewLabel = New System.Windows.Forms.Label()
            Me.fileNamingPatternLabel = New System.Windows.Forms.Label()
            Me.fileNamingPattern = New System.Windows.Forms.TextBox()
            Me.localSavePathBrowseButton = New System.Windows.Forms.Button()
            Me.localSavePath = New System.Windows.Forms.TextBox()
            Me.localSavePathLabel = New System.Windows.Forms.Label()
            Me.OthersTab = New System.Windows.Forms.TabPage()
            Me.elevatedRequiredPictureBox2 = New System.Windows.Forms.PictureBox()
            Me.elevatedRequiredPictureBox1 = New System.Windows.Forms.PictureBox()
            Me.openImageInExplorerMenu = New System.Windows.Forms.CheckBox()
            Me.uploadImageInExplorerMenu = New System.Windows.Forms.CheckBox()
            Me.start_with_windows = New System.Windows.Forms.CheckBox()
            Me.PluginsTab = New System.Windows.Forms.TabPage()
            Me.pluginListPanel = New HolzShots.UI.Controls.StackPanel()
            Me.openPluginFolderLinkLabel = New HolzShots.UI.Forms.ExplorerLinkLabel()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.Abort = New System.Windows.Forms.Button()
            Me.OpenSettingsJson = New System.Windows.Forms.Button()
            Me.Tabs.SuspendLayout()
            Me.SelectorsTab.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.Panel1.SuspendLayout()
            Me.UploadTab.SuspendLayout()
            Me.SaveLocalTab.SuspendLayout()
            Me.localSaveSettingsPanel.SuspendLayout()
            Me.OthersTab.SuspendLayout()
            CType(Me.elevatedRequiredPictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.elevatedRequiredPictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.PluginsTab.SuspendLayout()
            Me.SuspendLayout()
            '
            'Activate_Area
            '
            Me.Activate_Area.AutoSize = True
            Me.Activate_Area.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.Activate_Area.Location = New System.Drawing.Point(3, 8)
            Me.Activate_Area.Name = "Activate_Area"
            Me.Activate_Area.Size = New System.Drawing.Size(119, 19)
            Me.Activate_Area.TabIndex = 1
            Me.Activate_Area.Text = "Capture Selection"
            Me.Activate_Area.UseVisualStyleBackColor = True
            '
            'EnableIngameMode
            '
            Me.EnableIngameMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EnableIngameMode.AutoSize = True
            Me.EnableIngameMode.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.EnableIngameMode.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.EnableIngameMode.Location = New System.Drawing.Point(199, 33)
            Me.EnableIngameMode.Name = "EnableIngameMode"
            Me.EnableIngameMode.Size = New System.Drawing.Size(232, 20)
            Me.EnableIngameMode.TabIndex = 4
            Me.EnableIngameMode.Text = "Enable when fullscreen app is running"
            Me.EnableIngameMode.UseVisualStyleBackColor = True
            '
            'Activate_Fullscreen
            '
            Me.Activate_Fullscreen.AutoSize = True
            Me.Activate_Fullscreen.Location = New System.Drawing.Point(3, 8)
            Me.Activate_Fullscreen.Name = "Activate_Fullscreen"
            Me.Activate_Fullscreen.Size = New System.Drawing.Size(139, 19)
            Me.Activate_Fullscreen.TabIndex = 5
            Me.Activate_Fullscreen.Text = "Capture Entire Screen"
            Me.Activate_Fullscreen.UseVisualStyleBackColor = True
            '
            'savebtn
            '
            Me.savebtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.savebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.savebtn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.savebtn.Location = New System.Drawing.Point(338, 311)
            Me.savebtn.Name = "savebtn"
            Me.savebtn.Size = New System.Drawing.Size(122, 30)
            Me.savebtn.TabIndex = 6
            Me.savebtn.Text = "Save"
            Me.savebtn.UseVisualStyleBackColor = True
            '
            'Tabs
            '
            Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Tabs.Controls.Add(Me.SelectorsTab)
            Me.Tabs.Controls.Add(Me.UploadTab)
            Me.Tabs.Controls.Add(Me.SaveLocalTab)
            Me.Tabs.Controls.Add(Me.OthersTab)
            Me.Tabs.Controls.Add(Me.PluginsTab)
            Me.Tabs.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.Tabs.HotTrack = True
            Me.Tabs.Location = New System.Drawing.Point(6, 6)
            Me.Tabs.Margin = New System.Windows.Forms.Padding(12)
            Me.Tabs.Name = "Tabs"
            Me.Tabs.SelectedIndex = 0
            Me.Tabs.Size = New System.Drawing.Size(456, 290)
            Me.Tabs.TabIndex = 8
            '
            'SelectorsTab
            '
            Me.SelectorsTab.Controls.Add(Me.Panel3)
            Me.SelectorsTab.Controls.Add(Me.Panel2)
            Me.SelectorsTab.Controls.Add(Me.Panel1)
            Me.SelectorsTab.Location = New System.Drawing.Point(4, 24)
            Me.SelectorsTab.Name = "SelectorsTab"
            Me.SelectorsTab.Padding = New System.Windows.Forms.Padding(3)
            Me.SelectorsTab.Size = New System.Drawing.Size(448, 262)
            Me.SelectorsTab.TabIndex = 0
            Me.SelectorsTab.Text = "Hotkeys"
            Me.SelectorsTab.UseVisualStyleBackColor = True
            '
            'Panel3
            '
            Me.Panel3.Controls.Add(Me.windowStrokeLabel)
            Me.Panel3.Controls.Add(Me.setWindow)
            Me.Panel3.Controls.Add(Me.Activate_Window)
            Me.Panel3.Location = New System.Drawing.Point(7, 192)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(434, 30)
            Me.Panel3.TabIndex = 11
            '
            'windowStrokeLabel
            '
            Me.windowStrokeLabel.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
            Me.windowStrokeLabel.ForeColor = System.Drawing.SystemColors.ControlText
            Me.windowStrokeLabel.Location = New System.Drawing.Point(156, 0)
            Me.windowStrokeLabel.Name = "windowStrokeLabel"
            Me.windowStrokeLabel.Size = New System.Drawing.Size(190, 30)
            Me.windowStrokeLabel.TabIndex = 11
            Me.windowStrokeLabel.Text = "F10"
            Me.windowStrokeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'setWindow
            '
            Me.setWindow.Location = New System.Drawing.Point(344, 0)
            Me.setWindow.Name = "setWindow"
            Me.setWindow.Size = New System.Drawing.Size(90, 30)
            Me.setWindow.TabIndex = 10
            Me.setWindow.Text = "Set Hotkey"
            Me.setWindow.UseVisualStyleBackColor = True
            '
            'Activate_Window
            '
            Me.Activate_Window.AutoSize = True
            Me.Activate_Window.Location = New System.Drawing.Point(3, 7)
            Me.Activate_Window.Name = "Activate_Window"
            Me.Activate_Window.Size = New System.Drawing.Size(158, 19)
            Me.Activate_Window.TabIndex = 8
            Me.Activate_Window.Text = "Capture Current Window"
            Me.Activate_Window.UseVisualStyleBackColor = True
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.fullStrokeLabel)
            Me.Panel2.Controls.Add(Me.setFullscreen)
            Me.Panel2.Controls.Add(Me.Activate_Fullscreen)
            Me.Panel2.Location = New System.Drawing.Point(7, 127)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(434, 30)
            Me.Panel2.TabIndex = 10
            '
            'fullStrokeLabel
            '
            Me.fullStrokeLabel.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
            Me.fullStrokeLabel.Location = New System.Drawing.Point(148, 1)
            Me.fullStrokeLabel.Name = "fullStrokeLabel"
            Me.fullStrokeLabel.Size = New System.Drawing.Size(195, 30)
            Me.fullStrokeLabel.TabIndex = 10
            Me.fullStrokeLabel.Text = "F9"
            Me.fullStrokeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'setFullscreen
            '
            Me.setFullscreen.Location = New System.Drawing.Point(344, 1)
            Me.setFullscreen.Name = "setFullscreen"
            Me.setFullscreen.Size = New System.Drawing.Size(90, 30)
            Me.setFullscreen.TabIndex = 9
            Me.setFullscreen.Text = "Set Hotkey"
            Me.setFullscreen.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.selectorStrokeLabel)
            Me.Panel1.Controls.Add(Me.setSelector)
            Me.Panel1.Controls.Add(Me.EnableIngameMode)
            Me.Panel1.Controls.Add(Me.Activate_Area)
            Me.Panel1.Location = New System.Drawing.Point(7, 38)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(434, 56)
            Me.Panel1.TabIndex = 9
            '
            'selectorStrokeLabel
            '
            Me.selectorStrokeLabel.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
            Me.selectorStrokeLabel.Location = New System.Drawing.Point(156, 1)
            Me.selectorStrokeLabel.Name = "selectorStrokeLabel"
            Me.selectorStrokeLabel.Size = New System.Drawing.Size(187, 30)
            Me.selectorStrokeLabel.TabIndex = 9
            Me.selectorStrokeLabel.Text = "F8"
            Me.selectorStrokeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'setSelector
            '
            Me.setSelector.Location = New System.Drawing.Point(344, 1)
            Me.setSelector.Name = "setSelector"
            Me.setSelector.Size = New System.Drawing.Size(90, 30)
            Me.setSelector.TabIndex = 8
            Me.setSelector.Text = "Set Hotkey"
            Me.setSelector.UseVisualStyleBackColor = True
            '
            'UploadTab
            '
            Me.UploadTab.Controls.Add(Me.showCopyConfirmation)
            Me.UploadTab.Controls.Add(Me.enableSmartFormatForUpload)
            Me.UploadTab.Controls.Add(Me.disableShotEditorCheckBox)
            Me.UploadTab.Controls.Add(Me.deactivateLinkViewerCheckBox)
            Me.UploadTab.Controls.Add(Me.AutoCloseLinkViewer)
            Me.UploadTab.Controls.Add(Me.enableStatusToaster)
            Me.UploadTab.Controls.Add(Me.Label7)
            Me.UploadTab.Controls.Add(Me.defaultHosterBox)
            Me.UploadTab.Location = New System.Drawing.Point(4, 24)
            Me.UploadTab.Name = "UploadTab"
            Me.UploadTab.Size = New System.Drawing.Size(448, 262)
            Me.UploadTab.TabIndex = 7
            Me.UploadTab.Text = "Upload"
            Me.UploadTab.UseVisualStyleBackColor = True
            '
            'showCopyConfirmation
            '
            Me.showCopyConfirmation.AutoSize = True
            Me.showCopyConfirmation.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.showCopyConfirmation.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.showCopyConfirmation.Location = New System.Drawing.Point(23, 103)
            Me.showCopyConfirmation.Name = "showCopyConfirmation"
            Me.showCopyConfirmation.Size = New System.Drawing.Size(210, 20)
            Me.showCopyConfirmation.TabIndex = 16
            Me.showCopyConfirmation.Text = "Show „Link copied!"" confirmation"
            Me.showCopyConfirmation.UseVisualStyleBackColor = True
            '
            'enableSmartFormatForUpload
            '
            Me.enableSmartFormatForUpload.AutoSize = True
            Me.enableSmartFormatForUpload.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.enableSmartFormatForUpload.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.enableSmartFormatForUpload.Location = New System.Drawing.Point(23, 153)
            Me.enableSmartFormatForUpload.Name = "enableSmartFormatForUpload"
            Me.enableSmartFormatForUpload.Size = New System.Drawing.Size(202, 20)
            Me.enableSmartFormatForUpload.TabIndex = 15
            Me.enableSmartFormatForUpload.Text = "Enable SmartFormat for uploads"
            Me.enableSmartFormatForUpload.UseVisualStyleBackColor = True
            '
            'disableShotEditorCheckBox
            '
            Me.disableShotEditorCheckBox.AutoSize = True
            Me.disableShotEditorCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.disableShotEditorCheckBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.disableShotEditorCheckBox.Location = New System.Drawing.Point(23, 53)
            Me.disableShotEditorCheckBox.Name = "disableShotEditorCheckBox"
            Me.disableShotEditorCheckBox.Size = New System.Drawing.Size(381, 20)
            Me.disableShotEditorCheckBox.TabIndex = 14
            Me.disableShotEditorCheckBox.Text = "Upload image straight to default hoster (disable screenshot editor)"
            Me.disableShotEditorCheckBox.UseVisualStyleBackColor = True
            '
            'deactivateLinkViewerCheckBox
            '
            Me.deactivateLinkViewerCheckBox.AutoSize = True
            Me.deactivateLinkViewerCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.deactivateLinkViewerCheckBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.deactivateLinkViewerCheckBox.Location = New System.Drawing.Point(23, 78)
            Me.deactivateLinkViewerCheckBox.Name = "deactivateLinkViewerCheckBox"
            Me.deactivateLinkViewerCheckBox.Size = New System.Drawing.Size(327, 20)
            Me.deactivateLinkViewerCheckBox.TabIndex = 13
            Me.deactivateLinkViewerCheckBox.Text = "Copy link straight to clipboard (do not show link dialog)"
            Me.deactivateLinkViewerCheckBox.UseVisualStyleBackColor = True
            '
            'AutoCloseLinkViewer
            '
            Me.AutoCloseLinkViewer.AutoSize = True
            Me.AutoCloseLinkViewer.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.AutoCloseLinkViewer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.AutoCloseLinkViewer.Location = New System.Drawing.Point(23, 128)
            Me.AutoCloseLinkViewer.Name = "AutoCloseLinkViewer"
            Me.AutoCloseLinkViewer.Size = New System.Drawing.Size(239, 20)
            Me.AutoCloseLinkViewer.TabIndex = 11
            Me.AutoCloseLinkViewer.Text = "Close link dialog after a link was copied"
            Me.AutoCloseLinkViewer.UseVisualStyleBackColor = True
            '
            'enableStatusToaster
            '
            Me.enableStatusToaster.AutoSize = True
            Me.enableStatusToaster.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.enableStatusToaster.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.enableStatusToaster.Location = New System.Drawing.Point(23, 28)
            Me.enableStatusToaster.Name = "enableStatusToaster"
            Me.enableStatusToaster.Size = New System.Drawing.Size(180, 20)
            Me.enableStatusToaster.TabIndex = 1
            Me.enableStatusToaster.Text = "Enable upload status toaster"
            Me.enableStatusToaster.UseVisualStyleBackColor = True
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.Label7.Location = New System.Drawing.Point(20, 187)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(122, 15)
            Me.Label7.TabIndex = 10
            Me.Label7.Text = "Default Image Hoster:"
            '
            'defaultHosterBox
            '
            Me.defaultHosterBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.defaultHosterBox.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.defaultHosterBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.defaultHosterBox.FormattingEnabled = True
            Me.defaultHosterBox.Location = New System.Drawing.Point(148, 184)
            Me.defaultHosterBox.Name = "defaultHosterBox"
            Me.defaultHosterBox.Size = New System.Drawing.Size(180, 23)
            Me.defaultHosterBox.TabIndex = 4
            '
            'SaveLocalTab
            '
            Me.SaveLocalTab.Controls.Add(Me.enableLocalSaveCheckBox)
            Me.SaveLocalTab.Controls.Add(Me.localSaveSettingsPanel)
            Me.SaveLocalTab.Location = New System.Drawing.Point(4, 24)
            Me.SaveLocalTab.Name = "SaveLocalTab"
            Me.SaveLocalTab.Padding = New System.Windows.Forms.Padding(3)
            Me.SaveLocalTab.Size = New System.Drawing.Size(448, 262)
            Me.SaveLocalTab.TabIndex = 9
            Me.SaveLocalTab.Text = "Saving"
            Me.SaveLocalTab.UseVisualStyleBackColor = True
            '
            'enableLocalSaveCheckBox
            '
            Me.enableLocalSaveCheckBox.AutoSize = True
            Me.enableLocalSaveCheckBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.enableLocalSaveCheckBox.Location = New System.Drawing.Point(23, 29)
            Me.enableLocalSaveCheckBox.Name = "enableLocalSaveCheckBox"
            Me.enableLocalSaveCheckBox.Size = New System.Drawing.Size(125, 19)
            Me.enableLocalSaveCheckBox.TabIndex = 3
            Me.enableLocalSaveCheckBox.Text = "Save all screenhots"
            Me.enableLocalSaveCheckBox.UseVisualStyleBackColor = True
            '
            'localSaveSettingsPanel
            '
            Me.localSaveSettingsPanel.Controls.Add(Me.enableSmartFormatForSaving)
            Me.localSaveSettingsPanel.Controls.Add(Me.fileNamingPatternPreview)
            Me.localSaveSettingsPanel.Controls.Add(Me.fileNamingPatternPreviewLabel)
            Me.localSaveSettingsPanel.Controls.Add(Me.fileNamingPatternLabel)
            Me.localSaveSettingsPanel.Controls.Add(Me.fileNamingPattern)
            Me.localSaveSettingsPanel.Controls.Add(Me.localSavePathBrowseButton)
            Me.localSaveSettingsPanel.Controls.Add(Me.localSavePath)
            Me.localSaveSettingsPanel.Controls.Add(Me.localSavePathLabel)
            Me.localSaveSettingsPanel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.localSaveSettingsPanel.Location = New System.Drawing.Point(7, 52)
            Me.localSaveSettingsPanel.Name = "localSaveSettingsPanel"
            Me.localSaveSettingsPanel.Size = New System.Drawing.Size(416, 205)
            Me.localSaveSettingsPanel.TabIndex = 2
            '
            'enableSmartFormatForSaving
            '
            Me.enableSmartFormatForSaving.AutoSize = True
            Me.enableSmartFormatForSaving.Location = New System.Drawing.Point(16, 70)
            Me.enableSmartFormatForSaving.Name = "enableSmartFormatForSaving"
            Me.enableSmartFormatForSaving.Size = New System.Drawing.Size(220, 19)
            Me.enableSmartFormatForSaving.TabIndex = 7
            Me.enableSmartFormatForSaving.Text = "Enable SmartFormat for local images"
            Me.enableSmartFormatForSaving.UseVisualStyleBackColor = True
            '
            'fileNamingPatternPreview
            '
            Me.fileNamingPatternPreview.AutoSize = True
            Me.fileNamingPatternPreview.Location = New System.Drawing.Point(13, 180)
            Me.fileNamingPatternPreview.Name = "fileNamingPatternPreview"
            Me.fileNamingPatternPreview.Size = New System.Drawing.Size(22, 15)
            Me.fileNamingPatternPreview.TabIndex = 6
            Me.fileNamingPatternPreview.Text = "---"
            '
            'fileNamingPatternPreviewLabel
            '
            Me.fileNamingPatternPreviewLabel.AutoSize = True
            Me.fileNamingPatternPreviewLabel.Location = New System.Drawing.Point(13, 165)
            Me.fileNamingPatternPreviewLabel.Name = "fileNamingPatternPreviewLabel"
            Me.fileNamingPatternPreviewLabel.Size = New System.Drawing.Size(51, 15)
            Me.fileNamingPatternPreviewLabel.TabIndex = 5
            Me.fileNamingPatternPreviewLabel.Text = "Preview:"
            '
            'fileNamingPatternLabel
            '
            Me.fileNamingPatternLabel.AutoSize = True
            Me.fileNamingPatternLabel.Location = New System.Drawing.Point(13, 112)
            Me.fileNamingPatternLabel.Name = "fileNamingPatternLabel"
            Me.fileNamingPatternLabel.Size = New System.Drawing.Size(112, 15)
            Me.fileNamingPatternLabel.TabIndex = 4
            Me.fileNamingPatternLabel.Text = "File Naming Pattern"
            '
            'fileNamingPattern
            '
            Me.fileNamingPattern.Location = New System.Drawing.Point(16, 130)
            Me.fileNamingPattern.Name = "fileNamingPattern"
            Me.fileNamingPattern.Size = New System.Drawing.Size(391, 23)
            Me.fileNamingPattern.TabIndex = 3
            '
            'localSavePathBrowseButton
            '
            Me.localSavePathBrowseButton.Location = New System.Drawing.Point(364, 36)
            Me.localSavePathBrowseButton.Name = "localSavePathBrowseButton"
            Me.localSavePathBrowseButton.Size = New System.Drawing.Size(43, 25)
            Me.localSavePathBrowseButton.TabIndex = 2
            Me.localSavePathBrowseButton.Text = "..."
            Me.localSavePathBrowseButton.UseVisualStyleBackColor = True
            '
            'localSavePath
            '
            Me.localSavePath.Location = New System.Drawing.Point(16, 38)
            Me.localSavePath.Name = "localSavePath"
            Me.localSavePath.Size = New System.Drawing.Size(342, 23)
            Me.localSavePath.TabIndex = 1
            '
            'localSavePathLabel
            '
            Me.localSavePathLabel.AutoSize = True
            Me.localSavePathLabel.Location = New System.Drawing.Point(13, 20)
            Me.localSavePathLabel.Name = "localSavePathLabel"
            Me.localSavePathLabel.Size = New System.Drawing.Size(58, 15)
            Me.localSavePathLabel.TabIndex = 0
            Me.localSavePathLabel.Text = "Directory:"
            '
            'OthersTab
            '
            Me.OthersTab.Controls.Add(Me.elevatedRequiredPictureBox2)
            Me.OthersTab.Controls.Add(Me.elevatedRequiredPictureBox1)
            Me.OthersTab.Controls.Add(Me.openImageInExplorerMenu)
            Me.OthersTab.Controls.Add(Me.uploadImageInExplorerMenu)
            Me.OthersTab.Controls.Add(Me.start_with_windows)
            Me.OthersTab.Location = New System.Drawing.Point(4, 24)
            Me.OthersTab.Name = "OthersTab"
            Me.OthersTab.Size = New System.Drawing.Size(448, 262)
            Me.OthersTab.TabIndex = 4
            Me.OthersTab.Text = "Misc"
            Me.OthersTab.UseVisualStyleBackColor = True
            '
            'elevatedRequiredPictureBox2
            '
            Me.elevatedRequiredPictureBox2.Location = New System.Drawing.Point(17, 81)
            Me.elevatedRequiredPictureBox2.Name = "elevatedRequiredPictureBox2"
            Me.elevatedRequiredPictureBox2.Size = New System.Drawing.Size(16, 16)
            Me.elevatedRequiredPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.elevatedRequiredPictureBox2.TabIndex = 14
            Me.elevatedRequiredPictureBox2.TabStop = False
            '
            'elevatedRequiredPictureBox1
            '
            Me.elevatedRequiredPictureBox1.Location = New System.Drawing.Point(17, 56)
            Me.elevatedRequiredPictureBox1.Name = "elevatedRequiredPictureBox1"
            Me.elevatedRequiredPictureBox1.Size = New System.Drawing.Size(16, 16)
            Me.elevatedRequiredPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.elevatedRequiredPictureBox1.TabIndex = 13
            Me.elevatedRequiredPictureBox1.TabStop = False
            '
            'openImageInExplorerMenu
            '
            Me.openImageInExplorerMenu.AutoSize = True
            Me.openImageInExplorerMenu.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.openImageInExplorerMenu.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.openImageInExplorerMenu.Location = New System.Drawing.Point(43, 80)
            Me.openImageInExplorerMenu.Name = "openImageInExplorerMenu"
            Me.openImageInExplorerMenu.Size = New System.Drawing.Size(267, 20)
            Me.openImageInExplorerMenu.TabIndex = 12
            Me.openImageInExplorerMenu.Text = "Add „ShotEditor"" to explorer's context menu"
            Me.openImageInExplorerMenu.UseVisualStyleBackColor = True
            '
            'uploadImageInExplorerMenu
            '
            Me.uploadImageInExplorerMenu.AutoSize = True
            Me.uploadImageInExplorerMenu.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.uploadImageInExplorerMenu.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.uploadImageInExplorerMenu.Location = New System.Drawing.Point(43, 54)
            Me.uploadImageInExplorerMenu.Name = "uploadImageInExplorerMenu"
            Me.uploadImageInExplorerMenu.Size = New System.Drawing.Size(250, 20)
            Me.uploadImageInExplorerMenu.TabIndex = 11
            Me.uploadImageInExplorerMenu.Text = "Add „Upload"" to explorer's context menu"
            Me.uploadImageInExplorerMenu.UseVisualStyleBackColor = True
            '
            'start_with_windows
            '
            Me.start_with_windows.AutoSize = True
            Me.start_with_windows.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.start_with_windows.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.start_with_windows.Location = New System.Drawing.Point(43, 28)
            Me.start_with_windows.Name = "start_with_windows"
            Me.start_with_windows.Size = New System.Drawing.Size(190, 20)
            Me.start_with_windows.TabIndex = 0
            Me.start_with_windows.Text = "Start HolzShots with Windows"
            Me.start_with_windows.UseVisualStyleBackColor = True
            '
            'PluginsTab
            '
            Me.PluginsTab.Controls.Add(Me.pluginListPanel)
            Me.PluginsTab.Controls.Add(Me.openPluginFolderLinkLabel)
            Me.PluginsTab.Controls.Add(Me.Label10)
            Me.PluginsTab.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.PluginsTab.Location = New System.Drawing.Point(4, 24)
            Me.PluginsTab.Margin = New System.Windows.Forms.Padding(0)
            Me.PluginsTab.Name = "PluginsTab"
            Me.PluginsTab.Size = New System.Drawing.Size(448, 262)
            Me.PluginsTab.TabIndex = 8
            Me.PluginsTab.Text = "Plugins"
            Me.PluginsTab.UseVisualStyleBackColor = True
            '
            'pluginListPanel
            '
            Me.pluginListPanel.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pluginListPanel.Location = New System.Drawing.Point(0, 34)
            Me.pluginListPanel.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
            Me.pluginListPanel.Name = "pluginListPanel"
            Me.pluginListPanel.Padding = New System.Windows.Forms.Padding(0, 1, 0, 0)
            Me.pluginListPanel.Size = New System.Drawing.Size(448, 228)
            Me.pluginListPanel.TabIndex = 20
            '
            'openPluginFolderLinkLabel
            '
            Me.openPluginFolderLinkLabel.ActiveLinkColor = System.Drawing.SystemColors.Highlight
            Me.openPluginFolderLinkLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.openPluginFolderLinkLabel.AutoSize = True
            Me.openPluginFolderLinkLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.openPluginFolderLinkLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.openPluginFolderLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.openPluginFolderLinkLabel.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.openPluginFolderLinkLabel.Location = New System.Drawing.Point(330, 10)
            Me.openPluginFolderLinkLabel.Name = "openPluginFolderLinkLabel"
            Me.openPluginFolderLinkLabel.Size = New System.Drawing.Size(107, 15)
            Me.openPluginFolderLinkLabel.TabIndex = 17
            Me.openPluginFolderLinkLabel.TabStop = True
            Me.openPluginFolderLinkLabel.Text = "Open plugin folder"
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.Label10.Location = New System.Drawing.Point(2, 11)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(91, 15)
            Me.Label10.TabIndex = 6
            Me.Label10.Text = "Loaded Plugins:"
            '
            'Abort
            '
            Me.Abort.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Abort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.Abort.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Abort.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.Abort.Location = New System.Drawing.Point(210, 311)
            Me.Abort.Name = "Abort"
            Me.Abort.Size = New System.Drawing.Size(122, 30)
            Me.Abort.TabIndex = 7
            Me.Abort.Text = "Cancel"
            Me.Abort.UseVisualStyleBackColor = True
            '
            'OpenSettingsJson
            '
            Me.OpenSettingsJson.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OpenSettingsJson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.OpenSettingsJson.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.OpenSettingsJson.Location = New System.Drawing.Point(6, 311)
            Me.OpenSettingsJson.Name = "OpenSettingsJson"
            Me.OpenSettingsJson.Size = New System.Drawing.Size(142, 30)
            Me.OpenSettingsJson.TabIndex = 10
            Me.OpenSettingsJson.Text = "Open Settings (JSON)"
            Me.OpenSettingsJson.UseVisualStyleBackColor = True
            '
            'SettingsWindow
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.CancelButton = Me.Abort
            Me.ClientSize = New System.Drawing.Size(466, 353)
            Me.Controls.Add(Me.OpenSettingsJson)
            Me.Controls.Add(Me.Tabs)
            Me.Controls.Add(Me.Abort)
            Me.Controls.Add(Me.savebtn)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "SettingsWindow"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Settings - HolzShots"
            Me.Tabs.ResumeLayout(False)
            Me.SelectorsTab.ResumeLayout(False)
            Me.Panel3.ResumeLayout(False)
            Me.Panel3.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.UploadTab.ResumeLayout(False)
            Me.UploadTab.PerformLayout()
            Me.SaveLocalTab.ResumeLayout(False)
            Me.SaveLocalTab.PerformLayout()
            Me.localSaveSettingsPanel.ResumeLayout(False)
            Me.localSaveSettingsPanel.PerformLayout()
            Me.OthersTab.ResumeLayout(False)
            Me.OthersTab.PerformLayout()
            CType(Me.elevatedRequiredPictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.elevatedRequiredPictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.PluginsTab.ResumeLayout(False)
            Me.PluginsTab.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents savebtn As System.Windows.Forms.Button
        Friend WithEvents Tabs As System.Windows.Forms.TabControl
        Friend WithEvents SelectorsTab As System.Windows.Forms.TabPage
        Friend WithEvents EnableIngameMode As System.Windows.Forms.CheckBox
        Friend WithEvents Abort As System.Windows.Forms.Button
        Friend WithEvents OthersTab As System.Windows.Forms.TabPage
        Friend WithEvents start_with_windows As System.Windows.Forms.CheckBox
        Friend WithEvents enableStatusToaster As System.Windows.Forms.CheckBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents defaultHosterBox As System.Windows.Forms.ComboBox
        Friend WithEvents Activate_Area As System.Windows.Forms.CheckBox
        Friend WithEvents Activate_Fullscreen As System.Windows.Forms.CheckBox
        Friend WithEvents Activate_Window As System.Windows.Forms.CheckBox
        Friend WithEvents UploadTab As System.Windows.Forms.TabPage
        Friend WithEvents AutoCloseLinkViewer As System.Windows.Forms.CheckBox
        Friend WithEvents PluginsTab As System.Windows.Forms.TabPage
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents deactivateLinkViewerCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents openImageInExplorerMenu As System.Windows.Forms.CheckBox
        Friend WithEvents uploadImageInExplorerMenu As System.Windows.Forms.CheckBox
        Friend WithEvents elevatedRequiredPictureBox1 As System.Windows.Forms.PictureBox
        Friend WithEvents elevatedRequiredPictureBox2 As System.Windows.Forms.PictureBox
        Friend WithEvents disableShotEditorCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents SaveLocalTab As System.Windows.Forms.TabPage
        Friend WithEvents localSaveSettingsPanel As System.Windows.Forms.Panel
        Friend WithEvents localSavePath As System.Windows.Forms.TextBox
        Friend WithEvents localSavePathLabel As System.Windows.Forms.Label
        Friend WithEvents localSavePathBrowseButton As System.Windows.Forms.Button
        Friend WithEvents enableLocalSaveCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents fileNamingPatternLabel As System.Windows.Forms.Label
        Friend WithEvents fileNamingPattern As System.Windows.Forms.TextBox
        Friend WithEvents fileNamingPatternPreview As System.Windows.Forms.Label
        Friend WithEvents fileNamingPatternPreviewLabel As System.Windows.Forms.Label
        Friend WithEvents enableSmartFormatForSaving As System.Windows.Forms.CheckBox
        Friend WithEvents enableSmartFormatForUpload As System.Windows.Forms.CheckBox
        Friend WithEvents openPluginFolderLinkLabel As HolzShots.UI.Forms.ExplorerLinkLabel
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents setSelector As System.Windows.Forms.Button
        Friend WithEvents setFullscreen As System.Windows.Forms.Button
        Friend WithEvents setWindow As System.Windows.Forms.Button
        Friend WithEvents windowStrokeLabel As System.Windows.Forms.Label
        Friend WithEvents fullStrokeLabel As System.Windows.Forms.Label
        Friend WithEvents selectorStrokeLabel As System.Windows.Forms.Label
        Friend WithEvents pluginListPanel As HolzShots.UI.Controls.StackPanel
        Friend WithEvents showCopyConfirmation As CheckBox
        Friend WithEvents OpenSettingsJson As Button
    End Class
End Namespace
