Namespace UI.Dialogs
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FlyoutNotifier
        Inherits FlyoutWindow

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
            Me.bodyLabel = New System.Windows.Forms.Label()
            Me.titleLabel = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'bodyLabel
            '
            Me.bodyLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.bodyLabel.Location = New System.Drawing.Point(12, 30)
            Me.bodyLabel.Name = "bodyLabel"
            Me.bodyLabel.Size = New System.Drawing.Size(189, 38)
            Me.bodyLabel.TabIndex = 1
            Me.bodyLabel.Text = "B"
            '
            'titleLabel
            '
            Me.titleLabel.AutoSize = True
            Me.titleLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!)
            Me.titleLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer))
            Me.titleLabel.Location = New System.Drawing.Point(12, 6)
            Me.titleLabel.Name = "titleLabel"
            Me.titleLabel.Size = New System.Drawing.Size(18, 21)
            Me.titleLabel.TabIndex = 2
            Me.titleLabel.Text = "T"
            '
            'FlyoutNotifier
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.ClientSize = New System.Drawing.Size(213, 77)
            Me.Controls.Add(Me.titleLabel)
            Me.Controls.Add(Me.bodyLabel)
            Me.Name = "FlyoutNotifier"
            Me.Opacity = 0.0R
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents bodyLabel As System.Windows.Forms.Label
        Friend WithEvents titleLabel As System.Windows.Forms.Label
    End Class
End Namespace