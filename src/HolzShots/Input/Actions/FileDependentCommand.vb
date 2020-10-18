Imports System.IO

Namespace Input.Actions
    Public MustInherit Class FileDependentCommand

        Private Shared ReadOnly AllowedExtensions As String() = {".bmp", ".jpg", ".jpeg", ".png", ".tif", ".tiff"}

        Public Const FileNameParameter = "fileName"

        Protected Shared Function CanProcessFile(fileName As String) As Boolean
            Dim ext = Path.GetExtension(fileName)
            Return AllowedExtensions.Contains(ext)
        End Function

        Protected Shared Function ShowFileSelector(title As String) As String
            Using ofd As New OpenFileDialog()
                ofd.Title = title
                ofd.Filter = $"{UI.Localization.DialogFilterImages}|{SupportedFilesFilter}"
                ofd.Multiselect = False
                Dim res = ofd.ShowDialog()
                Return If(
                    res = DialogResult.OK AndAlso File.Exists(ofd.FileName),
                    ofd.FileName,
                    Nothing
                )
            End Using
        End Function
    End Class
End Namespace
