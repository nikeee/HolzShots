namespace HolzShots.Drawing.Tools.UI
{
    partial class CensorSettingsControl
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
            CensorColorSelectorLabel = new Label();
            CensorDiameterTrackBarLabel = new Label();
            CensorDiameterTrackBar = new TrackBar();
            CensorColorSelector = new Windows.Forms.ColorSelector();
            ((System.ComponentModel.ISupportInitialize)CensorDiameterTrackBar).BeginInit();
            SuspendLayout();
            // 
            // CensorColorSelectorLabel
            // 
            CensorColorSelectorLabel.AutoSize = true;
            CensorColorSelectorLabel.Location = new Point(3, 13);
            CensorColorSelectorLabel.Name = "CensorColorSelectorLabel";
            CensorColorSelectorLabel.Size = new Size(39, 15);
            CensorColorSelectorLabel.TabIndex = 30;
            CensorColorSelectorLabel.Text = "Color:";
            // 
            // CensorDiameterTrackBarLabel
            // 
            CensorDiameterTrackBarLabel.AutoSize = true;
            CensorDiameterTrackBarLabel.Dock = DockStyle.Bottom;
            CensorDiameterTrackBarLabel.Location = new Point(0, 40);
            CensorDiameterTrackBarLabel.Name = "CensorDiameterTrackBarLabel";
            CensorDiameterTrackBarLabel.Size = new Size(42, 15);
            CensorDiameterTrackBarLabel.TabIndex = 29;
            CensorDiameterTrackBarLabel.Text = "Width:";
            // 
            // CensorDiameterTrackBar
            // 
            CensorDiameterTrackBar.Dock = DockStyle.Bottom;
            CensorDiameterTrackBar.Location = new Point(0, 55);
            CensorDiameterTrackBar.Maximum = 100;
            CensorDiameterTrackBar.Minimum = 1;
            CensorDiameterTrackBar.Name = "CensorDiameterTrackBar";
            CensorDiameterTrackBar.Size = new Size(300, 45);
            CensorDiameterTrackBar.TabIndex = 27;
            CensorDiameterTrackBar.TabStop = false;
            CensorDiameterTrackBar.Value = 1;
            // 
            // CensorColorSelector
            // 
            CensorColorSelector.Cursor = Cursors.Hand;
            CensorColorSelector.Location = new Point(48, 10);
            CensorColorSelector.Name = "CensorColorSelector";
            CensorColorSelector.Size = new Size(20, 20);
            CensorColorSelector.TabIndex = 31;
            // 
            // CensorSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(CensorColorSelector);
            Controls.Add(CensorColorSelectorLabel);
            Controls.Add(CensorDiameterTrackBarLabel);
            Controls.Add(CensorDiameterTrackBar);
            MinimumSize = new Size(300, 100);
            Name = "CensorSettingsControl";
            Size = new Size(300, 100);
            ((System.ComponentModel.ISupportInitialize)CensorDiameterTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Label CensorColorSelectorLabel;
        internal Label CensorDiameterTrackBarLabel;
        internal TrackBar CensorDiameterTrackBar;
        internal Windows.Forms.ColorSelector CensorColorSelector;
    }
}
