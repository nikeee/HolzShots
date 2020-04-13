Imports HolzShots.UI.Controls

Namespace UI.Specialized

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Friend Class ShotEditor
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
            Me.ShareStrip = New System.Windows.Forms.ToolStrip()
            Me.UploadToHoster = New System.Windows.Forms.ToolStripSplitButton()
            Me.save_btn = New System.Windows.Forms.ToolStripButton()
            Me.DruckTeil = New System.Drawing.Printing.PrintDocument()
            Me.DruckDialog = New System.Windows.Forms.PrintDialog()
            Me.EditStrip = New System.Windows.Forms.ToolStrip()
            Me.CensorTool = New System.Windows.Forms.ToolStripButton()
            Me.MarkerTool = New System.Windows.Forms.ToolStripButton()
            Me.TextToolButton = New System.Windows.Forms.ToolStripButton()
            Me.CroppingTool = New System.Windows.Forms.ToolStripButton()
            Me.EraserTool = New System.Windows.Forms.ToolStripButton()
            Me.BlurTool = New System.Windows.Forms.ToolStripButton()
            Me.EllipseTool = New System.Windows.Forms.ToolStripButton()
            Me.PipettenTool = New System.Windows.Forms.ToolStripButton()
            Me.BrightenTool = New System.Windows.Forms.ToolStripButton()
            Me.ArrowTool = New System.Windows.Forms.ToolStripButton()
            Me.UndoStuff = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
            Me.ZensToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.MarkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.TextToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.CropToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.EraseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.PixelateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.KreisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ArrowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ResetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.UploadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ChooseServiceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.CensorSettingsPanel = New System.Windows.Forms.Panel()
            Me.GlassLabel2 = New System.Windows.Forms.Label()
            Me.GlassLabel3 = New System.Windows.Forms.Label()
            Me.Pinsel_Width_Zensursula = New System.Windows.Forms.Label()
            Me.Zensursula_Viewer = New HolzShots.UI.Controls.ColorViewer()
            Me.ZensursulaBar = New System.Windows.Forms.TrackBar()
            Me.MarkerSettingsPanel = New System.Windows.Forms.Panel()
            Me.GlassLabel4 = New System.Windows.Forms.Label()
            Me.GlassLabel1 = New System.Windows.Forms.Label()
            Me.Pinsel_Width_Marker = New System.Windows.Forms.Label()
            Me.Marker_Viewer = New HolzShots.UI.Controls.ColorViewer()
            Me.MarkerBar = New System.Windows.Forms.TrackBar()
            Me.EraserSettingsPanel = New System.Windows.Forms.Panel()
            Me.GlassLabel5 = New System.Windows.Forms.Label()
            Me.Eraser_Diameter = New System.Windows.Forms.Label()
            Me.EraserBar = New System.Windows.Forms.TrackBar()
            Me.EllipseSettingsPanel = New System.Windows.Forms.Panel()
            Me.EllipseOrRectangleBox = New System.Windows.Forms.PictureBox()
            Me.GlassLabel12 = New System.Windows.Forms.Label()
            Me.EllipseOrRectangle = New System.Windows.Forms.TrackBar()
            Me.GlassLabel8 = New System.Windows.Forms.Label()
            Me.GlassLabel7 = New System.Windows.Forms.Label()
            Me.Ellipse_Width = New System.Windows.Forms.Label()
            Me.Ellipse_Viewer = New HolzShots.UI.Controls.ColorViewer()
            Me.EllipseBar = New System.Windows.Forms.TrackBar()
            Me.BrightenSettingsPanel = New System.Windows.Forms.Panel()
            Me.BigColorViewer1 = New HolzShots.UI.Controls.BigColorViewer()
            Me.BlackWhiteTracker = New System.Windows.Forms.TrackBar()
            Me.GlassLabel9 = New System.Windows.Forms.Label()
            Me.GlassLabel6 = New System.Windows.Forms.Label()
            Me.ArrowSettingsPanel = New System.Windows.Forms.Panel()
            Me.ArrowWidthSlider = New System.Windows.Forms.TrackBar()
            Me.GlassLabel11 = New System.Windows.Forms.Label()
            Me.ArrowWidthLabel = New System.Windows.Forms.Label()
            Me.GlassLabel10 = New System.Windows.Forms.Label()
            Me.ArrowColorviewer = New HolzShots.UI.Controls.ColorViewer()
            Me.BlurSettingsPanel = New System.Windows.Forms.Panel()
            Me.GlassLabel14 = New System.Windows.Forms.Label()
            Me.BlurnessBar = New System.Windows.Forms.TrackBar()
            Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
            Me.ScaleTool = New System.Windows.Forms.ToolStripButton()
            Me.DrawCursor = New System.Windows.Forms.ToolStripButton()
            Me.BottomToolStrip = New System.Windows.Forms.ToolStrip()
            Me.MouseInfoLabel = New System.Windows.Forms.ToolStripLabel()
            Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.ImageInfoLabel = New System.Windows.Forms.ToolStripLabel()
            Me.CopyPrintToolStrip = New System.Windows.Forms.ToolStrip()
            Me.CopyToClipboard = New System.Windows.Forms.ToolStripButton()
            Me.Print = New System.Windows.Forms.ToolStripButton()
            Me.ThePanel = New HolzShots.UI.Controls.PaintPanel()
            Me.autoCloseShotEditor = New System.Windows.Forms.CheckBox()
            Me.ShareStrip.SuspendLayout()
            Me.EditStrip.SuspendLayout()
            Me.CensorSettingsPanel.SuspendLayout()
            CType(Me.ZensursulaBar, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.MarkerSettingsPanel.SuspendLayout()
            CType(Me.MarkerBar, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.EraserSettingsPanel.SuspendLayout()
            CType(Me.EraserBar, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.EllipseSettingsPanel.SuspendLayout()
            CType(Me.EllipseOrRectangleBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.EllipseOrRectangle, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.EllipseBar, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.BrightenSettingsPanel.SuspendLayout()
            CType(Me.BlackWhiteTracker, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ArrowSettingsPanel.SuspendLayout()
            CType(Me.ArrowWidthSlider, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.BlurSettingsPanel.SuspendLayout()
            CType(Me.BlurnessBar, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ToolStrip1.SuspendLayout()
            Me.BottomToolStrip.SuspendLayout()
            Me.CopyPrintToolStrip.SuspendLayout()
            Me.SuspendLayout()
            '
            'ShareStrip
            '
            Me.ShareStrip.AllowItemReorder = True
            Me.ShareStrip.AutoSize = False
            Me.ShareStrip.BackColor = System.Drawing.Color.Transparent
            Me.ShareStrip.Dock = System.Windows.Forms.DockStyle.None
            Me.ShareStrip.GripMargin = New System.Windows.Forms.Padding(0)
            Me.ShareStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ShareStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
            Me.ShareStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UploadToHoster, Me.save_btn})
            Me.ShareStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
            Me.ShareStrip.Location = New System.Drawing.Point(5, 4)
            Me.ShareStrip.Name = "ShareStrip"
            Me.ShareStrip.Padding = New System.Windows.Forms.Padding(0)
            Me.ShareStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
            Me.ShareStrip.Size = New System.Drawing.Size(111, 44)
            Me.ShareStrip.TabIndex = 1
            Me.ShareStrip.Text = "Actions"
            '
            'UploadToHoster
            '
            Me.UploadToHoster.BackColor = System.Drawing.Color.Transparent
            Me.UploadToHoster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.UploadToHoster.DropDownButtonWidth = 15
            Me.UploadToHoster.Image = Global.HolzShots.My.Resources.Resources.uploadMedium
            Me.UploadToHoster.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.UploadToHoster.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.UploadToHoster.Margin = New System.Windows.Forms.Padding(5)
            Me.UploadToHoster.Name = "UploadToHoster"
            Me.UploadToHoster.Size = New System.Drawing.Size(52, 36)
            Me.UploadToHoster.Text = "Upload to {0} (Strg+Q)"
            '
            'save_btn
            '
            Me.save_btn.BackColor = System.Drawing.Color.Transparent
            Me.save_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.save_btn.Image = Global.HolzShots.My.Resources.Resources.saveMedium
            Me.save_btn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.save_btn.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.save_btn.Margin = New System.Windows.Forms.Padding(5)
            Me.save_btn.Name = "save_btn"
            Me.save_btn.Size = New System.Drawing.Size(36, 36)
            Me.save_btn.Text = "Save (Ctrl+Shift+S)"
            '
            'DruckTeil
            '
            Me.DruckTeil.DocumentName = "Screenshot"
            '
            'DruckDialog
            '
            Me.DruckDialog.UseEXDialog = True
            '
            'EditStrip
            '
            Me.EditStrip.AllowItemReorder = True
            Me.EditStrip.AutoSize = False
            Me.EditStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.EditStrip.Dock = System.Windows.Forms.DockStyle.None
            Me.EditStrip.GripMargin = New System.Windows.Forms.Padding(0)
            Me.EditStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.EditStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
            Me.EditStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CensorTool, Me.MarkerTool, Me.TextToolButton, Me.CroppingTool, Me.EraserTool, Me.BlurTool, Me.EllipseTool, Me.PipettenTool, Me.BrightenTool, Me.ArrowTool, Me.UndoStuff, Me.ToolStripDropDownButton1})
            Me.EditStrip.Location = New System.Drawing.Point(5, 47)
            Me.EditStrip.Name = "EditStrip"
            Me.EditStrip.Padding = New System.Windows.Forms.Padding(0)
            Me.EditStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
            Me.EditStrip.Size = New System.Drawing.Size(475, 39)
            Me.EditStrip.TabIndex = 11
            Me.EditStrip.Text = "Edit"
            '
            'CensorTool
            '
            Me.CensorTool.CheckOnClick = True
            Me.CensorTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.CensorTool.Image = Global.HolzShots.My.Resources.Resources.censorMedium
            Me.CensorTool.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.CensorTool.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.CensorTool.Margin = New System.Windows.Forms.Padding(2, 0, 2, 2)
            Me.CensorTool.Name = "CensorTool"
            Me.CensorTool.Size = New System.Drawing.Size(36, 37)
            Me.CensorTool.Text = "Redact (Ctrl+A)"
            '
            'MarkerTool
            '
            Me.MarkerTool.CheckOnClick = True
            Me.MarkerTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.MarkerTool.Image = Global.HolzShots.My.Resources.Resources.highlighterMedium
            Me.MarkerTool.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.MarkerTool.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.MarkerTool.Margin = New System.Windows.Forms.Padding(2, 0, 2, 2)
            Me.MarkerTool.Name = "MarkerTool"
            Me.MarkerTool.Size = New System.Drawing.Size(36, 37)
            Me.MarkerTool.Text = "Mark (Ctrl+S)"
            '
            'TextToolButton
            '
            Me.TextToolButton.CheckOnClick = True
            Me.TextToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.TextToolButton.Image = Global.HolzShots.My.Resources.Resources.textMedium
            Me.TextToolButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.TextToolButton.Margin = New System.Windows.Forms.Padding(2, 0, 2, 2)
            Me.TextToolButton.Name = "TextToolButton"
            Me.TextToolButton.Size = New System.Drawing.Size(36, 37)
            Me.TextToolButton.Text = "Insert Text (Ctrl+T)"
            '
            'CroppingTool
            '
            Me.CroppingTool.CheckOnClick = True
            Me.CroppingTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.CroppingTool.Image = Global.HolzShots.My.Resources.Resources.cropMedium
            Me.CroppingTool.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.CroppingTool.Margin = New System.Windows.Forms.Padding(2, 0, 2, 2)
            Me.CroppingTool.Name = "CroppingTool"
            Me.CroppingTool.Size = New System.Drawing.Size(36, 37)
            Me.CroppingTool.Text = "Crop Image (Ctrl+D)"
            '
            'EraserTool
            '
            Me.EraserTool.CheckOnClick = True
            Me.EraserTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.EraserTool.Image = Global.HolzShots.My.Resources.Resources.Eraser
            Me.EraserTool.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.EraserTool.Margin = New System.Windows.Forms.Padding(2, 0, 2, 2)
            Me.EraserTool.Name = "EraserTool"
            Me.EraserTool.Size = New System.Drawing.Size(36, 37)
            Me.EraserTool.Text = "Eraser (Ctrl+E)"
            '
            'BlurTool
            '
            Me.BlurTool.CheckOnClick = True
            Me.BlurTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.BlurTool.Image = Global.HolzShots.My.Resources.Resources.blurMedium
            Me.BlurTool.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.BlurTool.Margin = New System.Windows.Forms.Padding(2, 0, 2, 2)
            Me.BlurTool.Name = "BlurTool"
            Me.BlurTool.Size = New System.Drawing.Size(36, 37)
            Me.BlurTool.Text = "Blur Area (Ctrl+F)"
            '
            'EllipseTool
            '
            Me.EllipseTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.EllipseTool.Image = Global.HolzShots.My.Resources.Resources.circleMedium
            Me.EllipseTool.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.EllipseTool.Margin = New System.Windows.Forms.Padding(2, 0, 2, 2)
            Me.EllipseTool.Name = "EllipseTool"
            Me.EllipseTool.Size = New System.Drawing.Size(36, 37)
            Me.EllipseTool.Text = "Ellipse (Ctrl+H)"
            '
            'PipettenTool
            '
            Me.PipettenTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.PipettenTool.Image = Global.HolzShots.My.Resources.Resources.pickerMedium
            Me.PipettenTool.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.PipettenTool.Margin = New System.Windows.Forms.Padding(2, 0, 2, 2)
            Me.PipettenTool.Name = "PipettenTool"
            Me.PipettenTool.Size = New System.Drawing.Size(36, 37)
            Me.PipettenTool.Text = "Eye Dropper"
            '
            'BrightenTool
            '
            Me.BrightenTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.BrightenTool.Image = Global.HolzShots.My.Resources.Resources.brightenMedium
            Me.BrightenTool.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.BrightenTool.Margin = New System.Windows.Forms.Padding(2, 0, 2, 2)
            Me.BrightenTool.Name = "BrightenTool"
            Me.BrightenTool.Size = New System.Drawing.Size(36, 37)
            Me.BrightenTool.Text = "Brighten or Darken Image"
            '
            'ArrowTool
            '
            Me.ArrowTool.CheckOnClick = True
            Me.ArrowTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ArrowTool.Image = Global.HolzShots.My.Resources.Resources.arrowMedium
            Me.ArrowTool.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ArrowTool.Margin = New System.Windows.Forms.Padding(2, 0, 2, 2)
            Me.ArrowTool.Name = "ArrowTool"
            Me.ArrowTool.Size = New System.Drawing.Size(36, 37)
            Me.ArrowTool.Text = "Arrow (Ctrl+G)"
            '
            'UndoStuff
            '
            Me.UndoStuff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.UndoStuff.Image = Global.HolzShots.My.Resources.Resources.undoMedium
            Me.UndoStuff.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.UndoStuff.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.UndoStuff.Margin = New System.Windows.Forms.Padding(20, 0, 0, 2)
            Me.UndoStuff.Name = "UndoStuff"
            Me.UndoStuff.Size = New System.Drawing.Size(36, 37)
            Me.UndoStuff.Text = "Undo (Ctrl+Z)"
            '
            'ToolStripDropDownButton1
            '
            Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZensToolStripMenuItem, Me.MarkToolStripMenuItem, Me.TextToolStripMenuItem, Me.CropToolStripMenuItem, Me.EraseToolStripMenuItem, Me.PixelateToolStripMenuItem, Me.KreisToolStripMenuItem, Me.ArrowToolStripMenuItem, Me.ResetToolStripMenuItem, Me.UploadToolStripMenuItem, Me.SaveToolStripMenuItem, Me.ClipboardToolStripMenuItem, Me.PrintToolStripMenuItem, Me.ChooseServiceToolStripMenuItem})
            Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
            Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
            Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(45, 36)
            Me.ToolStripDropDownButton1.Text = "ToolStripDropDownButton1"
            Me.ToolStripDropDownButton1.Visible = False
            '
            'ZensToolStripMenuItem
            '
            Me.ZensToolStripMenuItem.Name = "ZensToolStripMenuItem"
            Me.ZensToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
            Me.ZensToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.ZensToolStripMenuItem.Text = "Zens"
            Me.ZensToolStripMenuItem.Visible = False
            '
            'MarkToolStripMenuItem
            '
            Me.MarkToolStripMenuItem.Name = "MarkToolStripMenuItem"
            Me.MarkToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
            Me.MarkToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.MarkToolStripMenuItem.Text = "Mark"
            Me.MarkToolStripMenuItem.Visible = False
            '
            'TextToolStripMenuItem
            '
            Me.TextToolStripMenuItem.Name = "TextToolStripMenuItem"
            Me.TextToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
            Me.TextToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.TextToolStripMenuItem.Text = "Text"
            Me.TextToolStripMenuItem.Visible = False
            '
            'CropToolStripMenuItem
            '
            Me.CropToolStripMenuItem.Name = "CropToolStripMenuItem"
            Me.CropToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
            Me.CropToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.CropToolStripMenuItem.Text = "Crop"
            Me.CropToolStripMenuItem.Visible = False
            '
            'EraseToolStripMenuItem
            '
            Me.EraseToolStripMenuItem.Name = "EraseToolStripMenuItem"
            Me.EraseToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
            Me.EraseToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.EraseToolStripMenuItem.Text = "Erase"
            Me.EraseToolStripMenuItem.Visible = False
            '
            'PixelateToolStripMenuItem
            '
            Me.PixelateToolStripMenuItem.Name = "PixelateToolStripMenuItem"
            Me.PixelateToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
            Me.PixelateToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.PixelateToolStripMenuItem.Text = "Pixelate"
            Me.PixelateToolStripMenuItem.Visible = False
            '
            'KreisToolStripMenuItem
            '
            Me.KreisToolStripMenuItem.Name = "KreisToolStripMenuItem"
            Me.KreisToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
            Me.KreisToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.KreisToolStripMenuItem.Text = "Circle"
            Me.KreisToolStripMenuItem.Visible = False
            '
            'ArrowToolStripMenuItem
            '
            Me.ArrowToolStripMenuItem.Name = "ArrowToolStripMenuItem"
            Me.ArrowToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
            Me.ArrowToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.ArrowToolStripMenuItem.Text = "Arrow"
            Me.ArrowToolStripMenuItem.Visible = False
            '
            'ResetToolStripMenuItem
            '
            Me.ResetToolStripMenuItem.Name = "ResetToolStripMenuItem"
            Me.ResetToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
            Me.ResetToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.ResetToolStripMenuItem.Text = "Reset"
            Me.ResetToolStripMenuItem.Visible = False
            '
            'UploadToolStripMenuItem
            '
            Me.UploadToolStripMenuItem.Name = "UploadToolStripMenuItem"
            Me.UploadToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
            Me.UploadToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.UploadToolStripMenuItem.Text = "Upload"
            Me.UploadToolStripMenuItem.Visible = False
            '
            'SaveToolStripMenuItem
            '
            Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
            Me.SaveToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
            Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.SaveToolStripMenuItem.Text = "Save"
            Me.SaveToolStripMenuItem.Visible = False
            '
            'ClipboardToolStripMenuItem
            '
            Me.ClipboardToolStripMenuItem.Name = "ClipboardToolStripMenuItem"
            Me.ClipboardToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
            Me.ClipboardToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.ClipboardToolStripMenuItem.Text = "Clipboard"
            Me.ClipboardToolStripMenuItem.Visible = False
            '
            'PrintToolStripMenuItem
            '
            Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
            Me.PrintToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
            Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.PrintToolStripMenuItem.Text = "Print"
            Me.PrintToolStripMenuItem.Visible = False
            '
            'ChooseServiceToolStripMenuItem
            '
            Me.ChooseServiceToolStripMenuItem.Name = "ChooseServiceToolStripMenuItem"
            Me.ChooseServiceToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
            Me.ChooseServiceToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
            Me.ChooseServiceToolStripMenuItem.Text = "ChooseService"
            Me.ChooseServiceToolStripMenuItem.Visible = False
            '
            'CensorSettingsPanel
            '
            Me.CensorSettingsPanel.BackColor = System.Drawing.SystemColors.Control
            Me.CensorSettingsPanel.Controls.Add(Me.GlassLabel2)
            Me.CensorSettingsPanel.Controls.Add(Me.GlassLabel3)
            Me.CensorSettingsPanel.Controls.Add(Me.Pinsel_Width_Zensursula)
            Me.CensorSettingsPanel.Controls.Add(Me.Zensursula_Viewer)
            Me.CensorSettingsPanel.Controls.Add(Me.ZensursulaBar)
            Me.CensorSettingsPanel.Location = New System.Drawing.Point(481, 0)
            Me.CensorSettingsPanel.Name = "CensorSettingsPanel"
            Me.CensorSettingsPanel.Size = New System.Drawing.Size(273, 82)
            Me.CensorSettingsPanel.TabIndex = 14
            Me.CensorSettingsPanel.Visible = False
            '
            'GlassLabel2
            '
            Me.GlassLabel2.AutoSize = True
            Me.GlassLabel2.Location = New System.Drawing.Point(49, 52)
            Me.GlassLabel2.Name = "GlassLabel2"
            Me.GlassLabel2.Size = New System.Drawing.Size(39, 13)
            Me.GlassLabel2.TabIndex = 26
            Me.GlassLabel2.Text = "Color:"
            '
            'GlassLabel3
            '
            Me.GlassLabel3.AutoSize = True
            Me.GlassLabel3.Location = New System.Drawing.Point(18, 11)
            Me.GlassLabel3.Name = "GlassLabel3"
            Me.GlassLabel3.Size = New System.Drawing.Size(70, 13)
            Me.GlassLabel3.TabIndex = 22
            Me.GlassLabel3.Text = "Width:"
            '
            'Pinsel_Width_Zensursula
            '
            Me.Pinsel_Width_Zensursula.AutoSize = True
            Me.Pinsel_Width_Zensursula.Location = New System.Drawing.Point(187, 11)
            Me.Pinsel_Width_Zensursula.Name = "Pinsel_Width_Zensursula"
            Me.Pinsel_Width_Zensursula.Size = New System.Drawing.Size(13, 13)
            Me.Pinsel_Width_Zensursula.TabIndex = 21
            Me.Pinsel_Width_Zensursula.Text = "a"
            '
            'Zensursula_Viewer
            '
            Me.Zensursula_Viewer.Color = System.Drawing.Color.Black
            Me.Zensursula_Viewer.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Zensursula_Viewer.Location = New System.Drawing.Point(93, 50)
            Me.Zensursula_Viewer.Name = "Zensursula_Viewer"
            Me.Zensursula_Viewer.Size = New System.Drawing.Size(20, 20)
            Me.Zensursula_Viewer.TabIndex = 19
            '
            'ZensursulaBar
            '
            Me.ZensursulaBar.Location = New System.Drawing.Point(83, 6)
            Me.ZensursulaBar.Maximum = 100
            Me.ZensursulaBar.Minimum = 1
            Me.ZensursulaBar.Name = "ZensursulaBar"
            Me.ZensursulaBar.Size = New System.Drawing.Size(104, 45)
            Me.ZensursulaBar.TabIndex = 16
            Me.ZensursulaBar.TabStop = False
            Me.ZensursulaBar.Value = 1
            '
            'MarkerSettingsPanel
            '
            Me.MarkerSettingsPanel.BackColor = System.Drawing.SystemColors.Control
            Me.MarkerSettingsPanel.Controls.Add(Me.GlassLabel4)
            Me.MarkerSettingsPanel.Controls.Add(Me.GlassLabel1)
            Me.MarkerSettingsPanel.Controls.Add(Me.Pinsel_Width_Marker)
            Me.MarkerSettingsPanel.Controls.Add(Me.Marker_Viewer)
            Me.MarkerSettingsPanel.Controls.Add(Me.MarkerBar)
            Me.MarkerSettingsPanel.Location = New System.Drawing.Point(427, 236)
            Me.MarkerSettingsPanel.Name = "MarkerSettingsPanel"
            Me.MarkerSettingsPanel.Size = New System.Drawing.Size(273, 82)
            Me.MarkerSettingsPanel.TabIndex = 20
            Me.MarkerSettingsPanel.Visible = False
            '
            'GlassLabel4
            '
            Me.GlassLabel4.AutoSize = True
            Me.GlassLabel4.Location = New System.Drawing.Point(42, 51)
            Me.GlassLabel4.Name = "GlassLabel4"
            Me.GlassLabel4.Size = New System.Drawing.Size(39, 13)
            Me.GlassLabel4.TabIndex = 26
            Me.GlassLabel4.Text = "Color:"
            '
            'GlassLabel1
            '
            Me.GlassLabel1.AutoSize = True
            Me.GlassLabel1.Location = New System.Drawing.Point(11, 11)
            Me.GlassLabel1.Name = "GlassLabel1"
            Me.GlassLabel1.Size = New System.Drawing.Size(70, 13)
            Me.GlassLabel1.TabIndex = 23
            Me.GlassLabel1.Text = "Width:"
            '
            'Pinsel_Width_Marker
            '
            Me.Pinsel_Width_Marker.AutoSize = True
            Me.Pinsel_Width_Marker.Location = New System.Drawing.Point(193, 11)
            Me.Pinsel_Width_Marker.Name = "Pinsel_Width_Marker"
            Me.Pinsel_Width_Marker.Size = New System.Drawing.Size(13, 13)
            Me.Pinsel_Width_Marker.TabIndex = 20
            Me.Pinsel_Width_Marker.Text = "a"
            '
            'Marker_Viewer
            '
            Me.Marker_Viewer.Color = System.Drawing.Color.Black
            Me.Marker_Viewer.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Marker_Viewer.Location = New System.Drawing.Point(93, 50)
            Me.Marker_Viewer.Name = "Marker_Viewer"
            Me.Marker_Viewer.Size = New System.Drawing.Size(20, 20)
            Me.Marker_Viewer.TabIndex = 19
            '
            'MarkerBar
            '
            Me.MarkerBar.Location = New System.Drawing.Point(83, 6)
            Me.MarkerBar.Maximum = 100
            Me.MarkerBar.Minimum = 1
            Me.MarkerBar.Name = "MarkerBar"
            Me.MarkerBar.Size = New System.Drawing.Size(104, 45)
            Me.MarkerBar.TabIndex = 16
            Me.MarkerBar.TabStop = False
            Me.MarkerBar.Value = 1
            '
            'EraserSettingsPanel
            '
            Me.EraserSettingsPanel.BackColor = System.Drawing.SystemColors.Control
            Me.EraserSettingsPanel.Controls.Add(Me.GlassLabel5)
            Me.EraserSettingsPanel.Controls.Add(Me.Eraser_Diameter)
            Me.EraserSettingsPanel.Controls.Add(Me.EraserBar)
            Me.EraserSettingsPanel.Location = New System.Drawing.Point(525, 176)
            Me.EraserSettingsPanel.Name = "EraserSettingsPanel"
            Me.EraserSettingsPanel.Size = New System.Drawing.Size(273, 82)
            Me.EraserSettingsPanel.TabIndex = 21
            Me.EraserSettingsPanel.Visible = False
            '
            'GlassLabel5
            '
            Me.GlassLabel5.AutoSize = True
            Me.GlassLabel5.Location = New System.Drawing.Point(4, 1)
            Me.GlassLabel5.Name = "GlassLabel5"
            Me.GlassLabel5.Size = New System.Drawing.Size(105, 13)
            Me.GlassLabel5.TabIndex = 24
            Me.GlassLabel5.Text = "Diameter:"
            '
            'Eraser_Diameter
            '
            Me.Eraser_Diameter.AutoSize = True
            Me.Eraser_Diameter.Location = New System.Drawing.Point(184, 34)
            Me.Eraser_Diameter.Name = "Eraser_Diameter"
            Me.Eraser_Diameter.Size = New System.Drawing.Size(13, 13)
            Me.Eraser_Diameter.TabIndex = 21
            Me.Eraser_Diameter.Text = "a"
            '
            'EraserBar
            '
            Me.EraserBar.Location = New System.Drawing.Point(83, 34)
            Me.EraserBar.Maximum = 100
            Me.EraserBar.Minimum = 1
            Me.EraserBar.Name = "EraserBar"
            Me.EraserBar.Size = New System.Drawing.Size(104, 45)
            Me.EraserBar.TabIndex = 16
            Me.EraserBar.TabStop = False
            Me.EraserBar.Value = 1
            '
            'EllipseSettingsPanel
            '
            Me.EllipseSettingsPanel.BackColor = System.Drawing.SystemColors.Control
            Me.EllipseSettingsPanel.Controls.Add(Me.EllipseOrRectangleBox)
            Me.EllipseSettingsPanel.Controls.Add(Me.GlassLabel12)
            Me.EllipseSettingsPanel.Controls.Add(Me.EllipseOrRectangle)
            Me.EllipseSettingsPanel.Controls.Add(Me.GlassLabel8)
            Me.EllipseSettingsPanel.Controls.Add(Me.GlassLabel7)
            Me.EllipseSettingsPanel.Controls.Add(Me.Ellipse_Width)
            Me.EllipseSettingsPanel.Controls.Add(Me.Ellipse_Viewer)
            Me.EllipseSettingsPanel.Controls.Add(Me.EllipseBar)
            Me.EllipseSettingsPanel.Location = New System.Drawing.Point(275, 306)
            Me.EllipseSettingsPanel.Name = "EllipseSettingsPanel"
            Me.EllipseSettingsPanel.Size = New System.Drawing.Size(273, 82)
            Me.EllipseSettingsPanel.TabIndex = 21
            Me.EllipseSettingsPanel.Visible = False
            '
            'EllipseOrRectangleBox
            '
            Me.EllipseOrRectangleBox.Location = New System.Drawing.Point(242, 50)
            Me.EllipseOrRectangleBox.Name = "EllipseOrRectangleBox"
            Me.EllipseOrRectangleBox.Size = New System.Drawing.Size(16, 16)
            Me.EllipseOrRectangleBox.TabIndex = 28
            Me.EllipseOrRectangleBox.TabStop = False
            '
            'GlassLabel12
            '
            Me.GlassLabel12.AutoSize = True
            Me.GlassLabel12.Location = New System.Drawing.Point(119, 50)
            Me.GlassLabel12.Name = "GlassLabel12"
            Me.GlassLabel12.Size = New System.Drawing.Size(46, 13)
            Me.GlassLabel12.TabIndex = 27
            Me.GlassLabel12.Text = "Mode:"
            '
            'EllipseOrRectangle
            '
            Me.EllipseOrRectangle.Location = New System.Drawing.Point(166, 50)
            Me.EllipseOrRectangle.Maximum = 1
            Me.EllipseOrRectangle.Name = "EllipseOrRectangle"
            Me.EllipseOrRectangle.Size = New System.Drawing.Size(70, 45)
            Me.EllipseOrRectangle.TabIndex = 26
            Me.EllipseOrRectangle.TabStop = False
            '
            'GlassLabel8
            '
            Me.GlassLabel8.AutoSize = True
            Me.GlassLabel8.Location = New System.Drawing.Point(41, 50)
            Me.GlassLabel8.Name = "GlassLabel8"
            Me.GlassLabel8.Size = New System.Drawing.Size(39, 13)
            Me.GlassLabel8.TabIndex = 25
            Me.GlassLabel8.Text = "Color:"
            '
            'GlassLabel7
            '
            Me.GlassLabel7.AutoSize = True
            Me.GlassLabel7.Location = New System.Drawing.Point(11, 15)
            Me.GlassLabel7.Name = "GlassLabel7"
            Me.GlassLabel7.Size = New System.Drawing.Size(69, 13)
            Me.GlassLabel7.TabIndex = 24
            Me.GlassLabel7.Text = "Width:"
            '
            'Ellipse_Width
            '
            Me.Ellipse_Width.AutoSize = True
            Me.Ellipse_Width.Location = New System.Drawing.Point(193, 15)
            Me.Ellipse_Width.Name = "Ellipse_Width"
            Me.Ellipse_Width.Size = New System.Drawing.Size(13, 13)
            Me.Ellipse_Width.TabIndex = 22
            Me.Ellipse_Width.Text = "a"
            '
            'Ellipse_Viewer
            '
            Me.Ellipse_Viewer.Color = System.Drawing.Color.Black
            Me.Ellipse_Viewer.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Ellipse_Viewer.Location = New System.Drawing.Point(87, 50)
            Me.Ellipse_Viewer.Name = "Ellipse_Viewer"
            Me.Ellipse_Viewer.Size = New System.Drawing.Size(20, 20)
            Me.Ellipse_Viewer.TabIndex = 19
            '
            'EllipseBar
            '
            Me.EllipseBar.Location = New System.Drawing.Point(83, 6)
            Me.EllipseBar.Maximum = 100
            Me.EllipseBar.Minimum = 1
            Me.EllipseBar.Name = "EllipseBar"
            Me.EllipseBar.Size = New System.Drawing.Size(104, 45)
            Me.EllipseBar.TabIndex = 16
            Me.EllipseBar.TabStop = False
            Me.EllipseBar.Value = 1
            '
            'BrightenSettingsPanel
            '
            Me.BrightenSettingsPanel.BackColor = System.Drawing.SystemColors.Control
            Me.BrightenSettingsPanel.Controls.Add(Me.BigColorViewer1)
            Me.BrightenSettingsPanel.Controls.Add(Me.BlackWhiteTracker)
            Me.BrightenSettingsPanel.Controls.Add(Me.GlassLabel9)
            Me.BrightenSettingsPanel.Controls.Add(Me.GlassLabel6)
            Me.BrightenSettingsPanel.Location = New System.Drawing.Point(486, 125)
            Me.BrightenSettingsPanel.Name = "BrightenSettingsPanel"
            Me.BrightenSettingsPanel.Size = New System.Drawing.Size(273, 82)
            Me.BrightenSettingsPanel.TabIndex = 26
            Me.BrightenSettingsPanel.Visible = False
            '
            'BigColorViewer1
            '
            Me.BigColorViewer1.Color = System.Drawing.Color.Silver
            Me.BigColorViewer1.Location = New System.Drawing.Point(61, 48)
            Me.BigColorViewer1.Name = "BigColorViewer1"
            Me.BigColorViewer1.Size = New System.Drawing.Size(158, 26)
            Me.BigColorViewer1.TabIndex = 27
            Me.BigColorViewer1.TabStop = False
            '
            'BlackWhiteTracker
            '
            Me.BlackWhiteTracker.Location = New System.Drawing.Point(73, 12)
            Me.BlackWhiteTracker.Maximum = 510
            Me.BlackWhiteTracker.Name = "BlackWhiteTracker"
            Me.BlackWhiteTracker.Size = New System.Drawing.Size(122, 45)
            Me.BlackWhiteTracker.TabIndex = 26
            Me.BlackWhiteTracker.TabStop = False
            Me.BlackWhiteTracker.Value = 1
            '
            'GlassLabel9
            '
            Me.GlassLabel9.AutoSize = True
            Me.GlassLabel9.Location = New System.Drawing.Point(201, 12)
            Me.GlassLabel9.Name = "GlassLabel9"
            Me.GlassLabel9.Size = New System.Drawing.Size(57, 13)
            Me.GlassLabel9.TabIndex = 26
            Me.GlassLabel9.Text = "Brighten"
            '
            'GlassLabel6
            '
            Me.GlassLabel6.AutoSize = True
            Me.GlassLabel6.Location = New System.Drawing.Point(3, 12)
            Me.GlassLabel6.Name = "GlassLabel6"
            Me.GlassLabel6.Size = New System.Drawing.Size(64, 13)
            Me.GlassLabel6.TabIndex = 25
            Me.GlassLabel6.Text = "Darken"
            '
            'ArrowSettingsPanel
            '
            Me.ArrowSettingsPanel.BackColor = System.Drawing.SystemColors.Control
            Me.ArrowSettingsPanel.Controls.Add(Me.ArrowWidthSlider)
            Me.ArrowSettingsPanel.Controls.Add(Me.GlassLabel11)
            Me.ArrowSettingsPanel.Controls.Add(Me.ArrowWidthLabel)
            Me.ArrowSettingsPanel.Controls.Add(Me.GlassLabel10)
            Me.ArrowSettingsPanel.Controls.Add(Me.ArrowColorviewer)
            Me.ArrowSettingsPanel.Location = New System.Drawing.Point(397, 394)
            Me.ArrowSettingsPanel.Name = "ArrowSettingsPanel"
            Me.ArrowSettingsPanel.Size = New System.Drawing.Size(273, 82)
            Me.ArrowSettingsPanel.TabIndex = 26
            Me.ArrowSettingsPanel.Visible = False
            '
            'ArrowWidthSlider
            '
            Me.ArrowWidthSlider.Location = New System.Drawing.Point(84, 3)
            Me.ArrowWidthSlider.Maximum = 100
            Me.ArrowWidthSlider.Name = "ArrowWidthSlider"
            Me.ArrowWidthSlider.Size = New System.Drawing.Size(112, 45)
            Me.ArrowWidthSlider.TabIndex = 26
            Me.ArrowWidthSlider.TabStop = False
            Me.ArrowWidthSlider.Value = 1
            '
            'GlassLabel11
            '
            Me.GlassLabel11.AutoSize = True
            Me.GlassLabel11.Location = New System.Drawing.Point(11, 10)
            Me.GlassLabel11.Name = "GlassLabel11"
            Me.GlassLabel11.Size = New System.Drawing.Size(70, 13)
            Me.GlassLabel11.TabIndex = 28
            Me.GlassLabel11.Text = "Width:"
            '
            'ArrowWidthLabel
            '
            Me.ArrowWidthLabel.AutoSize = True
            Me.ArrowWidthLabel.Location = New System.Drawing.Point(202, 3)
            Me.ArrowWidthLabel.Name = "ArrowWidthLabel"
            Me.ArrowWidthLabel.Size = New System.Drawing.Size(13, 13)
            Me.ArrowWidthLabel.TabIndex = 27
            Me.ArrowWidthLabel.Text = "a"
            '
            'GlassLabel10
            '
            Me.GlassLabel10.AutoSize = True
            Me.GlassLabel10.Location = New System.Drawing.Point(42, 50)
            Me.GlassLabel10.Name = "GlassLabel10"
            Me.GlassLabel10.Size = New System.Drawing.Size(39, 13)
            Me.GlassLabel10.TabIndex = 25
            Me.GlassLabel10.Text = "Color:"
            '
            'ArrowColorviewer
            '
            Me.ArrowColorviewer.Color = System.Drawing.Color.Black
            Me.ArrowColorviewer.Cursor = System.Windows.Forms.Cursors.Hand
            Me.ArrowColorviewer.Location = New System.Drawing.Point(93, 50)
            Me.ArrowColorviewer.Name = "ArrowColorviewer"
            Me.ArrowColorviewer.Size = New System.Drawing.Size(20, 20)
            Me.ArrowColorviewer.TabIndex = 19
            '
            'BlurSettingsPanel
            '
            Me.BlurSettingsPanel.BackColor = System.Drawing.SystemColors.Control
            Me.BlurSettingsPanel.Controls.Add(Me.GlassLabel14)
            Me.BlurSettingsPanel.Controls.Add(Me.BlurnessBar)
            Me.BlurSettingsPanel.Location = New System.Drawing.Point(394, 346)
            Me.BlurSettingsPanel.Name = "BlurSettingsPanel"
            Me.BlurSettingsPanel.Size = New System.Drawing.Size(273, 82)
            Me.BlurSettingsPanel.TabIndex = 27
            Me.BlurSettingsPanel.Visible = False
            '
            'GlassLabel14
            '
            Me.GlassLabel14.AutoSize = True
            Me.GlassLabel14.Location = New System.Drawing.Point(30, 22)
            Me.GlassLabel14.Name = "GlassLabel14"
            Me.GlassLabel14.Size = New System.Drawing.Size(42, 13)
            Me.GlassLabel14.TabIndex = 22
            Me.GlassLabel14.Text = "Diameter:"
            '
            'BlurnessBar
            '
            Me.BlurnessBar.Location = New System.Drawing.Point(78, 13)
            Me.BlurnessBar.Maximum = 30
            Me.BlurnessBar.Minimum = 5
            Me.BlurnessBar.Name = "BlurnessBar"
            Me.BlurnessBar.Size = New System.Drawing.Size(175, 45)
            Me.BlurnessBar.TabIndex = 16
            Me.BlurnessBar.TabStop = False
            Me.BlurnessBar.Value = 5
            '
            'ToolStrip1
            '
            Me.ToolStrip1.AllowItemReorder = True
            Me.ToolStrip1.AutoSize = False
            Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
            Me.ToolStrip1.GripMargin = New System.Windows.Forms.Padding(0)
            Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScaleTool, Me.DrawCursor})
            Me.ToolStrip1.Location = New System.Drawing.Point(140, 21)
            Me.ToolStrip1.Name = "ToolStrip1"
            Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0)
            Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
            Me.ToolStrip1.Size = New System.Drawing.Size(115, 30)
            Me.ToolStrip1.TabIndex = 27
            Me.ToolStrip1.Text = "Edit"
            '
            'ScaleTool
            '
            Me.ScaleTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ScaleTool.Image = Global.HolzShots.My.Resources.Resources.scaleSmall
            Me.ScaleTool.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ScaleTool.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
            Me.ScaleTool.Name = "ScaleTool"
            Me.ScaleTool.Size = New System.Drawing.Size(23, 20)
            Me.ScaleTool.Text = "Scale Image"
            '
            'DrawCursor
            '
            Me.DrawCursor.CheckOnClick = True
            Me.DrawCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.DrawCursor.Image = Global.HolzShots.My.Resources.Resources.cursorMedium
            Me.DrawCursor.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.DrawCursor.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
            Me.DrawCursor.Name = "DrawCursor"
            Me.DrawCursor.Size = New System.Drawing.Size(23, 20)
            Me.DrawCursor.Text = "Draw Cursor"
            '
            'BottomToolStrip
            '
            Me.BottomToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.BottomToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.BottomToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MouseInfoLabel, Me.ToolStripSeparator1, Me.ImageInfoLabel})
            Me.BottomToolStrip.Location = New System.Drawing.Point(0, 588)
            Me.BottomToolStrip.Name = "BottomToolStrip"
            Me.BottomToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
            Me.BottomToolStrip.Size = New System.Drawing.Size(759, 25)
            Me.BottomToolStrip.TabIndex = 30
            '
            'MouseInfoLabel
            '
            Me.MouseInfoLabel.AutoSize = False
            Me.MouseInfoLabel.BackColor = System.Drawing.Color.Transparent
            Me.MouseInfoLabel.Image = Global.HolzShots.My.Resources.Resources.cursorPositionSmall
            Me.MouseInfoLabel.Margin = New System.Windows.Forms.Padding(15, 1, 0, 2)
            Me.MouseInfoLabel.Name = "MouseInfoLabel"
            Me.MouseInfoLabel.Size = New System.Drawing.Size(120, 22)
            Me.MouseInfoLabel.Text = "MouseInfoLabel"
            '
            'ToolStripSeparator1
            '
            Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
            Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
            '
            'ImageInfoLabel
            '
            Me.ImageInfoLabel.BackColor = System.Drawing.Color.Transparent
            Me.ImageInfoLabel.Image = Global.HolzShots.My.Resources.Resources.imageDimensionsSmall
            Me.ImageInfoLabel.Margin = New System.Windows.Forms.Padding(5, 1, 0, 2)
            Me.ImageInfoLabel.Name = "ImageInfoLabel"
            Me.ImageInfoLabel.Size = New System.Drawing.Size(105, 22)
            Me.ImageInfoLabel.Text = "ImageInfoLabel"
            '
            'CopyPrintToolStrip
            '
            Me.CopyPrintToolStrip.BackColor = System.Drawing.Color.Transparent
            Me.CopyPrintToolStrip.Dock = System.Windows.Forms.DockStyle.None
            Me.CopyPrintToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.CopyPrintToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyToClipboard, Me.Print})
            Me.CopyPrintToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
            Me.CopyPrintToolStrip.Location = New System.Drawing.Point(116, 6)
            Me.CopyPrintToolStrip.Name = "CopyPrintToolStrip"
            Me.CopyPrintToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
            Me.CopyPrintToolStrip.Size = New System.Drawing.Size(24, 42)
            Me.CopyPrintToolStrip.TabIndex = 31
            '
            'CopyToClipboard
            '
            Me.CopyToClipboard.BackColor = System.Drawing.Color.Transparent
            Me.CopyToClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.CopyToClipboard.Image = Global.HolzShots.My.Resources.Resources.clipboardSmall
            Me.CopyToClipboard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.CopyToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.CopyToClipboard.Margin = New System.Windows.Forms.Padding(0)
            Me.CopyToClipboard.Name = "CopyToClipboard"
            Me.CopyToClipboard.Size = New System.Drawing.Size(22, 20)
            Me.CopyToClipboard.Text = "Copy to Clipboard (Ctrl+C)"
            '
            'Print
            '
            Me.Print.BackColor = System.Drawing.Color.Transparent
            Me.Print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.Print.Image = Global.HolzShots.My.Resources.Resources.printerSmall
            Me.Print.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.Print.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.Print.Margin = New System.Windows.Forms.Padding(0)
            Me.Print.Name = "Print"
            Me.Print.Size = New System.Drawing.Size(22, 20)
            Me.Print.Text = "Print (Ctrl+P)"
            '
            'ThePanel
            '
            Me.ThePanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ThePanel.ArrowColor = System.Drawing.Color.Empty
            Me.ThePanel.ArrowWidth = 0
            Me.ThePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(231, Byte), Integer))
            Me.ThePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.ThePanel.BlurFactor = 7
            Me.ThePanel.BrightenColor = System.Drawing.Color.Black
            Me.ThePanel.CurrentTool = HolzShots.UI.Controls.PaintPanel.Tool.None
            Me.ThePanel.Cursor = System.Windows.Forms.Cursors.Default
            Me.ThePanel.DrawCursor = False
            Me.ThePanel.EllipseColor = System.Drawing.Color.Empty
            Me.ThePanel.EllipseWidth = 0
            Me.ThePanel.EraserDiameter = 0
            Me.ThePanel.Location = New System.Drawing.Point(0, 85)
            Me.ThePanel.Margin = New System.Windows.Forms.Padding(0)
            Me.ThePanel.MarkerColor = System.Drawing.Color.Empty
            Me.ThePanel.MarkerWidth = 0
            Me.ThePanel.Name = "ThePanel"
            Me.ThePanel.Size = New System.Drawing.Size(759, 503)
            Me.ThePanel.TabIndex = 12
            Me.ThePanel.ZensursulaColor = System.Drawing.Color.Empty
            Me.ThePanel.ZensursulaWidth = 0
            '
            'autoCloseShotEditor
            '
            Me.autoCloseShotEditor.AutoSize = True
            Me.autoCloseShotEditor.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.autoCloseShotEditor.Location = New System.Drawing.Point(148, 9)
            Me.autoCloseShotEditor.Name = "autoCloseShotEditor"
            Me.autoCloseShotEditor.Size = New System.Drawing.Size(208, 18)
            Me.autoCloseShotEditor.TabIndex = 32
            Me.autoCloseShotEditor.Text = "Close ShotEditor when uploading"
            Me.autoCloseShotEditor.UseVisualStyleBackColor = True
            '
            'ShotEditor
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(247, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(759, 613)
            Me.Controls.Add(Me.autoCloseShotEditor)
            Me.Controls.Add(Me.ThePanel)
            Me.Controls.Add(Me.EditStrip)
            Me.Controls.Add(Me.CopyPrintToolStrip)
            Me.Controls.Add(Me.BlurSettingsPanel)
            Me.Controls.Add(Me.ToolStrip1)
            Me.Controls.Add(Me.BrightenSettingsPanel)
            Me.Controls.Add(Me.ArrowSettingsPanel)
            Me.Controls.Add(Me.EllipseSettingsPanel)
            Me.Controls.Add(Me.EraserSettingsPanel)
            Me.Controls.Add(Me.MarkerSettingsPanel)
            Me.Controls.Add(Me.CensorSettingsPanel)
            Me.Controls.Add(Me.ShareStrip)
            Me.Controls.Add(Me.BottomToolStrip)
            Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MinimumSize = New System.Drawing.Size(775, 200)
            Me.Name = "ShotEditor"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
            Me.Text = "ShotEditor"
            Me.ShareStrip.ResumeLayout(False)
            Me.ShareStrip.PerformLayout()
            Me.EditStrip.ResumeLayout(False)
            Me.EditStrip.PerformLayout()
            Me.CensorSettingsPanel.ResumeLayout(False)
            Me.CensorSettingsPanel.PerformLayout()
            CType(Me.ZensursulaBar, System.ComponentModel.ISupportInitialize).EndInit()
            Me.MarkerSettingsPanel.ResumeLayout(False)
            Me.MarkerSettingsPanel.PerformLayout()
            CType(Me.MarkerBar, System.ComponentModel.ISupportInitialize).EndInit()
            Me.EraserSettingsPanel.ResumeLayout(False)
            Me.EraserSettingsPanel.PerformLayout()
            CType(Me.EraserBar, System.ComponentModel.ISupportInitialize).EndInit()
            Me.EllipseSettingsPanel.ResumeLayout(False)
            Me.EllipseSettingsPanel.PerformLayout()
            CType(Me.EllipseOrRectangleBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.EllipseOrRectangle, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.EllipseBar, System.ComponentModel.ISupportInitialize).EndInit()
            Me.BrightenSettingsPanel.ResumeLayout(False)
            Me.BrightenSettingsPanel.PerformLayout()
            CType(Me.BlackWhiteTracker, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ArrowSettingsPanel.ResumeLayout(False)
            Me.ArrowSettingsPanel.PerformLayout()
            CType(Me.ArrowWidthSlider, System.ComponentModel.ISupportInitialize).EndInit()
            Me.BlurSettingsPanel.ResumeLayout(False)
            Me.BlurSettingsPanel.PerformLayout()
            CType(Me.BlurnessBar, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ToolStrip1.ResumeLayout(False)
            Me.ToolStrip1.PerformLayout()
            Me.BottomToolStrip.ResumeLayout(False)
            Me.BottomToolStrip.PerformLayout()
            Me.CopyPrintToolStrip.ResumeLayout(False)
            Me.CopyPrintToolStrip.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ShareStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents save_btn As System.Windows.Forms.ToolStripButton
        Friend WithEvents DruckTeil As System.Drawing.Printing.PrintDocument
        Friend WithEvents DruckDialog As System.Windows.Forms.PrintDialog
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
        Friend WithEvents CensorSettingsPanel As System.Windows.Forms.Panel
        Friend WithEvents ZensursulaBar As System.Windows.Forms.TrackBar
        Friend WithEvents Zensursula_Viewer As ColorViewer
        Friend WithEvents MarkerSettingsPanel As System.Windows.Forms.Panel
        Friend WithEvents Marker_Viewer As ColorViewer
        Friend WithEvents MarkerBar As System.Windows.Forms.TrackBar
        Friend WithEvents EraserSettingsPanel As System.Windows.Forms.Panel
        Friend WithEvents EraserBar As System.Windows.Forms.TrackBar
        Friend WithEvents EllipseSettingsPanel As System.Windows.Forms.Panel
        Friend WithEvents Ellipse_Viewer As ColorViewer
        Friend WithEvents EllipseBar As System.Windows.Forms.TrackBar
        Friend WithEvents Pinsel_Width_Zensursula As System.Windows.Forms.Label
        Friend WithEvents Pinsel_Width_Marker As System.Windows.Forms.Label
        Friend WithEvents Eraser_Diameter As System.Windows.Forms.Label
        Friend WithEvents Ellipse_Width As System.Windows.Forms.Label
        Friend WithEvents GlassLabel2 As System.Windows.Forms.Label
        Friend WithEvents GlassLabel3 As System.Windows.Forms.Label
        Friend WithEvents GlassLabel4 As System.Windows.Forms.Label
        Friend WithEvents GlassLabel1 As System.Windows.Forms.Label
        Friend WithEvents GlassLabel5 As System.Windows.Forms.Label
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
        Friend WithEvents BigColorViewer1 As BigColorViewer
        Friend WithEvents ArrowSettingsPanel As System.Windows.Forms.Panel
        Friend WithEvents GlassLabel10 As System.Windows.Forms.Label
        Friend WithEvents ArrowColorviewer As ColorViewer
        Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
        Friend WithEvents ScaleTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents DrawCursor As System.Windows.Forms.ToolStripButton
        Friend WithEvents ArrowWidthSlider As System.Windows.Forms.TrackBar
        Friend WithEvents GlassLabel11 As System.Windows.Forms.Label
        Friend WithEvents ArrowWidthLabel As System.Windows.Forms.Label
        Friend WithEvents GlassLabel12 As System.Windows.Forms.Label
        Friend WithEvents EllipseOrRectangle As System.Windows.Forms.TrackBar
        Friend WithEvents EllipseOrRectangleBox As System.Windows.Forms.PictureBox
        Friend WithEvents BlurSettingsPanel As System.Windows.Forms.Panel
        Friend WithEvents GlassLabel14 As System.Windows.Forms.Label
        Friend WithEvents BlurnessBar As System.Windows.Forms.TrackBar
        Friend WithEvents BottomToolStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents ImageInfoLabel As System.Windows.Forms.ToolStripLabel
        Friend WithEvents MouseInfoLabel As System.Windows.Forms.ToolStripLabel
        Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents CopyPrintToolStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents CopyToClipboard As System.Windows.Forms.ToolStripButton
        Friend WithEvents Print As System.Windows.Forms.ToolStripButton
        Friend WithEvents autoCloseShotEditor As System.Windows.Forms.CheckBox
    End Class
End Namespace
