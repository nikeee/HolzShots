Imports HolzShots.UI.Controls

Namespace Drawing.Tools.UI

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ScaleWindow
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
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.PictureBox2 = New System.Windows.Forms.PictureBox()
            Me.PictureBox3 = New System.Windows.Forms.PictureBox()
            Me.Percent = New System.Windows.Forms.RadioButton()
            Me.Pixel = New System.Windows.Forms.RadioButton()
            Me.KeepAspectRatio = New System.Windows.Forms.CheckBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.UnitLabel2 = New System.Windows.Forms.Label()
            Me.UnitLabel1 = New System.Windows.Forms.Label()
            Me.okButton = New System.Windows.Forms.Button()
            Me.cnclButton = New System.Windows.Forms.Button()
            Me.HeightBox = New HolzShots.UI.Controls.NumericTextBox()
            Me.WidthBox = New HolzShots.UI.Controls.NumericTextBox()
            Me.ExplorerInfoPanel1 = New HolzShots.UI.Windows.Forms.ExplorerInfoPanel()
            CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ExplorerInfoPanel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'PictureBox2
            '
            Me.PictureBox2.Image = Global.HolzShots.My.Resources.Resources.horizontalMedium
            Me.PictureBox2.Location = New System.Drawing.Point(21, 37)
            Me.PictureBox2.Name = "PictureBox2"
            Me.PictureBox2.Size = New System.Drawing.Size(31, 31)
            Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.PictureBox2.TabIndex = 1
            Me.PictureBox2.TabStop = False
            '
            'PictureBox3
            '
            Me.PictureBox3.Image = Global.HolzShots.My.Resources.Resources.verticalMedium
            Me.PictureBox3.Location = New System.Drawing.Point(18, 78)
            Me.PictureBox3.Name = "PictureBox3"
            Me.PictureBox3.Size = New System.Drawing.Size(34, 31)
            Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.PictureBox3.TabIndex = 2
            Me.PictureBox3.TabStop = False
            '
            'Percent
            '
            Me.Percent.AutoSize = True
            Me.Percent.Checked = True
            Me.Percent.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.Percent.Location = New System.Drawing.Point(88, 16)
            Me.Percent.Name = "Percent"
            Me.Percent.Size = New System.Drawing.Size(71, 20)
            Me.Percent.TabIndex = 1
            Me.Percent.TabStop = True
            Me.Percent.Text = "Prozent"
            Me.Percent.UseVisualStyleBackColor = True
            '
            'Pixel
            '
            Me.Pixel.AutoSize = True
            Me.Pixel.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.Pixel.Location = New System.Drawing.Point(177, 16)
            Me.Pixel.Name = "Pixel"
            Me.Pixel.Size = New System.Drawing.Size(55, 20)
            Me.Pixel.TabIndex = 1
            Me.Pixel.TabStop = True
            Me.Pixel.Text = "Pixel"
            Me.Pixel.UseVisualStyleBackColor = True
            '
            'KeepAspectRatio
            '
            Me.KeepAspectRatio.AutoSize = True
            Me.KeepAspectRatio.Checked = True
            Me.KeepAspectRatio.CheckState = System.Windows.Forms.CheckState.Checked
            Me.KeepAspectRatio.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.KeepAspectRatio.Location = New System.Drawing.Point(52, 125)
            Me.KeepAspectRatio.Name = "KeepAspectRatio"
            Me.KeepAspectRatio.Size = New System.Drawing.Size(180, 20)
            Me.KeepAspectRatio.TabIndex = 5
            Me.KeepAspectRatio.Text = "Seitenverhältnis beibehalten"
            Me.KeepAspectRatio.UseVisualStyleBackColor = True
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(58, 45)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(65, 15)
            Me.Label2.TabIndex = 15
            Me.Label2.Text = "Horizontal:"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(75, 90)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(48, 15)
            Me.Label3.TabIndex = 16
            Me.Label3.Text = "Vertikal:"
            '
            'UnitLabel2
            '
            Me.UnitLabel2.AutoSize = True
            Me.UnitLabel2.Location = New System.Drawing.Point(240, 45)
            Me.UnitLabel2.Name = "UnitLabel2"
            Me.UnitLabel2.Size = New System.Drawing.Size(17, 15)
            Me.UnitLabel2.TabIndex = 17
            Me.UnitLabel2.Text = "%"
            '
            'UnitLabel1
            '
            Me.UnitLabel1.AutoSize = True
            Me.UnitLabel1.Location = New System.Drawing.Point(240, 90)
            Me.UnitLabel1.Name = "UnitLabel1"
            Me.UnitLabel1.Size = New System.Drawing.Size(17, 15)
            Me.UnitLabel1.TabIndex = 18
            Me.UnitLabel1.Text = "%"
            '
            'okButton
            '
            Me.okButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.okButton.BackColor = System.Drawing.SystemColors.Control
            Me.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.okButton.Location = New System.Drawing.Point(177, 15)
            Me.okButton.Name = "okButton"
            Me.okButton.Size = New System.Drawing.Size(87, 27)
            Me.okButton.TabIndex = 21
            Me.okButton.Text = "OK"
            Me.okButton.UseVisualStyleBackColor = False
            '
            'cnclButton
            '
            Me.cnclButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cnclButton.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.cnclButton.Location = New System.Drawing.Point(84, 15)
            Me.cnclButton.Name = "cnclButton"
            Me.cnclButton.Size = New System.Drawing.Size(87, 27)
            Me.cnclButton.TabIndex = 22
            Me.cnclButton.Text = "Abbrechen"
            Me.cnclButton.UseVisualStyleBackColor = True
            '
            'HeightBox
            '
            Me.HeightBox.AllowSpace = False
            Me.HeightBox.Location = New System.Drawing.Point(129, 86)
            Me.HeightBox.Name = "HeightBox"
            Me.HeightBox.Size = New System.Drawing.Size(103, 23)
            Me.HeightBox.TabIndex = 20
            Me.HeightBox.Text = "100"
            '
            'WidthBox
            '
            Me.WidthBox.AllowSpace = False
            Me.WidthBox.Location = New System.Drawing.Point(129, 42)
            Me.WidthBox.Name = "WidthBox"
            Me.WidthBox.Size = New System.Drawing.Size(103, 23)
            Me.WidthBox.TabIndex = 19
            Me.WidthBox.Text = "100"
            '
            'ExplorerInfoPanel1
            '
            Me.ExplorerInfoPanel1.Controls.Add(Me.okButton)
            Me.ExplorerInfoPanel1.Controls.Add(Me.cnclButton)
            Me.ExplorerInfoPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.ExplorerInfoPanel1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.ExplorerInfoPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(91, Byte), Integer))
            Me.ExplorerInfoPanel1.Location = New System.Drawing.Point(0, 157)
            Me.ExplorerInfoPanel1.Name = "ExplorerInfoPanel1"
            Me.ExplorerInfoPanel1.Size = New System.Drawing.Size(274, 54)
            Me.ExplorerInfoPanel1.TabIndex = 23
            '
            'ScaleWindow
            '
            Me.AcceptButton = Me.okButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.ClientSize = New System.Drawing.Size(274, 211)
            Me.ControlBox = False
            Me.Controls.Add(Me.ExplorerInfoPanel1)
            Me.Controls.Add(Me.HeightBox)
            Me.Controls.Add(Me.WidthBox)
            Me.Controls.Add(Me.UnitLabel1)
            Me.Controls.Add(Me.UnitLabel2)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.KeepAspectRatio)
            Me.Controls.Add(Me.Pixel)
            Me.Controls.Add(Me.Percent)
            Me.Controls.Add(Me.PictureBox3)
            Me.Controls.Add(Me.PictureBox2)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "ScaleWindow"
            Me.Padding = New System.Windows.Forms.Padding(0, 58, 0, 0)
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Bild skalieren"
            CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ExplorerInfoPanel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
        Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
        Friend WithEvents Percent As System.Windows.Forms.RadioButton
        Friend WithEvents Pixel As System.Windows.Forms.RadioButton
        Friend WithEvents KeepAspectRatio As System.Windows.Forms.CheckBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents UnitLabel2 As System.Windows.Forms.Label
        Friend WithEvents UnitLabel1 As System.Windows.Forms.Label
        Friend WithEvents WidthBox As NumericTextBox
        Friend WithEvents HeightBox As NumericTextBox
        Friend WithEvents okButton As Button
        Friend WithEvents cnclButton As Button
        Friend WithEvents ExplorerInfoPanel1 As HolzShots.UI.Windows.Forms.ExplorerInfoPanel
    End Class
End Namespace
