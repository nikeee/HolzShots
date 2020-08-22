Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices

Module Extensions

    Private _imageFormats As New Dictionary(Of ImageFormat, ImageFormatMetadata) From {
            {ImageFormat.Png, New ImageFormatMetadata("image/png", ".png")},
            {ImageFormat.Jpeg, New ImageFormatMetadata("image/jpeg", ".jpg")},
            {ImageFormat.Gif, New ImageFormatMetadata("image/gif", ".gif")},
            {ImageFormat.Bmp, New ImageFormatMetadata("image/bmp", ".bmp")},
            {ImageFormat.Tiff, New ImageFormatMetadata("image/tiff", ".tiff")}
        }

    <Extension()>
    Public Function GetImageFormatFromFileExtension(fileName As String) As ImageFormat
        Debug.Assert(Not String.IsNullOrEmpty(fileName))
        Dim extension = Path.GetExtension(fileName)

        Debug.Assert(extension IsNot Nothing)
        Dim res = _imageFormats.SingleOrDefault(Function(i) i.Value?.Extension = extension).Key
        Debug.Assert(res IsNot Nothing)
        Return res
    End Function

    <Extension()>
    Public Function GetFormatMetadata(format As ImageFormat) As ImageFormatMetadata
        Debug.Assert(_imageFormats IsNot Nothing)

        Dim res = _imageFormats(format)
        Debug.Assert(res IsNot Nothing)
        Return res
    End Function

    Class ImageFormatMetadata
        Public ReadOnly Property MimeType As String
        Public ReadOnly Property Extension As String
        Sub New(mimeType As String, extension As String)
            Me.MimeType = mimeType
            Me.Extension = extension
        End Sub
    End Class

End Module
