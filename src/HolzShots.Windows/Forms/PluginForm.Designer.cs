namespace HolzShots.Windows.Forms
{
    partial class PluginForm
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.PrimingLabel = new System.Windows.Forms.Label();
            this.PluginPanel = new HolzShots.Windows.Forms.Controls.StackPanel();
            this.OpenPluginsDirectoryLabel = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(387, 326);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 61;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // PrimingLabel
            // 
            this.PrimingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PrimingLabel.AutoSize = true;
            this.PrimingLabel.Location = new System.Drawing.Point(12, 322);
            this.PrimingLabel.Name = "PrimingLabel";
            this.PrimingLabel.Size = new System.Drawing.Size(245, 30);
            this.PrimingLabel.TabIndex = 62;
            this.PrimingLabel.Text = "Add plugins or custom uploaders by copying\r\nthe DLL or JSON files to the plugin d" +
    "irectory.";
            // 
            // PluginPanel
            // 
            this.PluginPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PluginPanel.Location = new System.Drawing.Point(-1, 0);
            this.PluginPanel.Name = "PluginPanel";
            this.PluginPanel.Size = new System.Drawing.Size(476, 319);
            this.PluginPanel.TabIndex = 63;
            // 
            // OpenPluginsDirectoryLabel
            // 
            this.OpenPluginsDirectoryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenPluginsDirectoryLabel.AutoSize = true;
            this.OpenPluginsDirectoryLabel.BackColor = System.Drawing.Color.Transparent;
            this.OpenPluginsDirectoryLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenPluginsDirectoryLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.OpenPluginsDirectoryLabel.Location = new System.Drawing.Point(267, 330);
            this.OpenPluginsDirectoryLabel.Name = "OpenPluginsDirectoryLabel";
            this.OpenPluginsDirectoryLabel.Size = new System.Drawing.Size(114, 15);
            this.OpenPluginsDirectoryLabel.TabIndex = 60;
            this.OpenPluginsDirectoryLabel.TabStop = true;
            this.OpenPluginsDirectoryLabel.Text = "Open Plugins Folder";
            this.OpenPluginsDirectoryLabel.Click += new System.EventHandler(this.OpenPluginsDirectoryLabel_LinkClicked);
            // 
            // PluginForm
            // 
            this.AcceptButton = this.CloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(474, 361);
            this.Controls.Add(this.PluginPanel);
            this.Controls.Add(this.PrimingLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OpenPluginsDirectoryLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "PluginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plugins";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal ExplorerLinkLabel OpenPluginsDirectoryLabel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label PrimingLabel;
        private Controls.StackPanel PluginPanel;
    }
}
