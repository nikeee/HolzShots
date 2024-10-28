namespace HolzShots.Drawing.Tools.UI
{
    partial class BrightnessSettingsControl
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
            DarkBrightnessTrackBarLabel = new Label();
            BrightnessTrackBar = new TrackBar();
            BrightnessPreview = new Windows.Forms.ColorView();
            LightBrightnessTrackBarLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)BrightnessTrackBar).BeginInit();
            SuspendLayout();
            // 
            // DarkBrightnessTrackBarLabel
            // 
            DarkBrightnessTrackBarLabel.AutoSize = true;
            DarkBrightnessTrackBarLabel.Dock = DockStyle.Left;
            DarkBrightnessTrackBarLabel.Location = new Point(0, 45);
            DarkBrightnessTrackBarLabel.Name = "DarkBrightnessTrackBarLabel";
            DarkBrightnessTrackBarLabel.Size = new Size(31, 15);
            DarkBrightnessTrackBarLabel.TabIndex = 29;
            DarkBrightnessTrackBarLabel.Text = "Dark";
            // 
            // BrightnessTrackBar
            // 
            BrightnessTrackBar.Dock = DockStyle.Top;
            BrightnessTrackBar.Location = new Point(0, 0);
            BrightnessTrackBar.Maximum = 100;
            BrightnessTrackBar.Minimum = 1;
            BrightnessTrackBar.Name = "BrightnessTrackBar";
            BrightnessTrackBar.Size = new Size(300, 45);
            BrightnessTrackBar.TabIndex = 27;
            BrightnessTrackBar.TabStop = false;
            BrightnessTrackBar.Value = 1;
            // 
            // BrightnessPreview
            // 
            BrightnessPreview.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BrightnessPreview.Location = new Point(37, 40);
            BrightnessPreview.Name = "BrightnessPreview";
            BrightnessPreview.Size = new Size(223, 26);
            BrightnessPreview.TabIndex = 32;
            BrightnessPreview.TabStop = false;
            // 
            // LightBrightnessTrackBarLabel
            // 
            LightBrightnessTrackBarLabel.AutoSize = true;
            LightBrightnessTrackBarLabel.Dock = DockStyle.Right;
            LightBrightnessTrackBarLabel.Location = new Point(266, 45);
            LightBrightnessTrackBarLabel.Name = "LightBrightnessTrackBarLabel";
            LightBrightnessTrackBarLabel.Size = new Size(34, 15);
            LightBrightnessTrackBarLabel.TabIndex = 33;
            LightBrightnessTrackBarLabel.Text = "Light";
            // 
            // BrightnessSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(LightBrightnessTrackBarLabel);
            Controls.Add(BrightnessPreview);
            Controls.Add(DarkBrightnessTrackBarLabel);
            Controls.Add(BrightnessTrackBar);
            MinimumSize = new Size(300, 100);
            Name = "BrightnessSettingsControl";
            Size = new Size(300, 100);
            ((System.ComponentModel.ISupportInitialize)BrightnessTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Label BrightnessColorSelectorLabel;
        internal Label DarkBrightnessTrackBarLabel;
        internal TrackBar BrightnessTrackBar;
        internal Windows.Forms.ColorView BrightnessPreview;
        internal Label LightBrightnessTrackBarLabel;
    }
}
