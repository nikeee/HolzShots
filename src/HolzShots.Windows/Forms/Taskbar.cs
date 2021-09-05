using System.Runtime.InteropServices;

namespace HolzShots.Windows.Forms
{
    /// <summary> Wrapper for taskbar progress api that makes it easier to interact with a single window. </summary>
    public readonly struct TaskbarProgressManager : IEquatable<TaskbarProgressManager>
    {
        private readonly IntPtr _windowHandle;

        internal TaskbarProgressManager(IntPtr windowHandle) => _windowHandle = windowHandle;

        public void SetProgressValue(ulong completed, ulong total) => Taskbar.SetProgressValue(_windowHandle, completed, total);
        public void SetProgressValue(float unitIntervalValue)
        {
            if (float.IsNaN(unitIntervalValue))
                unitIntervalValue = 0.0f;
            unitIntervalValue = Math.Clamp(unitIntervalValue, 0.0f, 1.0f);

            var percentage = (ulong)(unitIntervalValue * 100);
            SetProgressValue(percentage, 100);
        }
        public void SetProgressState(TaskbarProgressBarState state) => Taskbar.SetProgressState(_windowHandle, state);

        public override int GetHashCode() => _windowHandle.GetHashCode();
        public override bool Equals(object? obj) => obj is TaskbarProgressManager tpm && Equals(tpm);
        public bool Equals(TaskbarProgressManager other) => other._windowHandle == _windowHandle;
        public static bool operator ==(TaskbarProgressManager left, TaskbarProgressManager right) => left._windowHandle == right._windowHandle;
        public static bool operator !=(TaskbarProgressManager left, TaskbarProgressManager right) => left._windowHandle != right._windowHandle;
    }

    public static class Taskbar
    {
        private static IntPtr _ownerHandle;
        internal static IntPtr OwnerHandle
        {
            get
            {
                if (_ownerHandle == IntPtr.Zero)
                {
                    System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();

                    _ownerHandle = currentProcess == null || currentProcess.MainWindowHandle == IntPtr.Zero
                        ? IntPtr.Zero
                        : currentProcess.MainWindowHandle;
                }
                return _ownerHandle;
            }
        }

        private static readonly Lazy<ITaskbarList4> _instance = new(() =>
        {
            if (!IsPlatformSupported)
                throw new Exception("Taskbar API not supported");

            var inst = (ITaskbarList4)new CTaskbarList();
            inst.HrInit();
            return inst;
        }, false);

        private static ITaskbarList4 Instance => _instance.Value;

        internal static void SetProgressValue(IntPtr windowHandle, ulong completed, ulong total)
        {
            if (IsPlatformSupported)
                Instance.SetProgressValue(windowHandle, completed, total);
        }
        internal static void SetProgressState(IntPtr windowHandle, TaskbarProgressBarState state)
        {
            if (IsPlatformSupported)
            {
                var handle = windowHandle == IntPtr.Zero
                    ? OwnerHandle
                    : windowHandle;
                Instance.SetProgressState(handle, state);
            }
        }

        private static readonly Version _windowsSeven = new Version(6, 1);
        public static bool IsPlatformSupported => Environment.OSVersion.Version >= _windowsSeven;

        public static TaskbarProgressManager CreateProgressManagerForWindow(IWin32Window window) => CreateProgressManagerForWindow(window?.Handle ?? IntPtr.Zero);
        public static TaskbarProgressManager CreateProgressManagerForWindow(IntPtr windowHandle)
        {
            if (!IsPlatformSupported)
                return new TaskbarProgressManager(IntPtr.Zero);

            var handle = windowHandle == IntPtr.Zero
                ? OwnerHandle
                : windowHandle;
            return new TaskbarProgressManager(handle);
        }
    }

    public enum TaskbarProgressBarState
    {
        NoProgress = 0,
        Indeterminate = 0x1,
        Normal = 0x2,
        Error = 0x4,
        Paused = 0x8,
    }

    [ComImport]
    [Guid("c43dc798-95d1-4bea-9030-bb99e2983a1a")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ITaskbarList4
    {
        // ITaskbarList
        [PreserveSig]
        void HrInit();
        [PreserveSig]
        void AddTab(IntPtr hwnd);
        [PreserveSig]
        void DeleteTab(IntPtr hwnd);
        [PreserveSig]
        void ActivateTab(IntPtr hwnd);
        [PreserveSig]
        void SetActiveAlt(IntPtr hwnd);

        // ITaskbarList2
        [PreserveSig]
        void MarkFullscreenWindow(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

        // ITaskbarList3
        [PreserveSig]
        void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
        [PreserveSig]
        void SetProgressState(IntPtr hwnd, TaskbarProgressBarState tbpFlags);
        [PreserveSig]
        void RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);
        [PreserveSig]
        void UnregisterTab(IntPtr hwndTab);
        [PreserveSig]
        void SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);
        [PreserveSig]
        void SetTabActive(IntPtr hwndTab, IntPtr hwndInsertBefore, uint dwReserved);
        [PreserveSig]
        IntPtr ThumbBarAddButtons(
            IntPtr hwnd,
            uint cButtons,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] pButtons);
        [PreserveSig]
        IntPtr ThumbBarUpdateButtons(
            IntPtr hwnd,
            uint cButtons,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] pButtons);
        [PreserveSig]
        void ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);
        [PreserveSig]
        void SetOverlayIcon(
          IntPtr hwnd,
          IntPtr hIcon,
          [MarshalAs(UnmanagedType.LPWStr)] string pszDescription);
        [PreserveSig]
        void SetThumbnailTooltip(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.LPWStr)] string pszTip);
        [PreserveSig]
        void SetThumbnailClip(
            IntPtr hwnd,
            IntPtr prcClip);

        // ITaskbarList4
        void SetTabProperties(IntPtr hwndTab, IntPtr stpFlags);
    }

    [Guid("56FDF344-FD6D-11d0-958A-006097C9A090")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComImport]
    internal class CTaskbarList { }
}
