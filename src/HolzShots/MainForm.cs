#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using HolzShots.Input.Keyboard;
using HolzShots.Input.Actions;
using HolzShots.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace HolzShots
{
    public partial class MainForm : Form
    {
        public DateTime ApplicationStarted { get; private set; }

        private readonly HolzShotsApplication _application;
        private HolzShotsActionCollection _actionContainer = null!;
        private KeyboardHook _keyboardHook = null!;
        private bool _forceClose = false;

        public MainForm(HolzShotsApplication application)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));

            InitializeComponent();

            Text = "HolzShots";
            TrayIcon.ContextMenuStrip = trayMenu;
            trayMenu.Renderer = EnvironmentEx.ToolStripRendererForCurrentTheme;
            EnsureInvisibility();
        }

        private void EnsureInvisibility()
        {
            Opacity = 0.0f;
            Visible = false;
            ShowInTaskbar = false;
        }

        async Task InitializeWithAvailableWindowHandle()
        {
            // Quite some hack for making the main window invisible: https://stackoverflow.com/a/189045
            // SetVisibleCore did not work for us :(
            BeginInvoke(() => EnsureInvisibility());

            // This call is used to ensure that the working directory is the same as the directory of the .exe file
            // The Startupmanager needs this.
            // We also need this because we ship ffmpeg right out of the box, so we can assume that "ffmpeg.exe" just points to the version next to the executable
            EnvironmentEx.CurrentStartupManager.FixWorkingDirectory();

            _keyboardHook = KeyboardHookSelector.CreateHookForCurrentPlatform(this);

            await UserSettings.Load(this).ConfigureAwait(true); // We're dealing with UI code here, we want to keep the context

            _application.InitializeCommands(UserSettings.Manager);
            ApplicationStarted = DateTime.Now;

            // TODO: Check if we need this:
            SettingsUpdated(null, UserSettings.Current);
            UserSettings.Manager.OnSettingsUpdated += SettingsUpdated;

            var isAutorun = EnvironmentEx.CurrentStartupManager.IsStartedUp;
            var args = EnvironmentEx.CurrentStartupManager.CommandLineArguments;

            StartWithWindowsToolStripMenuItem.Checked = EnvironmentEx.CurrentStartupManager.IsRegistered;

            Properties.Settings.Default.Upgrade();

            await _application.LoadPlugins();

            if (JumpLists.AreSupported)
                JumpLists.RegisterTasks();

            await _application.ProcessCommandLineArguments(args).ConfigureAwait(true);

            var saveSettings = false;
            var openFirstStartExperience = false;

            if (!isAutorun && Properties.Settings.Default.IsFirstRun)
            {
                Properties.Settings.Default.IsFirstRun = false;
                openFirstStartExperience = true;
                saveSettings = true;
            }

            if (saveSettings)
                Properties.Settings.Default.Save();

            if (openFirstStartExperience)
                ShowFirstStartExperience();

            // Just here to force the JIT to load some dependencies that the AreaSelector uses (so the first invocation isnt slow)
            // Ref: https://stackoverflow.com/a/3747473
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(Input.Selection.AreaSelector).TypeHandle);
        }

        private void SettingsUpdated(object? sender, HSSettings newSettings)
        {
            _actionContainer?.Dispose();

            Trace.WriteLine("Updated settings");

            if (DateTime.Now - ApplicationStarted > TimeSpan.FromSeconds(2))
                // If _settingsUpdates is 0, the function was invoke on application startup
                // We only want to show this message when the user edits this file
                NotificationManager.SettingsUpdated();

            var parsedBindings = newSettings.KeyBindings.Select(_application.CommandManager.GetHotkeyActionFromKeyBinding).ToArray();

            _actionContainer = new HolzShotsActionCollection(_keyboardHook, parsedBindings);

            try
            {
                _actionContainer.Refresh();
            }
            catch (AggregateException ex)
            {
                var registrationExceptions = ex.InnerExceptions.OfType<HotkeyRegistrationException>();
                NotificationManager.ErrorRegisteringHotkeys(registrationExceptions);
            }
        }


        private void ShowFirstStartExperience()
        {
            // TODO: Proper resources
            using var icon = Properties.Resources.Logo_64x64;
            var action = FirstStartDialog.ShowDialog(icon);
            switch (action)
            {
                case FirstStartAction.OpenPlugins:
                    OpenPlugins(null, new EventArgs());
                    return;
                case FirstStartAction.OpenSettings:
                    _application.CommandManager.Dispatch<OpenSettingsJsonCommand>(UserSettings.Current);
                    return;
                case FirstStartAction.None: return; // Intentionally left empty
                default:
                    throw new UnreachableException($"Unhandled action: '{action}'");
            }
        }

        protected override void OnShown(EventArgs e) => EnsureInvisibility();

        protected override void OnLoad(EventArgs e) => InitializeWithAvailableWindowHandle();

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_forceClose)
            {
                e.Cancel = false;
                TrayIcon.Visible = false;
            }
            else
            {
                e.Cancel = true;

                // As the form was somehow attemted to close, we should make sure that it is at least _now_ closed
                EnsureInvisibility();
            }
        }

        #region Tray Menu Event Handlers

        private async void UploadFile(object sender, EventArgs e)
        {
            await _application.CommandManager.Dispatch<UploadFileCommand>(UserSettings.Current).ConfigureAwait(true);
        }
        private async void OpenImage(object sender, EventArgs e)
        {
            await _application.CommandManager.Dispatch<EditImageCommand>(UserSettings.Current).ConfigureAwait(true);
        }
        private async void SelectArea(object sender, EventArgs e)
        {
            await _application.CommandManager.Dispatch<CaptureSelectedAreaCommand>(UserSettings.Current).ConfigureAwait(true);
        }
        private async void StartScreenRecording(object sender, EventArgs e)
        {
            await _application.CommandManager.Dispatch<StartOrStopVideoCommand>(UserSettings.Current).ConfigureAwait(true);
        }
        private async void OpenSettingsJson(object sender, EventArgs e)
        {
            await _application.CommandManager.Dispatch<OpenSettingsJsonCommand>(UserSettings.Current).ConfigureAwait(true);
        }
        private async void UpdateCustomUploaders(object sender, EventArgs e)
        {
            await _application.CommandManager.Dispatch<UpdateUploaderSpecsCommand>(UserSettings.Current).ConfigureAwait(true);
        }

        private void OpenPlugins(object? sender, EventArgs e)
        {
            Debug.Assert(_application.Uploaders.Loaded);

            var pluginsModel = new PluginFormModel(
                _application.Uploaders.GetMetadata(),
                _application.Uploaders.Plugins.PluginDirectory
            );

            var form = new PluginForm(pluginsModel);
            form.Show();
        }
        private void StartWithWindows(object sender, EventArgs e)
        {
            if (EnvironmentEx.CurrentStartupManager.IsRegistered)
                EnvironmentEx.CurrentStartupManager.Unregister();
            else
                EnvironmentEx.CurrentStartupManager.Register();

            StartWithWindowsToolStripMenuItem.Checked = EnvironmentEx.CurrentStartupManager.IsRegistered;
        }
        private async void TriggerTrayIconDoubleClickCommand(object sender, EventArgs e)
        {
            var commandToRun = UserSettings.Current.TrayIconDoubleClickCommand;
            if (commandToRun is null)
                return;

            if (_application.CommandManager.IsRegisteredCommand(commandToRun.CommandName))
                await _application.CommandManager.Dispatch(commandToRun, UserSettings.Current).ConfigureAwait(true); // Can throw exceptions and silently kill the application
        }

        private void OpenAbout(object sender, EventArgs e) => AboutForm.Instance.Show();
        private void OpenFeedbackAndIssues(object sender, EventArgs e) => IO.HolzShotsPaths.OpenLink(LibraryInformation.IssuesUrl);

        private void ExitApplication(object? sender, EventArgs e)
        {
            _forceClose = true;

            // We have to dispose here
            // If we wouldn't do it here, the finalizer of the MainWindow would do it
            // Then, the handle of the MainWindow is already destroyed -> InvokeRequired in InvokeWrapper returns false
            // (see: https://stackoverflow.com/a/4014468)
            // This throws an exception that the hotkey cannot be unregistered because it was not registered.
            // It was registered, but on a different thread.
            // Because InvokeRequired returns false, UnregisterHotkeyInternal is effectively called in the GC/Finalizer thread.
            _actionContainer?.Dispose();

            // Defensive copy of Application.OpenForm
            var forms = new List<Form>(Application.OpenForms.Cast<Form>());

            try
            {
                foreach (var f in forms)
                    if (f != this)
                        f.Close();
            }
            finally
            {
                Application.Exit();
            }
        }

        public void Terminate() => ExitApplication(null, new EventArgs());

        #endregion
    }
}

#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
