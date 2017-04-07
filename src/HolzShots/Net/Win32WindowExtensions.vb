Imports System.Runtime.CompilerServices

Namespace Net
    Module Win32WindowExtensions
        <Extension()>
        Public Function GetHandle(window As IWin32Window) As IntPtr
            Return If(window Is Nothing, IntPtr.Zero, window.Handle)
        End Function
    End Module
End Namespace
