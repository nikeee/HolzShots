Imports HolzShots.Composition.Command
Imports HolzShots.UI

Namespace Input.Actions
    <Command("editImage")>
    Public Class EditImageCommand
        Inherits FileDependentCommand
        Implements ICommand(Of HSSettings)

        Private Const OpenInShotEditor = "Open Image in ShotEditor"

        Public Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task Implements ICommand(Of HSSettings).Invoke
            If parameters Is Nothing Then Throw New ArgumentNullException(NameOf(parameters))
            If settingsContext Is Nothing Then Throw New ArgumentNullException(NameOf(settingsContext))

            Dim fileName = If(
                parameters.Count <> 1 OrElse Not parameters.ContainsKey(FileNameParameter),
                ShowFileSelector(OpenInShotEditor),
                parameters(FileNameParameter)
            )

            If fileName Is Nothing Then Return Task.CompletedTask ' We did not get a valid file name (user cancelled or something else was strange)

            If Not CanProcessFile(fileName) Then
                ' TODO: Error Message
                Return Task.CompletedTask
            End If

            Dim bmp As New Bitmap(fileName)
            Dim shot = Screenshot.FromImported(bmp)
            Dim editor As New ShotEditor(shot, settingsContext)
            AddHandler editor.Disposed, Sub() bmp.Dispose()
            editor.Show()

            Return Task.CompletedTask
        End Function
    End Class
End Namespace
