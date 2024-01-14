using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using HolzShots.IO;
using HolzShots.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace HolzShots;

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

        var imageResources = Path.Combine(HolzShotsPaths.SystemPath, "imageres.dll");

        if (File.Exists(imageResources))
        {
            var fullScreen = new JumpListLink(Application.ExecutablePath, "Capture entire screen")
            {
                Arguments = CommandLine.FullscreenScreenshotCliCommand,
                IconReference = new IconReference(imageResources, 105),
            };
            jumpList.AddUserTasks(fullScreen);
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
