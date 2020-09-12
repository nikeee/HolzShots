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
            this.OpenPluginsDirectory = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.SuspendLayout();
            // 
            // OpenPluginsDirectory
            // 
            this.OpenPluginsDirectory.ActiveLinkColor = System.Drawing.SystemColors.Highlight;
            this.OpenPluginsDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenPluginsDirectory.AutoSize = true;
            this.OpenPluginsDirectory.BackColor = System.Drawing.Color.Transparent;
            this.OpenPluginsDirectory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenPluginsDirectory.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.OpenPluginsDirectory.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.OpenPluginsDirectory.Location = new System.Drawing.Point(458, 9);
            this.OpenPluginsDirectory.Name = "OpenPluginsDirectory";
            this.OpenPluginsDirectory.Size = new System.Drawing.Size(114, 15);
            this.OpenPluginsDirectory.TabIndex = 60;
            this.OpenPluginsDirectory.TabStop = true;
            this.OpenPluginsDirectory.Text = "Open Plugins Folder";
            // 
            // PluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.OpenPluginsDirectory);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PluginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plugins";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal ExplorerLinkLabel OpenPluginsDirectory;
    }
}
