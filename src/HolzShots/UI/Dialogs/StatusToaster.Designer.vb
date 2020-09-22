Namespace UI.Dialogs
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class StatusToaster
        Inherits HolzShots.Windows.Forms.FlyoutForm

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
            Me.stuffUploadedBar = New System.Windows.Forms.ProgressBar()
            Me.speedLabel = New System.Windows.Forms.Label()
            Me.statusTextLabel = New System.Windows.Forms.Label()
            Me.uploadedBytesLabel = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'stuffUploadedBar
            '
            Me.stuffUploadedBar.Location = New System.Drawing.Point(12, 27)
            Me.stuffUploadedBar.Name = "stuffUploadedBar"
            Me.stuffUploadedBar.Size = New System.Drawing.Size(189, 23)
            Me.stuffUploadedBar.TabIndex = 0
            '
            'speedLabel
            '
            Me.speedLabel.AutoSize = True
            Me.speedLabel.Location = New System.Drawing.Point(152, 53)
            Me.speedLabel.Name = "speedLabel"
            Me.speedLabel.Size = New System.Drawing.Size(49, 15)
            Me.speedLabel.TabIndex = 1
            Me.speedLabel.Text = "0.0 KiB/s"
            '
            'statusTextLabel
            '
            Me.statusTextLabel.AutoSize = True
            Me.statusTextLabel.Location = New System.Drawing.Point(9, 9)
            Me.statusTextLabel.Name = "statusTextLabel"
            Me.statusTextLabel.Size = New System.Drawing.Size(137, 15)
            Me.statusTextLabel.TabIndex = 2
            Me.statusTextLabel.Text = "Image is being uploaded...."
            '
            'uploadedBytesLabel
            '
            Me.uploadedBytesLabel.AutoSize = True
            Me.uploadedBytesLabel.Location = New System.Drawing.Point(9, 53)
            Me.uploadedBytesLabel.Name = "uploadedBytesLabel"
            Me.uploadedBytesLabel.Size = New System.Drawing.Size(98, 15)
            Me.uploadedBytesLabel.TabIndex = 3
            Me.uploadedBytesLabel.Text = "0.0 KB of 12 MiB"
            '
            'StatusToaster
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.ClientSize = New System.Drawing.Size(213, 77)
            Me.Controls.Add(Me.uploadedBytesLabel)
            Me.Controls.Add(Me.statusTextLabel)
            Me.Controls.Add(Me.speedLabel)
            Me.Controls.Add(Me.stuffUploadedBar)
            Me.MaximumSize = New System.Drawing.Size(213, 77)
            Me.MinimumSize = New System.Drawing.Size(213, 77)
            Me.Name = "StatusToaster"
            Me.Opacity = 0R
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents stuffUploadedBar As System.Windows.Forms.ProgressBar
        Friend WithEvents speedLabel As System.Windows.Forms.Label
        Friend WithEvents statusTextLabel As System.Windows.Forms.Label
        Friend WithEvents uploadedBytesLabel As System.Windows.Forms.Label
    End Class
End Namespace
