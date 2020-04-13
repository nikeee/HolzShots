Imports System.Runtime.CompilerServices
Imports System.Security.Principal
Imports Microsoft.WindowsAPICodePack.Taskbar

Namespace Interop
    Friend Module InteropHelper
        Friend Sub DisplayNope(ex As Exception)
            Debug.Assert(False)
            If ex IsNot Nothing Then
                MessageBox.Show("Nope :(", $"Oh snap!{Environment.NewLine}{ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Nope :(", "Oh snap!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Sub

        <Extension()>
        Public Function OpenAndSelectFileInExplorer(filePath As String) As Boolean
            If String.IsNullOrWhiteSpace(filePath) Then Return False ' TODO: Might throw exception?

            Dim args = $"/e, /select, ""{filePath}"""
            Try
                Process.Start("explorer", args)
                Return True
            Catch ex As Exception
                DisplayNope(ex)
                Return False
            End Try
        End Function

        <Extension()>
        Public Function OpenFolderInExplorer(folderPath As String) As Boolean
            If String.IsNullOrWhiteSpace(folderPath) Then Return False ' TODO: Might throw exception?

            Dim psi = New ProcessStartInfo("explorer", folderPath) With {
                .Verb = "open",
                .UseShellExecute = True
            }

            Try
                Process.Start(psi)
                Return True
            Catch ex As Exception
                DisplayNope(ex)
                Return False
            End Try
        End Function

        <Extension()>
        Public Function SetAsClipboardText(text As String) As Boolean
            Try
                Clipboard.SetText(text)
                Return True
            Catch
                Return False
            End Try
        End Function

        <Extension()>
        Public Function SafeProcessStart(text As String, ParamArray args As String()) As Boolean
            Try
                Process.Start(text, String.Join(" ", args))
                Return True
            Catch ex As Exception
                DisplayNope(ex)
                Return False
            End Try
        End Function
        Public Sub AddToRecentDocs(path As String)
            ' TODO: Use this again?
            If TaskbarManager.IsPlatformSupported Then
                Native.Shell32.SHAddToRecentDocs(Native.Shell32.ShellAddToRecentDocsFlags.Path, path)
            End If
        End Sub

        Private _isAdministrator As Lazy(Of Boolean) = New Lazy(Of Boolean)(Function()
                                                                                Dim pricipal = New WindowsPrincipal(WindowsIdentity.GetCurrent())
                                                                                Return pricipal.IsInRole(WindowsBuiltInRole.Administrator)
                                                                            End Function, False)
        Public Function IsAdministrator() As Boolean
            Return _isAdministrator.Value
        End Function
    End Module
End Namespace
