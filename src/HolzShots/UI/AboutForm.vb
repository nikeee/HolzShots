Imports System.Text
Imports HolzShots.Common
Imports HolzShots.Interop
Imports Microsoft.WindowsAPICodePack.Dialogs

Namespace UI
    Friend Class AboutForm
        Public Shared Property AsIsAboutInstanciated As Boolean
        Private Const YoutubePlaylistPage As String = "https://holz.nu/tunnel"

        Public Sub New()
            InitializeComponent()
            AsIsAboutInstanciated = True
        End Sub

        Private Sub AboutFormFormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
            AsIsAboutInstanciated = False
        End Sub

        Private Sub AboutFormNewLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            versionLabel.Text = $"{Application.ProductVersion} ({LibraryInformation.VersionDate.ToShortDateString()})"
            timestampLabel.Text = $"2010-{DateTime.Now.Year} {LibraryInformation.PublisherName}"
        End Sub

        Private Shared Sub HolzShotsLinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles holzShotsLinkLabel.LinkClicked
            LibraryInformation.SiteUrl.SafeProcessStart()
        End Sub

        Private Shared Sub YoutubePLaylistLinkLabelLinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles youtubePlaylistLinkLabel.LinkClicked
            YoutubePlaylistPage.SafeProcessStart()
        End Sub

        Private Sub ShowGfxResourcesLinklabelLinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles showGfxResourcesLinklabel.LinkClicked
            ShowGfx(Me)
        End Sub

        Private Shared Sub ShowGfx(parent As IWin32Window)
            Const title = "Über die Grafiken..."

            Dim sb As New StringBuilder()
            sb.AppendLine("Filzstift-Icons von:")
            sb.AppendLine("Everaldo Coelho - www.everaldo.com").AppendLine()
            sb.AppendLine("Info- und Kalender-Icon von:")
            sb.AppendLine("Visual Pharm - www.visualpharm.com").AppendLine()
            sb.AppendLine("Lizenzfreie/Restliche Icons:")
            sb.AppendLine("www.iconfinder.com und VS2015ImageLibrary")

            If Not TaskDialog.IsPlatformSupported Then
                MessageBox.Show(parent, sb.ToString(), title)
                Return
            End If

            Using td As New TaskDialog()
                td.OwnerWindowHandle = If(parent?.Handle, IntPtr.Zero)
                td.Icon = TaskDialogStandardIcon.Information
                td.Caption = title
                td.InstructionText = "Grafik-Ressourcen von HolzShots"
                td.Text = sb.ToString()
                td.StandardButtons = TaskDialogStandardButtons.Close
                td.Show()
            End Using
        End Sub
    End Class
End Namespace
