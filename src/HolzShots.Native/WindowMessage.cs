namespace HolzShots.Native;

/// <summary>
/// An incomplete list of window messages.
///
/// You can find the rest here:
/// https://docs.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues
/// There is also this list of window messages:
/// https://wiki.winehq.org/List_Of_Windows_Messages
/// </summary>
public enum WindowMessage : uint
{
    // General Window Messages
    WM = 0x0,
    WM_SetRedraw = 0xb,
    WM_NcCalcSize = WM | 0x83,
    WM_NcHitTest = WM | 0x84,
    WM_DwmCompositionChanged = WM | 0x31E,
    WM_Paint = WM | 0xF,

    /// <summary>
    /// Base for messages that are set by the user (and not by the system).
    /// https://docs.microsoft.com/en-us/windows/win32/winmsg/wm-user
    /// </summary>
    WM_User = WM | 0x400,

    // TreeView Messages
    TVM = 0x1100,
    TVM_SetExtendedStyle = TVM | 44,
    TVM_GetExtendedStyle = TVM | 45,
    TVM_SetAutoScrollInfo = TVM | 59,

    EM = 0x1500,
    EM_SetCueBanner = EM | 0x1,
}
