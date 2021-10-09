namespace HolzShots.Capture.Video.UI
{
    partial class RecordingControls
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
            this.StopRecordingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StopRecordingButton
            // 
            this.StopRecordingButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopRecordingButton.Location = new System.Drawing.Point(0, 0);
            this.StopRecordingButton.Margin = new System.Windows.Forms.Padding(10);
            this.StopRecordingButton.Name = "StopRecordingButton";
            this.StopRecordingButton.Size = new System.Drawing.Size(170, 44);
            this.StopRecordingButton.TabIndex = 0;
            this.StopRecordingButton.Text = "Stop Recording";
            this.StopRecordingButton.UseVisualStyleBackColor = true;
            this.StopRecordingButton.Click += new System.EventHandler(this.StopRecordingButton_Click);
            // 
            // RecordingControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(170, 44);
            this.ControlBox = false;
            this.Controls.Add(this.StopRecordingButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecordingControls";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Screen Recorder";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StopRecordingButton;
    }
}