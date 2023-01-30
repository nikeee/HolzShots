namespace HolzShots.Windows.Forms
{
    partial class UploadResultForm
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
            this.CopyMarkdown = new System.Windows.Forms.Button();
            this.CopyHTML = new System.Windows.Forms.Button();
            this.CopyDirectLink = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CloseLabel = new HolzShots.Windows.Forms.ExplorerLinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CopyMarkdown
            // 
            this.CopyMarkdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyMarkdown.Location = new System.Drawing.Point(14, 13);
            this.CopyMarkdown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CopyMarkdown.Name = "CopyMarkdown";
            this.CopyMarkdown.Size = new System.Drawing.Size(188, 31);
            this.CopyMarkdown.TabIndex = 1;
            this.CopyMarkdown.Text = "Copy Markdown";
            this.CopyMarkdown.UseVisualStyleBackColor = true;
            this.CopyMarkdown.Click += new System.EventHandler(this.CopyMarkdownClick);
            // 
            // CopyHTML
            // 
            this.CopyHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyHTML.Location = new System.Drawing.Point(14, 52);
            this.CopyHTML.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CopyHTML.Name = "CopyHTML";
            this.CopyHTML.Size = new System.Drawing.Size(188, 31);
            this.CopyHTML.TabIndex = 2;
            this.CopyHTML.Text = "Copy HTML";
            this.CopyHTML.UseVisualStyleBackColor = true;
            this.CopyHTML.Click += new System.EventHandler(this.CopyHTMLClick);
            // 
            // CopyDirectLink
            // 
            this.CopyDirectLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyDirectLink.Location = new System.Drawing.Point(14, 91);
            this.CopyDirectLink.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CopyDirectLink.Name = "CopyDirectLink";
            this.CopyDirectLink.Size = new System.Drawing.Size(188, 56);
            this.CopyDirectLink.TabIndex = 0;
            this.CopyDirectLink.Text = "Copy direct link";
            this.CopyDirectLink.UseVisualStyleBackColor = true;
            this.CopyDirectLink.Click += new System.EventHandler(this.CopyDirectLinkClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.Controls.Add(this.CloseLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 155);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 29);
            this.panel1.TabIndex = 3;
            // 
            // CloseLabel
            // 
            this.CloseLabel.AutoSize = true;
            this.CloseLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.CloseLabel.Location = new System.Drawing.Point(86, 7);
            this.CloseLabel.Name = "CloseLabel";
            this.CloseLabel.Size = new System.Drawing.Size(36, 15);
            this.CloseLabel.TabIndex = 3;
            this.CloseLabel.TabStop = true;
            this.CloseLabel.Text = "Close";
            this.CloseLabel.Click += new System.EventHandler(this.CloseLabel_Click);
            // 
            // UploadResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(214, 184);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CopyDirectLink);
            this.Controls.Add(this.CopyHTML);
            this.Controls.Add(this.CopyMarkdown);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(230, 200);
            this.MinimumSize = new System.Drawing.Size(230, 200);
            this.Name = "UploadResultForm";
            this.Load += new System.EventHandler(this.FormLoad);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CopyMarkdown;
        private System.Windows.Forms.Button CopyHTML;
        private System.Windows.Forms.Button CopyDirectLink;
        private System.Windows.Forms.Panel panel1;
        private ExplorerLinkLabel CloseLabel;
    }
}
