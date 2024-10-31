Imports HolzShots.UI.Controls

Namespace UI

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Public Class ShotEditor
        Inherits System.Windows.Forms.Form
        'Inherits HolzShots.UI.Forms.Aero.GlassForm

        'Wird vom Windows Form-Designer benötigt.
        Private components As System.ComponentModel.IContainer

        'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        'Das Bearbeiten ist mit dem Windows Form-Designer möglich.
        'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ShotEditor))
            ShareStrip = New ToolStrip()
            UploadToHoster = New ToolStripSplitButton()
            SaveButton = New ToolStripButton()
            EditStrip = New ToolStrip()
            CensorTool = New ToolStripButton()
            MarkerTool = New ToolStripButton()
            CroppingTool = New ToolStripButton()
            EraserTool = New ToolStripButton()
            BlurTool = New ToolStripButton()
            EllipseTool = New ToolStripButton()
            PipettenTool = New ToolStripButton()
            BrightenTool = New ToolStripButton()
            ArrowTool = New ToolStripButton()
            UndoStuff = New ToolStripButton()
            ToolStripDropDownButton1 = New ToolStripDropDownButton()
            ZensToolStripMenuItem = New ToolStripMenuItem()
            MarkToolStripMenuItem = New ToolStripMenuItem()
            CropToolStripMenuItem = New ToolStripMenuItem()
            EraseToolStripMenuItem = New ToolStripMenuItem()
            PixelateToolStripMenuItem = New ToolStripMenuItem()
            KreisToolStripMenuItem = New ToolStripMenuItem()
            ArrowToolStripMenuItem = New ToolStripMenuItem()
            ResetToolStripMenuItem = New ToolStripMenuItem()
            UploadToolStripMenuItem = New ToolStripMenuItem()
            SaveToolStripMenuItem = New ToolStripMenuItem()
            ClipboardToolStripMenuItem = New ToolStripMenuItem()
            PrintToolStripMenuItem = New ToolStripMenuItem()
            ChooseServiceToolStripMenuItem = New ToolStripMenuItem()
            ToolStrip1 = New ToolStrip()
            ScaleTool = New ToolStripButton()
            DrawCursor = New ToolStripButton()
            BottomToolStrip = New ToolStrip()
            MouseInfoLabel = New ToolStripLabel()
            ToolStripSeparator1 = New ToolStripSeparator()
            ImageInfoLabel = New ToolStripLabel()
            CopyPrintToolStrip = New ToolStrip()
            CopyToClipboard = New ToolStripButton()
            ThePanel = New PaintPanel2()
            AutoCloseShotEditor = New CheckBox()
            CurrentToolSettingsPanel = New Panel()
            ShareStrip.SuspendLayout()
            EditStrip.SuspendLayout()
            ToolStrip1.SuspendLayout()
            BottomToolStrip.SuspendLayout()
            CopyPrintToolStrip.SuspendLayout()
            SuspendLayout()
            '
            ' ShareStrip
            '
            ShareStrip.AllowItemReorder = True
            ShareStrip.AutoSize = False
            ShareStrip.BackColor = Color.Transparent
            ShareStrip.Dock = DockStyle.None
            ShareStrip.GripMargin = New Padding(0)
            ShareStrip.GripStyle = ToolStripGripStyle.Hidden
            ShareStrip.ImageScalingSize = New Size(32, 32)
            ShareStrip.Items.AddRange(New ToolStripItem() {UploadToHoster, SaveButton})
            ShareStrip.LayoutStyle = ToolStripLayoutStyle.Flow
            ShareStrip.Location = New Point(5, 4)
            ShareStrip.Name = "ShareStrip"
            ShareStrip.Padding = New Padding(0)
            ShareStrip.RenderMode = ToolStripRenderMode.System
            ShareStrip.Size = New Size(111, 44)
            ShareStrip.TabIndex = 1
            ShareStrip.Text = "Actions"
            '
            ' UploadToHoster
            '
            UploadToHoster.BackColor = Color.Transparent
            UploadToHoster.DisplayStyle = ToolStripItemDisplayStyle.Image
            UploadToHoster.DropDownButtonWidth = 15
            UploadToHoster.Image = My.Resources.Resources.uploadMedium
            UploadToHoster.ImageScaling = ToolStripItemImageScaling.None
            UploadToHoster.ImageTransparentColor = Color.Magenta
            UploadToHoster.Margin = New Padding(5)
            UploadToHoster.Name = "UploadToHoster"
            UploadToHoster.Size = New Size(52, 36)
            UploadToHoster.Text = "Upload to {0} (Strg+Q)"
            '
            ' SaveButton
            '
            SaveButton.BackColor = Color.Transparent
            SaveButton.DisplayStyle = ToolStripItemDisplayStyle.Image
            SaveButton.Image = My.Resources.Resources.saveMedium
            SaveButton.ImageScaling = ToolStripItemImageScaling.None
            SaveButton.ImageTransparentColor = Color.Magenta
            SaveButton.Margin = New Padding(5)
            SaveButton.Name = "SaveButton"
            SaveButton.Size = New Size(36, 36)
            SaveButton.Text = "Save (Ctrl+Shift+S)"
            '
            ' EditStrip
            '
            EditStrip.AllowItemReorder = True
            EditStrip.AutoSize = False
            EditStrip.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(0), CByte(0))
            EditStrip.Dock = DockStyle.None
            EditStrip.GripMargin = New Padding(0)
            EditStrip.GripStyle = ToolStripGripStyle.Hidden
            EditStrip.ImageScalingSize = New Size(32, 32)
            EditStrip.Items.AddRange(New ToolStripItem() {CensorTool, MarkerTool, CroppingTool, EraserTool, BlurTool, EllipseTool, PipettenTool, BrightenTool, ArrowTool, UndoStuff, ToolStripDropDownButton1})
            EditStrip.Location = New Point(5, 47)
            EditStrip.Name = "EditStrip"
            EditStrip.Padding = New Padding(0)
            EditStrip.RenderMode = ToolStripRenderMode.System
            EditStrip.Size = New Size(439, 39)
            EditStrip.TabIndex = 11
            EditStrip.Text = "Edit"
            '
            ' CensorTool
            '
            CensorTool.CheckOnClick = True
            CensorTool.DisplayStyle = ToolStripItemDisplayStyle.Image
            CensorTool.Image = My.Resources.Resources.censorMedium
            CensorTool.ImageScaling = ToolStripItemImageScaling.None
            CensorTool.ImageTransparentColor = Color.Magenta
            CensorTool.Margin = New Padding(2, 0, 2, 2)
            CensorTool.Name = "CensorTool"
            CensorTool.Size = New Size(36, 37)
            CensorTool.Text = "Redact (Ctrl+A)"
            '
            ' MarkerTool
            '
            MarkerTool.CheckOnClick = True
            MarkerTool.DisplayStyle = ToolStripItemDisplayStyle.Image
            MarkerTool.Image = My.Resources.Resources.highlighterMedium
            MarkerTool.ImageScaling = ToolStripItemImageScaling.None
            MarkerTool.ImageTransparentColor = Color.Magenta
            MarkerTool.Margin = New Padding(2, 0, 2, 2)
            MarkerTool.Name = "MarkerTool"
            MarkerTool.Size = New Size(36, 37)
            MarkerTool.Text = "Mark (Ctrl+S)"
            '
            ' CroppingTool
            '
            CroppingTool.CheckOnClick = True
            CroppingTool.DisplayStyle = ToolStripItemDisplayStyle.Image
            CroppingTool.Image = My.Resources.Resources.cropMedium
            CroppingTool.ImageTransparentColor = Color.Magenta
            CroppingTool.Margin = New Padding(2, 0, 2, 2)
            CroppingTool.Name = "CroppingTool"
            CroppingTool.Size = New Size(36, 37)
            CroppingTool.Text = "Crop Image (Ctrl+D)"
            '
            ' EraserTool
            '
            EraserTool.CheckOnClick = True
            EraserTool.DisplayStyle = ToolStripItemDisplayStyle.Image
            EraserTool.Image = My.Resources.Resources.Eraser
            EraserTool.ImageTransparentColor = Color.Magenta
            EraserTool.Margin = New Padding(2, 0, 2, 2)
            EraserTool.Name = "EraserTool"
            EraserTool.Size = New Size(36, 37)
            EraserTool.Text = "Eraser (Ctrl+E)"
            '
            ' BlurTool
            '
            BlurTool.CheckOnClick = True
            BlurTool.DisplayStyle = ToolStripItemDisplayStyle.Image
            BlurTool.Image = My.Resources.Resources.blurMedium
            BlurTool.ImageTransparentColor = Color.Magenta
            BlurTool.Margin = New Padding(2, 0, 2, 2)
            BlurTool.Name = "BlurTool"
            BlurTool.Size = New Size(36, 37)
            BlurTool.Text = "Blur Area (Ctrl+F)"
            '
            ' EllipseTool
            '
            EllipseTool.DisplayStyle = ToolStripItemDisplayStyle.Image
            EllipseTool.Image = My.Resources.Resources.circleMedium
            EllipseTool.ImageTransparentColor = Color.Magenta
            EllipseTool.Margin = New Padding(2, 0, 2, 2)
            EllipseTool.Name = "EllipseTool"
            EllipseTool.Size = New Size(36, 37)
            EllipseTool.Text = "Ellipse (Ctrl+H)"
            '
            ' PipettenTool
            '
            PipettenTool.DisplayStyle = ToolStripItemDisplayStyle.Image
            PipettenTool.Image = My.Resources.Resources.pickerMedium
            PipettenTool.ImageTransparentColor = Color.Magenta
            PipettenTool.Margin = New Padding(2, 0, 2, 2)
            PipettenTool.Name = "PipettenTool"
            PipettenTool.Size = New Size(36, 37)
            PipettenTool.Text = "Eye Dropper"
            '
            ' BrightenTool
            '
            BrightenTool.DisplayStyle = ToolStripItemDisplayStyle.Image
            BrightenTool.Image = My.Resources.Resources.brightenMedium
            BrightenTool.ImageTransparentColor = Color.Magenta
            BrightenTool.Margin = New Padding(2, 0, 2, 2)
            BrightenTool.Name = "BrightenTool"
            BrightenTool.Size = New Size(36, 37)
            BrightenTool.Text = "Brighten or Darken Image"
            '
            ' ArrowTool
            '
            ArrowTool.CheckOnClick = True
            ArrowTool.DisplayStyle = ToolStripItemDisplayStyle.Image
            ArrowTool.Image = My.Resources.Resources.arrowMedium
            ArrowTool.ImageTransparentColor = Color.Magenta
            ArrowTool.Margin = New Padding(2, 0, 2, 2)
            ArrowTool.Name = "ArrowTool"
            ArrowTool.Size = New Size(36, 37)
            ArrowTool.Text = "Arrow (Ctrl+G)"
            '
            ' UndoStuff
            '
            UndoStuff.DisplayStyle = ToolStripItemDisplayStyle.Image
            UndoStuff.Image = My.Resources.Resources.undoMedium
            UndoStuff.ImageScaling = ToolStripItemImageScaling.None
            UndoStuff.ImageTransparentColor = Color.Magenta
            UndoStuff.Margin = New Padding(20, 0, 0, 2)
            UndoStuff.Name = "UndoStuff"
            UndoStuff.Size = New Size(36, 37)
            UndoStuff.Text = "Undo (Ctrl+Z)"
            '
            ' ToolStripDropDownButton1
            '
            ToolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image
            ToolStripDropDownButton1.DropDownItems.AddRange(New ToolStripItem() {ZensToolStripMenuItem, MarkToolStripMenuItem, CropToolStripMenuItem, EraseToolStripMenuItem, PixelateToolStripMenuItem, KreisToolStripMenuItem, ArrowToolStripMenuItem, ResetToolStripMenuItem, UploadToolStripMenuItem, SaveToolStripMenuItem, ClipboardToolStripMenuItem, PrintToolStripMenuItem, ChooseServiceToolStripMenuItem})
            ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), Image)
            ToolStripDropDownButton1.ImageTransparentColor = Color.Magenta
            ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
            ToolStripDropDownButton1.Size = New Size(45, 36)
            ToolStripDropDownButton1.Text = "ToolStripDropDownButton1"
            ToolStripDropDownButton1.Visible = False
            '
            ' ZensToolStripMenuItem
            '
            ZensToolStripMenuItem.Name = "ZensToolStripMenuItem"
            ZensToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.A
            ZensToolStripMenuItem.Size = New Size(226, 22)
            ZensToolStripMenuItem.Text = "Zens"
            ZensToolStripMenuItem.Visible = False
            '
            ' MarkToolStripMenuItem
            '
            MarkToolStripMenuItem.Name = "MarkToolStripMenuItem"
            MarkToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.S
            MarkToolStripMenuItem.Size = New Size(226, 22)
            MarkToolStripMenuItem.Text = "Mark"
            MarkToolStripMenuItem.Visible = False
            '
            ' CropToolStripMenuItem
            '
            CropToolStripMenuItem.Name = "CropToolStripMenuItem"
            CropToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.D
            CropToolStripMenuItem.Size = New Size(226, 22)
            CropToolStripMenuItem.Text = "Crop"
            CropToolStripMenuItem.Visible = False
            '
            ' EraseToolStripMenuItem
            '
            EraseToolStripMenuItem.Name = "EraseToolStripMenuItem"
            EraseToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.E
            EraseToolStripMenuItem.Size = New Size(226, 22)
            EraseToolStripMenuItem.Text = "Erase"
            EraseToolStripMenuItem.Visible = False
            '
            ' PixelateToolStripMenuItem
            '
            PixelateToolStripMenuItem.Name = "PixelateToolStripMenuItem"
            PixelateToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.F
            PixelateToolStripMenuItem.Size = New Size(226, 22)
            PixelateToolStripMenuItem.Text = "Pixelate"
            PixelateToolStripMenuItem.Visible = False
            '
            ' KreisToolStripMenuItem
            '
            KreisToolStripMenuItem.Name = "KreisToolStripMenuItem"
            KreisToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.H
            KreisToolStripMenuItem.Size = New Size(226, 22)
            KreisToolStripMenuItem.Text = "Circle"
            KreisToolStripMenuItem.Visible = False
            '
            ' ArrowToolStripMenuItem
            '
            ArrowToolStripMenuItem.Name = "ArrowToolStripMenuItem"
            ArrowToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.G
            ArrowToolStripMenuItem.Size = New Size(226, 22)
            ArrowToolStripMenuItem.Text = "Arrow"
            ArrowToolStripMenuItem.Visible = False
            '
            ' ResetToolStripMenuItem
            '
            ResetToolStripMenuItem.Name = "ResetToolStripMenuItem"
            ResetToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Z
            ResetToolStripMenuItem.Size = New Size(226, 22)
            ResetToolStripMenuItem.Text = "Reset"
            ResetToolStripMenuItem.Visible = False
            '
            ' UploadToolStripMenuItem
            '
            UploadToolStripMenuItem.Name = "UploadToolStripMenuItem"
            UploadToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Q
            UploadToolStripMenuItem.Size = New Size(226, 22)
            UploadToolStripMenuItem.Text = "Upload"
            UploadToolStripMenuItem.Visible = False
            '
            ' SaveToolStripMenuItem
            '
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
            SaveToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Shift Or Keys.S
            SaveToolStripMenuItem.Size = New Size(226, 22)
            SaveToolStripMenuItem.Text = "Save"
            SaveToolStripMenuItem.Visible = False
            '
            ' ClipboardToolStripMenuItem
            '
            ClipboardToolStripMenuItem.Name = "ClipboardToolStripMenuItem"
            ClipboardToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.C
            ClipboardToolStripMenuItem.Size = New Size(226, 22)
            ClipboardToolStripMenuItem.Text = "Clipboard"
            ClipboardToolStripMenuItem.Visible = False
            '
            ' PrintToolStripMenuItem
            '
            PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
            PrintToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.P
            PrintToolStripMenuItem.Size = New Size(226, 22)
            PrintToolStripMenuItem.Text = "Print"
            PrintToolStripMenuItem.Visible = False
            '
            ' ChooseServiceToolStripMenuItem
            '
            ChooseServiceToolStripMenuItem.Name = "ChooseServiceToolStripMenuItem"
            ChooseServiceToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Shift Or Keys.Q
            ChooseServiceToolStripMenuItem.Size = New Size(226, 22)
            ChooseServiceToolStripMenuItem.Text = "ChooseService"
            ChooseServiceToolStripMenuItem.Visible = False
            '
            ' ToolStrip1
            '
            ToolStrip1.AllowItemReorder = True
            ToolStrip1.AutoSize = False
            ToolStrip1.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(0), CByte(0))
            ToolStrip1.Dock = DockStyle.None
            ToolStrip1.GripMargin = New Padding(0)
            ToolStrip1.GripStyle = ToolStripGripStyle.Hidden
            ToolStrip1.Items.AddRange(New ToolStripItem() {ScaleTool, DrawCursor})
            ToolStrip1.Location = New Point(140, 21)
            ToolStrip1.Name = "ToolStrip1"
            ToolStrip1.Padding = New Padding(0)
            ToolStrip1.RenderMode = ToolStripRenderMode.System
            ToolStrip1.Size = New Size(115, 30)
            ToolStrip1.TabIndex = 27
            ToolStrip1.Text = "Edit"
            '
            ' ScaleTool
            '
            ScaleTool.DisplayStyle = ToolStripItemDisplayStyle.Image
            ScaleTool.Image = My.Resources.Resources.scaleSmall
            ScaleTool.ImageTransparentColor = Color.Magenta
            ScaleTool.Margin = New Padding(2, 5, 2, 5)
            ScaleTool.Name = "ScaleTool"
            ScaleTool.Size = New Size(23, 20)
            ScaleTool.Text = "Scale Image"
            '
            ' DrawCursor
            '
            DrawCursor.CheckOnClick = True
            DrawCursor.DisplayStyle = ToolStripItemDisplayStyle.Image
            DrawCursor.Image = My.Resources.Resources.cursorMedium
            DrawCursor.ImageTransparentColor = Color.Magenta
            DrawCursor.Margin = New Padding(2, 5, 2, 5)
            DrawCursor.Name = "DrawCursor"
            DrawCursor.Size = New Size(23, 20)
            DrawCursor.Text = "Draw Cursor"
            '
            ' BottomToolStrip
            '
            BottomToolStrip.Dock = DockStyle.Bottom
            BottomToolStrip.GripStyle = ToolStripGripStyle.Hidden
            BottomToolStrip.Items.AddRange(New ToolStripItem() {MouseInfoLabel, ToolStripSeparator1, ImageInfoLabel})
            BottomToolStrip.Location = New Point(0, 588)
            BottomToolStrip.Name = "BottomToolStrip"
            BottomToolStrip.RenderMode = ToolStripRenderMode.System
            BottomToolStrip.Size = New Size(759, 25)
            BottomToolStrip.TabIndex = 30
            '
            ' MouseInfoLabel
            '
            MouseInfoLabel.AutoSize = False
            MouseInfoLabel.BackColor = Color.Transparent
            MouseInfoLabel.Image = My.Resources.Resources.cursorPositionSmall
            MouseInfoLabel.Margin = New Padding(15, 1, 0, 2)
            MouseInfoLabel.Name = "MouseInfoLabel"
            MouseInfoLabel.Size = New Size(120, 22)
            MouseInfoLabel.Text = "MouseInfoLabel"
            '
            ' ToolStripSeparator1
            '
            ToolStripSeparator1.Name = "ToolStripSeparator1"
            ToolStripSeparator1.Size = New Size(6, 25)
            '
            ' ImageInfoLabel
            '
            ImageInfoLabel.BackColor = Color.Transparent
            ImageInfoLabel.Image = My.Resources.Resources.imageDimensionsSmall
            ImageInfoLabel.Margin = New Padding(5, 1, 0, 2)
            ImageInfoLabel.Name = "ImageInfoLabel"
            ImageInfoLabel.Size = New Size(105, 22)
            ImageInfoLabel.Text = "ImageInfoLabel"
            '
            ' CopyPrintToolStrip
            '
            CopyPrintToolStrip.BackColor = Color.Transparent
            CopyPrintToolStrip.Dock = DockStyle.None
            CopyPrintToolStrip.GripStyle = ToolStripGripStyle.Hidden
            CopyPrintToolStrip.Items.AddRange(New ToolStripItem() {CopyToClipboard})
            CopyPrintToolStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow
            CopyPrintToolStrip.Location = New Point(116, 6)
            CopyPrintToolStrip.Name = "CopyPrintToolStrip"
            CopyPrintToolStrip.RenderMode = ToolStripRenderMode.System
            CopyPrintToolStrip.Size = New Size(24, 22)
            CopyPrintToolStrip.TabIndex = 31
            '
            ' CopyToClipboard
            '
            CopyToClipboard.BackColor = Color.Transparent
            CopyToClipboard.DisplayStyle = ToolStripItemDisplayStyle.Image
            CopyToClipboard.Image = My.Resources.Resources.clipboardSmall
            CopyToClipboard.ImageScaling = ToolStripItemImageScaling.None
            CopyToClipboard.ImageTransparentColor = Color.Magenta
            CopyToClipboard.Margin = New Padding(0)
            CopyToClipboard.Name = "CopyToClipboard"
            CopyToClipboard.Size = New Size(22, 20)
            CopyToClipboard.Text = "Copy to Clipboard (Ctrl+C)"
            '
            ' ThePanel
            '
            ThePanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            ThePanel.BackColor = Color.FromArgb(CByte(207), CByte(217), CByte(231))
            ThePanel.BackgroundImageLayout = ImageLayout.None
            ThePanel.Font = New Font("Segoe UI", 9F)
            ThePanel.Location = New Point(0, 120)
            ThePanel.Margin = New Padding(0)
            ThePanel.Name = "ThePanel"
            ThePanel.Size = New Size(759, 468)
            ThePanel.TabIndex = 12
            '
            ' AutoCloseShotEditor
            '
            AutoCloseShotEditor.AutoSize = True
            AutoCloseShotEditor.FlatStyle = FlatStyle.System
            AutoCloseShotEditor.Location = New Point(148, 9)
            AutoCloseShotEditor.Name = "AutoCloseShotEditor"
            AutoCloseShotEditor.Size = New Size(208, 20)
            AutoCloseShotEditor.TabIndex = 32
            AutoCloseShotEditor.Text = "Close ShotEditor when uploading"
            AutoCloseShotEditor.UseVisualStyleBackColor = True
            '
            ' CurrentToolSettingsPanel
            '
            CurrentToolSettingsPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            CurrentToolSettingsPanel.BackColor = SystemColors.Control
            CurrentToolSettingsPanel.Location = New Point(447, 4)
            CurrentToolSettingsPanel.Name = "CurrentToolSettingsPanel"
            CurrentToolSettingsPanel.Size = New Size(298, 82)
            CurrentToolSettingsPanel.TabIndex = 33
            CurrentToolSettingsPanel.Visible = False
            '
            ' ShotEditor
            '
            AutoScaleMode = AutoScaleMode.None
            BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(247))
            ClientSize = New Size(759, 613)
            Controls.Add(CurrentToolSettingsPanel)
            Controls.Add(AutoCloseShotEditor)
            Controls.Add(ThePanel)
            Controls.Add(EditStrip)
            Controls.Add(CopyPrintToolStrip)
            Controls.Add(ToolStrip1)
            Controls.Add(ShareStrip)
            Controls.Add(BottomToolStrip)
            Font = New Font("Segoe UI", 9F)
            Icon = CType(resources.GetObject("$this.Icon"), Icon)
            MinimumSize = New Size(775, 200)
            Name = "ShotEditor"
            SizeGripStyle = SizeGripStyle.Show
            Text = "ShotEditor"
            ShareStrip.ResumeLayout(False)
            ShareStrip.PerformLayout()
            EditStrip.ResumeLayout(False)
            EditStrip.PerformLayout()
            ToolStrip1.ResumeLayout(False)
            ToolStrip1.PerformLayout()
            BottomToolStrip.ResumeLayout(False)
            BottomToolStrip.PerformLayout()
            CopyPrintToolStrip.ResumeLayout(False)
            CopyPrintToolStrip.PerformLayout()
            ResumeLayout(False)
            PerformLayout()

        End Sub
        Friend WithEvents ShareStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents SaveButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents EditStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents CensorTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents MarkerTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents UndoStuff As System.Windows.Forms.ToolStripButton
        Friend WithEvents CroppingTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents ThePanel As PaintPanel2
        Friend WithEvents ArrowTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents EraserTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents BlurTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
        Friend WithEvents ZensToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents MarkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CropToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents EraseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents PixelateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ArrowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ResetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents UploadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ClipboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents EllipseTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents KreisToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents UploadToHoster As System.Windows.Forms.ToolStripSplitButton
        Friend WithEvents ChooseServiceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents PipettenTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents BrightenTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
        Friend WithEvents ScaleTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents DrawCursor As System.Windows.Forms.ToolStripButton
        Friend WithEvents BottomToolStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents ImageInfoLabel As System.Windows.Forms.ToolStripLabel
        Friend WithEvents MouseInfoLabel As System.Windows.Forms.ToolStripLabel
        Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents CopyPrintToolStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents CopyToClipboard As System.Windows.Forms.ToolStripButton
        Friend WithEvents AutoCloseShotEditor As System.Windows.Forms.CheckBox
        Friend WithEvents CurrentToolSettingsPanel As Panel
    End Class
End Namespace
