Imports System.Windows.Forms.VisualStyles
Imports HolzShots.Composition
Imports HolzShots.Interop

Namespace UI.Controls
    Public Class PluginInfoItem

        Private Shared ReadOnly HotRenderer As New VisualStyleRenderer(VisualStyleElement.CreateElement("LISTVIEW", 6, 6))
        Private Shared ReadOnly SeperatorPen As New Pen(Color.FromArgb(255, &HCC, &HCC, &HCC))
        Private Const BorderPadding As Integer = 5
        Private Const HotMargin As Integer = BorderPadding

        Private ReadOnly _pluginMetadata As IPluginMetadata

        Sub New(pluginMetadata As IPluginMetadata)
            If pluginMetadata Is Nothing Then Throw New ArgumentNullException(NameOf(pluginMetadata))
            Debug.Assert(Not String.IsNullOrWhiteSpace(pluginMetadata.Author))
            Debug.Assert(Not String.IsNullOrWhiteSpace(pluginMetadata.Name))

            _pluginMetadata = pluginMetadata
            InitializeComponent()
            SetupUiStuff()
        End Sub

        Private Sub SetupUiStuff()
            pluginNameLabel.Text = _pluginMetadata.Name
            pluginAuthor.Text = _pluginMetadata.Author
            pluginVersion.Text = _pluginMetadata.Version.ToString()

            authorWebSite.Enabled = Not String.IsNullOrWhiteSpace(_pluginMetadata.Url)
            reportBug.Enabled = Not String.IsNullOrWhiteSpace(_pluginMetadata.BugsUrl)

            ' TODO: Fix Plugin Settings
            ' pluginSettings.Visible = (_pluginMetadata.SettingsMode And SettingsModes.ShowInPluginList) = SettingsModes.ShowInPluginList
            pluginSettings.Visible = False
        End Sub

#Region "Invalidation"

        Private _isHot As Boolean
        Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
            UpdateStatus()
            MyBase.OnMouseEnter(e)
        End Sub
        Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
            UpdateStatus()
            MyBase.OnMouseLeave(e)
        End Sub
        Private Sub UpdateStatus()
            _isHot = ClientRectangle.Contains(PointToClient(MousePosition))
            Invalidate(True)
        End Sub
#End Region
#Region "Painting"

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            If _isHot Then
                HotRenderer.DrawBackground(e.Graphics, New Rectangle(ClientRectangle.X + HotMargin, ClientRectangle.Y, ClientRectangle.Width - HotMargin * 2, ClientRectangle.Height - 1))
            End If
            e.Graphics.DrawLine(SeperatorPen, New Point(0 + BorderPadding, Height - 1), New Point(Width - 1 - BorderPadding, Height - 1))
        End Sub

#End Region

        Private Sub PluginSettingsLinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles pluginSettings.LinkClicked
            ' TODO: Fix Plugin Settings
            'If _pluginMetadata.SettingsMode <> SettingsModes.NoSettings Then
            '    Try
            '        Dim dialog = _pluginMetadata.SettingsDialog
            '        If dialog IsNot Nothing Then
            '            dialog.ShowDialog(Me)
            '        End If
            '    Catch ex As Exception
            '        HumanInterop.ErrorWhileOpeningSettingsDialog(ex)
            '    End Try
            'End If
        End Sub

        Private Sub AuthorWebSiteLinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles authorWebSite.LinkClicked
            If Not String.IsNullOrWhiteSpace(_pluginMetadata.Url) Then
                _pluginMetadata.Url.SafeProcessStart()
            End If
        End Sub

        Private Sub ReportBugLinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles reportBug.LinkClicked
            If Not String.IsNullOrWhiteSpace(_pluginMetadata.BugsUrl) Then
                _pluginMetadata.BugsUrl.SafeProcessStart()
            End If
        End Sub
    End Class
End Namespace
