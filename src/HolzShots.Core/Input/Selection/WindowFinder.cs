using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HolzShots.Native;

namespace HolzShots.Input.Selection
{
    class WindowFinder
    {
        public static Task<IReadOnlyList<WindowRectangle>> GetCurrentWindowRectanglesAsync(IntPtr excludedHandle, CancellationToken ct)
        {
            return Task.Run(
                () => new List<WindowRectangle>(GetCurrentWindowRectangles(excludedHandle)) as IReadOnlyList<WindowRectangle>,
                ct
            );
        }

        private static readonly ISet<string> _ignoredWindowClasses = new HashSet<string>()
        {
            "Shell_TrayWnd", // Task bar
            "Shell_Secondary", // Task bar on second/third/... screen
        };

        public static ISet<WindowRectangle> GetCurrentWindowRectangles(IntPtr excludedHandle)
        {
            var result = new HashSet<WindowRectangle>();

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
                if (className != null && _ignoredWindowClasses.Contains(className))
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

                result.Add(new WindowRectangle(windowHandle, r, title));

                // As we capture a window, we also want its client rectangle:
                // (so the user doesn't need to have the title bar in his screenshot)
                // We don't want to capture the client area (yet?) as it pollutes the widnow list
                /*
                var clientRectangle = WindowHelpers.GetClientRectangle(windowHandle);
                if (clientRectangle != null && clientRectangle.Value != r)
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
}
