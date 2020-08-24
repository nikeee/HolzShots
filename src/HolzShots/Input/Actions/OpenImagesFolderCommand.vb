Imports System.Threading.Tasks
Imports HolzShots.Interop.LocalDisk
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("openImages")>
    Public Class OpenImagesFolderCommand
        Implements ICommand(Of HSSettings)

        Public Function Invoke(params As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task Implements ICommand(Of HSSettings).Invoke
            ScreenshotDumper.OpenPictureDumpFolder()
            Return Task.CompletedTask
        End Function
    End Class
End Namespace
