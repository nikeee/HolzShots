Imports System.Threading.Tasks
Imports HolzShots.ScreenshotRelated
Imports HolzShots.Composition.Command

Namespace Input.Actions

    <Command("captureWindow")>
    Public Class WindowCommand
        Inherits CapturingCommand

        Public Overrides Async Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task
            ' TODO: Add proper assertion
            ' Debug.Assert(ManagedSettings.EnableWindowScreenshot)

            ' TODO: Re-add proper if condition
            ' If ManagedSettings.EnableWindowScreenshot Then
            Dim h As IntPtr = Native.User32.GetForegroundWindow()

            Dim info As Native.User32.WindowPlacement
            Native.User32.GetWindowPlacement(h, info)

            Dim shot = ScreenshotMethods.CaptureWindow(h)
            Await ProcessCapturing(shot).ConfigureAwait(True)
            ' End If
        End Function
    End Class
End Namespace
