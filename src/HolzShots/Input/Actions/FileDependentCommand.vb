Imports Microsoft.WindowsAPICodePack.Dialogs
Imports System.IO

Namespace Input.Actions
    ' TODO: Split this into multiple files

    Public MustInherit Class FileDependentCommand
        Protected Function ShowFileSelector(title As String) As String
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
