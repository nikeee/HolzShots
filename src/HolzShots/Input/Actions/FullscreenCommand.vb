Imports System.Threading.Tasks
Imports HolzShots.ScreenshotRelated
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("captureEntireScreen")>
    Public Class FullscreenCommand
        Implements ICommand

        Public Async Function Invoke(params As IReadOnlyDictionary(Of String, String)) As Task Implements ICommand.Invoke
            ' TODO: Add proper assertion
            ' Debug.Assert(ManagedSettings.EnableFullscreenScreenshot)

            ' TODO: Re-add proper if condition
            ' If ManagedSettings.EnableFullscreenScreenshot Then
            Dim shot = ScreenshotMethods.CaptureFullscreen()
            Debug.Assert(shot IsNot Nothing)
            Await ProceedWithScreenshot(shot).ConfigureAwait(True)
            'End If
        End Function
    End Class
End Namespace
