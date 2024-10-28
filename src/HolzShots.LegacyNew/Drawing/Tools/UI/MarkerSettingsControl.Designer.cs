namespace HolzShots.Drawing.Tools.UI
{
    partial class MarkerSettingsControl
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
            MarkerColorSelectorLabel = new Label();
            MarkerDiameterTrackBarLabel = new Label();
            MarkerDiameterTrackBar = new TrackBar();
            MarkerColorSelector = new Windows.Forms.ColorSelector();
            ((System.ComponentModel.ISupportInitialize)MarkerDiameterTrackBar).BeginInit();
            SuspendLayout();
            //
            // MarkerColorSelectorLabel
            //
            MarkerColorSelectorLabel.AutoSize = true;
            MarkerColorSelectorLabel.Location = new Point(3, 13);
            MarkerColorSelectorLabel.Name = "MarkerColorSelectorLabel";
            MarkerColorSelectorLabel.Size = new Size(39, 15);
            MarkerColorSelectorLabel.TabIndex = 30;
            MarkerColorSelectorLabel.Text = "Color:";
            //
            // MarkerDiameterTrackBarLabel
            //
            MarkerDiameterTrackBarLabel.AutoSize = true;
            MarkerDiameterTrackBarLabel.Dock = DockStyle.Bottom;
            MarkerDiameterTrackBarLabel.Location = new Point(0, 40);
            MarkerDiameterTrackBarLabel.Name = "MarkerDiameterTrackBarLabel";
            MarkerDiameterTrackBarLabel.Size = new Size(42, 15);
            MarkerDiameterTrackBarLabel.TabIndex = 29;
            MarkerDiameterTrackBarLabel.Text = "Width:";
            //
            // MarkerDiameterTrackBar
            //
            MarkerDiameterTrackBar.Dock = DockStyle.Bottom;
            MarkerDiameterTrackBar.Location = new Point(0, 55);
            MarkerDiameterTrackBar.Maximum = 100;
            MarkerDiameterTrackBar.Minimum = 1;
            MarkerDiameterTrackBar.Name = "MarkerDiameterTrackBar";
            MarkerDiameterTrackBar.Size = new Size(300, 45);
            MarkerDiameterTrackBar.TabIndex = 27;
            MarkerDiameterTrackBar.TabStop = false;
            MarkerDiameterTrackBar.Value = 1;
            //
            // MarkerColorSelector
            //
            MarkerColorSelector.Cursor = Cursors.Hand;
            MarkerColorSelector.Location = new Point(48, 10);
            MarkerColorSelector.Name = "MarkerColorSelector";
            MarkerColorSelector.Size = new Size(20, 20);
            MarkerColorSelector.TabIndex = 31;
            //
            // MarkerSettingsControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(MarkerColorSelector);
            Controls.Add(MarkerColorSelectorLabel);
            Controls.Add(MarkerDiameterTrackBarLabel);
            Controls.Add(MarkerDiameterTrackBar);
            MinimumSize = new Size(300, 100);
            Name = "MarkerSettingsControl";
            Size = new Size(300, 100);
            ((System.ComponentModel.ISupportInitialize)MarkerDiameterTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Label MarkerColorSelectorLabel;
        internal Label MarkerDiameterTrackBarLabel;
        internal TrackBar MarkerDiameterTrackBar;
        internal Windows.Forms.ColorSelector MarkerColorSelector;
    }
}
