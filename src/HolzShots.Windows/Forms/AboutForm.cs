using System.Text;
using System.Windows.Forms;

namespace HolzShots.Windows.Forms;

public partial class AboutForm : Form
{
    private static Lazy<AboutForm> _instance = new(() => new AboutForm());
    public static AboutForm Instance => _instance.Value;

    public AboutForm()
    {
        InitializeComponent();

        VersionLabel.Text = $"{Application.ProductVersion} ({LibraryInformation.VersionDate.ToShortDateString()})";
        ApplicationTitleLabel.Text = LibraryInformation.Name;
    }

    private void LicenseLabel_LinkClicked(object sender, EventArgs e) => IO.HolzShotsPaths.OpenLink(LibraryInformation.LicenseUrl);
    private void HolzShotsLinkLabel_LinkClicked(object sender, EventArgs e) => IO.HolzShotsPaths.OpenLink(LibraryInformation.SiteUrl);
    private void SendFeedbackLink_LinkClicked(object sender, EventArgs e) => IO.HolzShotsPaths.OpenLink(LibraryInformation.IssuesUrl);

    private void ShowGfxResourcesLinklabel_LinkClicked(object sender, EventArgs e)
    {
        const string title = "About Graphics";
        MessageBox.Show(this, AboutDialog.GetGraphicsText(), title);
    }

    private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        _instance = new Lazy<AboutForm>(() => new AboutForm());
    }
}

public static class AboutDialog
{
    public static void Show(System.Drawing.Bitmap icon, string version)
    {
        var page = new TaskDialogPage()
        {
            AllowMinimize = true,
            Icon = new TaskDialogIcon(icon),
            Caption = "About " + LibraryInformation.Name,
            Heading = LibraryInformation.Name,
            Text = $"Open Source, {ThisAssembly.Constants.SpdxLicense} licensed screenshot utility that gets out of your way.\n",
            /*
               <a href="website">Website</a>
               <a href="issues">Report Issue</a>
               <a href="license">License</a>
            */
            AllowCancel = false,
            Expander = new TaskDialogExpander()
            {
                Text = "Images/Icons used:\n" + GetGraphicsText(),
            },
            Footnote = new TaskDialogFootnote()
            {
                Text = $"Version: {version}",
            },
            Buttons = [
                TaskDialogButton.Close,
            ],
        };

        // TODO: Maybe check if an update is available?
        /*
        System.Threading.Tasks.Task.Run(async () =>
        {
            await System.Threading.Tasks.Task.Delay(5000);
            page.Footnote.Text += " Update available!";
        });
        */

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
