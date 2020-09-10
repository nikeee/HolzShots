using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace HolzShots.Windows.Forms
{
    public partial class AboutForm : Form
    {
        public static Lazy<AboutForm> _instance = new Lazy<AboutForm>(() => new AboutForm());
        public static AboutForm Instance => _instance.Value;

        public AboutForm()
        {
            InitializeComponent();

            VersionLabel.Text = $"{Application.ProductVersion} ({LibraryInformation.VersionDate.ToShortDateString()})";
            ApplicationTitleLabel.Text = LibraryInformation.Name;
        }

        private void LicenseLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => IO.HolzShotsPaths.OpenLink(LibraryInformation.LicenseUrl);
        private void HolzShotsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => IO.HolzShotsPaths.OpenLink(LibraryInformation.SiteUrl);
        private void SendFeedbackLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => IO.HolzShotsPaths.OpenLink(LibraryInformation.IssuesUrl);

        private void ShowGfxResourcesLinklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            const string title = "About Graphics";
            MessageBox.Show(this, AboutDialog.GetGraphicsText(), title);
        }
    }
    public static class AboutDialog
    {
        public static void ShowDialog(System.Drawing.Bitmap icon, string version)
        {
            var page = new TaskDialogPage()
            {
                AllowMinimize = true,
                Icon =  new TaskDialogIcon(icon),
                Caption = "About HolzShots",
                Heading = "HolzShots",
                Text = $"Open Source, GPL-3.0 licensed screenshot utility.\n",
                AllowCancel = false,
                Expander = new TaskDialogExpander()
                {
                    Text = "About graphics used:\n" + GetGraphicsText(),
                },
                Footnote = new TaskDialogFootnote()
                {
                    Text = $"Version: {version}",
                },
                Buttons = new TaskDialogButtonCollection()
                {
                    TaskDialogButton.Close,
                },
            };

            TaskDialog.ShowDialog(page, TaskDialogStartupLocation.CenterScreen);
        }

        internal static string GetGraphicsText()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Marker Icons by:");
            sb.AppendLine("Everaldo Coelho - www.everaldo.com").AppendLine();
            sb.AppendLine("Info icon by:");
            sb.AppendLine("Visual Pharm - www.visualpharm.com").AppendLine();
            sb.AppendLine("Free/remaining icons:");
            sb.AppendLine("www.iconfinder.com and VS2017ImageLibrary");
            return sb.ToString();
        }
    }

}
