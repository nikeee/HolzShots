Imports System.Collections.Immutable
Imports System.Linq
Imports HolzShots.Input
Imports HolzShots.Input.Actions
Imports HolzShots.Interop
Imports HolzShots.My
Imports HolzShots.Composition.Command

Namespace UI.Specialized
    Friend Class MainWindow

        Public Shared ReadOnly Property Instance As MainWindow = New MainWindow()

        Private _forceClose As Boolean = False

        Private _keyboardHook As KeyboardHook
        Private _actionContainer As HolzShotsActionCollection
        Public Shared ReadOnly CommandManager As CommandManager = New CommandManager()


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
            If _forceClose = False Then
                e.Cancel = True
                HideForm()
            Else
                e.Cancel = False
                TrayIcon.Visible = False
            End If
        End Sub

        Private Sub SettingsUpdated(sender As Object, newSettings As HSSettings)
            _actionContainer?.Dispose()

            ' TODO: Issue toaster notifiction that the settings have been updated

            Dim parsedBindings = newSettings.KeyBindings.Select(AddressOf CommandManager.GetHotkeyActionFromKeyBinding).ToArray()

            _actionContainer = New HolzShotsActionCollection(_keyboardHook, parsedBindings)
            _actionContainer.Refresh()
        End Sub

        Private Sub RegisterCommands()
            ' TODO: This looks like it could be integrated in our plugin system
            CommandManager.RegisterCommand(New SelectAreaCommand())
            CommandManager.RegisterCommand(New FullscreenCommand())
            CommandManager.RegisterCommand(New WindowCommand())
            CommandManager.RegisterCommand(New OpenImagesFolderCommand())
            CommandManager.RegisterCommand(New OpenSettingsJsonCommand())
            CommandManager.RegisterCommand(New UploadImageCommand())
            CommandManager.RegisterCommand(New EditImageCommand())
        End Sub

        Private Async Sub MainWindowLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            HideForm()

            HolzShotsEnvironment.CurrentStartupManager.FixWorkingDirectory()

            Drawing.DpiAwarenessFix.SetDpiAwareness()

            _keyboardHook = KeyboardHookSelector.CreateHookForCurrentPlatform(Me)
            RegisterCommands()

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
            _forceClose = True
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
        Private Shared Sub OpenAbout()
            If Not AboutForm.IsAboutInstanciated Then
                Dim newAboutForm As New AboutForm()
                newAboutForm.Show()
            End If
        End Sub

#End Region
#Region "UI Events"

        Private Async Sub TrayIconMouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TrayIcon.MouseDoubleClick
            Dim commandToRun = UserSettings.Current.TrayIconDoubleClickCommand

            If commandToRun IsNot Nothing AndAlso CommandManager.IsRegisteredCommand(commandToRun) Then

                Debug.WriteLine($"Executing command from tray icon double click: {commandToRun}")
                Await CommandManager.Dispatch(commandToRun).ConfigureAwait(True) ' Can swallow exceptions
                Debug.WriteLine($"Done with: {commandToRun}")
            End If
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

        Private Async Sub SettingsJsonMenuItemClick(sender As Object, e As EventArgs) Handles settingsJsonMenuItem.Click
            Await CommandManager.Dispatch(Of OpenSettingsJsonCommand)().ConfigureAwait(True)
        End Sub

        Private Async Sub SelectAreaMenuItemClick(sender As Object, e As EventArgs) Handles selectAreaMenuItem.Click
            Await CommandManager.Dispatch(Of SelectAreaCommand)().ConfigureAwait(True)
        End Sub

        Private Async Sub OpenImageMenuItemClick(sender As Object, e As EventArgs) Handles openImageMenuItem.Click
            Await CommandManager.Dispatch(Of EditImageCommand)().ConfigureAwait(True)
        End Sub

        Private Async Sub UploadImageMenuItemClick(sender As Object, e As EventArgs) Handles uploadImageMenuItem.Click
            Await CommandManager.Dispatch(Of UploadImageCommand)().ConfigureAwait(True)
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
