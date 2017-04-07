Imports HolzShots.Input

Namespace UI
    Friend Interface IHotkeySelector
        Inherits IDisposable

        ReadOnly Property SelectedKeyStroke As Hotkey
        Function ShowDialog() As DialogResult

    End Interface
End Namespace
