Imports System.Threading.Tasks
Imports HolzShots.Composition.Command

Namespace Input.Actions
    <Command("captureClipboard")>
    Public Class CaptureClipboardCommand
        Inherits CapturingCommand

        Public Overrides Async Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task
            If parameters Is Nothing Then Throw New ArgumentNullException(NameOf(parameters))
            If settingsContext Is Nothing Then Throw New ArgumentNullException(NameOf(settingsContext))

            Dim image = GetClipboardImage()
            If image Is Nothing Then Return

            Dim shot = Screenshot.FromImage(image, Cursor.Position, ScreenshotSource.Clipboard)
            Await ProcessCapturing(shot, settingsContext).ConfigureAwait(True)
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
