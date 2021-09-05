using System;
using System.Diagnostics;
using System.Windows.Forms;
using HolzShots.Windows.Forms;
using System.IO;
using HolzShots.IO;
using Microsoft.WindowsAPICodePack.Taskbar;
using Microsoft.WindowsAPICodePack.Shell;

namespace HolzShots.New
{
    static class JumpLists
    {
        public static bool AreSupported => TaskbarManager.IsPlatformSupported;

        public static void RegisterTasks()
        {
            Debug.Assert(TaskbarManager.IsPlatformSupported);

            if (Properties.Settings.Default.UserTasksInitialized)
                return;

            Properties.Settings.Default.UserTasksInitialized = true;
            Properties.Settings.Default.Save();

            var jumpList = JumpList.CreateJumpList();
            jumpList.ClearAllUserTasks();

            var imgres = Path.Combine(HolzShotsPaths.SystemPath, "imageres.dll");

            if (File.Exists(imgres))
            {
                var fullscreen = new JumpListLink(Application.ExecutablePath, "Capture entire screen")
                {
                    Arguments = CommandLine.FullscreenScreenshotCliCommand,
                    IconReference = new IconReference(imgres, 105),
                };
                jumpList.AddUserTasks(fullscreen);
            }

            var selector = new JumpListLink(Application.ExecutablePath, "Capture Region")
            {
                Arguments = CommandLine.AreaSelectorCliCommand,
                IconReference = new IconReference(Application.ExecutablePath, 0),
            };

            jumpList.AddUserTasks(selector);

            try
            {
                jumpList.Refresh();
            }
            catch (UnauthorizedAccessException)
            {
                // No deal when this fails :)
            }
        }
    }
}
