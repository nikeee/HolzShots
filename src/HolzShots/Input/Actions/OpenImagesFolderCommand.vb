Imports System.Threading.Tasks
Imports HolzShots.Interop.LocalDisk
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("openImages")>
    Public Class OpenImagesFolderCommand
        Implements ICommand

        Public Function Invoke(params As IReadOnlyDictionary(Of String, String)) As Task Implements ICommand.Invoke
            ScreenshotDumper.OpenPictureDumpFolder()
            Return Task.CompletedTask
        End Function
    End Class
End Namespace
