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
            Me.savebtn = New System.Windows.Forms.Button()
            Me.Abort = New System.Windows.Forms.Button()
            Me.PluginsTab = New System.Windows.Forms.TabPage()
            Me.pluginListPanel = New HolzShots.UI.Controls.StackPanel()
            Me.openPluginFolderLinkLabel = New HolzShots.UI.Forms.ExplorerLinkLabel()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.OthersTab = New System.Windows.Forms.TabPage()
            Me.start_with_windows = New System.Windows.Forms.CheckBox()
            Me.Tabs = New System.Windows.Forms.TabControl()
            Me.PluginsTab.SuspendLayout()
            Me.OthersTab.SuspendLayout()
            Me.Tabs.SuspendLayout()
            Me.SuspendLayout()
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
            'OthersTab
            '
            Me.OthersTab.Controls.Add(Me.start_with_windows)
            Me.OthersTab.Location = New System.Drawing.Point(4, 24)
            Me.OthersTab.Name = "OthersTab"
            Me.OthersTab.Size = New System.Drawing.Size(448, 262)
            Me.OthersTab.TabIndex = 4
            Me.OthersTab.Text = "Misc"
            Me.OthersTab.UseVisualStyleBackColor = True
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
            'Tabs
            '
            Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
            'SettingsWindow
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.CancelButton = Me.Abort
            Me.ClientSize = New System.Drawing.Size(466, 353)
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
            Me.PluginsTab.ResumeLayout(False)
            Me.PluginsTab.PerformLayout()
            Me.OthersTab.ResumeLayout(False)
            Me.OthersTab.PerformLayout()
            Me.Tabs.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents savebtn As System.Windows.Forms.Button
        Friend WithEvents Abort As System.Windows.Forms.Button
        Friend WithEvents PluginsTab As TabPage
        Friend WithEvents pluginListPanel As Controls.StackPanel
        Friend WithEvents openPluginFolderLinkLabel As ExplorerLinkLabel
        Friend WithEvents Label10 As Label
        Friend WithEvents OthersTab As TabPage
        Friend WithEvents start_with_windows As CheckBox
        Friend WithEvents Tabs As TabControl
    End Class
End Namespace
