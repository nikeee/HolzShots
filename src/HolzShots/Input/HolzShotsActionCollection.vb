Namespace Input
    Public Class HolzShotsActionCollection
        Inherits HotkeyActionCollection
        Implements IDisposable

        Private ReadOnly _lockObj As New Object
        Private disposedValue As Boolean

        Public Sub New(hook As KeyboardHook, ParamArray actions() As IHotkeyAction)
            MyBase.New(hook, actions)
        End Sub

        Public Overrides Sub Refresh()
            Debug.Assert(Hook IsNot Nothing)
            Debug.Assert(_lockObj IsNot Nothing)

            Dim exeptions As New List(Of Exception)(Count)
            SyncLock _lockObj
                Hook.UnregisterAllHotkeys()

                For Each action In Actions
                    Dim h = action.Hotkey
                    If action.Enabled Then
                        Try
                            Hook.RegisterHotkey(h)
                        Catch ex As Exception
                            exeptions.Add(ex)
                            Continue For
                        End Try

                        AddHandler h.KeyPressed, AddressOf action.Invoke
                    End If
                Next
            End SyncLock

            If exeptions.Count > 0 Then
                Throw New AggregateException("A number of Hotkeys failed to register", exeptions)
            End If
        End Sub

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects)
                End If

                SyncLock _lockObj
                    Hook.UnregisterAllHotkeys()
                End SyncLock
                disposedValue = True
            End If
        End Sub

        ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
        Protected Overrides Sub Finalize()
            Dispose(disposing:=False)
            MyBase.Finalize()
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(disposing:=True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class
End Namespace
