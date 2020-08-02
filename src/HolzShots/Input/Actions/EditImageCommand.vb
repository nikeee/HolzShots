Imports System.Threading.Tasks
Imports HolzShots.ScreenshotRelated
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("editImage")>
    Public Class EditImageCommand
        Inherits FileDependentCommand
        Implements ICommand

        Private Const OpenInShotEditor = "Open Image in ShotEditor"

        Public Function Invoke(ParamArray params() As String) As Task Implements ICommand.Invoke
            Dim fileName = If(
                params Is Nothing OrElse params.Length <> 1,
                ShowFileSelector(OpenInShotEditor),
                params(0)
            )

            If Not CanProcessFile(fileName) Then
                ' TODO: Error Message
                Return Task.CompletedTask
            End If

            If fileName IsNot Nothing Then
                OpenSpecificImage(fileName)
            End If
            Return Task.CompletedTask
        End Function
    End Class
End Namespace
