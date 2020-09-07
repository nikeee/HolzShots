Imports System.Threading.Tasks
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("openSettingsJson")>
    Public Class OpenSettingsJsonCommand
        Implements ICommand(Of HSSettings)

        Public Function Invoke(params As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task Implements ICommand(Of HSSettings).Invoke
            UserSettings.OpenSettingsInDefaultEditor()
            Return Task.CompletedTask
        End Function
    End Class
End Namespace
