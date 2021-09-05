using System.Diagnostics;

namespace HolzShots.Windows.Forms
{
    public static class FirstStartDialog
    {
        public static FirstStartAction ShowDialog(System.Drawing.Bitmap icon)
        {
            var close = TaskDialogButton.Close;
            var openSettings = new TaskDialogButton("Open settings.json");
            var openPlugins = new TaskDialogButton("Open plugin page");

            var welcomeText = string.Join("\n",
                "Happy to see you! Let us get started right away.",
                "",
                "We've already created a settings file for you. You should check it out!",
                "Also, if you've got some plugins at your disposal, you can open the plugins folder.",
                "",
                "If you choose to open your settings.json, keep in mind that we'll use your default JSON editor for that.",
                "",
                "You can always leave feedback via the tray menu."
            );

            var page = new TaskDialogPage()
            {
                AllowMinimize = false,
                Icon = new TaskDialogIcon(icon),

                Caption = "",
                Heading = "Welcome to HolzShots",
                Text = welcomeText,

                AllowCancel = false,
                Buttons = new TaskDialogButtonCollection()
                {
                    openSettings,
                    openPlugins,
                    close,
                },
                DefaultButton = openSettings,
            };

            var pressedButton = TaskDialog.ShowDialog(page, TaskDialogStartupLocation.CenterScreen);

            if (pressedButton == close)
                return FirstStartAction.None;
            if (pressedButton == openPlugins)
                return FirstStartAction.OpenPlugins;
            if (pressedButton == openSettings)
                return FirstStartAction.OpenSettings;

            Debug.Fail("Unhandled pressed button. You most likely forgot to add an if state handing that button.");
            return FirstStartAction.None;
        }
    }

    public enum FirstStartAction
    {
        None,
        OpenSettings,
        OpenPlugins,
    }
}
