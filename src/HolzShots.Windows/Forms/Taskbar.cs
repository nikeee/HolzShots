using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HolzShots.Windows.Forms;

/// <summary> Wrapper for taskbar progress api that makes it easier to interact with a single window. </summary>
public readonly struct TaskbarProgressManager : IEquatable<TaskbarProgressManager>
{
    private readonly nint _windowHandle;

    internal TaskbarProgressManager(nint windowHandle) => _windowHandle = windowHandle;

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
    private static nint _ownerHandle;
    internal static nint OwnerHandle
    {
        get
        {
            if (_ownerHandle == nint.Zero)
            {
                var currentProcess = System.Diagnostics.Process.GetCurrentProcess();

                _ownerHandle = currentProcess == null || currentProcess.MainWindowHandle == nint.Zero
                    ? nint.Zero
                    : currentProcess.MainWindowHandle;
            }
            return _ownerHandle;
        }
    }

    private static readonly Lazy<ITaskbarList4> _instance = new(() =>
    {
        if (!IsPlatformSupported)
            throw new Exception("Taskbar API not supported");

        var instance = (ITaskbarList4)new CTaskbarList();
        instance.HrInit();
        return instance;
    }, false);

    private static ITaskbarList4 Instance => _instance.Value;

    internal static void SetProgressValue(nint windowHandle, ulong completed, ulong total)
    {
        if (IsPlatformSupported)
            Instance.SetProgressValue(windowHandle, completed, total);
    }
    internal static void SetProgressState(nint windowHandle, TaskbarProgressBarState state)
    {
        if (IsPlatformSupported)
        {
            var handle = windowHandle == nint.Zero
                ? OwnerHandle
                : windowHandle;
            Instance.SetProgressState(handle, state);
        }
    }

    private static readonly Version _windowsSeven = new(6, 1);
    public static bool IsPlatformSupported => Environment.OSVersion.Version >= _windowsSeven;

    public static TaskbarProgressManager CreateProgressManagerForWindow(IWin32Window window) => CreateProgressManagerForWindow(window?.Handle ?? nint.Zero);
    public static TaskbarProgressManager CreateProgressManagerForWindow(nint windowHandle)
    {
        if (!IsPlatformSupported)
            return new TaskbarProgressManager(nint.Zero);

        var handle = windowHandle == nint.Zero
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
    void AddTab(nint hwnd);
    [PreserveSig]
    void DeleteTab(nint hwnd);
    [PreserveSig]
    void ActivateTab(nint hwnd);
    [PreserveSig]
    void SetActiveAlt(nint hwnd);

    // ITaskbarList2
    [PreserveSig]
    void MarkFullscreenWindow(
        nint hwnd,
        [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

    // ITaskbarList3
    [PreserveSig]
    void SetProgressValue(nint hwnd, ulong ullCompleted, ulong ullTotal);
    [PreserveSig]
    void SetProgressState(nint hwnd, TaskbarProgressBarState tbpFlags);
    [PreserveSig]
    void RegisterTab(nint hwndTab, nint hwndMDI);
    [PreserveSig]
    void UnregisterTab(nint hwndTab);
    [PreserveSig]
    void SetTabOrder(nint hwndTab, nint hwndInsertBefore);
    [PreserveSig]
    void SetTabActive(nint hwndTab, nint hwndInsertBefore, uint dwReserved);
    [PreserveSig]
    nint ThumbBarAddButtons(
        nint hwnd,
        uint cButtons,
        [MarshalAs(UnmanagedType.LPArray)] nint[] pButtons);
    [PreserveSig]
    nint ThumbBarUpdateButtons(
        nint hwnd,
        uint cButtons,
        [MarshalAs(UnmanagedType.LPArray)] nint[] pButtons);
    [PreserveSig]
    void ThumbBarSetImageList(nint hwnd, nint himl);
    [PreserveSig]
    void SetOverlayIcon(
      nint hwnd,
      nint hIcon,
      [MarshalAs(UnmanagedType.LPWStr)] string pszDescription);
    [PreserveSig]
    void SetThumbnailTooltip(
        nint hwnd,
        [MarshalAs(UnmanagedType.LPWStr)] string pszTip);
    [PreserveSig]
    void SetThumbnailClip(
        nint hwnd,
        nint prcClip);

    // ITaskbarList4
    void SetTabProperties(nint hwndTab, nint stpFlags);
}

[Guid("56FDF344-FD6D-11d0-958A-006097C9A090")]
[ClassInterface(ClassInterfaceType.None)]
[ComImport]
internal class CTaskbarList { }
