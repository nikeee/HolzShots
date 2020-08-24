Imports System.Threading.Tasks
Imports HolzShots.ScreenshotRelated
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("captureEntireScreen")>
    Public Class FullscreenCommand
        Inherits CapturingCommand

        Public Overrides Async Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task
            ' TODO: Add proper assertion
            ' Debug.Assert(ManagedSettings.EnableFullscreenScreenshot)

            ' TODO: Re-add proper if condition
            ' If ManagedSettings.EnableFullscreenScreenshot Then
            Dim shot = ScreenshotMethods.CaptureFullscreen()
            Debug.Assert(shot IsNot Nothing)
            Await ProcessCapturing(shot).ConfigureAwait(True)
            'End If
        End Function
    End Class
End Namespace
