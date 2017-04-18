Namespace UI.Controls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class PluginInfoItem
        Inherits System.Windows.Forms.UserControl

        'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
            Me.pluginNameLabel = New System.Windows.Forms.Label()
            Me.pluginVersion = New System.Windows.Forms.Label()
            Me.pluginAuthor = New System.Windows.Forms.Label()
            Me.pluginSettings = New HolzShots.UI.Windows.Forms.ExplorerLinkLabel()
            Me.reportBug = New HolzShots.UI.Windows.Forms.ExplorerLinkLabel()
            Me.authorWebSite = New HolzShots.UI.Windows.Forms.ExplorerLinkLabel()
            Me.SuspendLayout()
            '
            'pluginNameLabel
            '
            Me.pluginNameLabel.AutoSize = True
            Me.pluginNameLabel.BackColor = System.Drawing.Color.Transparent
            Me.pluginNameLabel.Font = New System.Drawing.Font("Segoe UI", 11.0!)
            Me.pluginNameLabel.Location = New System.Drawing.Point(14, 10)
            Me.pluginNameLabel.Name = "pluginNameLabel"
            Me.pluginNameLabel.Size = New System.Drawing.Size(115, 20)
            Me.pluginNameLabel.TabIndex = 0
            Me.pluginNameLabel.Text = "Den Plugin - yo!"
            '
            'pluginVersion
            '
            Me.pluginVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pluginVersion.BackColor = System.Drawing.Color.Transparent
            Me.pluginVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.pluginVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
            Me.pluginVersion.Location = New System.Drawing.Point(250, 14)
            Me.pluginVersion.Name = "pluginVersion"
            Me.pluginVersion.Size = New System.Drawing.Size(72, 15)
            Me.pluginVersion.TabIndex = 4
            Me.pluginVersion.Text = "1.3.3.7"
            Me.pluginVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'pluginAuthor
            '
            Me.pluginAuthor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.pluginAuthor.AutoSize = True
            Me.pluginAuthor.BackColor = System.Drawing.Color.Transparent
            Me.pluginAuthor.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.pluginAuthor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
            Me.pluginAuthor.Location = New System.Drawing.Point(15, 34)
            Me.pluginAuthor.Name = "pluginAuthor"
            Me.pluginAuthor.Size = New System.Drawing.Size(109, 15)
            Me.pluginAuthor.TabIndex = 5
            Me.pluginAuthor.Text = "Niklas Mollenhauer"
            '
            'pluginSettings
            '
            Me.pluginSettings.ActiveLinkColor = System.Drawing.SystemColors.Highlight
            Me.pluginSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pluginSettings.AutoSize = True
            Me.pluginSettings.BackColor = System.Drawing.Color.Transparent
            Me.pluginSettings.Cursor = System.Windows.Forms.Cursors.Hand
            Me.pluginSettings.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.pluginSettings.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.pluginSettings.Location = New System.Drawing.Point(176, 34)
            Me.pluginSettings.Name = "pluginSettings"
            Me.pluginSettings.Size = New System.Drawing.Size(49, 15)
            Me.pluginSettings.TabIndex = 3
            Me.pluginSettings.TabStop = True
            Me.pluginSettings.Text = "Settings"
            '
            'reportBug
            '
            Me.reportBug.ActiveLinkColor = System.Drawing.SystemColors.Highlight
            Me.reportBug.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.reportBug.AutoSize = True
            Me.reportBug.BackColor = System.Drawing.Color.Transparent
            Me.reportBug.Cursor = System.Windows.Forms.Cursors.Hand
            Me.reportBug.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.reportBug.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.reportBug.Location = New System.Drawing.Point(292, 34)
            Me.reportBug.Name = "reportBug"
            Me.reportBug.Size = New System.Drawing.Size(33, 15)
            Me.reportBug.TabIndex = 2
            Me.reportBug.TabStop = True
            Me.reportBug.Text = "Bugs"
            '
            'authorWebSite
            '
            Me.authorWebSite.ActiveLinkColor = System.Drawing.SystemColors.Highlight
            Me.authorWebSite.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.authorWebSite.AutoSize = True
            Me.authorWebSite.BackColor = System.Drawing.Color.Transparent
            Me.authorWebSite.Cursor = System.Windows.Forms.Cursors.Hand
            Me.authorWebSite.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.authorWebSite.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.authorWebSite.Location = New System.Drawing.Point(231, 34)
            Me.authorWebSite.Name = "authorWebSite"
            Me.authorWebSite.Size = New System.Drawing.Size(55, 15)
            Me.authorWebSite.TabIndex = 1
            Me.authorWebSite.TabStop = True
            Me.authorWebSite.Text = "Webseite"
            '
            'PluginInfoItem
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.Controls.Add(Me.pluginAuthor)
            Me.Controls.Add(Me.pluginVersion)
            Me.Controls.Add(Me.pluginSettings)
            Me.Controls.Add(Me.reportBug)
            Me.Controls.Add(Me.authorWebSite)
            Me.Controls.Add(Me.pluginNameLabel)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Margin = New System.Windows.Forms.Padding(0)
            Me.Name = "PluginInfoItem"
            Me.Size = New System.Drawing.Size(330, 52)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents pluginNameLabel As System.Windows.Forms.Label
        Friend WithEvents authorWebSite As HolzShots.UI.Windows.Forms.ExplorerLinkLabel
        Friend WithEvents reportBug As HolzShots.UI.Windows.Forms.ExplorerLinkLabel
        Friend WithEvents pluginSettings As HolzShots.UI.Windows.Forms.ExplorerLinkLabel
        Friend WithEvents pluginVersion As System.Windows.Forms.Label
        Friend WithEvents pluginAuthor As System.Windows.Forms.Label

    End Class
End Namespace
