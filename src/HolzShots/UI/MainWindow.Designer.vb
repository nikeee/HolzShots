Namespace UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class MainWindow
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
            Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
            Me.trayMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.ExtrasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.FeedbackAndIssuesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
            Me.UploadImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.OpenImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SelectAreaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
            Me.StartWithWindowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.PluginsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.OpenSettingsjsonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.trayMenu.SuspendLayout()
            Me.SuspendLayout()
            '
            'TrayIcon
            '
            Me.TrayIcon.Icon = CType(resources.GetObject("TrayIcon.Icon"), System.Drawing.Icon)
            Me.TrayIcon.Text = "HolzShots"
            Me.TrayIcon.Visible = True
            '
            'trayMenu
            '
            Me.trayMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExtrasToolStripMenuItem, Me.ToolStripSeparator2, Me.UploadImageToolStripMenuItem, Me.OpenImageToolStripMenuItem, Me.SelectAreaToolStripMenuItem, Me.ToolStripSeparator3, Me.StartWithWindowsToolStripMenuItem, Me.PluginsToolStripMenuItem, Me.OpenSettingsjsonToolStripMenuItem, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem})
            Me.trayMenu.Name = "trayMenu"
            Me.trayMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
            Me.trayMenu.Size = New System.Drawing.Size(177, 198)
            '
            'ExtrasToolStripMenuItem
            '
            Me.ExtrasToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem, Me.FeedbackAndIssuesToolStripMenuItem})
            Me.ExtrasToolStripMenuItem.Name = "ExtrasToolStripMenuItem"
            Me.ExtrasToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
            Me.ExtrasToolStripMenuItem.Text = "Extras"
            '
            'AboutToolStripMenuItem
            '
            Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
            Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
            Me.AboutToolStripMenuItem.Text = "About"
            '
            'FeedbackAndIssuesToolStripMenuItem
            '
            Me.FeedbackAndIssuesToolStripMenuItem.Name = "FeedbackAndIssuesToolStripMenuItem"
            Me.FeedbackAndIssuesToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
            Me.FeedbackAndIssuesToolStripMenuItem.Text = "Feedback and Issues"
            '
            'ToolStripSeparator2
            '
            Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
            Me.ToolStripSeparator2.Size = New System.Drawing.Size(173, 6)
            '
            'UploadImageToolStripMenuItem
            '
            Me.UploadImageToolStripMenuItem.Name = "UploadImageToolStripMenuItem"
            Me.UploadImageToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
            Me.UploadImageToolStripMenuItem.Text = "Upload Image"
            '
            'OpenImageToolStripMenuItem
            '
            Me.OpenImageToolStripMenuItem.Name = "OpenImageToolStripMenuItem"
            Me.OpenImageToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
            Me.OpenImageToolStripMenuItem.Text = "Open Image"
            '
            'SelectAreaToolStripMenuItem
            '
            Me.SelectAreaToolStripMenuItem.Name = "SelectAreaToolStripMenuItem"
            Me.SelectAreaToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
            Me.SelectAreaToolStripMenuItem.Text = "Select Area"
            '
            'ToolStripSeparator3
            '
            Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
            Me.ToolStripSeparator3.Size = New System.Drawing.Size(173, 6)
            '
            'StartWithWindowsToolStripMenuItem
            '
            Me.StartWithWindowsToolStripMenuItem.Name = "StartWithWindowsToolStripMenuItem"
            Me.StartWithWindowsToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
            Me.StartWithWindowsToolStripMenuItem.Text = "Start with Windows"
            '
            'PluginsToolStripMenuItem
            '
            Me.PluginsToolStripMenuItem.Name = "PluginsToolStripMenuItem"
            Me.PluginsToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
            Me.PluginsToolStripMenuItem.Text = "Plugins"
            '
            'OpenSettingsjsonToolStripMenuItem
            '
            Me.OpenSettingsjsonToolStripMenuItem.Name = "OpenSettingsjsonToolStripMenuItem"
            Me.OpenSettingsjsonToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
            Me.OpenSettingsjsonToolStripMenuItem.Text = "Open settings.json"
            '
            'ToolStripSeparator1
            '
            Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
            Me.ToolStripSeparator1.Size = New System.Drawing.Size(173, 6)
            '
            'ExitToolStripMenuItem
            '
            Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
            Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
            Me.ExitToolStripMenuItem.Text = "Exit"
            '
            'MainWindow
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(106, 32)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
            Me.Name = "MainWindow"
            Me.Text = "MainWindow"
            Me.trayMenu.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents TrayIcon As System.Windows.Forms.NotifyIcon
        Friend WithEvents trayMenu As ContextMenuStrip
        Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents OpenSettingsjsonToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents UploadImageToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents OpenImageToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents SelectAreaToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents StartWithWindowsToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents ExtrasToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents FeedbackAndIssuesToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
        Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
        Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
        Friend WithEvents PluginsToolStripMenuItem As ToolStripMenuItem
    End Class
End Namespace
