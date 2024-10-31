namespace HolzShots.UI
{
    partial class ShotEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ShotEditor));
            BottomToolStrip = new ToolStrip();
            MouseInfoLabel = new ToolStripLabel();
            ToolStripSeparator1 = new ToolStripSeparator();
            ImageInfoLabel = new ToolStripLabel();
            AutoCloseShotEditor = new CheckBox();
            ThePanel = new PaintPanel();
            ShareStrip = new ToolStrip();
            UploadToHoster = new ToolStripSplitButton();
            SaveButton = new ToolStripButton();
            CopyPrintToolStrip = new ToolStrip();
            CopyToClipboard = new ToolStripButton();
            CurrentToolSettingsPanel = new Panel();
            WeirdToolsStrip = new ToolStrip();
            ScaleTool = new ToolStripButton();
            DrawCursor = new ToolStripButton();
            EditStrip = new ToolStrip();
            CensorTool = new ToolStripButton();
            MarkerTool = new ToolStripButton();
            CroppingTool = new ToolStripButton();
            EraserTool = new ToolStripButton();
            BlurTool = new ToolStripButton();
            EllipseTool = new ToolStripButton();
            PipettenTool = new ToolStripButton();
            BrightenTool = new ToolStripButton();
            ArrowTool = new ToolStripButton();
            UndoStuff = new ToolStripButton();
            ToolStripDropDownButton1 = new ToolStripDropDownButton();
            CensorToolStripMenuItem = new ToolStripMenuItem();
            MarkToolStripMenuItem = new ToolStripMenuItem();
            CropToolStripMenuItem = new ToolStripMenuItem();
            EraseToolStripMenuItem = new ToolStripMenuItem();
            BlurToolStripMenuItem = new ToolStripMenuItem();
            EllipseToolStripMenuItem = new ToolStripMenuItem();
            ArrowToolStripMenuItem = new ToolStripMenuItem();
            ResetToolStripMenuItem = new ToolStripMenuItem();
            UploadToolStripMenuItem = new ToolStripMenuItem();
            SaveToolStripMenuItem = new ToolStripMenuItem();
            ClipboardToolStripMenuItem = new ToolStripMenuItem();
            PrintToolStripMenuItem = new ToolStripMenuItem();
            ChooseServiceToolStripMenuItem = new ToolStripMenuItem();
            BottomToolStrip.SuspendLayout();
            ShareStrip.SuspendLayout();
            CopyPrintToolStrip.SuspendLayout();
            WeirdToolsStrip.SuspendLayout();
            EditStrip.SuspendLayout();
            SuspendLayout();
            // 
            // BottomToolStrip
            // 
            BottomToolStrip.Dock = DockStyle.Bottom;
            BottomToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            BottomToolStrip.Items.AddRange(new ToolStripItem[] { MouseInfoLabel, ToolStripSeparator1, ImageInfoLabel });
            BottomToolStrip.Location = new Point(0, 613);
            BottomToolStrip.Name = "BottomToolStrip";
            BottomToolStrip.RenderMode = ToolStripRenderMode.System;
            BottomToolStrip.Size = new Size(800, 25);
            BottomToolStrip.TabIndex = 31;
            // 
            // MouseInfoLabel
            // 
            MouseInfoLabel.AutoSize = false;
            MouseInfoLabel.BackColor = Color.Transparent;
            MouseInfoLabel.Image = (Image)resources.GetObject("MouseInfoLabel.Image");
            MouseInfoLabel.Margin = new Padding(15, 1, 0, 2);
            MouseInfoLabel.Name = "MouseInfoLabel";
            MouseInfoLabel.Size = new Size(120, 22);
            MouseInfoLabel.Text = "MouseInfoLabel";
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            ToolStripSeparator1.Size = new Size(6, 25);
            // 
            // ImageInfoLabel
            // 
            ImageInfoLabel.BackColor = Color.Transparent;
            ImageInfoLabel.Image = (Image)resources.GetObject("ImageInfoLabel.Image");
            ImageInfoLabel.Margin = new Padding(5, 1, 0, 2);
            ImageInfoLabel.Name = "ImageInfoLabel";
            ImageInfoLabel.Size = new Size(105, 22);
            ImageInfoLabel.Text = "ImageInfoLabel";
            ImageInfoLabel.MouseUp += ImageInfoLabel_MouseUp;
            // 
            // AutoCloseShotEditor
            // 
            AutoCloseShotEditor.AutoSize = true;
            AutoCloseShotEditor.FlatStyle = FlatStyle.System;
            AutoCloseShotEditor.Location = new Point(154, 36);
            AutoCloseShotEditor.Name = "AutoCloseShotEditor";
            AutoCloseShotEditor.Size = new Size(208, 20);
            AutoCloseShotEditor.TabIndex = 33;
            AutoCloseShotEditor.Text = "Close ShotEditor when uploading";
            AutoCloseShotEditor.UseVisualStyleBackColor = true;
            // 
            // ThePanel
            // 
            ThePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ThePanel.BackColor = Color.FromArgb(207, 217, 231);
            ThePanel.BackgroundImageLayout = ImageLayout.None;
            ThePanel.Font = new Font("Segoe UI", 9F);
            ThePanel.Location = new Point(0, 114);
            ThePanel.Margin = new Padding(0);
            ThePanel.Name = "ThePanel";
            ThePanel.Size = new Size(800, 499);
            ThePanel.TabIndex = 34;
            // 
            // ShareStrip
            // 
            ShareStrip.AllowItemReorder = true;
            ShareStrip.AutoSize = false;
            ShareStrip.BackColor = Color.Transparent;
            ShareStrip.Dock = DockStyle.None;
            ShareStrip.GripMargin = new Padding(0);
            ShareStrip.GripStyle = ToolStripGripStyle.Hidden;
            ShareStrip.ImageScalingSize = new Size(32, 32);
            ShareStrip.Items.AddRange(new ToolStripItem[] { UploadToHoster, SaveButton });
            ShareStrip.LayoutStyle = ToolStripLayoutStyle.Flow;
            ShareStrip.Location = new Point(9, 12);
            ShareStrip.Name = "ShareStrip";
            ShareStrip.Padding = new Padding(0);
            ShareStrip.RenderMode = ToolStripRenderMode.System;
            ShareStrip.Size = new Size(111, 44);
            ShareStrip.TabIndex = 35;
            ShareStrip.Text = "Actions";
            ShareStrip.Paint += ToolStripPaint;
            // 
            // UploadToHoster
            // 
            UploadToHoster.BackColor = Color.Transparent;
            UploadToHoster.DisplayStyle = ToolStripItemDisplayStyle.Image;
            UploadToHoster.DropDownButtonWidth = 15;
            UploadToHoster.Image = (Image)resources.GetObject("UploadToHoster.Image");
            UploadToHoster.ImageScaling = ToolStripItemImageScaling.None;
            UploadToHoster.ImageTransparentColor = Color.Magenta;
            UploadToHoster.Margin = new Padding(5);
            UploadToHoster.Name = "UploadToHoster";
            UploadToHoster.Size = new Size(52, 36);
            UploadToHoster.Text = "Upload to {0} (Strg+Q)";
            // 
            // SaveButton
            // 
            SaveButton.BackColor = Color.Transparent;
            SaveButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SaveButton.Image = (Image)resources.GetObject("SaveButton.Image");
            SaveButton.ImageScaling = ToolStripItemImageScaling.None;
            SaveButton.ImageTransparentColor = Color.Magenta;
            SaveButton.Margin = new Padding(5);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(36, 36);
            SaveButton.Text = "Save (Ctrl+Shift+S)";
            SaveButton.Click += SaveImage;
            // 
            // CopyPrintToolStrip
            // 
            CopyPrintToolStrip.BackColor = Color.Transparent;
            CopyPrintToolStrip.Dock = DockStyle.None;
            CopyPrintToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            CopyPrintToolStrip.Items.AddRange(new ToolStripItem[] { CopyToClipboard });
            CopyPrintToolStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            CopyPrintToolStrip.Location = new Point(120, 15);
            CopyPrintToolStrip.Name = "CopyPrintToolStrip";
            CopyPrintToolStrip.RenderMode = ToolStripRenderMode.System;
            CopyPrintToolStrip.Size = new Size(24, 22);
            CopyPrintToolStrip.TabIndex = 36;
            CopyPrintToolStrip.Paint += ToolStripPaint;
            // 
            // CopyToClipboard
            // 
            CopyToClipboard.BackColor = Color.Transparent;
            CopyToClipboard.DisplayStyle = ToolStripItemDisplayStyle.Image;
            CopyToClipboard.Image = (Image)resources.GetObject("CopyToClipboard.Image");
            CopyToClipboard.ImageScaling = ToolStripItemImageScaling.None;
            CopyToClipboard.ImageTransparentColor = Color.Magenta;
            CopyToClipboard.Margin = new Padding(0);
            CopyToClipboard.Name = "CopyToClipboard";
            CopyToClipboard.Size = new Size(22, 20);
            CopyToClipboard.Text = "Copy to Clipboard (Ctrl+C)";
            CopyToClipboard.Click += CopyImage;
            // 
            // CurrentToolSettingsPanel
            // 
            CurrentToolSettingsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CurrentToolSettingsPanel.BackColor = SystemColors.Control;
            CurrentToolSettingsPanel.Location = new Point(490, 15);
            CurrentToolSettingsPanel.Name = "CurrentToolSettingsPanel";
            CurrentToolSettingsPanel.Size = new Size(298, 82);
            CurrentToolSettingsPanel.TabIndex = 37;
            CurrentToolSettingsPanel.Visible = false;
            // 
            // WeirdToolsStrip
            // 
            WeirdToolsStrip.AllowItemReorder = true;
            WeirdToolsStrip.AutoSize = false;
            WeirdToolsStrip.BackColor = Color.FromArgb(0, 0, 0, 0);
            WeirdToolsStrip.Dock = DockStyle.None;
            WeirdToolsStrip.GripMargin = new Padding(0);
            WeirdToolsStrip.GripStyle = ToolStripGripStyle.Hidden;
            WeirdToolsStrip.Items.AddRange(new ToolStripItem[] { ScaleTool, DrawCursor });
            WeirdToolsStrip.Location = new Point(154, 9);
            WeirdToolsStrip.Name = "WeirdToolsStrip";
            WeirdToolsStrip.Padding = new Padding(0);
            WeirdToolsStrip.RenderMode = ToolStripRenderMode.System;
            WeirdToolsStrip.Size = new Size(115, 30);
            WeirdToolsStrip.TabIndex = 38;
            WeirdToolsStrip.Text = "Edit";
            WeirdToolsStrip.Paint += ToolStripPaint;
            // 
            // ScaleTool
            // 
            ScaleTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ScaleTool.Image = (Image)resources.GetObject("ScaleTool.Image");
            ScaleTool.ImageTransparentColor = Color.Magenta;
            ScaleTool.Margin = new Padding(2, 5, 2, 5);
            ScaleTool.Name = "ScaleTool";
            ScaleTool.Size = new Size(23, 20);
            ScaleTool.Text = "Scale Image";
            ScaleTool.Click += ScaleToolClick;
            // 
            // DrawCursor
            // 
            DrawCursor.CheckOnClick = true;
            DrawCursor.DisplayStyle = ToolStripItemDisplayStyle.Image;
            DrawCursor.Image = (Image)resources.GetObject("DrawCursor.Image");
            DrawCursor.ImageTransparentColor = Color.Magenta;
            DrawCursor.Margin = new Padding(2, 5, 2, 5);
            DrawCursor.Name = "DrawCursor";
            DrawCursor.Size = new Size(23, 20);
            DrawCursor.Text = "Draw Cursor";
            DrawCursor.Click += DrawCursor_Click;
            // 
            // EditStrip
            // 
            EditStrip.AllowItemReorder = true;
            EditStrip.AutoSize = false;
            EditStrip.BackColor = Color.FromArgb(0, 0, 0, 0);
            EditStrip.Dock = DockStyle.None;
            EditStrip.GripMargin = new Padding(0);
            EditStrip.GripStyle = ToolStripGripStyle.Hidden;
            EditStrip.ImageScalingSize = new Size(32, 32);
            EditStrip.Items.AddRange(new ToolStripItem[] { CensorTool, MarkerTool, CroppingTool, EraserTool, BlurTool, EllipseTool, PipettenTool, BrightenTool, ArrowTool, UndoStuff, ToolStripDropDownButton1 });
            EditStrip.Location = new Point(9, 75);
            EditStrip.Name = "EditStrip";
            EditStrip.Padding = new Padding(0);
            EditStrip.RenderMode = ToolStripRenderMode.System;
            EditStrip.Size = new Size(439, 39);
            EditStrip.TabIndex = 39;
            EditStrip.Text = "Edit";
            EditStrip.Paint += ToolStripPaint;
            // 
            // CensorTool
            // 
            CensorTool.CheckOnClick = true;
            CensorTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            CensorTool.Image = (Image)resources.GetObject("CensorTool.Image");
            CensorTool.ImageScaling = ToolStripItemImageScaling.None;
            CensorTool.ImageTransparentColor = Color.Magenta;
            CensorTool.Margin = new Padding(2, 0, 2, 2);
            CensorTool.Name = "CensorTool";
            CensorTool.Size = new Size(36, 37);
            CensorTool.Text = "Redact (Ctrl+A)";
            CensorTool.Click += CensorToolClick;
            // 
            // MarkerTool
            // 
            MarkerTool.CheckOnClick = true;
            MarkerTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            MarkerTool.Image = (Image)resources.GetObject("MarkerTool.Image");
            MarkerTool.ImageScaling = ToolStripItemImageScaling.None;
            MarkerTool.ImageTransparentColor = Color.Magenta;
            MarkerTool.Margin = new Padding(2, 0, 2, 2);
            MarkerTool.Name = "MarkerTool";
            MarkerTool.Size = new Size(36, 37);
            MarkerTool.Text = "Mark (Ctrl+S)";
            MarkerTool.Click += MarkerToolClick;
            // 
            // CroppingTool
            // 
            CroppingTool.CheckOnClick = true;
            CroppingTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            CroppingTool.Image = (Image)resources.GetObject("CroppingTool.Image");
            CroppingTool.ImageTransparentColor = Color.Magenta;
            CroppingTool.Margin = new Padding(2, 0, 2, 2);
            CroppingTool.Name = "CroppingTool";
            CroppingTool.Size = new Size(36, 37);
            CroppingTool.Text = "Crop Image (Ctrl+D)";
            CroppingTool.Click += CropToolClick;
            // 
            // EraserTool
            // 
            EraserTool.CheckOnClick = true;
            EraserTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            EraserTool.Image = (Image)resources.GetObject("EraserTool.Image");
            EraserTool.ImageTransparentColor = Color.Magenta;
            EraserTool.Margin = new Padding(2, 0, 2, 2);
            EraserTool.Name = "EraserTool";
            EraserTool.Size = new Size(36, 37);
            EraserTool.Text = "Eraser (Ctrl+E)";
            EraserTool.Click += EraserToolClick;
            // 
            // BlurTool
            // 
            BlurTool.CheckOnClick = true;
            BlurTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            BlurTool.Image = (Image)resources.GetObject("BlurTool.Image");
            BlurTool.ImageTransparentColor = Color.Magenta;
            BlurTool.Margin = new Padding(2, 0, 2, 2);
            BlurTool.Name = "BlurTool";
            BlurTool.Size = new Size(36, 37);
            BlurTool.Text = "Blur Area (Ctrl+F)";
            BlurTool.Click += BlurToolClick;
            // 
            // EllipseTool
            // 
            EllipseTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            EllipseTool.Image = (Image)resources.GetObject("EllipseTool.Image");
            EllipseTool.ImageTransparentColor = Color.Magenta;
            EllipseTool.Margin = new Padding(2, 0, 2, 2);
            EllipseTool.Name = "EllipseTool";
            EllipseTool.Size = new Size(36, 37);
            EllipseTool.Text = "Ellipse (Ctrl+H)";
            EllipseTool.Click += EllipseToolClick;
            // 
            // PipettenTool
            // 
            PipettenTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            PipettenTool.Image = (Image)resources.GetObject("PipettenTool.Image");
            PipettenTool.ImageTransparentColor = Color.Magenta;
            PipettenTool.Margin = new Padding(2, 0, 2, 2);
            PipettenTool.Name = "PipettenTool";
            PipettenTool.Size = new Size(36, 37);
            PipettenTool.Text = "Eye Dropper";
            PipettenTool.Click += EyedropperToolClick;
            // 
            // BrightenTool
            // 
            BrightenTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            BrightenTool.Image = (Image)resources.GetObject("BrightenTool.Image");
            BrightenTool.ImageTransparentColor = Color.Magenta;
            BrightenTool.Margin = new Padding(2, 0, 2, 2);
            BrightenTool.Name = "BrightenTool";
            BrightenTool.Size = new Size(36, 37);
            BrightenTool.Text = "Brighten or Darken Image";
            BrightenTool.Click += BrightenToolClick;
            // 
            // ArrowTool
            // 
            ArrowTool.CheckOnClick = true;
            ArrowTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ArrowTool.Image = (Image)resources.GetObject("ArrowTool.Image");
            ArrowTool.ImageTransparentColor = Color.Magenta;
            ArrowTool.Margin = new Padding(2, 0, 2, 2);
            ArrowTool.Name = "ArrowTool";
            ArrowTool.Size = new Size(36, 37);
            ArrowTool.Text = "Arrow (Ctrl+G)";
            ArrowTool.Click += ArrowToolClick;
            // 
            // UndoStuff
            // 
            UndoStuff.DisplayStyle = ToolStripItemDisplayStyle.Image;
            UndoStuff.Image = (Image)resources.GetObject("UndoStuff.Image");
            UndoStuff.ImageScaling = ToolStripItemImageScaling.None;
            UndoStuff.ImageTransparentColor = Color.Magenta;
            UndoStuff.Margin = new Padding(20, 0, 0, 2);
            UndoStuff.Name = "UndoStuff";
            UndoStuff.Size = new Size(36, 37);
            UndoStuff.Text = "Undo (Ctrl+Z)";
            UndoStuff.Click += UndoClick;
            // 
            // ToolStripDropDownButton1
            // 
            ToolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ToolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { CensorToolStripMenuItem, MarkToolStripMenuItem, CropToolStripMenuItem, EraseToolStripMenuItem, BlurToolStripMenuItem, EllipseToolStripMenuItem, ArrowToolStripMenuItem, ResetToolStripMenuItem, UploadToolStripMenuItem, SaveToolStripMenuItem, ClipboardToolStripMenuItem, PrintToolStripMenuItem, ChooseServiceToolStripMenuItem });
            ToolStripDropDownButton1.Image = (Image)resources.GetObject("ToolStripDropDownButton1.Image");
            ToolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            ToolStripDropDownButton1.Name = "ToolStripDropDownButton1";
            ToolStripDropDownButton1.Size = new Size(45, 36);
            ToolStripDropDownButton1.Text = "ToolStripDropDownButton1";
            ToolStripDropDownButton1.Visible = false;
            // 
            // CensorToolStripMenuItem
            // 
            CensorToolStripMenuItem.Name = "CensorToolStripMenuItem";
            CensorToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            CensorToolStripMenuItem.Size = new Size(226, 22);
            CensorToolStripMenuItem.Text = "Censor";
            CensorToolStripMenuItem.Visible = false;
            CensorToolStripMenuItem.Click += CensorToolClick;
            // 
            // MarkToolStripMenuItem
            // 
            MarkToolStripMenuItem.Name = "MarkToolStripMenuItem";
            MarkToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            MarkToolStripMenuItem.Size = new Size(226, 22);
            MarkToolStripMenuItem.Text = "Mark";
            MarkToolStripMenuItem.Visible = false;
            MarkToolStripMenuItem.Click += MarkerToolClick;
            // 
            // CropToolStripMenuItem
            // 
            CropToolStripMenuItem.Name = "CropToolStripMenuItem";
            CropToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D;
            CropToolStripMenuItem.Size = new Size(226, 22);
            CropToolStripMenuItem.Text = "Crop";
            CropToolStripMenuItem.Visible = false;
            CropToolStripMenuItem.Click += CropToolClick;
            // 
            // EraseToolStripMenuItem
            // 
            EraseToolStripMenuItem.Name = "EraseToolStripMenuItem";
            EraseToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            EraseToolStripMenuItem.Size = new Size(226, 22);
            EraseToolStripMenuItem.Text = "Erase";
            EraseToolStripMenuItem.Visible = false;
            EraseToolStripMenuItem.Click += EraserToolClick;
            // 
            // BlurToolStripMenuItem
            // 
            BlurToolStripMenuItem.Name = "BlurToolStripMenuItem";
            BlurToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F;
            BlurToolStripMenuItem.Size = new Size(226, 22);
            BlurToolStripMenuItem.Text = "Blur";
            BlurToolStripMenuItem.Visible = false;
            BlurToolStripMenuItem.Click += BlurToolClick;
            // 
            // EllipseToolStripMenuItem
            // 
            EllipseToolStripMenuItem.Name = "EllipseToolStripMenuItem";
            EllipseToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.H;
            EllipseToolStripMenuItem.Size = new Size(226, 22);
            EllipseToolStripMenuItem.Text = "Circle";
            EllipseToolStripMenuItem.Visible = false;
            EllipseToolStripMenuItem.Click += EllipseToolClick;
            // 
            // ArrowToolStripMenuItem
            // 
            ArrowToolStripMenuItem.Name = "ArrowToolStripMenuItem";
            ArrowToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.G;
            ArrowToolStripMenuItem.Size = new Size(226, 22);
            ArrowToolStripMenuItem.Text = "Arrow";
            ArrowToolStripMenuItem.Visible = false;
            ArrowToolStripMenuItem.Click += ArrowToolClick;
            // 
            // ResetToolStripMenuItem
            // 
            ResetToolStripMenuItem.Name = "ResetToolStripMenuItem";
            ResetToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            ResetToolStripMenuItem.Size = new Size(226, 22);
            ResetToolStripMenuItem.Text = "Reset";
            ResetToolStripMenuItem.Visible = false;
            ResetToolStripMenuItem.Click += UndoClick;
            // 
            // UploadToolStripMenuItem
            // 
            UploadToolStripMenuItem.Name = "UploadToolStripMenuItem";
            UploadToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            UploadToolStripMenuItem.Size = new Size(226, 22);
            UploadToolStripMenuItem.Text = "Upload";
            UploadToolStripMenuItem.Visible = false;
            // 
            // SaveToolStripMenuItem
            // 
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            SaveToolStripMenuItem.Size = new Size(226, 22);
            SaveToolStripMenuItem.Text = "Save";
            SaveToolStripMenuItem.Visible = false;
            SaveToolStripMenuItem.Click += SaveImage;
            // 
            // ClipboardToolStripMenuItem
            // 
            ClipboardToolStripMenuItem.Name = "ClipboardToolStripMenuItem";
            ClipboardToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            ClipboardToolStripMenuItem.Size = new Size(226, 22);
            ClipboardToolStripMenuItem.Text = "Clipboard";
            ClipboardToolStripMenuItem.Visible = false;
            ClipboardToolStripMenuItem.Click += CopyImage;
            // 
            // PrintToolStripMenuItem
            // 
            PrintToolStripMenuItem.Name = "PrintToolStripMenuItem";
            PrintToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            PrintToolStripMenuItem.Size = new Size(226, 22);
            PrintToolStripMenuItem.Text = "Print";
            PrintToolStripMenuItem.Visible = false;
            // 
            // ChooseServiceToolStripMenuItem
            // 
            ChooseServiceToolStripMenuItem.Name = "ChooseServiceToolStripMenuItem";
            ChooseServiceToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Q;
            ChooseServiceToolStripMenuItem.Size = new Size(226, 22);
            ChooseServiceToolStripMenuItem.Text = "ChooseService";
            ChooseServiceToolStripMenuItem.Visible = false;
            // 
            // ShotEditor2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(800, 638);
            Controls.Add(EditStrip);
            Controls.Add(WeirdToolsStrip);
            Controls.Add(CurrentToolSettingsPanel);
            Controls.Add(CopyPrintToolStrip);
            Controls.Add(ShareStrip);
            Controls.Add(ThePanel);
            Controls.Add(AutoCloseShotEditor);
            Controls.Add(BottomToolStrip);
            Name = "ShotEditor2";
            Text = "Shot Editor";
            FormClosed += ShotEditorClosed;
            Load += ShotEditorLoad;
            BottomToolStrip.ResumeLayout(false);
            BottomToolStrip.PerformLayout();
            ShareStrip.ResumeLayout(false);
            ShareStrip.PerformLayout();
            CopyPrintToolStrip.ResumeLayout(false);
            CopyPrintToolStrip.PerformLayout();
            WeirdToolsStrip.ResumeLayout(false);
            WeirdToolsStrip.PerformLayout();
            EditStrip.ResumeLayout(false);
            EditStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal ToolStrip BottomToolStrip;
        internal ToolStripLabel MouseInfoLabel;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripLabel ImageInfoLabel;
        internal CheckBox AutoCloseShotEditor;
        internal PaintPanel ThePanel;
        internal ToolStrip ShareStrip;
        internal ToolStripSplitButton UploadToHoster;
        internal ToolStripButton SaveButton;
        internal ToolStrip CopyPrintToolStrip;
        internal ToolStripButton CopyToClipboard;
        internal Panel CurrentToolSettingsPanel;
        internal ToolStrip WeirdToolsStrip;
        internal ToolStripButton ScaleTool;
        internal ToolStripButton DrawCursor;
        internal ToolStrip EditStrip;
        internal ToolStripButton CensorTool;
        internal ToolStripButton MarkerTool;
        internal ToolStripButton CroppingTool;
        internal ToolStripButton EraserTool;
        internal ToolStripButton BlurTool;
        internal ToolStripButton EllipseTool;
        internal ToolStripButton PipettenTool;
        internal ToolStripButton BrightenTool;
        internal ToolStripButton ArrowTool;
        internal ToolStripButton UndoStuff;
        internal ToolStripDropDownButton ToolStripDropDownButton1;
        internal ToolStripMenuItem CensorToolStripMenuItem;
        internal ToolStripMenuItem MarkToolStripMenuItem;
        internal ToolStripMenuItem CropToolStripMenuItem;
        internal ToolStripMenuItem EraseToolStripMenuItem;
        internal ToolStripMenuItem BlurToolStripMenuItem;
        internal ToolStripMenuItem EllipseToolStripMenuItem;
        internal ToolStripMenuItem ArrowToolStripMenuItem;
        internal ToolStripMenuItem ResetToolStripMenuItem;
        internal ToolStripMenuItem UploadToolStripMenuItem;
        internal ToolStripMenuItem SaveToolStripMenuItem;
        internal ToolStripMenuItem ClipboardToolStripMenuItem;
        internal ToolStripMenuItem PrintToolStripMenuItem;
        internal ToolStripMenuItem ChooseServiceToolStripMenuItem;
    }
}
