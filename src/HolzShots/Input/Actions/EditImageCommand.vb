Imports System.Threading.Tasks
Imports HolzShots.ScreenshotRelated
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("editImage")>
    Public Class EditImageCommand
        Inherits FileDependentCommand
        Implements ICommand

        Private Const OpenInShotEditor = "Open Image in ShotEditor"

        Public Function Invoke() As Task Implements ICommand.Invoke
            Dim fileName = ShowFileSelector(OpenInShotEditor)
            If fileName IsNot Nothing Then
                OpenSpecificImage(fileName)
            End If
            Return Task.CompletedTask
        End Function
    End Class
End Namespace
