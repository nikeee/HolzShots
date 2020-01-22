Imports System.Linq

Namespace Interop
    Module ShellExtensions

        Private ReadOnly LinkedFileTypes As String() = {"pngfile", "jpegfile", "giffile"}
        Private Const RegkeyEntry As String = "HolzShots"
        Private Const RegkeyEntryOpen As String = RegkeyEntry + "Open"
        Private Const RegkeyEntryUpload As String = RegkeyEntry + "Upload"

        Private Sub CheckAdmin()
            If Not InteropHelper.IsAdministrator() Then
                HumanInterop.NoAdmin()
            End If
        End Sub

        Private _uploadWasChanged As Boolean
        Private _uploadChangedValue As Boolean
        Public Property ShellExtensionUpload() As Boolean
            Get
                If Not _uploadWasChanged Then
                    Return LinkedFileTypes.All(Function(s) ExplorerContextMenu.IsRegistered(s, RegkeyEntryUpload))
                End If
                Return _uploadChangedValue
            End Get
            Set(ByVal value As Boolean)
                CheckAdmin()
                _uploadChangedValue = value
                _uploadWasChanged = True
                If value Then
                    Dim cmd As String = $"""{Application.ExecutablePath}"" {UploadFromShellParameter} ""%1"""
                    For Each type As String In LinkedFileTypes
                        Try
                            ExplorerContextMenu.Register(type, RegkeyEntryUpload, "Upload image", cmd)
                        Catch
                        End Try
                    Next
                Else
                    For Each type As String In LinkedFileTypes
                        Try
                            ExplorerContextMenu.Unregister(type, RegkeyEntryUpload)
                        Catch
                        End Try
                    Next
                End If
            End Set
        End Property

        Private _openWasChanged As Boolean
        Private _openChangedValue As Boolean
        Public Property ShellExtensionOpen() As Boolean
            Get
                If Not _openWasChanged Then
                    Return LinkedFileTypes.All(Function(s) ExplorerContextMenu.IsRegistered(s, RegkeyEntryOpen))
                End If
                Return _openChangedValue
            End Get
            Set(ByVal value As Boolean)
                CheckAdmin()
                _openChangedValue = value
                _openWasChanged = True
                If value Then
                    Dim cmd As String = $"""{Application.ExecutablePath}"" {OpenFromShellParameter} ""%1"""
                    For Each type As String In LinkedFileTypes
                        Try
                            ExplorerContextMenu.Register(type, RegkeyEntryOpen, "Open with HolzShots", cmd)
                        Catch
                        End Try
                    Next
                Else
                    For Each type As String In LinkedFileTypes
                        Try
                            ExplorerContextMenu.Unregister(type, RegkeyEntryOpen)
                        Catch
                        End Try
                    Next
                End If
            End Set
        End Property
    End Module
End Namespace
