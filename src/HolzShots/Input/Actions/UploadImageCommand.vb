Imports System.Threading.Tasks
Imports HolzShots.ScreenshotRelated
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("uploadImage")>
    Public Class UploadImageCommand
        Inherits FileDependentCommand
        Implements ICommand

        Private Const UploadImage = "Select Image to Upload"

        Public Function Invoke() As Task Implements ICommand.Invoke
            Dim fileName = ShowFileSelector(UploadImage)
            If fileName IsNot Nothing Then
                UploadSpecificImage(fileName)
            End If
            Return Task.CompletedTask
        End Function
    End Class
End Namespace
