Namespace UI.Controls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class PaintPanel
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
            Me.WholePanel = New System.Windows.Forms.Panel()
            Me.TextPanel = New System.Windows.Forms.Panel()
            Me.InsertDate = New System.Windows.Forms.PictureBox()
            Me.CancelButton = New System.Windows.Forms.PictureBox()
            Me.SelectAll = New System.Windows.Forms.PictureBox()
            Me.ChangeFont = New System.Windows.Forms.PictureBox()
            Me.MoverBox = New System.Windows.Forms.PictureBox()
            Me.text_ok = New System.Windows.Forms.Button()
            Me.TextInput = New System.Windows.Forms.TextBox()
            Me.tools_bg = New System.Windows.Forms.PictureBox()
            Me.RawBox = New System.Windows.Forms.PictureBox()
            Me.TheFontDialog = New System.Windows.Forms.FontDialog()
            Me.EckenTeil = New System.Windows.Forms.PictureBox()
            Me.VerticalLinealBox = New System.Windows.Forms.PictureBox()
            Me.HorizontalLinealBox = New System.Windows.Forms.PictureBox()
            Me.WholePanel.SuspendLayout()
            Me.TextPanel.SuspendLayout()
            CType(Me.InsertDate, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CancelButton, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.SelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ChangeFont, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.MoverBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.tools_bg, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.RawBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.EckenTeil, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.VerticalLinealBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.HorizontalLinealBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'WholePanel
            '
            Me.WholePanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                        Or System.Windows.Forms.AnchorStyles.Left) _
                                       Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.WholePanel.AutoScroll = True
            Me.WholePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(227, Byte), Integer))
            Me.WholePanel.Controls.Add(Me.TextPanel)
            Me.WholePanel.Controls.Add(Me.RawBox)
            Me.WholePanel.Location = New System.Drawing.Point(20, 20)
            Me.WholePanel.Name = "WholePanel"
            Me.WholePanel.Size = New System.Drawing.Size(534, 441)
            Me.WholePanel.TabIndex = 14
            '
            'TextPanel
            '
            Me.TextPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption
            Me.TextPanel.Controls.Add(Me.InsertDate)
            Me.TextPanel.Controls.Add(Me.CancelButton)
            Me.TextPanel.Controls.Add(Me.SelectAll)
            Me.TextPanel.Controls.Add(Me.ChangeFont)
            Me.TextPanel.Controls.Add(Me.MoverBox)
            Me.TextPanel.Controls.Add(Me.text_ok)
            Me.TextPanel.Controls.Add(Me.TextInput)
            Me.TextPanel.Controls.Add(Me.tools_bg)
            Me.TextPanel.Location = New System.Drawing.Point(108, 2)
            Me.TextPanel.Name = "TextPanel"
            Me.TextPanel.Size = New System.Drawing.Size(223, 111)
            Me.TextPanel.TabIndex = 14
            Me.TextPanel.Visible = False
            '
            'InsertDate
            '
            Me.InsertDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.InsertDate.BackColor = System.Drawing.Color.Transparent
            Me.InsertDate.Image = Global.HolzShots.My.Resources.Resources.dateMedium
            Me.InsertDate.Location = New System.Drawing.Point(79, 75)
            Me.InsertDate.Name = "InsertDate"
            Me.InsertDate.Size = New System.Drawing.Size(32, 32)
            Me.InsertDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.InsertDate.TabIndex = 18
            Me.InsertDate.TabStop = False
            '
            'CancelButton
            '
            Me.CancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelButton.BackColor = System.Drawing.Color.Transparent
            Me.CancelButton.Image = Global.HolzShots.My.Resources.Resources.cancelMedium
            Me.CancelButton.Location = New System.Drawing.Point(117, 75)
            Me.CancelButton.Name = "CancelButton"
            Me.CancelButton.Size = New System.Drawing.Size(32, 32)
            Me.CancelButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.CancelButton.TabIndex = 17
            Me.CancelButton.TabStop = False
            '
            'SelectAll
            '
            Me.SelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SelectAll.BackColor = System.Drawing.Color.Transparent
            Me.SelectAll.Image = Global.HolzShots.My.Resources.Resources.selectAllMedium
            Me.SelectAll.Location = New System.Drawing.Point(41, 75)
            Me.SelectAll.Name = "SelectAll"
            Me.SelectAll.Size = New System.Drawing.Size(32, 32)
            Me.SelectAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.SelectAll.TabIndex = 16
            Me.SelectAll.TabStop = False
            '
            'ChangeFont
            '
            Me.ChangeFont.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ChangeFont.BackColor = System.Drawing.Color.Transparent
            Me.ChangeFont.Image = Global.HolzShots.My.Resources.Resources.fontMedium
            Me.ChangeFont.Location = New System.Drawing.Point(3, 75)
            Me.ChangeFont.Name = "ChangeFont"
            Me.ChangeFont.Size = New System.Drawing.Size(32, 32)
            Me.ChangeFont.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.ChangeFont.TabIndex = 3
            Me.ChangeFont.TabStop = False
            '
            'MoverBox
            '
            Me.MoverBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MoverBox.BackColor = System.Drawing.Color.Transparent
            Me.MoverBox.Cursor = System.Windows.Forms.Cursors.SizeAll
            Me.MoverBox.Image = Global.HolzShots.My.Resources.Resources.moveMedium
            Me.MoverBox.Location = New System.Drawing.Point(175, 75)
            Me.MoverBox.Name = "MoverBox"
            Me.MoverBox.Size = New System.Drawing.Size(44, 32)
            Me.MoverBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
            Me.MoverBox.TabIndex = 2
            Me.MoverBox.TabStop = False
            '
            'text_ok
            '
            Me.text_ok.Location = New System.Drawing.Point(175, 0)
            Me.text_ok.Name = "text_ok"
            Me.text_ok.Size = New System.Drawing.Size(47, 73)
            Me.text_ok.TabIndex = 1
            Me.text_ok.Text = "Ok"
            Me.text_ok.UseVisualStyleBackColor = True
            '
            'TextInput
            '
            Me.TextInput.AcceptsReturn = True
            Me.TextInput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                       Or System.Windows.Forms.AnchorStyles.Left) _
                                      Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TextInput.BackColor = System.Drawing.Color.AliceBlue
            Me.TextInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.TextInput.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TextInput.Location = New System.Drawing.Point(0, 0)
            Me.TextInput.Multiline = True
            Me.TextInput.Name = "TextInput"
            Me.TextInput.Size = New System.Drawing.Size(177, 73)
            Me.TextInput.TabIndex = 0
            Me.TextInput.Text = "Sample Text"
            Me.TextInput.WordWrap = False
            '
            'tools_bg
            '
            Me.tools_bg.Image = Global.HolzShots.My.Resources.Resources.TexttoolsBackground
            Me.tools_bg.Location = New System.Drawing.Point(0, 73)
            Me.tools_bg.Name = "tools_bg"
            Me.tools_bg.Size = New System.Drawing.Size(222, 37)
            Me.tools_bg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.tools_bg.TabIndex = 15
            Me.tools_bg.TabStop = False
            '
            'RawBox
            '
            Me.RawBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
            Me.RawBox.BackgroundImage = Global.HolzShots.My.Resources.Resources.PaintPanelBackground
            Me.RawBox.Location = New System.Drawing.Point(2, 6)
            Me.RawBox.Name = "RawBox"
            Me.RawBox.Size = New System.Drawing.Size(100, 50)
            Me.RawBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.RawBox.TabIndex = 12
            Me.RawBox.TabStop = False
            '
            'TheFontDialog
            '
            Me.TheFontDialog.ShowColor = True
            '
            'EckenTeil
            '
            Me.EckenTeil.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.EckenTeil.Location = New System.Drawing.Point(0, 0)
            Me.EckenTeil.Name = "EckenTeil"
            Me.EckenTeil.Size = New System.Drawing.Size(20, 20)
            Me.EckenTeil.TabIndex = 14
            Me.EckenTeil.TabStop = False
            '
            'VerticalLinealBox
            '
            Me.VerticalLinealBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                              Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.VerticalLinealBox.Location = New System.Drawing.Point(0, 20)
            Me.VerticalLinealBox.Name = "VerticalLinealBox"
            Me.VerticalLinealBox.Size = New System.Drawing.Size(20, 441)
            Me.VerticalLinealBox.TabIndex = 16
            Me.VerticalLinealBox.TabStop = False
            '
            'HorizontalLinealBox
            '
            Me.HorizontalLinealBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.HorizontalLinealBox.Location = New System.Drawing.Point(20, 0)
            Me.HorizontalLinealBox.Name = "HorizontalLinealBox"
            Me.HorizontalLinealBox.Size = New System.Drawing.Size(534, 20)
            Me.HorizontalLinealBox.TabIndex = 15
            Me.HorizontalLinealBox.TabStop = False
            '
            'PaintPanel
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.Controls.Add(Me.EckenTeil)
            Me.Controls.Add(Me.VerticalLinealBox)
            Me.Controls.Add(Me.HorizontalLinealBox)
            Me.Controls.Add(Me.WholePanel)
            Me.Name = "PaintPanel"
            Me.Size = New System.Drawing.Size(554, 461)
            Me.WholePanel.ResumeLayout(False)
            Me.WholePanel.PerformLayout()
            Me.TextPanel.ResumeLayout(False)
            Me.TextPanel.PerformLayout()
            CType(Me.InsertDate, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CancelButton, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.SelectAll, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ChangeFont, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.MoverBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.tools_bg, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.RawBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.EckenTeil, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.VerticalLinealBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.HorizontalLinealBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents RawBox As System.Windows.Forms.PictureBox
        Friend WithEvents WholePanel As System.Windows.Forms.Panel
        Friend WithEvents HorizontalLinealBox As System.Windows.Forms.PictureBox
        Friend WithEvents VerticalLinealBox As System.Windows.Forms.PictureBox
        Friend WithEvents EckenTeil As System.Windows.Forms.PictureBox
        Friend WithEvents TextPanel As System.Windows.Forms.Panel
        Friend WithEvents MoverBox As System.Windows.Forms.PictureBox
        Friend WithEvents text_ok As System.Windows.Forms.Button
        Friend WithEvents TextInput As System.Windows.Forms.TextBox
        Friend WithEvents TheFontDialog As System.Windows.Forms.FontDialog
        Friend WithEvents ChangeFont As System.Windows.Forms.PictureBox
        Friend WithEvents tools_bg As System.Windows.Forms.PictureBox
        Friend WithEvents SelectAll As System.Windows.Forms.PictureBox
        Friend WithEvents CancelButton As System.Windows.Forms.PictureBox
        Friend WithEvents InsertDate As System.Windows.Forms.PictureBox

    End Class
End Namespace
