Imports System.Threading.Tasks
Imports HolzShots.Interop
Imports HolzShots.ScreenshotRelated
Imports HolzShots.ScreenshotRelated.Selection
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("captureArea")>
    Public Class SelectAreaCommand
        Implements ICommand

        Public Async Function Invoke(params As IReadOnlyDictionary(Of String, String)) As Task Implements ICommand.Invoke

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
End Namespace
