namespace HolzShots.Windows.Forms.Controls
{
    partial class PluginItem
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
            this.pluginName = new System.Windows.Forms.Label();
            this.pluginAuthor = new System.Windows.Forms.Label();
            this.pluginVersion = new System.Windows.Forms.Label();
            this.pluginSettings = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.authorWebsite = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.reportBug = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.SuspendLayout();
            // 
            // pluginName
            // 
            this.pluginName.AutoSize = true;
            this.pluginName.BackColor = System.Drawing.Color.Transparent;
            this.pluginName.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pluginName.Location = new System.Drawing.Point(8, 5);
            this.pluginName.Name = "pluginName";
            this.pluginName.Size = new System.Drawing.Size(136, 18);
            this.pluginName.TabIndex = 1;
            this.pluginName.Text = "Den Plugin - yo!";
            // 
            // pluginAuthor
            // 
            this.pluginAuthor.AutoSize = true;
            this.pluginAuthor.BackColor = System.Drawing.Color.Transparent;
            this.pluginAuthor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pluginAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pluginAuthor.Location = new System.Drawing.Point(8, 23);
            this.pluginAuthor.Name = "pluginAuthor";
            this.pluginAuthor.Size = new System.Drawing.Size(109, 15);
            this.pluginAuthor.TabIndex = 6;
            this.pluginAuthor.Text = "Niklas Mollenhauer";
            // 
            // pluginVersion
            // 
            this.pluginVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginVersion.AutoSize = true;
            this.pluginVersion.BackColor = System.Drawing.Color.Transparent;
            this.pluginVersion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pluginVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pluginVersion.Location = new System.Drawing.Point(277, 5);
            this.pluginVersion.Name = "pluginVersion";
            this.pluginVersion.Size = new System.Drawing.Size(45, 17);
            this.pluginVersion.TabIndex = 6;
            this.pluginVersion.Text = "1.3.3.7";
            this.pluginVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pluginSettings
            // 
            this.pluginSettings.ActiveLinkColor = System.Drawing.SystemColors.Highlight;
            this.pluginSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginSettings.AutoSize = true;
            this.pluginSettings.BackColor = System.Drawing.Color.Transparent;
            this.pluginSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pluginSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pluginSettings.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.pluginSettings.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.pluginSettings.Location = new System.Drawing.Point(179, 23);
            this.pluginSettings.Name = "pluginSettings";
            this.pluginSettings.Size = new System.Drawing.Size(49, 15);
            this.pluginSettings.TabIndex = 9;
            this.pluginSettings.TabStop = true;
            this.pluginSettings.Text = "Settings";
            this.pluginSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.pluginSettings_LinkClicked);
            // 
            // authorWebsite
            // 
            this.authorWebsite.ActiveLinkColor = System.Drawing.SystemColors.Highlight;
            this.authorWebsite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.authorWebsite.AutoSize = true;
            this.authorWebsite.BackColor = System.Drawing.Color.Transparent;
            this.authorWebsite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.authorWebsite.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.authorWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.authorWebsite.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.authorWebsite.Location = new System.Drawing.Point(234, 23);
            this.authorWebsite.Name = "authorWebsite";
            this.authorWebsite.Size = new System.Drawing.Size(49, 15);
            this.authorWebsite.TabIndex = 8;
            this.authorWebsite.TabStop = true;
            this.authorWebsite.Text = "Website";
            this.authorWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.authorWebSite_LinkClicked);
            // 
            // reportBug
            // 
            this.reportBug.ActiveLinkColor = System.Drawing.SystemColors.Highlight;
            this.reportBug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reportBug.AutoSize = true;
            this.reportBug.BackColor = System.Drawing.Color.Transparent;
            this.reportBug.Cursor = System.Windows.Forms.Cursors.Hand;
            this.reportBug.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.reportBug.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.reportBug.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.reportBug.Location = new System.Drawing.Point(289, 23);
            this.reportBug.Name = "reportBug";
            this.reportBug.Size = new System.Drawing.Size(33, 15);
            this.reportBug.TabIndex = 7;
            this.reportBug.TabStop = true;
            this.reportBug.Text = "Bugs";
            this.reportBug.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.reportBug_LinkClicked);
            // 
            // PluginItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.pluginSettings);
            this.Controls.Add(this.authorWebsite);
            this.Controls.Add(this.reportBug);
            this.Controls.Add(this.pluginVersion);
            this.Controls.Add(this.pluginAuthor);
            this.Controls.Add(this.pluginName);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PluginItem";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(330, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label pluginName;
        internal System.Windows.Forms.Label pluginAuthor;
        private System.Windows.Forms.Label pluginVersion;
        internal ExplorerLinkLabel reportBug;
        internal ExplorerLinkLabel authorWebsite;
        internal ExplorerLinkLabel pluginSettings;
    }
}
