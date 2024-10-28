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
            TextToolButton = New ToolStripButton()
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
            TextToolStripMenuItem = New ToolStripMenuItem()
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
            EllipseSettingsPanel = New Panel()
            EllipseOrRectangleBox = New PictureBox()
            GlassLabel12 = New Label()
            EllipseOrRectangle = New TrackBar()
            GlassLabel8 = New Label()
            GlassLabel7 = New Label()
            Ellipse_Width = New Label()
            EllipseColorSelector = New Windows.Forms.ColorSelector()
            EllipseBar = New TrackBar()
            BrightenSettingsPanel = New Panel()
            BigColorViewer1 = New Windows.Forms.ColorView()
            BlackWhiteTracker = New TrackBar()
            GlassLabel9 = New Label()
            GlassLabel6 = New Label()
            ArrowSettingsPanel = New Panel()
            ArrowWidthSlider = New TrackBar()
            GlassLabel11 = New Label()
            ArrowWidthLabel = New Label()
            GlassLabel10 = New Label()
            ArrowColorviewer = New Windows.Forms.ColorSelector()
            ToolStrip1 = New ToolStrip()
            ScaleTool = New ToolStripButton()
            DrawCursor = New ToolStripButton()
            BottomToolStrip = New ToolStrip()
            MouseInfoLabel = New ToolStripLabel()
            ToolStripSeparator1 = New ToolStripSeparator()
            ImageInfoLabel = New ToolStripLabel()
            CopyPrintToolStrip = New ToolStrip()
            CopyToClipboard = New ToolStripButton()
            ThePanel = New PaintPanel()
            AutoCloseShotEditor = New CheckBox()
            CurrentToolSettingsPanel = New Panel()
            ShareStrip.SuspendLayout()
            EditStrip.SuspendLayout()
            EllipseSettingsPanel.SuspendLayout()
            CType(EllipseOrRectangleBox, ComponentModel.ISupportInitialize).BeginInit()
            CType(EllipseOrRectangle, ComponentModel.ISupportInitialize).BeginInit()
            CType(EllipseBar, ComponentModel.ISupportInitialize).BeginInit()
            BrightenSettingsPanel.SuspendLayout()
            CType(BlackWhiteTracker, ComponentModel.ISupportInitialize).BeginInit()
            ArrowSettingsPanel.SuspendLayout()
            CType(ArrowWidthSlider, ComponentModel.ISupportInitialize).BeginInit()
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
            EditStrip.Items.AddRange(New ToolStripItem() {CensorTool, MarkerTool, TextToolButton, CroppingTool, EraserTool, BlurTool, EllipseTool, PipettenTool, BrightenTool, ArrowTool, UndoStuff, ToolStripDropDownButton1})
            EditStrip.Location = New Point(5, 47)
            EditStrip.Name = "EditStrip"
            EditStrip.Padding = New Padding(0)
            EditStrip.RenderMode = ToolStripRenderMode.System
            EditStrip.Size = New Size(475, 39)
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
            ' TextToolButton
            ' 
            TextToolButton.CheckOnClick = True
            TextToolButton.DisplayStyle = ToolStripItemDisplayStyle.Image
            TextToolButton.Image = My.Resources.Resources.textMedium
            TextToolButton.ImageTransparentColor = Color.Magenta
            TextToolButton.Margin = New Padding(2, 0, 2, 2)
            TextToolButton.Name = "TextToolButton"
            TextToolButton.Size = New Size(36, 37)
            TextToolButton.Text = "Insert Text (Ctrl+T)"
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
            ToolStripDropDownButton1.DropDownItems.AddRange(New ToolStripItem() {ZensToolStripMenuItem, MarkToolStripMenuItem, TextToolStripMenuItem, CropToolStripMenuItem, EraseToolStripMenuItem, PixelateToolStripMenuItem, KreisToolStripMenuItem, ArrowToolStripMenuItem, ResetToolStripMenuItem, UploadToolStripMenuItem, SaveToolStripMenuItem, ClipboardToolStripMenuItem, PrintToolStripMenuItem, ChooseServiceToolStripMenuItem})
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
            ' TextToolStripMenuItem
            ' 
            TextToolStripMenuItem.Name = "TextToolStripMenuItem"
            TextToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.T
            TextToolStripMenuItem.Size = New Size(226, 22)
            TextToolStripMenuItem.Text = "Text"
            TextToolStripMenuItem.Visible = False
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
            ' EllipseSettingsPanel
            ' 
            EllipseSettingsPanel.BackColor = SystemColors.Control
            EllipseSettingsPanel.Controls.Add(EllipseOrRectangleBox)
            EllipseSettingsPanel.Controls.Add(GlassLabel12)
            EllipseSettingsPanel.Controls.Add(EllipseOrRectangle)
            EllipseSettingsPanel.Controls.Add(GlassLabel8)
            EllipseSettingsPanel.Controls.Add(GlassLabel7)
            EllipseSettingsPanel.Controls.Add(Ellipse_Width)
            EllipseSettingsPanel.Controls.Add(EllipseColorSelector)
            EllipseSettingsPanel.Controls.Add(EllipseBar)
            EllipseSettingsPanel.Location = New Point(193, 186)
            EllipseSettingsPanel.Name = "EllipseSettingsPanel"
            EllipseSettingsPanel.Size = New Size(273, 82)
            EllipseSettingsPanel.TabIndex = 21
            EllipseSettingsPanel.Visible = False
            ' 
            ' EllipseOrRectangleBox
            ' 
            EllipseOrRectangleBox.Location = New Point(242, 50)
            EllipseOrRectangleBox.Name = "EllipseOrRectangleBox"
            EllipseOrRectangleBox.Size = New Size(16, 16)
            EllipseOrRectangleBox.TabIndex = 28
            EllipseOrRectangleBox.TabStop = False
            ' 
            ' GlassLabel12
            ' 
            GlassLabel12.AutoSize = True
            GlassLabel12.Location = New Point(119, 50)
            GlassLabel12.Name = "GlassLabel12"
            GlassLabel12.Size = New Size(41, 15)
            GlassLabel12.TabIndex = 27
            GlassLabel12.Text = "Mode:"
            ' 
            ' EllipseOrRectangle
            ' 
            EllipseOrRectangle.Location = New Point(166, 50)
            EllipseOrRectangle.Maximum = 1
            EllipseOrRectangle.Name = "EllipseOrRectangle"
            EllipseOrRectangle.Size = New Size(70, 45)
            EllipseOrRectangle.TabIndex = 26
            EllipseOrRectangle.TabStop = False
            ' 
            ' GlassLabel8
            ' 
            GlassLabel8.AutoSize = True
            GlassLabel8.Location = New Point(42, 50)
            GlassLabel8.Name = "GlassLabel8"
            GlassLabel8.Size = New Size(39, 15)
            GlassLabel8.TabIndex = 25
            GlassLabel8.Text = "Color:"
            ' 
            ' GlassLabel7
            ' 
            GlassLabel7.AutoSize = True
            GlassLabel7.Location = New Point(41, 10)
            GlassLabel7.Name = "GlassLabel7"
            GlassLabel7.Size = New Size(42, 15)
            GlassLabel7.TabIndex = 24
            GlassLabel7.Text = "Width:"
            ' 
            ' Ellipse_Width
            ' 
            Ellipse_Width.AutoSize = True
            Ellipse_Width.Location = New Point(193, 15)
            Ellipse_Width.Name = "Ellipse_Width"
            Ellipse_Width.Size = New Size(13, 15)
            Ellipse_Width.TabIndex = 22
            Ellipse_Width.Text = "a"
            ' 
            ' EllipseColorSelector
            ' 
            EllipseColorSelector.Cursor = Cursors.Hand
            EllipseColorSelector.Location = New Point(87, 50)
            EllipseColorSelector.Name = "EllipseColorSelector"
            EllipseColorSelector.Size = New Size(20, 20)
            EllipseColorSelector.TabIndex = 19
            ' 
            ' EllipseBar
            ' 
            EllipseBar.Location = New Point(83, 6)
            EllipseBar.Maximum = 100
            EllipseBar.Minimum = 1
            EllipseBar.Name = "EllipseBar"
            EllipseBar.Size = New Size(104, 45)
            EllipseBar.TabIndex = 16
            EllipseBar.TabStop = False
            EllipseBar.Value = 1
            ' 
            ' BrightenSettingsPanel
            ' 
            BrightenSettingsPanel.BackColor = SystemColors.Control
            BrightenSettingsPanel.Controls.Add(BigColorViewer1)
            BrightenSettingsPanel.Controls.Add(BlackWhiteTracker)
            BrightenSettingsPanel.Controls.Add(GlassLabel9)
            BrightenSettingsPanel.Controls.Add(GlassLabel6)
            BrightenSettingsPanel.Location = New Point(193, 274)
            BrightenSettingsPanel.Name = "BrightenSettingsPanel"
            BrightenSettingsPanel.Size = New Size(273, 82)
            BrightenSettingsPanel.TabIndex = 26
            BrightenSettingsPanel.Visible = False
            ' 
            ' BigColorViewer1
            ' 
            BigColorViewer1.Location = New Point(61, 48)
            BigColorViewer1.Name = "BigColorViewer1"
            BigColorViewer1.Size = New Size(158, 26)
            BigColorViewer1.TabIndex = 27
            BigColorViewer1.TabStop = False
            ' 
            ' BlackWhiteTracker
            ' 
            BlackWhiteTracker.Location = New Point(73, 12)
            BlackWhiteTracker.Maximum = 510
            BlackWhiteTracker.Name = "BlackWhiteTracker"
            BlackWhiteTracker.Size = New Size(122, 45)
            BlackWhiteTracker.TabIndex = 26
            BlackWhiteTracker.TabStop = False
            BlackWhiteTracker.Value = 1
            ' 
            ' GlassLabel9
            ' 
            GlassLabel9.AutoSize = True
            GlassLabel9.Location = New Point(201, 12)
            GlassLabel9.Name = "GlassLabel9"
            GlassLabel9.Size = New Size(52, 15)
            GlassLabel9.TabIndex = 26
            GlassLabel9.Text = "Brighten"
            ' 
            ' GlassLabel6
            ' 
            GlassLabel6.AutoSize = True
            GlassLabel6.Location = New Point(31, 12)
            GlassLabel6.Name = "GlassLabel6"
            GlassLabel6.Size = New Size(44, 15)
            GlassLabel6.TabIndex = 25
            GlassLabel6.Text = "Darken"
            ' 
            ' ArrowSettingsPanel
            ' 
            ArrowSettingsPanel.BackColor = SystemColors.Control
            ArrowSettingsPanel.Controls.Add(ArrowWidthSlider)
            ArrowSettingsPanel.Controls.Add(GlassLabel11)
            ArrowSettingsPanel.Controls.Add(ArrowWidthLabel)
            ArrowSettingsPanel.Controls.Add(GlassLabel10)
            ArrowSettingsPanel.Controls.Add(ArrowColorviewer)
            ArrowSettingsPanel.Location = New Point(193, 362)
            ArrowSettingsPanel.Name = "ArrowSettingsPanel"
            ArrowSettingsPanel.Size = New Size(273, 82)
            ArrowSettingsPanel.TabIndex = 26
            ArrowSettingsPanel.Visible = False
            ' 
            ' ArrowWidthSlider
            ' 
            ArrowWidthSlider.Location = New Point(84, 3)
            ArrowWidthSlider.Maximum = 100
            ArrowWidthSlider.Name = "ArrowWidthSlider"
            ArrowWidthSlider.Size = New Size(112, 45)
            ArrowWidthSlider.TabIndex = 26
            ArrowWidthSlider.TabStop = False
            ArrowWidthSlider.Value = 1
            ' 
            ' GlassLabel11
            ' 
            GlassLabel11.AutoSize = True
            GlassLabel11.Location = New Point(46, 10)
            GlassLabel11.Name = "GlassLabel11"
            GlassLabel11.Size = New Size(42, 15)
            GlassLabel11.TabIndex = 28
            GlassLabel11.Text = "Width:"
            ' 
            ' ArrowWidthLabel
            ' 
            ArrowWidthLabel.AutoSize = True
            ArrowWidthLabel.Location = New Point(202, 3)
            ArrowWidthLabel.Name = "ArrowWidthLabel"
            ArrowWidthLabel.Size = New Size(13, 15)
            ArrowWidthLabel.TabIndex = 27
            ArrowWidthLabel.Text = "a"
            ' 
            ' GlassLabel10
            ' 
            GlassLabel10.AutoSize = True
            GlassLabel10.Location = New Point(44, 50)
            GlassLabel10.Name = "GlassLabel10"
            GlassLabel10.Size = New Size(39, 15)
            GlassLabel10.TabIndex = 25
            GlassLabel10.Text = "Color:"
            ' 
            ' ArrowColorviewer
            ' 
            ArrowColorviewer.Cursor = Cursors.Hand
            ArrowColorviewer.Location = New Point(93, 50)
            ArrowColorviewer.Name = "ArrowColorviewer"
            ArrowColorviewer.Size = New Size(20, 20)
            ArrowColorviewer.TabIndex = 19
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
            ThePanel.Size = New Size(165, 468)
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
            CurrentToolSettingsPanel.BorderStyle = BorderStyle.FixedSingle
            CurrentToolSettingsPanel.Location = New Point(472, 4)
            CurrentToolSettingsPanel.Name = "CurrentToolSettingsPanel"
            CurrentToolSettingsPanel.Size = New Size(273, 82)
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
            Controls.Add(BrightenSettingsPanel)
            Controls.Add(ArrowSettingsPanel)
            Controls.Add(EllipseSettingsPanel)
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
            EllipseSettingsPanel.ResumeLayout(False)
            EllipseSettingsPanel.PerformLayout()
            CType(EllipseOrRectangleBox, ComponentModel.ISupportInitialize).EndInit()
            CType(EllipseOrRectangle, ComponentModel.ISupportInitialize).EndInit()
            CType(EllipseBar, ComponentModel.ISupportInitialize).EndInit()
            BrightenSettingsPanel.ResumeLayout(False)
            BrightenSettingsPanel.PerformLayout()
            CType(BlackWhiteTracker, ComponentModel.ISupportInitialize).EndInit()
            ArrowSettingsPanel.ResumeLayout(False)
            ArrowSettingsPanel.PerformLayout()
            CType(ArrowWidthSlider, ComponentModel.ISupportInitialize).EndInit()
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
        Friend WithEvents TextToolButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents CroppingTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents ThePanel As PaintPanel
        Friend WithEvents ArrowTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents EraserTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents BlurTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
        Friend WithEvents ZensToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents MarkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TextToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
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
        Friend WithEvents EllipseSettingsPanel As System.Windows.Forms.Panel
        Friend WithEvents EllipseColorSelector As HolzShots.Windows.Forms.ColorSelector
        Friend WithEvents EllipseBar As System.Windows.Forms.TrackBar
        Friend WithEvents Ellipse_Width As System.Windows.Forms.Label
        Friend WithEvents GlassLabel8 As System.Windows.Forms.Label
        Friend WithEvents GlassLabel7 As System.Windows.Forms.Label
        Friend WithEvents UploadToHoster As System.Windows.Forms.ToolStripSplitButton
        Friend WithEvents ChooseServiceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents PipettenTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents BrightenTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents BrightenSettingsPanel As System.Windows.Forms.Panel
        Friend WithEvents BlackWhiteTracker As System.Windows.Forms.TrackBar
        Friend WithEvents GlassLabel9 As System.Windows.Forms.Label
        Friend WithEvents GlassLabel6 As System.Windows.Forms.Label
        Friend WithEvents BigColorViewer1 As HolzShots.Windows.Forms.ColorView
        Friend WithEvents ArrowSettingsPanel As System.Windows.Forms.Panel
        Friend WithEvents GlassLabel10 As System.Windows.Forms.Label
        Friend WithEvents ArrowColorviewer As HolzShots.Windows.Forms.ColorSelector
        Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
        Friend WithEvents ScaleTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents DrawCursor As System.Windows.Forms.ToolStripButton
        Friend WithEvents ArrowWidthSlider As System.Windows.Forms.TrackBar
        Friend WithEvents GlassLabel11 As System.Windows.Forms.Label
        Friend WithEvents ArrowWidthLabel As System.Windows.Forms.Label
        Friend WithEvents GlassLabel12 As System.Windows.Forms.Label
        Friend WithEvents EllipseOrRectangle As System.Windows.Forms.TrackBar
        Friend WithEvents EllipseOrRectangleBox As System.Windows.Forms.PictureBox
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
