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
            this.components = new System.ComponentModel.Container();
            this.pluginNameLabel = new System.Windows.Forms.Label();
            this.modelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pluginAuthor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pluginSettings = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.authorWebSite = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.reportBug = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.modelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pluginNameLabel
            // 
            this.pluginNameLabel.AutoSize = true;
            this.pluginNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.pluginNameLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.modelBindingSource, "Name", true));
            this.pluginNameLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.pluginNameLabel.Location = new System.Drawing.Point(3, 3);
            this.pluginNameLabel.Name = "pluginNameLabel";
            this.pluginNameLabel.Size = new System.Drawing.Size(115, 20);
            this.pluginNameLabel.TabIndex = 1;
            this.pluginNameLabel.Text = "Den Plugin - yo!";
            // 
            // modelBindingSource
            // 
            this.modelBindingSource.DataSource = typeof(HolzShots.Composition.IPluginMetadata);
            // 
            // pluginAuthor
            // 
            this.pluginAuthor.AutoSize = true;
            this.pluginAuthor.BackColor = System.Drawing.Color.Transparent;
            this.pluginAuthor.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.modelBindingSource, "Author", true));
            this.pluginAuthor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pluginAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pluginAuthor.Location = new System.Drawing.Point(4, 23);
            this.pluginAuthor.Name = "pluginAuthor";
            this.pluginAuthor.Size = new System.Drawing.Size(109, 15);
            this.pluginAuthor.TabIndex = 6;
            this.pluginAuthor.Text = "Niklas Mollenhauer";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.modelBindingSource, "Version", true));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label1.Location = new System.Drawing.Point(235, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "1.3.3.7";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            this.pluginSettings.Location = new System.Drawing.Point(164, 23);
            this.pluginSettings.Name = "pluginSettings";
            this.pluginSettings.Size = new System.Drawing.Size(49, 15);
            this.pluginSettings.TabIndex = 9;
            this.pluginSettings.TabStop = true;
            this.pluginSettings.Text = "Settings";
            this.pluginSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.pluginSettings_LinkClicked);
            // 
            // authorWebSite
            // 
            this.authorWebSite.ActiveLinkColor = System.Drawing.SystemColors.Highlight;
            this.authorWebSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.authorWebSite.AutoSize = true;
            this.authorWebSite.BackColor = System.Drawing.Color.Transparent;
            this.authorWebSite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.authorWebSite.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.authorWebSite.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.authorWebSite.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.authorWebSite.Location = new System.Drawing.Point(219, 23);
            this.authorWebSite.Name = "authorWebSite";
            this.authorWebSite.Size = new System.Drawing.Size(49, 15);
            this.authorWebSite.TabIndex = 8;
            this.authorWebSite.TabStop = true;
            this.authorWebSite.Text = "Website";
            this.authorWebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.authorWebSite_LinkClicked);
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
            this.reportBug.Location = new System.Drawing.Point(274, 23);
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
            this.Controls.Add(this.authorWebSite);
            this.Controls.Add(this.reportBug);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pluginAuthor);
            this.Controls.Add(this.pluginNameLabel);
            this.Name = "PluginItem";
            this.Size = new System.Drawing.Size(310, 45);
            ((System.ComponentModel.ISupportInitialize)(this.modelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label pluginNameLabel;
        internal System.Windows.Forms.Label pluginAuthor;
        private System.Windows.Forms.Label label1;
        internal ExplorerLinkLabel reportBug;
        internal ExplorerLinkLabel authorWebSite;
        internal ExplorerLinkLabel pluginSettings;
        private System.Windows.Forms.BindingSource modelBindingSource;
    }
}
