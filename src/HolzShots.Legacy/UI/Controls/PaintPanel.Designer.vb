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
            WholePanel = New Panel()
            RawBox = New PictureBox()
            TheFontDialog = New FontDialog()
            EckenTeil = New PictureBox()
            VerticalLinealBox = New PictureBox()
            HorizontalLinealBox = New PictureBox()
            WholePanel.SuspendLayout()
            CType(RawBox, ComponentModel.ISupportInitialize).BeginInit()
            CType(EckenTeil, ComponentModel.ISupportInitialize).BeginInit()
            CType(VerticalLinealBox, ComponentModel.ISupportInitialize).BeginInit()
            CType(HorizontalLinealBox, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' WholePanel
            ' 
            WholePanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            WholePanel.AutoScroll = True
            WholePanel.BackColor = Color.FromArgb(CByte(202), CByte(212), CByte(227))
            WholePanel.Controls.Add(RawBox)
            WholePanel.Location = New Point(23, 23)
            WholePanel.Margin = New Padding(4, 3, 4, 3)
            WholePanel.Name = "WholePanel"
            WholePanel.Size = New Size(623, 509)
            WholePanel.TabIndex = 14
            ' 
            ' RawBox
            ' 
            RawBox.BackColor = SystemColors.GradientActiveCaption
            RawBox.BackgroundImage = My.Resources.Resources.checkerboardBackground
            RawBox.Location = New Point(2, 7)
            RawBox.Margin = New Padding(4, 3, 4, 3)
            RawBox.Name = "RawBox"
            RawBox.Size = New Size(100, 50)
            RawBox.SizeMode = PictureBoxSizeMode.AutoSize
            RawBox.TabIndex = 12
            RawBox.TabStop = False
            ' 
            ' TheFontDialog
            ' 
            TheFontDialog.ShowColor = True
            ' 
            ' EckenTeil
            ' 
            EckenTeil.BackColor = Color.FromArgb(CByte(240), CByte(241), CByte(249))
            EckenTeil.Location = New Point(0, 0)
            EckenTeil.Margin = New Padding(4, 3, 4, 3)
            EckenTeil.Name = "EckenTeil"
            EckenTeil.Size = New Size(23, 23)
            EckenTeil.TabIndex = 14
            EckenTeil.TabStop = False
            ' 
            ' VerticalLinealBox
            ' 
            VerticalLinealBox.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
            VerticalLinealBox.Location = New Point(0, 23)
            VerticalLinealBox.Margin = New Padding(4, 3, 4, 3)
            VerticalLinealBox.Name = "VerticalLinealBox"
            VerticalLinealBox.Size = New Size(23, 509)
            VerticalLinealBox.TabIndex = 16
            VerticalLinealBox.TabStop = False
            ' 
            ' HorizontalLinealBox
            ' 
            HorizontalLinealBox.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            HorizontalLinealBox.Location = New Point(23, 0)
            HorizontalLinealBox.Margin = New Padding(4, 3, 4, 3)
            HorizontalLinealBox.Name = "HorizontalLinealBox"
            HorizontalLinealBox.Size = New Size(623, 23)
            HorizontalLinealBox.TabIndex = 15
            HorizontalLinealBox.TabStop = False
            ' 
            ' PaintPanel
            ' 
            AutoScaleDimensions = New SizeF(7F, 15F)
            AutoScaleMode = AutoScaleMode.Font
            BackColor = SystemColors.GradientActiveCaption
            BackgroundImageLayout = ImageLayout.None
            Controls.Add(EckenTeil)
            Controls.Add(VerticalLinealBox)
            Controls.Add(HorizontalLinealBox)
            Controls.Add(WholePanel)
            Margin = New Padding(4, 3, 4, 3)
            Name = "PaintPanel"
            Size = New Size(646, 532)
            WholePanel.ResumeLayout(False)
            WholePanel.PerformLayout()
            CType(RawBox, ComponentModel.ISupportInitialize).EndInit()
            CType(EckenTeil, ComponentModel.ISupportInitialize).EndInit()
            CType(VerticalLinealBox, ComponentModel.ISupportInitialize).EndInit()
            CType(HorizontalLinealBox, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)

        End Sub
        Friend WithEvents RawBox As System.Windows.Forms.PictureBox
        Friend WithEvents WholePanel As System.Windows.Forms.Panel
        Friend WithEvents HorizontalLinealBox As System.Windows.Forms.PictureBox
        Friend WithEvents VerticalLinealBox As System.Windows.Forms.PictureBox
        Friend WithEvents EckenTeil As System.Windows.Forms.PictureBox
        Friend WithEvents TheFontDialog As System.Windows.Forms.FontDialog

    End Class
End Namespace
