Imports System.Threading.Tasks
Imports HolzShots.ScreenshotRelated
Imports HolzShots.Composition.Command
Imports HolzShots.Threading
Imports HolzShots.UI
Imports System.Drawing.Drawing2D
Imports HolzShots.Drawing

Namespace Input.Actions
    <Command("captureEntireScreen")>
    Public Class FullscreenCommand
        Inherits CapturingCommand

        Public Overrides Async Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task
            ' TODO: Add proper assertion
            ' Debug.Assert(ManagedSettings.EnableFullscreenScreenshot)

            ' TODO: Re-add proper if condition
            ' If ManagedSettings.EnableFullscreenScreenshot Then

            Dim shot = CaptureFullScreen()
            Debug.Assert(shot IsNot Nothing)
            Await ProcessCapturing(shot, settingsContext).ConfigureAwait(True)
            'End If
        End Function

        Shared Function CaptureFullScreen() As Screenshot
            Using prio As New ProcessPriorityRequest()
                Dim screen = ScreenshotCreator.CaptureScreenshot(SystemInformation.VirtualScreen)
                Return Screenshot.FromImage(screen, Cursor.Position, ScreenshotSource.Fullscreen)
            End Using
        End Function
    End Class
End Namespace
