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
            WholePanel = New System.Windows.Forms.Panel()
            TextPanel = New System.Windows.Forms.Panel()
            InsertDate = New System.Windows.Forms.PictureBox()
            CancelButton = New System.Windows.Forms.PictureBox()
            SelectAll = New System.Windows.Forms.PictureBox()
            ChangeFont = New System.Windows.Forms.PictureBox()
            MoverBox = New System.Windows.Forms.PictureBox()
            text_ok = New System.Windows.Forms.Button()
            TextInput = New System.Windows.Forms.TextBox()
            tools_bg = New System.Windows.Forms.PictureBox()
            RawBox = New System.Windows.Forms.PictureBox()
            TheFontDialog = New System.Windows.Forms.FontDialog()
            EckenTeil = New System.Windows.Forms.PictureBox()
            VerticalLinealBox = New System.Windows.Forms.PictureBox()
            HorizontalLinealBox = New System.Windows.Forms.PictureBox()
            WholePanel.SuspendLayout()
            TextPanel.SuspendLayout()
            CType(InsertDate, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(CancelButton, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(SelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(ChangeFont, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(MoverBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(tools_bg, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(RawBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(EckenTeil, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(VerticalLinealBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(HorizontalLinealBox, System.ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            '
            'WholePanel
            '
            WholePanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                        Or System.Windows.Forms.AnchorStyles.Left) _
                                       Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            WholePanel.AutoScroll = True
            WholePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(227, Byte), Integer))
            WholePanel.Controls.Add(TextPanel)
            WholePanel.Controls.Add(RawBox)
            WholePanel.Location = New System.Drawing.Point(20, 20)
            WholePanel.Name = "WholePanel"
            WholePanel.Size = New System.Drawing.Size(534, 441)
            WholePanel.TabIndex = 14
            '
            'TextPanel
            '
            TextPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption
            TextPanel.Controls.Add(InsertDate)
            TextPanel.Controls.Add(CancelButton)
            TextPanel.Controls.Add(SelectAll)
            TextPanel.Controls.Add(ChangeFont)
            TextPanel.Controls.Add(MoverBox)
            TextPanel.Controls.Add(text_ok)
            TextPanel.Controls.Add(TextInput)
            TextPanel.Controls.Add(tools_bg)
            TextPanel.Location = New System.Drawing.Point(108, 2)
            TextPanel.Name = "TextPanel"
            TextPanel.Size = New System.Drawing.Size(223, 111)
            TextPanel.TabIndex = 14
            TextPanel.Visible = False
            '
            'InsertDate
            '
            InsertDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            InsertDate.BackColor = System.Drawing.Color.Transparent
            InsertDate.Image = Global.HolzShots.My.Resources.Resources.dateMedium
            InsertDate.Location = New System.Drawing.Point(79, 75)
            InsertDate.Name = "InsertDate"
            InsertDate.Size = New System.Drawing.Size(32, 32)
            InsertDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            InsertDate.TabIndex = 18
            InsertDate.TabStop = False
            '
            'CancelButton
            '
            CancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            CancelButton.BackColor = System.Drawing.Color.Transparent
            CancelButton.Image = Global.HolzShots.My.Resources.Resources.cancelMedium
            CancelButton.Location = New System.Drawing.Point(117, 75)
            CancelButton.Name = "CancelButton"
            CancelButton.Size = New System.Drawing.Size(32, 32)
            CancelButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            CancelButton.TabIndex = 17
            CancelButton.TabStop = False
            '
            'SelectAll
            '
            SelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            SelectAll.BackColor = System.Drawing.Color.Transparent
            SelectAll.Image = Global.HolzShots.My.Resources.Resources.selectAllMedium
            SelectAll.Location = New System.Drawing.Point(41, 75)
            SelectAll.Name = "SelectAll"
            SelectAll.Size = New System.Drawing.Size(32, 32)
            SelectAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            SelectAll.TabIndex = 16
            SelectAll.TabStop = False
            '
            'ChangeFont
            '
            ChangeFont.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            ChangeFont.BackColor = System.Drawing.Color.Transparent
            ChangeFont.Image = Global.HolzShots.My.Resources.Resources.fontMedium
            ChangeFont.Location = New System.Drawing.Point(3, 75)
            ChangeFont.Name = "ChangeFont"
            ChangeFont.Size = New System.Drawing.Size(32, 32)
            ChangeFont.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            ChangeFont.TabIndex = 3
            ChangeFont.TabStop = False
            '
            'MoverBox
            '
            MoverBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            MoverBox.BackColor = System.Drawing.Color.Transparent
            MoverBox.Cursor = System.Windows.Forms.Cursors.SizeAll
            MoverBox.Image = Global.HolzShots.My.Resources.Resources.moveMedium
            MoverBox.Location = New System.Drawing.Point(175, 75)
            MoverBox.Name = "MoverBox"
            MoverBox.Size = New System.Drawing.Size(44, 32)
            MoverBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
            MoverBox.TabIndex = 2
            MoverBox.TabStop = False
            '
            'text_ok
            '
            text_ok.Location = New System.Drawing.Point(175, 0)
            text_ok.Name = "text_ok"
            text_ok.Size = New System.Drawing.Size(47, 73)
            text_ok.TabIndex = 1
            text_ok.Text = "Ok"
            text_ok.UseVisualStyleBackColor = True
            '
            'TextInput
            '
            TextInput.AcceptsReturn = True
            TextInput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                       Or System.Windows.Forms.AnchorStyles.Left) _
                                      Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            TextInput.BackColor = System.Drawing.Color.AliceBlue
            TextInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            TextInput.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            TextInput.Location = New System.Drawing.Point(0, 0)
            TextInput.Multiline = True
            TextInput.Name = "TextInput"
            TextInput.Size = New System.Drawing.Size(177, 73)
            TextInput.TabIndex = 0
            TextInput.Text = "Ihr Text.."
            TextInput.WordWrap = False
            '
            'tools_bg
            '
            tools_bg.Image = Global.HolzShots.My.Resources.Resources.TexttoolsBackground
            tools_bg.Location = New System.Drawing.Point(0, 73)
            tools_bg.Name = "tools_bg"
            tools_bg.Size = New System.Drawing.Size(222, 37)
            tools_bg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            tools_bg.TabIndex = 15
            tools_bg.TabStop = False
            '
            'RawBox
            '
            RawBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
            RawBox.BackgroundImage = Global.HolzShots.My.Resources.Resources.PaintPanelBackground
            RawBox.Location = New System.Drawing.Point(2, 6)
            RawBox.Name = "RawBox"
            RawBox.Size = New System.Drawing.Size(100, 50)
            RawBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            RawBox.TabIndex = 12
            RawBox.TabStop = False
            '
            'TheFontDialog
            '
            TheFontDialog.ShowColor = True
            '
            'EckenTeil
            '
            EckenTeil.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(249, Byte), Integer))
            EckenTeil.Location = New System.Drawing.Point(0, 0)
            EckenTeil.Name = "EckenTeil"
            EckenTeil.Size = New System.Drawing.Size(20, 20)
            EckenTeil.TabIndex = 14
            EckenTeil.TabStop = False
            '
            'VerticalLinealBox
            '
            VerticalLinealBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                              Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            VerticalLinealBox.Location = New System.Drawing.Point(0, 20)
            VerticalLinealBox.Name = "VerticalLinealBox"
            VerticalLinealBox.Size = New System.Drawing.Size(20, 441)
            VerticalLinealBox.TabIndex = 16
            VerticalLinealBox.TabStop = False
            '
            'HorizontalLinealBox
            '
            HorizontalLinealBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            HorizontalLinealBox.Location = New System.Drawing.Point(20, 0)
            HorizontalLinealBox.Name = "HorizontalLinealBox"
            HorizontalLinealBox.Size = New System.Drawing.Size(534, 20)
            HorizontalLinealBox.TabIndex = 15
            HorizontalLinealBox.TabStop = False
            '
            'PaintPanel
            '
            AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            BackColor = System.Drawing.SystemColors.GradientActiveCaption
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Controls.Add(EckenTeil)
            Controls.Add(VerticalLinealBox)
            Controls.Add(HorizontalLinealBox)
            Controls.Add(WholePanel)
            Name = "PaintPanel"
            Size = New System.Drawing.Size(554, 461)
            WholePanel.ResumeLayout(False)
            WholePanel.PerformLayout()
            TextPanel.ResumeLayout(False)
            TextPanel.PerformLayout()
            CType(InsertDate, System.ComponentModel.ISupportInitialize).EndInit()
            CType(CancelButton, System.ComponentModel.ISupportInitialize).EndInit()
            CType(SelectAll, System.ComponentModel.ISupportInitialize).EndInit()
            CType(ChangeFont, System.ComponentModel.ISupportInitialize).EndInit()
            CType(MoverBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(tools_bg, System.ComponentModel.ISupportInitialize).EndInit()
            CType(RawBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(EckenTeil, System.ComponentModel.ISupportInitialize).EndInit()
            CType(VerticalLinealBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(HorizontalLinealBox, System.ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)

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