Imports System.Text
Imports HolzShots.Interop

Namespace ScreenshotRelated

    Friend Class ScreenshotMethodsHelper
        Private Sub New()
        End Sub

        Public Shared Sub StartRedraw(ByVal whandle As IntPtr)
            NativeMethods.LockWindowUpdate(IntPtr.Zero)
            Native.User32.SendMessage(whandle, 11, New IntPtr(1), IntPtr.Zero)
        End Sub

        Public Shared Sub StopRedraw(ByVal whandle As IntPtr)
            Native.User32.SendMessage(whandle, 11, IntPtr.Zero, IntPtr.Zero)
            NativeMethods.LockWindowUpdate(whandle)
        End Sub

        Friend Shared Function GetWindowTitle(windowHandle As IntPtr) As String

            Dim windowTitleLength As Integer = Native.User32.GetWindowTextLength(windowHandle)
            Dim windowTitleBuffer As New StringBuilder(windowTitleLength + 1)

            Dim unused = Native.User32.GetWindowText(windowHandle, windowTitleBuffer, windowTitleBuffer.Capacity)

            Return windowTitleBuffer.ToString()
        End Function

        Friend Shared Function GetProcessNameOfWindow(windowHandle As IntPtr) As String

            Dim pid As Integer
            Dim unused = NativeMethods.GetWindowThreadProcessId(windowHandle, pid)

            Dim process = Diagnostics.Process.GetProcessById(pid)

            Return process.ProcessName
        End Function

    End Class
End Namespace
