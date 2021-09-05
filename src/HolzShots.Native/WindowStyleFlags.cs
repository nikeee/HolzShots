namespace HolzShots.Native
{
    /// <summary>
    /// Enum that contians common window styles.
    /// 
    /// https://www.autohotkey.com/docs/misc/Styles.htm
    /// (we primarily use tree view styles)
    ///
    /// Some are also "extended" styles from Vista onwards:
    /// https://docs.microsoft.com/en-us/windows/win32/controls/tree-view-control-window-extended-styles
    /// </summary>
    [Flags]
    public enum WindowStyleFlags : int
    {
        /// <summary> +/-HScroll. Disables horizontal scrolling in the control. The control will not display any horizontal scroll bars. </summary>
        TVS_NoHScroll = 0x8000,
    }

    /// <summary>
    /// Some are also "extended" styles from Vista onwards:
    /// https://docs.microsoft.com/en-us/windows/win32/controls/tree-view-control-window-extended-styles
    ///
    /// Tree view ones can be found here:
    /// https://github.com/LorenzCK/WindowsFormsAero/blob/867684f7f0d3e8f440e26e07f9aec45c18c9a330/src/WindowsFormsAero/Native/TreeViewExtendedStyle.cs#L11-L22
    /// </summary>
    [Flags]
    public enum ExtendedWindowStyleFlags : int
    {
        TVS_EX_AutoHScroll = 0x20,
        TVS_EX_FadeInOutExpandOs = 0x40,
        WS_EX_NOACTIVATE = 0x08000000,
    }
}
