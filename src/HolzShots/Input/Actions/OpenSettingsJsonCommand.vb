Imports System.Threading.Tasks
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("openSettingsJson")>
    Public Class OpenSettingsJsonCommand
        Implements ICommand(Of HSSettings)

        Public Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task Implements ICommand(Of HSSettings).Invoke
            If parameters Is Nothing Then Throw New ArgumentNullException(NameOf(parameters))
            If settingsContext Is Nothing Then Throw New ArgumentNullException(NameOf(settingsContext))

            UserSettings.OpenSettingsInDefaultEditor()
            Return Task.CompletedTask
        End Function
    End Class
End Namespace
