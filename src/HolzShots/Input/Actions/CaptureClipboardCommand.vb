Imports System.Threading.Tasks
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("captureClipboard")>
    Public Class CaptureClipboardCommand
        Inherits CapturingCommand

        Public Overrides Async Function Invoke(parameters As IReadOnlyDictionary(Of String, String)) As Task

            Dim image = GetClipboardImage()
            If image Is Nothing Then Return

            Dim shot = Screenshot.FromImage(image, Cursor.Position, ScreenshotSource.Clipboard)
            Await ProcessCapturing(shot).ConfigureAwait(True)
        End Function

        Private Shared Function GetClipboardImage() As Image
            Try
                Return Clipboard.GetImage()
            Catch ex As Exception When _
                            TypeOf ex Is Runtime.InteropServices.ExternalException _
                            OrElse TypeOf ex Is System.Threading.ThreadStateException
                Interop.HumanInterop.RetrievingImageFromClipboardFailed(ex)
                Return Nothing
            End Try
        End Function
    End Class
End Namespace
