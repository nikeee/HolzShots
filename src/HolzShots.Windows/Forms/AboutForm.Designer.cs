namespace HolzShots.Windows.Forms
{
    partial class AboutForm
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
            this.ApplicationTitleLabel = new System.Windows.Forms.Label();
            this.TimestampLabel = new System.Windows.Forms.Label();
            this.HolzShotsLinkLabel = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.LicenseLabel = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.ShowGfxResourcesLinklabel = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.SendFeedbackLink = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.SuspendLayout();
            // 
            // ApplicationTitleLabel
            // 
            this.ApplicationTitleLabel.AutoSize = true;
            this.ApplicationTitleLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplicationTitleLabel.Location = new System.Drawing.Point(12, 9);
            this.ApplicationTitleLabel.Name = "ApplicationTitleLabel";
            this.ApplicationTitleLabel.Size = new System.Drawing.Size(163, 45);
            this.ApplicationTitleLabel.TabIndex = 58;
            this.ApplicationTitleLabel.Text = "HolzShots";
            // 
            // TimestampLabel
            // 
            this.TimestampLabel.AutoSize = true;
            this.TimestampLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TimestampLabel.Location = new System.Drawing.Point(17, 54);
            this.TimestampLabel.Name = "TimestampLabel";
            this.TimestampLabel.Size = new System.Drawing.Size(281, 15);
            this.TimestampLabel.TabIndex = 59;
            this.TimestampLabel.Text = "Open Source, GPL-3.0 Licensed Screenshot Software";
            // 
            // HolzShotsLinkLabel
            // 
            this.HolzShotsLinkLabel.ActiveLinkColor = System.Drawing.SystemColors.Highlight;
            this.HolzShotsLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HolzShotsLinkLabel.AutoSize = true;
            this.HolzShotsLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.HolzShotsLinkLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HolzShotsLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.HolzShotsLinkLabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.HolzShotsLinkLabel.Location = new System.Drawing.Point(225, 91);
            this.HolzShotsLinkLabel.Name = "HolzShotsLinkLabel";
            this.HolzShotsLinkLabel.Size = new System.Drawing.Size(77, 15);
            this.HolzShotsLinkLabel.TabIndex = 60;
            this.HolzShotsLinkLabel.TabStop = true;
            this.HolzShotsLinkLabel.Text = "holzshots.net";
            this.HolzShotsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HolzShotsLinkLabel_LinkClicked);
            // 
            // LicenseLabel
            // 
            this.LicenseLabel.ActiveLinkColor = System.Drawing.SystemColors.Highlight;
            this.LicenseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LicenseLabel.AutoSize = true;
            this.LicenseLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LicenseLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.LicenseLabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.LicenseLabel.Location = new System.Drawing.Point(17, 91);
            this.LicenseLabel.Name = "LicenseLabel";
            this.LicenseLabel.Size = new System.Drawing.Size(46, 15);
            this.LicenseLabel.TabIndex = 61;
            this.LicenseLabel.TabStop = true;
            this.LicenseLabel.Text = "License";
            this.LicenseLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LicenseLabel_LinkClicked);
            // 
            // ShowGfxResourcesLinklabel
            // 
            this.ShowGfxResourcesLinklabel.ActiveLinkColor = System.Drawing.SystemColors.Highlight;
            this.ShowGfxResourcesLinklabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowGfxResourcesLinklabel.AutoSize = true;
            this.ShowGfxResourcesLinklabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ShowGfxResourcesLinklabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.ShowGfxResourcesLinklabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.ShowGfxResourcesLinklabel.Location = new System.Drawing.Point(69, 91);
            this.ShowGfxResourcesLinklabel.Name = "ShowGfxResourcesLinklabel";
            this.ShowGfxResourcesLinklabel.Size = new System.Drawing.Size(53, 15);
            this.ShowGfxResourcesLinklabel.TabIndex = 62;
            this.ShowGfxResourcesLinklabel.TabStop = true;
            this.ShowGfxResourcesLinklabel.Text = "Graphics";
            this.ShowGfxResourcesLinklabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ShowGfxResourcesLinklabel_LinkClicked);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.VersionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VersionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.VersionLabel.Location = new System.Drawing.Point(169, 33);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(105, 15);
            this.VersionLabel.TabIndex = 63;
            this.VersionLabel.Text = "0.9.8.7 (11.08.2013)";
            // 
            // SendFeedbackLink
            // 
            this.SendFeedbackLink.ActiveLinkColor = System.Drawing.SystemColors.Highlight;
            this.SendFeedbackLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendFeedbackLink.AutoSize = true;
            this.SendFeedbackLink.BackColor = System.Drawing.Color.Transparent;
            this.SendFeedbackLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SendFeedbackLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.SendFeedbackLink.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.SendFeedbackLink.Location = new System.Drawing.Point(133, 91);
            this.SendFeedbackLink.Name = "SendFeedbackLink";
            this.SendFeedbackLink.Size = new System.Drawing.Size(86, 15);
            this.SendFeedbackLink.TabIndex = 64;
            this.SendFeedbackLink.TabStop = true;
            this.SendFeedbackLink.Text = "Send Feedback";
            this.SendFeedbackLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SendFeedbackLink_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(314, 115);
            this.Controls.Add(this.SendFeedbackLink);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.ShowGfxResourcesLinklabel);
            this.Controls.Add(this.LicenseLabel);
            this.Controls.Add(this.HolzShotsLinkLabel);
            this.Controls.Add(this.TimestampLabel);
            this.Controls.Add(this.ApplicationTitleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label ApplicationTitleLabel;
        internal System.Windows.Forms.Label TimestampLabel;
        internal ExplorerLinkLabel HolzShotsLinkLabel;
        internal ExplorerLinkLabel LicenseLabel;
        internal ExplorerLinkLabel ShowGfxResourcesLinklabel;
        internal System.Windows.Forms.Label VersionLabel;
        internal ExplorerLinkLabel SendFeedbackLink;
    }
}