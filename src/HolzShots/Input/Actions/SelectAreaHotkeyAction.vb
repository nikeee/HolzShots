Imports System.Threading.Tasks
Imports HolzShots.ScreenshotRelated

Namespace Input.Actions
    Public Class SelectAreaHotkeyAction
        Implements IHotkeyAction
        Public ReadOnly Property Hotkey As Hotkey Implements IHotkeyAction.Hotkey
        Public ReadOnly Property Enabled As Boolean Implements IHotkeyAction.Enabled
        Public Sub New(hotkey As Hotkey, enabled As Boolean)
            If hotkey Is Nothing Then Throw New ArgumentNullException(NameOf(hotkey))
            Me.Hotkey = hotkey
            Me.Enabled = enabled
        End Sub

        Public Async Function Invoke(sender As Object, e As HotkeyPressedEventArgs) As Task Implements IHotkeyAction.Invoke
            ' TODO: Consider removing Async/Await and just return the Task
            Await ScreenshotInvoker.DoSelector().ConfigureAwait(True)
        End Function
    End Class

    Public Class FullscreenHotkeyAction
        Implements IHotkeyAction
        Public ReadOnly Property Hotkey As Hotkey Implements IHotkeyAction.Hotkey
        Public ReadOnly Property Enabled As Boolean Implements IHotkeyAction.Enabled
        Public Sub New(hotkey As Hotkey, enabled As Boolean)
            If hotkey Is Nothing Then Throw New ArgumentNullException(NameOf(hotkey))
            Me.Hotkey = hotkey
            Me.Enabled = enabled
        End Sub

        Public Async Function Invoke(sender As Object, e As HotkeyPressedEventArgs) As Task Implements IHotkeyAction.Invoke
            ' TODO: Consider removing Async/Await and just return the Task
            Await ScreenshotInvoker.DoFullscreen().ConfigureAwait(True)
        End Function
    End Class

    Public Class WindowHotkeyAction
        Implements IHotkeyAction
        Public ReadOnly Property Hotkey As Hotkey Implements IHotkeyAction.Hotkey
        Public ReadOnly Property Enabled As Boolean Implements IHotkeyAction.Enabled
        Public Sub New(hotkey As Hotkey, enabled As Boolean)
            If hotkey Is Nothing Then Throw New ArgumentNullException(NameOf(hotkey))
            Me.Hotkey = hotkey
            Me.Enabled = enabled
        End Sub

        Public Async Function Invoke(sender As Object, e As HotkeyPressedEventArgs) As Task Implements IHotkeyAction.Invoke
            ' TODO: Consider removing Async/Await and just return the Task
            Await ScreenshotInvoker.DoWindow().ConfigureAwait(True)
        End Function
    End Class
End Namespace
