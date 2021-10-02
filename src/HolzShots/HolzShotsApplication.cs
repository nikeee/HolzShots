using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using HolzShots.Composition;
using HolzShots.Composition.Command;
using HolzShots.Input.Actions;
using HolzShots.IO;
using HolzShots.Windows.Forms;
using SingleInstanceCore;

namespace HolzShots
{
    public class HolzShotsApplication : ISingleInstance
    {
        private static HolzShotsApplication _instance = null!;
        public static HolzShotsApplication Instance => _instance ??= new HolzShotsApplication();

        private UploaderManager _uploaders = null!;
        public UploaderManager Uploaders
        {
            get
            {
                Debug.Assert(_uploaders != null);
                Debug.Assert(_uploaders.Loaded);
                return _uploaders;
            }
        }

        public CommandManager<HSSettings> CommandManager { get; private set; } = null!;

        private MainForm _form = null!;
        private HolzShotsApplication() { }

        public async Task LoadPlugins()
        {
            Debug.Assert(_uploaders == null);

            var plugins = new PluginUploaderSource(HolzShotsPaths.PluginDirectory);
            var customs = new CustomUploaderSource(HolzShotsPaths.CustomUploadersDirectory);

            _uploaders = new UploaderManager(plugins, customs);

            try
            {
                await _uploaders.Load().ConfigureAwait(false);
            }
            catch (PluginLoadingFailedException ex)
            {
                NotificationManager.PluginLoadingFailed(ex);
                Debugger.Break();
            }
        }

        public void InitializeCommands(SettingsManager<HSSettings> settingsManager)
        {
            CommandManager = new CommandManager<HSSettings>(UserSettings.Manager);
            // TODO: This looks like it could be integrated in our plugin system
            CommandManager.RegisterCommand(new SelectAreaCommand());
            CommandManager.RegisterCommand(new FullscreenCommand());
            CommandManager.RegisterCommand(new CaptureClipboardCommand());
            CommandManager.RegisterCommand(new WindowCommand());
            CommandManager.RegisterCommand(new OpenImagesFolderCommand());
            CommandManager.RegisterCommand(new OpenSettingsJsonCommand());
            CommandManager.RegisterCommand(new UploadImageCommand());
            CommandManager.RegisterCommand(new EditImageCommand());
            CommandManager.RegisterCommand(new CaptureVideoCommand());
        }

        internal async Task ProcessCommandLineArguments(string[] args)
        {
#if DEBUG
            System.Diagnostics.Trace.WriteLine($"Processing: {string.Join(", ", args)}");
#endif
            // TODO: Proper command line parsing, GH#60
            for (int i = 0; i <= args.Length - 1; i++)
            {
                switch (args[i])
                {
                    case CommandLine.FullscreenScreenshotCliCommand:
                        await CommandManager.Dispatch<FullscreenCommand>(UserSettings.Current).ConfigureAwait(true);
                        break;
                    case CommandLine.AreaSelectorCliCommand:
                        await CommandManager.Dispatch<SelectAreaCommand>(UserSettings.Current).ConfigureAwait(true);
                        break;
                    case CommandLine.UploadImageCliCommand:
                        {
                            // TODO: Maybe we can support overriding settings from the command line, too
                            var parameters = new Dictionary<string, string>();
                            if (i < args.Length - 1)
                                parameters[FileDependentCommand.FileNameParameter] = args[i + 1];

                            await CommandManager.Dispatch<UploadImageCommand>(UserSettings.Current, parameters).ConfigureAwait(true);
                            break;
                        }
                    case CommandLine.OpenImageCliCommand:
                        {
                            // TODO: Maybe we can support overriding settings from the command line, too
                            var parameters = new Dictionary<string, string>();
                            if (i < args.Length - 1)
                                parameters[FileDependentCommand.FileNameParameter] = args[i + 1];

                            await CommandManager.Dispatch<EditImageCommand>(UserSettings.Current, parameters).ConfigureAwait(true);
                            break;
                        }
                }
            }
        }


        public void Run()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(_form = new MainForm(this));
        }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        public void OnInstanceInvoked(string[] args) => ProcessCommandLineArguments(args); // Not awaiting this Task, swallowing exceptions
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }
}
