
namespace HolzShots.New
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExtrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FeedbackAndIssuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.UploadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.StartWithWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenSettingsjsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayMenu
            // 
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExtrasToolStripMenuItem,
            this.ToolStripSeparator2,
            this.UploadImageToolStripMenuItem,
            this.OpenImageToolStripMenuItem,
            this.SelectAreaToolStripMenuItem,
            this.ToolStripSeparator3,
            this.StartWithWindowsToolStripMenuItem,
            this.PluginsToolStripMenuItem,
            this.OpenSettingsjsonToolStripMenuItem,
            this.ToolStripSeparator1,
            this.ExitToolStripMenuItem});
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.trayMenu.Size = new System.Drawing.Size(177, 198);
            // 
            // ExtrasToolStripMenuItem
            // 
            this.ExtrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem,
            this.FeedbackAndIssuesToolStripMenuItem});
            this.ExtrasToolStripMenuItem.Name = "ExtrasToolStripMenuItem";
            this.ExtrasToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.ExtrasToolStripMenuItem.Text = "Extras";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.OpenAbout);
            // 
            // FeedbackAndIssuesToolStripMenuItem
            // 
            this.FeedbackAndIssuesToolStripMenuItem.Name = "FeedbackAndIssuesToolStripMenuItem";
            this.FeedbackAndIssuesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.FeedbackAndIssuesToolStripMenuItem.Text = "Feedback and Issues";
            this.FeedbackAndIssuesToolStripMenuItem.Click += new System.EventHandler(this.OpenFeedbackAndIssues);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(173, 6);
            // 
            // UploadImageToolStripMenuItem
            // 
            this.UploadImageToolStripMenuItem.Name = "UploadImageToolStripMenuItem";
            this.UploadImageToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.UploadImageToolStripMenuItem.Text = "Upload Image";
            this.UploadImageToolStripMenuItem.Click += new System.EventHandler(this.UploadImage);
            // 
            // OpenImageToolStripMenuItem
            // 
            this.OpenImageToolStripMenuItem.Name = "OpenImageToolStripMenuItem";
            this.OpenImageToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.OpenImageToolStripMenuItem.Text = "Open Image";
            this.OpenImageToolStripMenuItem.Click += new System.EventHandler(this.OpenImage);
            // 
            // SelectAreaToolStripMenuItem
            // 
            this.SelectAreaToolStripMenuItem.Name = "SelectAreaToolStripMenuItem";
            this.SelectAreaToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.SelectAreaToolStripMenuItem.Text = "Select Area";
            this.SelectAreaToolStripMenuItem.Click += new System.EventHandler(this.SelectArea);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(173, 6);
            // 
            // StartWithWindowsToolStripMenuItem
            // 
            this.StartWithWindowsToolStripMenuItem.Name = "StartWithWindowsToolStripMenuItem";
            this.StartWithWindowsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.StartWithWindowsToolStripMenuItem.Text = "Start with Windows";
            this.StartWithWindowsToolStripMenuItem.Click += new System.EventHandler(this.StartWithWindows);
            // 
            // PluginsToolStripMenuItem
            // 
            this.PluginsToolStripMenuItem.Name = "PluginsToolStripMenuItem";
            this.PluginsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.PluginsToolStripMenuItem.Text = "Plugins";
            this.PluginsToolStripMenuItem.Click += new System.EventHandler(this.OpenPlugins);
            // 
            // OpenSettingsjsonToolStripMenuItem
            // 
            this.OpenSettingsjsonToolStripMenuItem.Name = "OpenSettingsjsonToolStripMenuItem";
            this.OpenSettingsjsonToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.OpenSettingsjsonToolStripMenuItem.Text = "Open settings.json";
            this.OpenSettingsjsonToolStripMenuItem.Click += new System.EventHandler(this.OpenSettingsJson);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitApplication);
            // 
            // TrayIcon
            // 
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "HolzShots";
            this.TrayIcon.Visible = true;
            this.TrayIcon.DoubleClick += new System.EventHandler(this.TriggerTrayIconDoubleClickCommand);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(106, 32);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Opacity = 0D;
            this.Text = "Form1";
            this.trayMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ContextMenuStrip trayMenu;
        internal System.Windows.Forms.ToolStripMenuItem ExtrasToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem FeedbackAndIssuesToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem UploadImageToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem OpenImageToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem SelectAreaToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripMenuItem StartWithWindowsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem PluginsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem OpenSettingsjsonToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        internal System.Windows.Forms.NotifyIcon TrayIcon;
    }
}

