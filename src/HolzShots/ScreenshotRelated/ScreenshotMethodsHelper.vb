Imports System.Text
Imports HolzShots.Interop

Namespace ScreenshotRelated

    Friend Class ScreenshotMethodsHelper
        Private Sub New()
        End Sub

        Public Shared Sub StartRedraw(ByVal whandle As IntPtr)
            NativeMethods.LockWindowUpdate(IntPtr.Zero)
            NativeMethods.SendMessage(whandle, 11, New IntPtr(1), IntPtr.Zero)
        End Sub

        Public Shared Sub StopRedraw(ByVal whandle As IntPtr)
            NativeMethods.SendMessage(whandle, 11, IntPtr.Zero, IntPtr.Zero)
            NativeMethods.LockWindowUpdate(whandle)
        End Sub

        Friend Shared Sub GetWindowInformation(ByVal whandle As IntPtr, ByRef wndTitle As String, ByRef procName As String)
            Dim pid As Integer
            NativeMethods.GetWindowThreadProcessId(whandle, pid)
            Dim p As Process = Process.GetProcessById(pid)
            Dim windTitlelen As Integer = NativeMethods.GetWindowTextLength(whandle)
            Dim wndTitlesb As New StringBuilder(windTitlelen + 1)
            NativeMethods.GetWindowText(whandle, wndTitlesb, wndTitlesb.Capacity)
            wndTitle = wndTitlesb.ToString
            procName = p.ProcessName
        End Sub

    End Class
End Namespace
