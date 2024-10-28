namespace HolzShots.Drawing.Tools.UI
{
    partial class BlurSettingsControl
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
            BlurDiameterTrackBarLabel = new Label();
            BlurDiameterTrackBar = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)BlurDiameterTrackBar).BeginInit();
            SuspendLayout();
            //
            // BlurDiameterTrackBarLabel
            //
            BlurDiameterTrackBarLabel.AutoSize = true;
            BlurDiameterTrackBarLabel.Dock = DockStyle.Bottom;
            BlurDiameterTrackBarLabel.Location = new Point(0, 0);
            BlurDiameterTrackBarLabel.Name = "BlurDiameterTrackBarLabel";
            BlurDiameterTrackBarLabel.Padding = new Padding(0, 8, 0, 8);
            BlurDiameterTrackBarLabel.Size = new Size(55, 31);
            BlurDiameterTrackBarLabel.TabIndex = 18;
            BlurDiameterTrackBarLabel.Text = "Diameter";
            //
            // BlurDiameterTrackBar
            //
            BlurDiameterTrackBar.Dock = DockStyle.Bottom;
            BlurDiameterTrackBar.Location = new Point(0, 31);
            BlurDiameterTrackBar.Maximum = 100;
            BlurDiameterTrackBar.Minimum = 1;
            BlurDiameterTrackBar.Name = "BlurDiameterTrackBar";
            BlurDiameterTrackBar.Size = new Size(300, 45);
            BlurDiameterTrackBar.TabIndex = 1;
            BlurDiameterTrackBar.TabStop = false;
            BlurDiameterTrackBar.Value = 1;
            //
            // BlurSettingsControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(BlurDiameterTrackBarLabel);
            Controls.Add(BlurDiameterTrackBar);
            MinimumSize = new Size(300, 50);
            Name = "BlurSettingsControl";
            Size = new Size(300, 76);
            ((System.ComponentModel.ISupportInitialize)BlurDiameterTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Label BlurDiameterTrackBarLabel;
        internal TrackBar BlurDiameterTrackBar;
    }
}
