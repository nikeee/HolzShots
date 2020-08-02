Imports System.Threading.Tasks
Imports HolzShots.Interop
Imports HolzShots.Interop.LocalDisk
Imports HolzShots.Net
Imports HolzShots.ScreenshotRelated
Imports HolzShots.ScreenshotRelated.Selection
Imports HolzShots.UI.Specialized
Imports HolzShots.Composition.Command

Namespace Input.Actions

    Public Class SelectAreaHotkeyAction

    End Class
    Public Class SelectAreaCommand
        Implements ICommand

        Public Shared ReadOnly CommandName As String = "captureArea"

        Public Async Function Invoke() As Task Implements ICommand.Invoke

            ' TODO: Add proper assertion
            ' Debug.Assert(ManagedSettings.EnableAreaScreenshot)
            Debug.Assert(Not AreaSelector.IsInAreaSelector)
            If UserSettings.Current.EnableIngameMode AndAlso HolzShotsEnvironment.IsFullScreen Then Return

            ' TODO: Re-add proper if condition
            'If ManagedSettings.EnableAreaScreenshot AndAlso Not AreaSelector.IsInAreaSelector Then
            If Not AreaSelector.IsInAreaSelector Then
                Dim shot As Screenshot
                Try
                    shot = Await ScreenshotMethods.CaptureSelection().ConfigureAwait(True)
                    Debug.Assert(shot IsNot Nothing)
                    If shot Is Nothing Then Throw New TaskCanceledException()
                Catch cancelled As TaskCanceledException
                    Debug.WriteLine("Area Selection cancelled")
                    Return
                End Try
                Debug.Assert(shot IsNot Nothing)
                Await ProceedWithScreenshot(shot).ConfigureAwait(True)
            End If

        End Function
    End Class

    Public Class FullscreenHotkeyAction

    End Class
    Public Class FullscreenCommand
        Implements ICommand

        Public Shared ReadOnly CommandName As String = "captureEntireScreen"

        Public Async Function Invoke() As Task Implements ICommand.Invoke
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

    Public Class WindowHotkeyAction

    End Class
    Public Class WindowCommand
        Implements ICommand

        Public Shared ReadOnly CommandName As String = "captureWindow"


        Public Async Function Invoke() As Task Implements ICommand.Invoke
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
