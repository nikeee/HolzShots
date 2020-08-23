Imports System.Text

Namespace ScreenshotRelated

    Friend Class ScreenshotMethodsHelper
        Private Sub New()
        End Sub

        Public Shared Sub StartRedraw(ByVal whandle As IntPtr)
            Native.User32.LockWindowUpdate(IntPtr.Zero)
            Native.User32.SendMessage(whandle, Native.WindowMessage.WM_SetRedraw, New IntPtr(1), IntPtr.Zero)
        End Sub

        Public Shared Sub StopRedraw(ByVal whandle As IntPtr)
            Native.User32.SendMessage(whandle, Native.WindowMessage.WM_SetRedraw, IntPtr.Zero, IntPtr.Zero)
            Native.User32.LockWindowUpdate(whandle)
        End Sub


        Friend Shared Function GetProcessNameOfWindow(windowHandle As IntPtr) As String

            Dim pid As Integer
            Dim unused = Native.User32.GetWindowThreadProcessId(windowHandle, pid)

            Dim process = Diagnostics.Process.GetProcessById(pid)

            Return process.ProcessName
        End Function

    End Class
End Namespace
