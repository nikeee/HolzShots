Imports HolzShots.Composition.Command
Imports HolzShots.IO

Namespace Input.Actions
    <Command("openImages")>
    Public Class OpenImagesFolderCommand
        Implements ICommand(Of HSSettings)

        Public Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task Implements ICommand(Of HSSettings).Invoke
            If parameters Is Nothing Then Throw New ArgumentNullException(NameOf(parameters))
            If settingsContext Is Nothing Then Throw New ArgumentNullException(NameOf(settingsContext))

            ScreenshotAggregator.OpenPictureSaveDirectory(settingsContext)
            Return Task.CompletedTask
        End Function
    End Class
End Namespace
