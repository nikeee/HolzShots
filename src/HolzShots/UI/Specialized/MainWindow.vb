Imports HolzShots.Input
Imports HolzShots.Input.Actions
Imports HolzShots.Interop
Imports HolzShots.My
Imports HolzShots.ScreenshotRelated

Namespace UI.Specialized
    Friend Class MainWindow

        Public Shared ReadOnly Property Instance As MainWindow = New MainWindow()

        Private _forceclose As Boolean = False

        Private _keyboardHook As KeyboardHook
        Public ReadOnly Property ActionContainer As HotkeyActionCollection

        Private Sub HideForm()
            Opacity = 0
            Visible = False
            ShowInTaskbar = False
            Hide()
        End Sub

#Region "Form Events"

        Private Sub New()
            InitializeComponent()
            TrayIcon.ContextMenu = trayIconMenu
        End Sub

        Private Sub MainWindowFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
            If _forceclose = False Then
                e.Cancel = True
                HideForm()
            Else
                e.Cancel = False
                TrayIcon.Visible = False
            End If
        End Sub

        Private Async Sub MainWindowLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            HideForm()

            HolzShotsEnvironment.CurrentStartupManager.FixWorkingDirectory()

            Drawing.DpiAwarenessFix.SetDpiAwareness()

            _keyboardHook = KeyboardHookSelector.CreateHookForCurrentPlatform(Me)

            Await UserSettings.Load(Me)
            SettingsUpdated(Me, UserSettings.Current)
            AddHandler UserSettings.Manager.OnSettingsUpdated, AddressOf SettingsUpdated

            Global.HolzShots.My.Settings.Upgrade()

            Dim isAutorun = HolzShotsEnvironment.CurrentStartupManager.IsStartedUp
            Dim args = HolzShotsEnvironment.CurrentStartupManager.CommandLineArguments

            Await MyApplication.ProcessCommandLineArguments(args).ConfigureAwait(True)

            Dim saveSettings As Boolean = False
            Dim openSettingsOnFinish As Boolean = False

            If String.IsNullOrWhiteSpace(Global.HolzShots.My.Settings.DefaultImageHoster) Then
                Global.HolzShots.My.Settings.DefaultImageHoster = "DirectUpload.net"
                saveSettings = True
            End If
            If Not isAutorun AndAlso Global.HolzShots.My.Settings.IsFirstRun Then
                Global.HolzShots.My.Settings.IsFirstRun = False
                openSettingsOnFinish = True
                saveSettings = True
            End If

            If saveSettings Then Global.HolzShots.My.Settings.Save()
            If openSettingsOnFinish Then OpenSettings()
        End Sub

#End Region

        Private Sub ExitApplication()
            _forceclose = True
            Try
                Dim forms = Application.OpenForms
                For i As Integer = forms.Count - 1 To 0
                    If forms(i) IsNot Me Then
                        forms(i).Close()
                    End If
                Next
                System.Windows.Forms.Application.Exit()
            Catch ex As Exception
                End
            End Try
        End Sub

#Region "Open Windows"

        Private Shared Sub OpenSettings()
            If Not SettingsWindow.Instance.Visible Then
                SettingsWindow.Instance.ShowDialog() ' Showdialog necessary?
            End If
        End Sub
        Private Shared Sub OpenSettingsJson()
            UserSettings.OpenSettingsInDefaultEditor()
        End Sub
        Private Shared Sub OpenAbout()
            If Not AboutForm.IsAboutInstanciated Then
                Dim newAboutForm As New AboutForm()
                newAboutForm.Show()
            End If
        End Sub

#End Region
#Region "UI Events"

        Private Async Sub TrayIconMouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TrayIcon.MouseDoubleClick
            Select Case TrayIconDoubleClickAction ' TODO: Entry in settings dialog
                Case TrayIconAction.InvokeAreaSelection
                    Await DoSelector().ConfigureAwait(True) ' Can swallow exceptions
                Case TrayIconAction.OpenScreenshotFolder
                    LocalDisk.ScreenshotDumper.OpenPictureDumpFolderIfEnabled()
                Case TrayIconAction.OpenSettings
                    OpenSettings()
                Case TrayIconAction.[Nothing]
                    Trace.WriteLine("Doin' nothin'")
            End Select
        End Sub
        Private Sub MainWindowShown(sender As Object, e As EventArgs) Handles Me.Shown
            HideForm()
        End Sub

#End Region

#Region "Tray Menu Actions"

        Private Sub ExitMenuItemClick(sender As Object, e As EventArgs) Handles exitMenuItem.Click
            ExitApplication()
        End Sub

        Private Sub SettingsMenuItemClick(sender As Object, e As EventArgs) Handles settingsMenuItem.Click
            OpenSettings()
        End Sub

        Private Sub SettingsJsonMenuItemClick(sender As Object, e As EventArgs) Handles settingsJsonMenuItem.Click
            OpenSettingsJson()
        End Sub

        Private Async Sub SelectAreaMenuItemClick(sender As Object, e As EventArgs) Handles selectAreaMenuItem.Click
            Await ScreenshotInvoker.DoSelector().ConfigureAwait(True) ' can swallow exceptions
        End Sub

        Private Sub OpenImageMenuItemClick(sender As Object, e As EventArgs) Handles openImageMenuItem.Click
            ScreenshotInvoker.OpenSelectedImage()
        End Sub

        Private Sub UploadImageMenuItemClick(sender As Object, e As EventArgs) Handles uploadImageMenuItem.Click
            ScreenshotInvoker.UploadSelectedImage()
        End Sub

        Private Sub AboutMenuItemClick(sender As Object, e As EventArgs) Handles aboutMenuItem.Click
            OpenAbout()
        End Sub

        Private Sub FeedbackMenuItemClick(sender As Object, e As EventArgs) Handles feedbackMenuItem.Click
            LibraryInformation.IssuesUrl.SafeProcessStart()
        End Sub

#End Region
    End Class
End Namespace
