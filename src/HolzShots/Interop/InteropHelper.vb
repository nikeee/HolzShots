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
    End Module
End Namespace
