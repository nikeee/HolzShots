Imports System.Threading.Tasks
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("openSettingsJson")>
    Public Class OpenSettingsJsonCommand
        Implements ICommand

        Public Function Invoke(params As IReadOnlyDictionary(Of String, String)) As Task Implements ICommand.Invoke
            UserSettings.OpenSettingsInDefaultEditor()
            Return Task.CompletedTask
        End Function
    End Class
End Namespace