Imports Microsoft.WindowsAPICodePack.Dialogs
Imports System.IO
Imports System.Linq

Namespace Input.Actions
    Public MustInherit Class FileDependentCommand

        Private Shared ReadOnly AllowedExtensions As String() = {".bmp", ".jpg", ".jpeg", ".png", ".tif", ".tiff"}

        Public Const FileNameParameter = "fileName"

        Protected Shared Function CanProcessFile(fileName As String) As Boolean
            Dim ext = Path.GetExtension(fileName)
            Return AllowedExtensions.Contains(ext)
        End Function

        Protected Shared Function ShowFileSelector(title As String) As String
            Using ofd As New CommonOpenFileDialog()
                ofd.Title = title
                ofd.Filters.Add(New CommonFileDialogFilter(UI.Localization.DialogFilterImages, SupportedFilesFilter))
                ofd.Multiselect = False
                If ofd.ShowDialog() = CommonFileDialogResult.Ok AndAlso File.Exists(ofd.FileName) Then
                    Return ofd.FileName
                End If
            End Using
            Return Nothing
        End Function
    End Class
End Namespace
