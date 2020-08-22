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

            If sender IsNot Me Then
                ' If sender is MainWindow, the function was invoke on application startup
                ' We only want to show this message when the user edits this file
                HumanInterop.SettingsUpdated()
            End If

            Dim parsedBindings = newSettings.KeyBindings.Select(AddressOf CommandManager.GetHotkeyActionFromKeyBinding).ToArray()

            _actionContainer = New HolzShotsActionCollection(_keyboardHook, parsedBindings)

            Try
                _actionContainer.Refresh()
            Catch ex As AggregateException
                Dim registrationExceptions = ex.InnerExceptions.OfType(Of HotkeyRegistrationException)
                HumanInterop.ErrorRegisteringHotkeys(registrationExceptions)
            End Try

        End Sub

        Private Shared Sub RegisterCommands()
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

            Await UserSettings.Load(Me).ConfigureAwait(True) ' We're dealing with UI code here, we want to keep the context

            SettingsUpdated(Me, UserSettings.Current)
            AddHandler UserSettings.Manager.OnSettingsUpdated, AddressOf SettingsUpdated

            Dim isAutorun = HolzShotsEnvironment.CurrentStartupManager.IsStartedUp
            Dim args = HolzShotsEnvironment.CurrentStartupManager.CommandLineArguments

            StartWithWindows.Checked = HolzShotsEnvironment.CurrentStartupManager.IsRegistered

            Await MyApplication.ProcessCommandLineArguments(args).ConfigureAwait(True)

            Dim saveSettings As Boolean = False
            Dim openSettingsOnFinish As Boolean = False

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
                ' We have to dispose here
                ' If we wouldn't do it here, the finalizer of the MainWindow would do it
                ' Then, the handle of the MainWindow is already destroyed -> InvokeRequired in InvokeWrapper returns false
                ' (see: https://stackoverflow.com/a/4014468)
                ' This throws an exception that the hotkey cannot be unregistered because it was not registered.
                ' It was registered, but on a different thread.
                ' Because InvokeRequired returns false, UnregisterHotkeyInternal is effectively called in the GC/Finalizer thread.
                _actionContainer?.Dispose()

                ' Defensive copy of Application.OpenForm
                Dim forms = New List(Of Form)(Application.OpenForms.Cast(Of Form)())
                For Each f In forms
                    If f IsNot Me Then
                        f.Close()
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

            If CommandManager.IsRegisteredCommand(commandToRun.CommandName) Then
                Await CommandManager.Dispatch(commandToRun.CommandName, commandToRun.Parameters).ConfigureAwait(True) ' Can throw exceptions and silently kill the application
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

        Private Sub StartWithWindowsClick(sender As Object, e As EventArgs) Handles StartWithWindows.Click
            If HolzShotsEnvironment.CurrentStartupManager.IsRegistered Then
                HolzShotsEnvironment.CurrentStartupManager.Unregister()
            Else
                HolzShotsEnvironment.CurrentStartupManager.Register()
            End If
            StartWithWindows.Checked = HolzShotsEnvironment.CurrentStartupManager.IsRegistered
        End Sub

#End Region
    End Class
End Namespace
