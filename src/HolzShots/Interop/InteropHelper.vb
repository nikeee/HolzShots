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

        Public Sub AddToRecentDocs(path As String)
            ' TODO: Use this again?
            If TaskbarManager.IsPlatformSupported Then
                Native.Shell32.SHAddToRecentDocs(Native.Shell32.ShellAddToRecentDocsFlags.Path, path)
            End If
        End Sub
    End Module
End Namespace
