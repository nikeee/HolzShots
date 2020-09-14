Imports System.Linq
Imports HolzShots.Input
Imports HolzShots.Input.Actions
Imports HolzShots.Interop
Imports HolzShots.My
Imports HolzShots.Composition.Command
Imports HolzShots.Windows.Forms

Namespace UI.Specialized
    Friend Class MainWindow

        Public Shared ReadOnly Property Instance As MainWindow = New MainWindow()
        Private Shared _commandManager As CommandManager(Of HSSettings)
        Public Shared Property CommandManager As CommandManager(Of HSSettings)
            Get
                Return _commandManager
            End Get
            Private Set(value As CommandManager(Of HSSettings))
                _commandManager = value
            End Set
        End Property


        Private _forceClose As Boolean = False

        Private _keyboardHook As KeyboardHook
        Private _actionContainer As HolzShotsActionCollection
        Private Shared _applicationStarted As DateTime

        Private Sub HideForm() Handles Me.Shown
            Opacity = 0
            Visible = False
            ShowInTaskbar = False
            Hide()
        End Sub

#Region "Form Events"

        Private Sub New()
            InitializeComponent()
            TrayIcon.ContextMenuStrip = trayMenu
            trayMenu.Renderer = EnvironmentEx.GetToolStripRendererForCurrentTheme()
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

            Trace.WriteLine("Updated settings")

            If DateTime.Now - _applicationStarted > TimeSpan.FromSeconds(2) Then
                ' If _settingsUpdates is 0, the function was invoke on application startup
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
            CommandManager.RegisterCommand(New CaptureClipboardCommand())
            CommandManager.RegisterCommand(New WindowCommand())
            CommandManager.RegisterCommand(New OpenImagesFolderCommand())
            CommandManager.RegisterCommand(New OpenSettingsJsonCommand())
            CommandManager.RegisterCommand(New UploadImageCommand())
            CommandManager.RegisterCommand(New EditImageCommand())
        End Sub

        Private Async Sub MainWindowLoad() Handles MyBase.Load
            HideForm()

            EnvironmentEx.CurrentStartupManager.FixWorkingDirectory()

            Drawing.DpiAwarenessFix.SetDpiAwareness()

            _keyboardHook = KeyboardHookSelector.CreateHookForCurrentPlatform(Me)

            Await UserSettings.Load(Me).ConfigureAwait(True) ' We're dealing with UI code here, we want to keep the context

            CommandManager = New CommandManager(Of HSSettings)(UserSettings.Manager)

            _applicationStarted = DateTime.Now
            ' TODO: Check if we need this:
            SettingsUpdated(Me, UserSettings.Current)
            AddHandler UserSettings.Manager.OnSettingsUpdated, AddressOf SettingsUpdated

            RegisterCommands()

            Dim isAutorun = EnvironmentEx.CurrentStartupManager.IsStartedUp
            Dim args = EnvironmentEx.CurrentStartupManager.CommandLineArguments

            StartWithWindowsToolStripMenuItem.Checked = EnvironmentEx.CurrentStartupManager.IsRegistered

            Await MyApplication.ProcessCommandLineArguments(args).ConfigureAwait(True)

            Dim saveSettings As Boolean = False
            Dim openSettingsOnFinish As Boolean = False

            If Not isAutorun AndAlso Global.HolzShots.My.Settings.IsFirstRun Then
                Global.HolzShots.My.Settings.IsFirstRun = False
                openSettingsOnFinish = True
                saveSettings = True
            End If

            If saveSettings Then Global.HolzShots.My.Settings.Save()
            If openSettingsOnFinish Then OpenPlugins()
        End Sub

#End Region

        Private Sub ExitApplication() Handles ExitToolStripMenuItem.Click
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

        Private Shared Sub OpenPlugins() Handles PluginsToolStripMenuItem.Click
            Debug.Assert(My.Application.Uploaders.Loaded)

            Dim pluginsModel = New PluginFormModel(
                My.Application.Uploaders.GetMetadata(),
                My.Application.Uploaders.Plugins.PluginDirectory
            )

            Dim form = New PluginForm(pluginsModel)
            form.Show()
        End Sub
        Private Shared Sub OpenAbout() Handles AboutToolStripMenuItem.Click
            AboutForm.Instance.Show()
        End Sub
        Private Shared Async Sub TriggerTrayIconDoubleClickCommand() Handles TrayIcon.MouseDoubleClick
            Dim commandToRun = UserSettings.Current.TrayIconDoubleClickCommand

            If commandToRun Is Nothing Then Return

            If CommandManager.IsRegisteredCommand(commandToRun.CommandName) Then
                Await CommandManager.Dispatch(commandToRun, UserSettings.Current).ConfigureAwait(True) ' Can throw exceptions and silently kill the application
            End If
        End Sub
        Private Shared Async Sub OpenSettingsJson() Handles OpenSettingsjsonToolStripMenuItem.Click
            Await CommandManager.Dispatch(Of OpenSettingsJsonCommand)(UserSettings.Current).ConfigureAwait(True)
        End Sub
        Private Shared Async Sub SelectArea() Handles SelectAreaToolStripMenuItem.Click
            Await CommandManager.Dispatch(Of SelectAreaCommand)(UserSettings.Current).ConfigureAwait(True)
        End Sub
        Private Shared Async Sub OpenImage() Handles OpenImageToolStripMenuItem.Click
            Await CommandManager.Dispatch(Of EditImageCommand)(UserSettings.Current).ConfigureAwait(True)
        End Sub
        Private Shared Async Sub UploadImage() Handles UploadImageToolStripMenuItem.Click
            Await CommandManager.Dispatch(Of UploadImageCommand)(UserSettings.Current).ConfigureAwait(True)
        End Sub
        Private Shared Sub OpenIssues() Handles FeedbackAndIssuesToolStripMenuItem.Click
            IO.HolzShotsPaths.OpenLink(LibraryInformation.IssuesUrl)
        End Sub
        Private Sub ToogleStartWithWindows() Handles StartWithWindowsToolStripMenuItem.Click
            If EnvironmentEx.CurrentStartupManager.IsRegistered Then
                EnvironmentEx.CurrentStartupManager.Unregister()
            Else
                EnvironmentEx.CurrentStartupManager.Register()
            End If
            StartWithWindowsToolStripMenuItem.Checked = EnvironmentEx.CurrentStartupManager.IsRegistered
        End Sub
    End Class
End Namespace
