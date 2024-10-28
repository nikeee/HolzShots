namespace HolzShots.Drawing.Tools.UI
{
    partial class EraserSettingsControl
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
            EraserDiameterTrackBarLabel = new Label();
            EraserDiameterTrackBar = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)EraserDiameterTrackBar).BeginInit();
            SuspendLayout();
            // 
            // EraserDiameterTrackBarLabel
            // 
            EraserDiameterTrackBarLabel.AutoSize = true;
            EraserDiameterTrackBarLabel.Dock = DockStyle.Bottom;
            EraserDiameterTrackBarLabel.Location = new Point(0, 0);
            EraserDiameterTrackBarLabel.Name = "EraserDiameterTrackBarLabel";
            EraserDiameterTrackBarLabel.Padding = new Padding(0, 8, 0, 8);
            EraserDiameterTrackBarLabel.Size = new Size(55, 31);
            EraserDiameterTrackBarLabel.TabIndex = 18;
            EraserDiameterTrackBarLabel.Text = "Diameter";
            // 
            // EraserDiameterTrackBar
            // 
            EraserDiameterTrackBar.Dock = DockStyle.Bottom;
            EraserDiameterTrackBar.Location = new Point(0, 31);
            EraserDiameterTrackBar.Maximum = 100;
            EraserDiameterTrackBar.Minimum = 1;
            EraserDiameterTrackBar.Name = "EraserDiameterTrackBar";
            EraserDiameterTrackBar.Size = new Size(300, 45);
            EraserDiameterTrackBar.TabIndex = 1;
            EraserDiameterTrackBar.TabStop = false;
            EraserDiameterTrackBar.Value = 1;
            // 
            // EraserSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(EraserDiameterTrackBarLabel);
            Controls.Add(EraserDiameterTrackBar);
            MinimumSize = new Size(300, 50);
            Name = "EraserSettingsControl";
            Size = new Size(300, 76);
            ((System.ComponentModel.ISupportInitialize)EraserDiameterTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Label EraserDiameterTrackBarLabel;
        internal TrackBar EraserDiameterTrackBar;
    }
}
