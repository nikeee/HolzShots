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

        private void LicenseLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenLink(LibraryInformation.LicenseUrl);
        private void HolzShotsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenLink(LibraryInformation.SiteUrl);
        private void SendFeedbackLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenLink(LibraryInformation.IssuesUrl);

        private static void OpenLink(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                Trace.WriteLine("Could not open link: " + url);
            }
        }

        private void ShowGfxResourcesLinklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            const string title = "About Graphics";

            var sb = new StringBuilder();
            sb.AppendLine("Marker Icons by:");
            sb.AppendLine("Everaldo Coelho - www.everaldo.com").AppendLine();
            sb.AppendLine("Info icon by:");
            sb.AppendLine("Visual Pharm - www.visualpharm.com").AppendLine();
            sb.AppendLine("Free/remaining icons:");
            sb.AppendLine("www.iconfinder.com and VS2017ImageLibrary");

            MessageBox.Show(this, sb.ToString(), title);
        }
    }
}
