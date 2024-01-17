using System.Windows.Forms;
using HolzShots.IO;

namespace HolzShots.Windows.Forms;

public static class AboutDialog
{
    public static void Show(System.Drawing.Bitmap icon)
    {
        var page = new TaskDialogPage()
        {
            EnableLinks = true,
            AllowMinimize = true,
            Icon = new TaskDialogIcon(icon),
            Caption = "About " + LibraryInformation.Name,
            Heading = LibraryInformation.Name,
            Text = $"""
            Open Source, <a href="{LibraryInformation.LicenseUrl}">{ThisAssembly.Constants.SpdxLicense}</a> licensed screenshot utility that gets out of your way.

            Visit our <a href="{LibraryInformation.SiteUrl}">website</a> or <a href="{LibraryInformation.IssuesUrl}">report an issue or send feedback</a>.
            """,
            AllowCancel = false,
            Expander = new TaskDialogExpander()
            {
                Text = """
                Images/Icons used:

                Marker Icons: Everaldo Coelho (<a href="https://everaldo.com">everaldo.com</a>)
                Info icon: Visual Pharm (<a href="https://visualpharm.com">visualpharm.com</a>)
                Free and remaining icons: <a href="https://iconfinder.com">iconfinder.com</a> and VS2017ImageLibrary
                """
            },
            Footnote = new TaskDialogFootnote()
            {
                Text = $"Version: {Application.ProductVersion} ({LibraryInformation.VersionDate.ToShortDateString()})",
            },
            Buttons = [
                TaskDialogButton.Close,
            ],
        };

        page.LinkClicked += (_, e) => HolzShotsPaths.OpenLink(e.LinkHref);

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
}
