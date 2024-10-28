namespace HolzShots.Drawing.Tools.UI
{
    partial class ArrowSettingsControl
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
            ArrowColorSelectorLabel = new Label();
            ArrowDiameterTrackBarLabel = new Label();
            ArrowDiameterTrackBar = new TrackBar();
            ArrowColorSelector = new Windows.Forms.ColorSelector();
            ((System.ComponentModel.ISupportInitialize)ArrowDiameterTrackBar).BeginInit();
            SuspendLayout();
            //
            // ArrowColorSelectorLabel
            //
            ArrowColorSelectorLabel.AutoSize = true;
            ArrowColorSelectorLabel.Location = new Point(3, 13);
            ArrowColorSelectorLabel.Name = "ArrowColorSelectorLabel";
            ArrowColorSelectorLabel.Size = new Size(39, 15);
            ArrowColorSelectorLabel.TabIndex = 30;
            ArrowColorSelectorLabel.Text = "Color:";
            //
            // ArrowDiameterTrackBarLabel
            //
            ArrowDiameterTrackBarLabel.AutoSize = true;
            ArrowDiameterTrackBarLabel.Dock = DockStyle.Bottom;
            ArrowDiameterTrackBarLabel.Location = new Point(0, 40);
            ArrowDiameterTrackBarLabel.Name = "ArrowDiameterTrackBarLabel";
            ArrowDiameterTrackBarLabel.Size = new Size(42, 15);
            ArrowDiameterTrackBarLabel.TabIndex = 29;
            ArrowDiameterTrackBarLabel.Text = "Width:";
            //
            // ArrowDiameterTrackBar
            //
            ArrowDiameterTrackBar.Dock = DockStyle.Bottom;
            ArrowDiameterTrackBar.Location = new Point(0, 55);
            ArrowDiameterTrackBar.Maximum = 100;
            ArrowDiameterTrackBar.Minimum = 1;
            ArrowDiameterTrackBar.Name = "ArrowDiameterTrackBar";
            ArrowDiameterTrackBar.Size = new Size(300, 45);
            ArrowDiameterTrackBar.TabIndex = 27;
            ArrowDiameterTrackBar.TabStop = false;
            ArrowDiameterTrackBar.Value = 1;
            //
            // ArrowColorSelector
            //
            ArrowColorSelector.Cursor = Cursors.Hand;
            ArrowColorSelector.Location = new Point(48, 10);
            ArrowColorSelector.Name = "ArrowColorSelector";
            ArrowColorSelector.Size = new Size(20, 20);
            ArrowColorSelector.TabIndex = 31;
            //
            // ArrowSettingsControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(ArrowColorSelector);
            Controls.Add(ArrowColorSelectorLabel);
            Controls.Add(ArrowDiameterTrackBarLabel);
            Controls.Add(ArrowDiameterTrackBar);
            MinimumSize = new Size(300, 100);
            Name = "ArrowSettingsControl";
            Size = new Size(300, 100);
            ((System.ComponentModel.ISupportInitialize)ArrowDiameterTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Label ArrowColorSelectorLabel;
        internal Label ArrowDiameterTrackBarLabel;
        internal TrackBar ArrowDiameterTrackBar;
        internal Windows.Forms.ColorSelector ArrowColorSelector;
    }
}
