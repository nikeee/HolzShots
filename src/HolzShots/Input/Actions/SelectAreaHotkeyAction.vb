Imports System.Threading.Tasks
Imports HolzShots.ScreenshotRelated

Namespace Input.Actions
    Public Class SelectAreaHotkeyAction
        Implements IHotkeyAction

        Public ReadOnly Property Hotkey As Hotkey Implements IHotkeyAction.Hotkey
            Get
                Return My.Settings.SelectorHotkey
            End Get
        End Property
        Public ReadOnly Property Enabled As Boolean Implements IHotkeyAction.Enabled
            Get
                Return My.Settings.SelectorHotkey IsNot Nothing
            End Get
        End Property

        Public Async Function Invoke(sender As Object, e As HotkeyPressedEventArgs) As Task Implements IHotkeyAction.Invoke
            ' TODO: Consider removing Async/Await and just return the Task
            Await ScreenshotInvoker.DoSelector().ConfigureAwait(True)
        End Function
    End Class

    Public Class FullscreenHotkeyAction
        Implements IHotkeyAction

        Public ReadOnly Property Hotkey As Hotkey Implements IHotkeyAction.Hotkey
            Get
                Return My.Settings.FullHotkey
            End Get
        End Property
        Public ReadOnly Property Enabled As Boolean Implements IHotkeyAction.Enabled
            Get
                Return My.Settings.FullHotkey IsNot Nothing
            End Get
        End Property

        Public Async Function Invoke(sender As Object, e As HotkeyPressedEventArgs) As Task Implements IHotkeyAction.Invoke
            ' TODO: Consider removing Async/Await and just return the Task
            Await ScreenshotInvoker.DoFullscreen().ConfigureAwait(True)
        End Function
    End Class

    Public Class WindowHotkeyAction
        Implements IHotkeyAction

        Public ReadOnly Property Hotkey As Hotkey Implements IHotkeyAction.Hotkey
            Get
                Return My.Settings.WindowHotkey
            End Get
        End Property
        Public ReadOnly Property Enabled As Boolean Implements IHotkeyAction.Enabled
            Get
                Return My.Settings.WindowHotkey IsNot Nothing
            End Get
        End Property

        Public Async Function Invoke(sender As Object, e As HotkeyPressedEventArgs) As Task Implements IHotkeyAction.Invoke
            ' TODO: Consider removing Async/Await and just return the Task
            Await ScreenshotInvoker.DoWindow().ConfigureAwait(True)
        End Function
    End Class
End Namespace
