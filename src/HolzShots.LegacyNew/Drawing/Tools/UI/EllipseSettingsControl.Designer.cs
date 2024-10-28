namespace HolzShots.Drawing.Tools.UI
{
    partial class EllipseSettingsControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            EllipseColorSelectorLabel = new Label();
            EllipseDiameterTrackBarLabel = new Label();
            EllipseDiameterTrackBar = new TrackBar();
            EllipseColorSelector = new Windows.Forms.ColorSelector();
            ModeSelector = new TrackBar();
            ModeSelectorLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)EllipseDiameterTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ModeSelector).BeginInit();
            SuspendLayout();
            // 
            // EllipseColorSelectorLabel
            // 
            EllipseColorSelectorLabel.AutoSize = true;
            EllipseColorSelectorLabel.Location = new Point(3, 13);
            EllipseColorSelectorLabel.Name = "EllipseColorSelectorLabel";
            EllipseColorSelectorLabel.Size = new Size(39, 15);
            EllipseColorSelectorLabel.TabIndex = 30;
            EllipseColorSelectorLabel.Text = "Color:";
            // 
            // EllipseDiameterTrackBarLabel
            // 
            EllipseDiameterTrackBarLabel.AutoSize = true;
            EllipseDiameterTrackBarLabel.Dock = DockStyle.Bottom;
            EllipseDiameterTrackBarLabel.Location = new Point(0, 40);
            EllipseDiameterTrackBarLabel.Name = "EllipseDiameterTrackBarLabel";
            EllipseDiameterTrackBarLabel.Size = new Size(42, 15);
            EllipseDiameterTrackBarLabel.TabIndex = 29;
            EllipseDiameterTrackBarLabel.Text = "Width:";
            // 
            // EllipseDiameterTrackBar
            // 
            EllipseDiameterTrackBar.Dock = DockStyle.Bottom;
            EllipseDiameterTrackBar.Location = new Point(0, 55);
            EllipseDiameterTrackBar.Maximum = 100;
            EllipseDiameterTrackBar.Minimum = 1;
            EllipseDiameterTrackBar.Name = "EllipseDiameterTrackBar";
            EllipseDiameterTrackBar.Size = new Size(300, 45);
            EllipseDiameterTrackBar.TabIndex = 27;
            EllipseDiameterTrackBar.TabStop = false;
            EllipseDiameterTrackBar.Value = 1;
            // 
            // EllipseColorSelector
            // 
            EllipseColorSelector.Cursor = Cursors.Hand;
            EllipseColorSelector.Location = new Point(48, 10);
            EllipseColorSelector.Name = "EllipseColorSelector";
            EllipseColorSelector.Size = new Size(20, 20);
            EllipseColorSelector.TabIndex = 31;
            // 
            // ModeSelector
            // 
            ModeSelector.Dock = DockStyle.Right;
            ModeSelector.Location = new Point(230, 0);
            ModeSelector.Maximum = 1;
            ModeSelector.Name = "ModeSelector";
            ModeSelector.Size = new Size(70, 40);
            ModeSelector.TabIndex = 32;
            ModeSelector.TabStop = false;
            // 
            // ModeSelectorLabel
            // 
            ModeSelectorLabel.AutoSize = true;
            ModeSelectorLabel.Dock = DockStyle.Right;
            ModeSelectorLabel.Location = new Point(189, 0);
            ModeSelectorLabel.Name = "ModeSelectorLabel";
            ModeSelectorLabel.Size = new Size(41, 15);
            ModeSelectorLabel.TabIndex = 33;
            ModeSelectorLabel.Text = "Mode:";
            // 
            // EllipseSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(ModeSelectorLabel);
            Controls.Add(ModeSelector);
            Controls.Add(EllipseColorSelector);
            Controls.Add(EllipseColorSelectorLabel);
            Controls.Add(EllipseDiameterTrackBarLabel);
            Controls.Add(EllipseDiameterTrackBar);
            MinimumSize = new Size(300, 100);
            Name = "EllipseSettingsControl";
            Size = new Size(300, 100);
            ((System.ComponentModel.ISupportInitialize)EllipseDiameterTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)ModeSelector).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Label EllipseColorSelectorLabel;
        internal Label EllipseDiameterTrackBarLabel;
        internal TrackBar EllipseDiameterTrackBar;
        internal Windows.Forms.ColorSelector EllipseColorSelector;
        internal TrackBar ModeSelector;
        internal Label ModeSelectorLabel;
    }
}
