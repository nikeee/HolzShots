namespace HolzShots.Windows.Forms
{
    partial class UploadStatusFlyoutForm
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
            this.uploadedBytesLabel = new System.Windows.Forms.Label();
            this.statusTextLabel = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.stuffUploadedBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // uploadedBytesLabel
            // 
            this.uploadedBytesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uploadedBytesLabel.AutoSize = true;
            this.uploadedBytesLabel.Location = new System.Drawing.Point(12, 53);
            this.uploadedBytesLabel.Name = "uploadedBytesLabel";
            this.uploadedBytesLabel.Size = new System.Drawing.Size(92, 15);
            this.uploadedBytesLabel.TabIndex = 7;
            this.uploadedBytesLabel.Text = "0.0 KB of 12 MiB";
            // 
            // statusTextLabel
            // 
            this.statusTextLabel.AutoSize = true;
            this.statusTextLabel.Location = new System.Drawing.Point(12, 9);
            this.statusTextLabel.Name = "statusTextLabel";
            this.statusTextLabel.Size = new System.Drawing.Size(149, 15);
            this.statusTextLabel.TabIndex = 6;
            this.statusTextLabel.Text = "Image is being uploaded....";
            // 
            // speedLabel
            // 
            this.speedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(150, 53);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(52, 15);
            this.speedLabel.TabIndex = 5;
            this.speedLabel.Text = "0.0 KiB/s";
            // 
            // stuffUploadedBar
            // 
            this.stuffUploadedBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stuffUploadedBar.Location = new System.Drawing.Point(12, 27);
            this.stuffUploadedBar.Name = "stuffUploadedBar";
            this.stuffUploadedBar.Size = new System.Drawing.Size(190, 23);
            this.stuffUploadedBar.TabIndex = 4;
            // 
            // UploadStatusFlyoutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(214, 74);
            this.Controls.Add(this.uploadedBytesLabel);
            this.Controls.Add(this.statusTextLabel);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.stuffUploadedBar);
            this.MaximumSize = new System.Drawing.Size(230, 90);
            this.MinimumSize = new System.Drawing.Size(230, 90);
            this.Name = "UploadStatusFlyoutForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label uploadedBytesLabel;
        internal System.Windows.Forms.Label statusTextLabel;
        internal System.Windows.Forms.Label speedLabel;
        internal System.Windows.Forms.ProgressBar stuffUploadedBar;
    }
}
