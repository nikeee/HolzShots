Imports System.Drawing.Imaging
Imports HolzShots.Composition.Command
Imports HolzShots.Drawing
Imports HolzShots.Net
Imports HolzShots.Windows.Forms
Imports HolzShots.Windows.Net

Namespace Input.Actions
    <Command("uploadImage")>
    Public Class UploadImageCommand
        Inherits FileDependentCommand
        Implements ICommand(Of HSSettings)

        Private Const UploadImage = "Select Image to Upload"

        Public Async Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task Implements ICommand(Of HSSettings).Invoke
            If parameters Is Nothing Then Throw New ArgumentNullException(NameOf(parameters))
            If settingsContext Is Nothing Then Throw New ArgumentNullException(NameOf(settingsContext))

            Dim fileName = If(
                parameters.Count <> 1 OrElse Not parameters.ContainsKey(FileNameParameter),
                ShowFileSelector(UploadImage),
                parameters(FileNameParameter)
            )

            If fileName Is Nothing Then Return ' We did not get a valid file name (user cancelled or something else was strange)

            If Not CanProcessFile(fileName) Then
                ' TODO: Error Message
                Return
            End If

            Using bmp As New Bitmap(fileName)
                Dim format As ImageFormat = ImageFormatInformation.GetImageFormatFromFileName(fileName)
                Debug.Assert(format IsNot Nothing)

                Try
                    Dim result = Await UploadDispatcher.InitiateUploadToDefaultUploader(bmp, settingsContext, My.Application.Uploaders, format, Nothing).ConfigureAwait(True)
                    UploadHelper.InvokeUploadFinishedUI(result, settingsContext)
                Catch ex As UploadCanceledException
                    NotificationManager.ShowOperationCanceled()
                Catch ex As UploadException
                    NotificationManager.UploadFailed(ex)
                End Try
            End Using

        End Function
    End Class
End Namespace
