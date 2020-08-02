Imports System.Threading.Tasks
Imports HolzShots.ScreenshotRelated
Imports HolzShots.Composition.Command

Namespace Input.Actions

    <Command("captureWindow")>
    Public Class WindowCommand
        Implements ICommand

        Public Async Function Invoke(ParamArray params() As String) As Task Implements ICommand.Invoke
            ' TODO: Add proper assertion
            ' Debug.Assert(ManagedSettings.EnableWindowScreenshot)

            ' TODO: Re-add proper if condition
            ' If ManagedSettings.EnableWindowScreenshot Then
            Dim h As IntPtr = Native.User32.GetForegroundWindow()

            Dim info As Native.User32.WindowPlacement
            Native.User32.GetWindowPlacement(h, info)

            Dim shot = ScreenshotMethods.CaptureWindow(h)
            Await ProceedWithScreenshot(shot).ConfigureAwait(True)
            ' End If
        End Function
    End Class
End Namespace
