Namespace UI.Specialized
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MainWindow
        Inherits System.Windows.Forms.Form

        'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
        <System.Diagnostics.DebuggerNonUserCode()> _
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
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
            Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
            Me.trayIconMenu = New System.Windows.Forms.ContextMenu()
            Me.extrasMenuItem = New System.Windows.Forms.MenuItem()
            Me.aboutMenuItem = New System.Windows.Forms.MenuItem()
            Me.feedbackMenuItem = New System.Windows.Forms.MenuItem()
            Me.Separator1 = New System.Windows.Forms.MenuItem()
            Me.StartWithWindows = New System.Windows.Forms.MenuItem()
            Me.Separator2 = New System.Windows.Forms.MenuItem()
            Me.uploadImageMenuItem = New System.Windows.Forms.MenuItem()
            Me.openImageMenuItem = New System.Windows.Forms.MenuItem()
            Me.selectAreaMenuItem = New System.Windows.Forms.MenuItem()
            Me.settingsMenuItem = New System.Windows.Forms.MenuItem()
            Me.settingsJsonMenuItem = New System.Windows.Forms.MenuItem()
            Me.Separator3 = New System.Windows.Forms.MenuItem()
            Me.exitMenuItem = New System.Windows.Forms.MenuItem()
            Me.SuspendLayout()
            '
            'TrayIcon
            '
            Me.TrayIcon.Icon = CType(resources.GetObject("TrayIcon.Icon"), System.Drawing.Icon)
            Me.TrayIcon.Text = "HolzShots"
            Me.TrayIcon.Visible = True
            '
            'trayIconMenu
            '
            Me.trayIconMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.extrasMenuItem, Me.Separator1, Me.StartWithWindows, Me.Separator2, Me.uploadImageMenuItem, Me.openImageMenuItem, Me.selectAreaMenuItem, Me.settingsMenuItem, Me.settingsJsonMenuItem, Me.Separator3, Me.exitMenuItem})
            '
            'extrasMenuItem
            '
            Me.extrasMenuItem.Index = 0
            Me.extrasMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.aboutMenuItem, Me.feedbackMenuItem})
            Me.extrasMenuItem.Text = "Extras"
            '
            'aboutMenuItem
            '
            Me.aboutMenuItem.Index = 0
            Me.aboutMenuItem.Text = "About"
            '
            'feedbackMenuItem
            '
            Me.feedbackMenuItem.Index = 1
            Me.feedbackMenuItem.Text = "Feedback"
            '
            'Separator1
            '
            Me.Separator1.Index = 1
            Me.Separator1.Text = "-"
            '
            'StartWithWindows
            '
            Me.StartWithWindows.Index = 2
            Me.StartWithWindows.Text = "Start with Windows"
            '
            'Separator2
            '
            Me.Separator2.Index = 3
            Me.Separator2.Text = "-"
            '
            'uploadImageMenuItem
            '
            Me.uploadImageMenuItem.Index = 4
            Me.uploadImageMenuItem.Text = "Upload image"
            '
            'openImageMenuItem
            '
            Me.openImageMenuItem.Index = 5
            Me.openImageMenuItem.Text = "Open image"
            '
            'selectAreaMenuItem
            '
            Me.selectAreaMenuItem.Index = 6
            Me.selectAreaMenuItem.Text = "Select Area"
            '
            'settingsMenuItem
            '
            Me.settingsMenuItem.Index = 7
            Me.settingsMenuItem.Text = "Settings"
            '
            'settingsJsonMenuItem
            '
            Me.settingsJsonMenuItem.Index = 8
            Me.settingsJsonMenuItem.Text = "Settings (JSON)"
            '
            'Separator3
            '
            Me.Separator3.Index = 9
            Me.Separator3.Text = "-"
            '
            'exitMenuItem
            '
            Me.exitMenuItem.Index = 10
            Me.exitMenuItem.Text = "Exit"
            '
            'MainWindow
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(91, 182)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "MainWindow"
            Me.Text = "MainWindow"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents TrayIcon As System.Windows.Forms.NotifyIcon
        Friend WithEvents trayIconMenu As System.Windows.Forms.ContextMenu
        Friend WithEvents extrasMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents settingsMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents Separator3 As System.Windows.Forms.MenuItem
        Friend WithEvents aboutMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents feedbackMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents uploadImageMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents openImageMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents selectAreaMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents exitMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents settingsJsonMenuItem As MenuItem
        Friend WithEvents StartWithWindows As MenuItem
        Friend WithEvents Separator1 As MenuItem
        Friend WithEvents Separator2 As MenuItem
    End Class
End Namespace