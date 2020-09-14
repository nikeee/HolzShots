Namespace UI.Dialogs
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class UploadResultWindow
        Inherits HolzShots.Windows.Forms.FlyoutForm

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
            Me.copyDirect = New System.Windows.Forms.Button()
            Me.copyHTML = New System.Windows.Forms.Button()
            Me.copyBB = New System.Windows.Forms.Button()
            Me.ExplorerInfoPanel1 = New HolzShots.Windows.Forms.ExplorerInfoPanel()
            Me.closeWindowLabel = New HolzShots.Windows.Forms.ExplorerLinkLabel()
            Me.ExplorerInfoPanel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'copyDirect
            '
            Me.copyDirect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.copyDirect.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.copyDirect.Location = New System.Drawing.Point(12, 74)
            Me.copyDirect.Name = "copyDirect"
            Me.copyDirect.Size = New System.Drawing.Size(211, 62)
            Me.copyDirect.TabIndex = 0
            Me.copyDirect.Text = "Copy direct link"
            Me.copyDirect.UseVisualStyleBackColor = True
            '
            'copyHTML
            '
            Me.copyHTML.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.copyHTML.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.copyHTML.Location = New System.Drawing.Point(12, 43)
            Me.copyHTML.Name = "copyHTML"
            Me.copyHTML.Size = New System.Drawing.Size(211, 25)
            Me.copyHTML.TabIndex = 3
            Me.copyHTML.Text = "Copy HTML code"
            Me.copyHTML.UseVisualStyleBackColor = True
            '
            'copyBB
            '
            Me.copyBB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.copyBB.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.copyBB.Location = New System.Drawing.Point(12, 12)
            Me.copyBB.Name = "copyBB"
            Me.copyBB.Size = New System.Drawing.Size(211, 25)
            Me.copyBB.TabIndex = 2
            Me.copyBB.Text = "Copy BBCode"
            Me.copyBB.UseVisualStyleBackColor = True
            '
            'ExplorerInfoPanel1
            '
            Me.ExplorerInfoPanel1.Controls.Add(Me.closeWindowLabel)
            Me.ExplorerInfoPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.ExplorerInfoPanel1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.ExplorerInfoPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(91, Byte), Integer))
            Me.ExplorerInfoPanel1.Location = New System.Drawing.Point(0, 152)
            Me.ExplorerInfoPanel1.Name = "ExplorerInfoPanel1"
            Me.ExplorerInfoPanel1.Size = New System.Drawing.Size(235, 37)
            Me.ExplorerInfoPanel1.TabIndex = 6
            '
            'closeWindowLabel
            '
            Me.closeWindowLabel.ActiveLinkColor = System.Drawing.SystemColors.Highlight
            Me.closeWindowLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.closeWindowLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.closeWindowLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.closeWindowLabel.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.closeWindowLabel.Location = New System.Drawing.Point(75, 11)
            Me.closeWindowLabel.Name = "closeWindowLabel"
            Me.closeWindowLabel.Size = New System.Drawing.Size(80, 15)
            Me.closeWindowLabel.TabIndex = 1
            Me.closeWindowLabel.TabStop = True
            Me.closeWindowLabel.Text = "Close"
            Me.closeWindowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'UploadResultWindow
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.CancelButton = Me.closeWindowLabel
            Me.ClientSize = New System.Drawing.Size(235, 189)
            Me.Controls.Add(Me.ExplorerInfoPanel1)
            Me.Controls.Add(Me.copyBB)
            Me.Controls.Add(Me.copyHTML)
            Me.Controls.Add(Me.copyDirect)
            Me.MaximumSize = New System.Drawing.Size(251, 205)
            Me.MinimumSize = New System.Drawing.Size(251, 205)
            Me.Name = "UploadResultWindow"
            Me.Opacity = 0R
            Me.ExplorerInfoPanel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents copyDirect As System.Windows.Forms.Button
        Friend WithEvents copyHTML As System.Windows.Forms.Button
        Friend WithEvents copyBB As System.Windows.Forms.Button
        Friend WithEvents ExplorerInfoPanel1 As HolzShots.Windows.Forms.ExplorerInfoPanel
        Friend WithEvents closeWindowLabel As HolzShots.Windows.Forms.ExplorerLinkLabel
    End Class
End Namespace
