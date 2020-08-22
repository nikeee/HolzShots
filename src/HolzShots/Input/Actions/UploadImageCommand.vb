Imports System.Threading.Tasks
Imports HolzShots.Composition.Command
Imports System.Drawing.Imaging
Imports HolzShots.Net
Imports HolzShots.Interop
Imports HolzShots.Drawing

Namespace Input.Actions
    <Command("uploadImage")>
    Public Class UploadImageCommand
        Inherits FileDependentCommand
        Implements ICommand

        Private Const UploadImage = "Select Image to Upload"

        Public Async Function Invoke(params As IReadOnlyDictionary(Of String, String)) As Task Implements ICommand.Invoke
            Dim fileName = If(
                params Is Nothing OrElse params.Count <> 1 OrElse Not params.ContainsKey(FileNameParameter),
                ShowFileSelector(UploadImage),
                params(FileNameParameter)
            )

            If Not CanProcessFile(fileName) Then
                ' TODO: Error Message
                Return
            End If

            If fileName Is Nothing Then Return

            Using bmp As New Bitmap(fileName)
                Dim format As ImageFormat = ImageFormatInformation.GetImageFormatFromFileName(fileName)
                Debug.Assert(format IsNot Nothing)

                Try
                    Dim result = Await UploadHelper.UploadToDefaultUploader(bmp, format).ConfigureAwait(True)
                    UploadHelper.InvokeUploadFinishedUi(result)
                Catch ex As UploadCanceledException
                    HumanInterop.ShowOperationCanceled()
                Catch ex As UploadException
                    UploadHelper.InvokeUploadFailedUi(ex)
                End Try
            End Using

        End Function
    End Class
End Namespace
