

Namespace UI

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class AboutForm
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.holzShotsLinkLabel = New HolzShots.UI.Forms.ExplorerLinkLabel()
            Me.showGfxResourcesLinklabel = New HolzShots.UI.Forms.ExplorerLinkLabel()
            Me.versionLabel = New System.Windows.Forms.Label()
            Me.timestampLabel = New System.Windows.Forms.Label()
            Me.applicationTitleLabel = New System.Windows.Forms.Label()
            Me.LicenseLabel = New HolzShots.UI.Forms.ExplorerLinkLabel()
            Me.SuspendLayout()
            '
            'Label4
            '
            Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label4.AutoSize = True
            Me.Label4.BackColor = System.Drawing.Color.Transparent
            Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(121, Byte), Integer))
            Me.Label4.Location = New System.Drawing.Point(22, 76)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(48, 15)
            Me.Label4.TabIndex = 43
            Me.Label4.Text = "Version:"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'holzShotsLinkLabel
            '
            Me.holzShotsLinkLabel.ActiveLinkColor = System.Drawing.SystemColors.Highlight
            Me.holzShotsLinkLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.holzShotsLinkLabel.AutoSize = True
            Me.holzShotsLinkLabel.BackColor = System.Drawing.Color.Transparent
            Me.holzShotsLinkLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.holzShotsLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.holzShotsLinkLabel.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.holzShotsLinkLabel.Location = New System.Drawing.Point(173, 114)
            Me.holzShotsLinkLabel.Name = "holzShotsLinkLabel"
            Me.holzShotsLinkLabel.Size = New System.Drawing.Size(77, 13)
            Me.holzShotsLinkLabel.TabIndex = 34
            Me.holzShotsLinkLabel.TabStop = True
            Me.holzShotsLinkLabel.Text = "holzshots.net"
            '
            'showGfxResourcesLinklabel
            '
            Me.showGfxResourcesLinklabel.ActiveLinkColor = System.Drawing.SystemColors.Highlight
            Me.showGfxResourcesLinklabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.showGfxResourcesLinklabel.AutoSize = True
            Me.showGfxResourcesLinklabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.showGfxResourcesLinklabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.showGfxResourcesLinklabel.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.showGfxResourcesLinklabel.Location = New System.Drawing.Point(117, 114)
            Me.showGfxResourcesLinklabel.Name = "showGfxResourcesLinklabel"
            Me.showGfxResourcesLinklabel.Size = New System.Drawing.Size(52, 13)
            Me.showGfxResourcesLinklabel.TabIndex = 55
            Me.showGfxResourcesLinklabel.TabStop = True
            Me.showGfxResourcesLinklabel.Text = "Graphics"
            '
            'versionLabel
            '
            Me.versionLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.versionLabel.AutoSize = True
            Me.versionLabel.BackColor = System.Drawing.Color.Transparent
            Me.versionLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.versionLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(91, Byte), Integer))
            Me.versionLabel.Location = New System.Drawing.Point(68, 76)
            Me.versionLabel.Name = "versionLabel"
            Me.versionLabel.Size = New System.Drawing.Size(105, 30)
            Me.versionLabel.TabIndex = 42
            Me.versionLabel.Text = "0.9.8.7 (11.08.2013)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Alpha" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
            '
            'timestampLabel
            '
            Me.timestampLabel.AutoSize = True
            Me.timestampLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.timestampLabel.Location = New System.Drawing.Point(17, 54)
            Me.timestampLabel.Name = "timestampLabel"
            Me.timestampLabel.Size = New System.Drawing.Size(179, 15)
            Me.timestampLabel.TabIndex = 56
            Me.timestampLabel.Text = "© 2010-2017 Niklas Mollenhauer"
            '
            'applicationTitleLabel
            '
            Me.applicationTitleLabel.AutoSize = True
            Me.applicationTitleLabel.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.applicationTitleLabel.Location = New System.Drawing.Point(12, 9)
            Me.applicationTitleLabel.Name = "applicationTitleLabel"
            Me.applicationTitleLabel.Size = New System.Drawing.Size(163, 45)
            Me.applicationTitleLabel.TabIndex = 57
            Me.applicationTitleLabel.Text = "HolzShots"
            '
            'LicenseLabel
            '
            Me.LicenseLabel.ActiveLinkColor = System.Drawing.SystemColors.Highlight
            Me.LicenseLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LicenseLabel.AutoSize = True
            Me.LicenseLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.LicenseLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.LicenseLabel.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.LicenseLabel.Location = New System.Drawing.Point(68, 114)
            Me.LicenseLabel.Name = "LicenseLabel"
            Me.LicenseLabel.Size = New System.Drawing.Size(44, 13)
            Me.LicenseLabel.TabIndex = 58
            Me.LicenseLabel.TabStop = True
            Me.LicenseLabel.Text = "License"
            '
            'AboutForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.BackColor = System.Drawing.Color.White
            Me.ClientSize = New System.Drawing.Size(262, 136)
            Me.Controls.Add(Me.LicenseLabel)
            Me.Controls.Add(Me.applicationTitleLabel)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.timestampLabel)
            Me.Controls.Add(Me.holzShotsLinkLabel)
            Me.Controls.Add(Me.showGfxResourcesLinklabel)
            Me.Controls.Add(Me.versionLabel)
            Me.DoubleBuffered = True
            Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "AboutForm"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "About HolzShots"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents holzShotsLinkLabel As HolzShots.UI.Forms.ExplorerLinkLabel
        Friend WithEvents versionLabel As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents showGfxResourcesLinklabel As HolzShots.UI.Forms.ExplorerLinkLabel
        Friend WithEvents timestampLabel As System.Windows.Forms.Label
        Friend WithEvents LicenseLabel As HolzShots.UI.Forms.ExplorerLinkLabel
        Friend WithEvents applicationTitleLabel As Label
    End Class
End Namespace
