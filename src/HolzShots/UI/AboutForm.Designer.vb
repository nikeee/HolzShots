

Namespace UI

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AboutForm
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.headerImage = New System.Windows.Forms.PictureBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Panel1 = New HolzShots.UI.Windows.Forms.ExplorerInfoPanel()
            Me.holzShotsLinkLabel = New HolzShots.UI.Windows.Forms.ExplorerLinkLabel()
            Me.showGfxResourcesLinklabel = New HolzShots.UI.Windows.Forms.ExplorerLinkLabel()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.versionLabel = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label15 = New System.Windows.Forms.Label()
            Me.youtubePlaylistLinkLabel = New HolzShots.UI.Windows.Forms.ExplorerLinkLabel()
            Me.timestampLabel = New System.Windows.Forms.Label()
            CType(Me.headerImage, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'headerImage
            '
            Me.headerImage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.headerImage.BackColor = System.Drawing.Color.Transparent
            Me.headerImage.Image = Global.HolzShots.My.Resources.Resources.Header
            Me.headerImage.Location = New System.Drawing.Point(12, 12)
            Me.headerImage.Name = "headerImage"
            Me.headerImage.Size = New System.Drawing.Size(250, 70)
            Me.headerImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.headerImage.TabIndex = 0
            Me.headerImage.TabStop = False
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.ForeColor = System.Drawing.Color.Navy
            Me.Label2.Location = New System.Drawing.Point(20, 91)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(114, 21)
            Me.Label2.TabIndex = 35
            Me.Label2.Text = "Vielen Dank an"
            '
            'Panel1
            '
            Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Controls.Add(Me.holzShotsLinkLabel)
            Me.Panel1.Controls.Add(Me.showGfxResourcesLinklabel)
            Me.Panel1.Controls.Add(Me.versionLabel)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(91, Byte), Integer))
            Me.Panel1.Location = New System.Drawing.Point(0, 183)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(299, 60)
            Me.Panel1.TabIndex = 31
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
            Me.holzShotsLinkLabel.Location = New System.Drawing.Point(210, 36)
            Me.holzShotsLinkLabel.Name = "holzShotsLinkLabel"
            Me.holzShotsLinkLabel.Size = New System.Drawing.Size(77, 15)
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
            Me.showGfxResourcesLinklabel.Location = New System.Drawing.Point(154, 36)
            Me.showGfxResourcesLinklabel.Name = "showGfxResourcesLinklabel"
            Me.showGfxResourcesLinklabel.Size = New System.Drawing.Size(51, 15)
            Me.showGfxResourcesLinklabel.TabIndex = 55
            Me.showGfxResourcesLinklabel.TabStop = True
            Me.showGfxResourcesLinklabel.Text = "Grafiken"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.BackColor = System.Drawing.Color.Transparent
            Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(121, Byte), Integer))
            Me.Label4.Location = New System.Drawing.Point(21, 11)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(48, 15)
            Me.Label4.TabIndex = 43
            Me.Label4.Text = "Version:"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'versionLabel
            '
            Me.versionLabel.AutoSize = True
            Me.versionLabel.BackColor = System.Drawing.Color.Transparent
            Me.versionLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.versionLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(91, Byte), Integer))
            Me.versionLabel.Location = New System.Drawing.Point(67, 11)
            Me.versionLabel.Name = "versionLabel"
            Me.versionLabel.Size = New System.Drawing.Size(105, 30)
            Me.versionLabel.TabIndex = 42
            Me.versionLabel.Text = "0.9.8.7 (11.08.2013)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Alpha" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.BackColor = System.Drawing.Color.Transparent
            Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(18, Byte), Integer))
            Me.Label3.Location = New System.Drawing.Point(75, 121)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(173, 19)
            Me.Label3.TabIndex = 54
            Me.Label3.Text = "Diverse YouTube-Uploader"
            '
            'Label15
            '
            Me.Label15.AutoSize = True
            Me.Label15.BackColor = System.Drawing.Color.Transparent
            Me.Label15.Location = New System.Drawing.Point(76, 140)
            Me.Label15.Name = "Label15"
            Me.Label15.Size = New System.Drawing.Size(129, 13)
            Me.Label15.TabIndex = 53
            Me.Label15.Text = "für meine Tunnel-Musik"
            '
            'youtubePlaylistLinkLabel
            '
            Me.youtubePlaylistLinkLabel.ActiveLinkColor = System.Drawing.SystemColors.Highlight
            Me.youtubePlaylistLinkLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.youtubePlaylistLinkLabel.AutoSize = True
            Me.youtubePlaylistLinkLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.youtubePlaylistLinkLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.youtubePlaylistLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.youtubePlaylistLinkLabel.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.youtubePlaylistLinkLabel.Location = New System.Drawing.Point(76, 153)
            Me.youtubePlaylistLinkLabel.Name = "youtubePlaylistLinkLabel"
            Me.youtubePlaylistLinkLabel.Size = New System.Drawing.Size(92, 13)
            Me.youtubePlaylistLinkLabel.TabIndex = 52
            Me.youtubePlaylistLinkLabel.TabStop = True
            Me.youtubePlaylistLinkLabel.Text = "Youtube Playlists"
            '
            'timestampLabel
            '
            Me.timestampLabel.AutoSize = True
            Me.timestampLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.timestampLabel.Location = New System.Drawing.Point(97, 54)
            Me.timestampLabel.Name = "timestampLabel"
            Me.timestampLabel.Size = New System.Drawing.Size(179, 15)
            Me.timestampLabel.TabIndex = 56
            Me.timestampLabel.Text = "© 2010-2017 Niklas Mollenhauer"
            '
            'AboutForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.ClientSize = New System.Drawing.Size(299, 243)
            Me.Controls.Add(Me.timestampLabel)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label15)
            Me.Controls.Add(Me.youtubePlaylistLinkLabel)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.headerImage)
            Me.DoubleBuffered = True
            Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "AboutForm"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Über HolzShots"
            CType(Me.headerImage, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents headerImage As System.Windows.Forms.PictureBox
        Friend WithEvents Panel1 As HolzShots.UI.Windows.Forms.ExplorerInfoPanel
        Friend WithEvents holzShotsLinkLabel As HolzShots.UI.Windows.Forms.ExplorerLinkLabel
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents versionLabel As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label15 As System.Windows.Forms.Label
        Friend WithEvents youtubePlaylistLinkLabel As HolzShots.UI.Windows.Forms.ExplorerLinkLabel
        Friend WithEvents showGfxResourcesLinklabel As HolzShots.UI.Windows.Forms.ExplorerLinkLabel
        Friend WithEvents timestampLabel As System.Windows.Forms.Label
    End Class
End Namespace