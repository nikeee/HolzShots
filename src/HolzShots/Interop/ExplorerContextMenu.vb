Imports Microsoft.Win32

Namespace Interop
    Friend Class ExplorerContextMenu

        Public Shared Function IsRegistered(ByVal fileType As String, ByVal shellKeyName As String) As Boolean
            Debug.Assert(Not String.IsNullOrEmpty(fileType) AndAlso Not String.IsNullOrEmpty(shellKeyName))

            ' path to the registry location

            Dim regPath As String = $"{fileType}\shell\{shellKeyName}"

            ' remove context menu from the registry

            Return Registry.ClassesRoot.OpenSubKey(regPath) IsNot Nothing
        End Function
        Public Shared Sub Register(ByVal fileType As String, ByVal shellKeyName As String, ByVal menuText As String, ByVal menuCommand As String)
            ' create path to registry location
            Debug.Assert(Not String.IsNullOrEmpty(fileType) AndAlso
                         Not String.IsNullOrEmpty(shellKeyName) AndAlso
                         Not String.IsNullOrEmpty(menuText) AndAlso
                         Not String.IsNullOrEmpty(menuCommand))

            Dim regPath As String = $"{fileType}\shell\{shellKeyName}"

            ' add context menu to the registry

            Using key As RegistryKey = Registry.ClassesRoot.CreateSubKey(regPath)
                key.SetValue(Nothing, menuText)
            End Using

            ' add command that is invoked to the registry

            Using key As RegistryKey = Registry.ClassesRoot.CreateSubKey($"{regPath}\command")
                key.SetValue(Nothing, menuCommand)
            End Using
        End Sub

        Public Shared Sub Unregister(ByVal fileType As String, ByVal shellKeyName As String)
            Debug.Assert(Not String.IsNullOrEmpty(fileType) AndAlso Not String.IsNullOrEmpty(shellKeyName))

            ' path to the registry location

            Dim regPath As String = $"{fileType}\shell\{shellKeyName}"

            ' remove context menu from the registry

            Registry.ClassesRoot.DeleteSubKeyTree(regPath)
        End Sub
    End Class
End Namespace
