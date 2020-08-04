Imports System.Threading.Tasks
Imports HolzShots.ScreenshotRelated
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("uploadImage")>
    Public Class UploadImageCommand
        Inherits FileDependentCommand
        Implements ICommand

        Private Const UploadImage = "Select Image to Upload"

        Public Function Invoke(params As IReadOnlyDictionary(Of String, String)) As Task Implements ICommand.Invoke
            Dim fileName = If(
                params Is Nothing OrElse params.Count <> 1 OrElse Not params.ContainsKey(FileNameParameter),
                ShowFileSelector(UploadImage),
                params(FileNameParameter)
            )

            If Not CanProcessFile(fileName) Then
                ' TODO: Error Message
                Return Task.CompletedTask
            End If

            If fileName IsNot Nothing Then
                UploadSpecificImage(fileName)
            End If
            Return Task.CompletedTask
        End Function
    End Class
End Namespace
