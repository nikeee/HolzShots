Imports System.Threading.Tasks
Imports HolzShots.Composition.Command
Imports HolzShots.UI.Specialized

Namespace Input.Actions
    <Command("editImage")>
    Public Class EditImageCommand
        Inherits FileDependentCommand
        Implements ICommand

        Private Const OpenInShotEditor = "Open Image in ShotEditor"

        Public Function Invoke(params As IReadOnlyDictionary(Of String, String)) As Task Implements ICommand.Invoke
            Dim fileName = If(
                params Is Nothing OrElse params.Count <> 1 OrElse Not params.ContainsKey(FileNameParameter),
                ShowFileSelector(OpenInShotEditor),
                params(FileNameParameter)
            )

            If fileName Is Nothing Then Return Task.CompletedTask ' We did not get a valid file name (user cancelled or something else was strange)

            If Not CanProcessFile(fileName) Then
                ' TODO: Error Message
                Return Task.CompletedTask
            End If

            Dim bmp As New Bitmap(fileName)
            Dim shot = Screenshot.FromImported(bmp)
            Dim editor As New ShotEditor(shot)
            AddHandler editor.Disposed, Sub() bmp.Dispose()
            editor.Show()

            Return Task.CompletedTask
        End Function
    End Class
End Namespace
