Imports HolzShots.UI.Controls

Namespace UI.Dialogs
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class PipettenColorViewer
        'Inherits HolzShots.UI.Windows.Forms.Aero.glassForm
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
        '<System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CopyLinkLabel5 = New HolzShots.UI.Controls.CopyLinkLabel()
            Me.CopyLinkLabel4 = New HolzShots.UI.Controls.CopyLinkLabel()
            Me.CopyLinkLabel3 = New HolzShots.UI.Controls.CopyLinkLabel()
            Me.CopyLinkLabel2 = New HolzShots.UI.Controls.CopyLinkLabel()
            Me.CopyLinkLabel1 = New HolzShots.UI.Controls.CopyLinkLabel()
            Me.ColorBox = New HolzShots.UI.Controls.BigColorViewer()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.Label1.Location = New System.Drawing.Point(14, 45)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(74, 15)
            Me.Label1.TabIndex = 7
            Me.Label1.Text = "Kopieren als:"
            '
            'CopyLinkLabel5
            '
            Me.CopyLinkLabel5.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.CopyLinkLabel5.AutoSize = True
            Me.CopyLinkLabel5.Cursor = System.Windows.Forms.Cursors.Hand
            Me.CopyLinkLabel5.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.CopyLinkLabel5.LinkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(204, Byte), Integer))
            Me.CopyLinkLabel5.Location = New System.Drawing.Point(14, 150)
            Me.CopyLinkLabel5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.CopyLinkLabel5.Name = "CopyLinkLabel5"
            Me.CopyLinkLabel5.Size = New System.Drawing.Size(105, 15)
            Me.CopyLinkLabel5.TabIndex = 8
            Me.CopyLinkLabel5.TabStop = True
            Me.CopyLinkLabel5.Text = "CopyLinkLabel5"
            '
            'CopyLinkLabel4
            '
            Me.CopyLinkLabel4.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.CopyLinkLabel4.AutoSize = True
            Me.CopyLinkLabel4.Cursor = System.Windows.Forms.Cursors.Hand
            Me.CopyLinkLabel4.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.CopyLinkLabel4.LinkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(204, Byte), Integer))
            Me.CopyLinkLabel4.Location = New System.Drawing.Point(14, 128)
            Me.CopyLinkLabel4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.CopyLinkLabel4.Name = "CopyLinkLabel4"
            Me.CopyLinkLabel4.Size = New System.Drawing.Size(105, 15)
            Me.CopyLinkLabel4.TabIndex = 6
            Me.CopyLinkLabel4.TabStop = True
            Me.CopyLinkLabel4.Text = "CopyLinkLabel4"
            '
            'CopyLinkLabel3
            '
            Me.CopyLinkLabel3.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.CopyLinkLabel3.AutoSize = True
            Me.CopyLinkLabel3.Cursor = System.Windows.Forms.Cursors.Hand
            Me.CopyLinkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.CopyLinkLabel3.LinkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(204, Byte), Integer))
            Me.CopyLinkLabel3.Location = New System.Drawing.Point(14, 106)
            Me.CopyLinkLabel3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.CopyLinkLabel3.Name = "CopyLinkLabel3"
            Me.CopyLinkLabel3.Size = New System.Drawing.Size(105, 15)
            Me.CopyLinkLabel3.TabIndex = 5
            Me.CopyLinkLabel3.TabStop = True
            Me.CopyLinkLabel3.Text = "CopyLinkLabel3"
            '
            'CopyLinkLabel2
            '
            Me.CopyLinkLabel2.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.CopyLinkLabel2.AutoSize = True
            Me.CopyLinkLabel2.Cursor = System.Windows.Forms.Cursors.Hand
            Me.CopyLinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.CopyLinkLabel2.LinkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(204, Byte), Integer))
            Me.CopyLinkLabel2.Location = New System.Drawing.Point(14, 84)
            Me.CopyLinkLabel2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.CopyLinkLabel2.Name = "CopyLinkLabel2"
            Me.CopyLinkLabel2.Size = New System.Drawing.Size(105, 15)
            Me.CopyLinkLabel2.TabIndex = 4
            Me.CopyLinkLabel2.TabStop = True
            Me.CopyLinkLabel2.Text = "CopyLinkLabel2"
            '
            'CopyLinkLabel1
            '
            Me.CopyLinkLabel1.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.CopyLinkLabel1.AutoSize = True
            Me.CopyLinkLabel1.Cursor = System.Windows.Forms.Cursors.Hand
            Me.CopyLinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.CopyLinkLabel1.LinkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(204, Byte), Integer))
            Me.CopyLinkLabel1.Location = New System.Drawing.Point(14, 62)
            Me.CopyLinkLabel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.CopyLinkLabel1.Name = "CopyLinkLabel1"
            Me.CopyLinkLabel1.Size = New System.Drawing.Size(105, 15)
            Me.CopyLinkLabel1.TabIndex = 3
            Me.CopyLinkLabel1.TabStop = True
            Me.CopyLinkLabel1.Text = "CopyLinkLabel1"
            '
            'ColorBox
            '
            Me.ColorBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ColorBox.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.ColorBox.Location = New System.Drawing.Point(17, 14)
            Me.ColorBox.Margin = New System.Windows.Forms.Padding(17, 3, 17, 3)
            Me.ColorBox.Name = "ColorBox"
            Me.ColorBox.Size = New System.Drawing.Size(160, 28)
            Me.ColorBox.TabIndex = 2
            Me.ColorBox.TabStop = False
            '
            'PipettenColorViewer
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(247, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(195, 189)
            Me.Controls.Add(Me.CopyLinkLabel5)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.CopyLinkLabel4)
            Me.Controls.Add(Me.CopyLinkLabel3)
            Me.Controls.Add(Me.CopyLinkLabel2)
            Me.Controls.Add(Me.CopyLinkLabel1)
            Me.Controls.Add(Me.ColorBox)
            Me.Font = New System.Drawing.Font("Segoe UI", 8.9!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "PipettenColorViewer"
            Me.Padding = New System.Windows.Forms.Padding(0, 0, 0, 6)
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.Text = "Farbe kopieren"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ColorBox As UI.Controls.BigColorViewer
        Friend WithEvents CopyLinkLabel1 As CopyLinkLabel
        Friend WithEvents CopyLinkLabel2 As CopyLinkLabel
        Friend WithEvents CopyLinkLabel3 As CopyLinkLabel
        Friend WithEvents CopyLinkLabel4 As CopyLinkLabel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CopyLinkLabel5 As CopyLinkLabel
    End Class
End Namespace