Imports System.Text

Namespace ScreenshotRelated

    Friend Class ScreenshotMethodsHelper
        Private Sub New()
        End Sub

        Friend Shared Function GetWindowTitle(windowHandle As IntPtr) As String

            Dim windowTitleLength As Integer = Native.User32.GetWindowTextLength(windowHandle)
            Dim windowTitleBuffer As New StringBuilder(windowTitleLength + 1)

            Dim unused = Native.User32.GetWindowText(windowHandle, windowTitleBuffer, windowTitleBuffer.Capacity)

            Return windowTitleBuffer.ToString()
        End Function

        Friend Shared Function GetProcessNameOfWindow(windowHandle As IntPtr) As String

            Dim pid As Integer
            Dim unused = Native.User32.GetWindowThreadProcessId(windowHandle, pid)

            Dim process = Diagnostics.Process.GetProcessById(pid)

            Return process.ProcessName
        End Function

    End Class
End Namespace
