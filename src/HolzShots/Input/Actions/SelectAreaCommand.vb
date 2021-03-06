Imports HolzShots.Composition.Command
Imports HolzShots.Drawing
Imports HolzShots.Input.Selection
Imports HolzShots.Threading

Namespace Input.Actions
    <Command("captureArea")>
    Public Class SelectAreaCommand
        Inherits CapturingCommand

        Public Overrides Async Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task
            If parameters Is Nothing Then Throw New ArgumentNullException(NameOf(parameters))
            If settingsContext Is Nothing Then Throw New ArgumentNullException(NameOf(settingsContext))

            ' TODO: Add proper assertion
            ' Debug.Assert(ManagedSettings.EnableAreaScreenshot)
            Debug.Assert(Not SelectionSemaphore.IsInAreaSelection)

            If Not settingsContext.EnableHotkeysDuringFullscreen AndAlso HolzShots.Windows.Forms.EnvironmentEx.IsFullscreenAppRunning() Then Return

            ' TODO: Re-add proper if condition
            'If ManagedSettings.EnableAreaScreenshot AndAlso Not SelectionSemaphore.IsInAreaSelection Then
            If Not SelectionSemaphore.IsInAreaSelection Then
                Dim shot As Screenshot
                Try
                    shot = Await CaptureSelection(settingsContext).ConfigureAwait(True)
                    Debug.Assert(shot IsNot Nothing)
                    If shot Is Nothing Then Throw New TaskCanceledException()
                Catch cancelled As TaskCanceledException
                    Debug.WriteLine("Area Selection cancelled")
                    Return
                End Try
                Debug.Assert(shot IsNot Nothing)
                Await ProcessCapturing(shot, settingsContext).ConfigureAwait(True)
            End If

        End Function

        Shared Async Function CaptureSelection(settingsContext As HSSettings) As Task(Of Screenshot)
            If settingsContext Is Nothing Then Throw New ArgumentNullException(NameOf(settingsContext))

            Debug.Assert(Not SelectionSemaphore.IsInAreaSelection)

            If SelectionSemaphore.IsInAreaSelection Then Return Nothing

            Using prio As New ProcessPriorityRequest()
                Using screen = ScreenshotCreator.CaptureScreenshot(SystemInformation.VirtualScreen)
                    Using selector = AreaSelector.Create(screen, settingsContext)
                        Dim selectedArea = Await selector.PromptSelectionAsync().ConfigureAwait(True)

                        Debug.Assert(selectedArea.Width > 0)
                        Debug.Assert(selectedArea.Height > 0)

                        Dim selectedImage As New Bitmap(selectedArea.Width, selectedArea.Height)

                        Using g As Graphics = Graphics.FromImage(selectedImage)
                            g.DrawImage(screen, New Rectangle(0, 0, selectedArea.Width, selectedArea.Height), selectedArea, GraphicsUnit.Pixel)
                        End Using

                        Return Screenshot.FromImage(selectedImage, Cursor.Position, ScreenshotSource.Selected)
                    End Using
                End Using
            End Using
        End Function
    End Class
End Namespace
