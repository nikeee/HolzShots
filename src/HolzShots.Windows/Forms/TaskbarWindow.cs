using System;
using System.Drawing;

namespace HolzShots.Windows.Forms
{
    public sealed class TaskbarWindow
    {
        private static Lazy<TaskbarWindow> _instance = new(Initialize);
        public static TaskbarWindow Instance => _instance.Value;

        public Rectangle Rectangle { get; }
        public Native.Shell32.TaskbarPosition Position { get; }

        private TaskbarWindow(Native.Shell32.TaskbarPosition position, Rectangle rectangle)
        {
            Rectangle = rectangle;
            Position = position;
        }

        private static TaskbarWindow Initialize()
        {
            var data = new Native.Shell32.AppBarData();
            data.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(data);
            var retval = Native.Shell32.SHAppBarMessage(Native.Shell32.Abm.GetTaskBarPos, ref data);

            return retval == IntPtr.Zero
                ? new TaskbarWindow(Native.Shell32.TaskbarPosition.Unknown, new Rectangle(0, 0, -1, -1))
                : new TaskbarWindow(data.uEdge, data.rc);
        }
    }
}
