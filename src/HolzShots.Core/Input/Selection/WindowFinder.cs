using System.Collections.Generic;
using HolzShots.Native;

namespace HolzShots.Input.Selection;

class WindowFinder
{
    public static Task<IReadOnlyList<WindowRectangle>> GetCurrentWindowRectanglesAsync(IntPtr excludedHandle, bool allowEntireScreen, CancellationToken ct)
    {
        return Task.Run(
            () => new List<WindowRectangle>(GetCurrentWindowRectangles(excludedHandle, allowEntireScreen)) as IReadOnlyList<WindowRectangle>,
            ct
        );
    }

    private static readonly ISet<string> _ignoredWindowClasses = new HashSet<string>()
    {
        "Shell_TrayWnd", // Task bar
        "Shell_Secondary", // Task bar on second/third/... screen
    };

    public static ISet<WindowRectangle> GetCurrentWindowRectangles(IntPtr excludedHandle, bool allowEntireScreen)
    {
        var result = new HashSet<WindowRectangle>();

        var forbittenSize = System.Windows.Forms.SystemInformation.VirtualScreen.Size;

        bool ProcessWindowHandle(IntPtr windowHandle, int _)
        {
            // We want to filter out all windows that are not suitable for screenshots
            // There is a nice post on OldNewThing about that:
            // https://devblogs.microsoft.com/oldnewthing/20200302-00/?p=103507

            if (windowHandle == excludedHandle)
                return true;
            if (!WindowHelpers.IsWindowVisibleOnScreen(windowHandle))
                return true;

            var className = WindowHelpers.GetWindowClass(windowHandle);
            if (className is not null && _ignoredWindowClasses.Contains(className))
                return true;

            var windowRectangle = WindowHelpers.GetWindowRectangle(windowHandle);
            if (windowRectangle == null)
                return true;

            var r = windowRectangle.Value;
            if (!r.HasArea())
                return true;

            if (r.X == -32000 && r.Y == -32000)
                return true; // There is a "hack" that hidden windows are put to this coordinate. We skip these.

            foreach (var windowThatMayOverlayTheCurrentWindow in result)
            {
                // Windows enumerates the windows from top Z to bottom.
                // This means that if we get a windows rectangle that is entirely contained in another window, it is entirely overlayed by a previous enumerated window
                // See: https://stackoverflow.com/questions/295996
                if (windowThatMayOverlayTheCurrentWindow.Rectangle.Contains(r))
                    return true;
            }

            var title = WindowHelpers.GetWindowTitle(windowHandle).Trim();
            if (string.IsNullOrWhiteSpace(title))
                title = null;

            if (!allowEntireScreen)
            {
                // In some cases, we want to disallow selecting the entire screen (probably by accident),
                // because it would make it hard for the user to cancel it (for example, it may be hard for screen recording, when the user selects the entire screen)
                if (title == null && className == "WorkerW")
                    return true; // This is the window that spans the entire screen
                if (windowRectangle.HasValue && windowRectangle.Value.Size == forbittenSize)
                    return true;
            }

            result.Add(new WindowRectangle(windowHandle, r, title));

            // As we capture a window, we also want its client rectangle:
            // (so the user doesn't need to have the title bar in his screenshot)
            // We don't want to capture the client area (yet?) as it pollutes the widnow list
            /*
            var clientRectangle = WindowHelpers.GetClientRectangle(windowHandle);
            if (clientRectangle is not null && clientRectangle.Value != r)
            {
                var mappedClientRectangle = WindowHelpers.MapWindowPoints(windowHandle, IntPtr.Zero, clientRectangle.Value);
                if (mappedClientRectangle.HasValue)
                {
                    result.Add(new WindowRectangle(windowHandle, mappedClientRectangle.Value, title));
                }
            }
            */


            return true;
        }

        User32.EnumWindows(ProcessWindowHandle, IntPtr.Zero);

        return result;
    }
}
