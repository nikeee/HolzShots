Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports HolzShots.Common.Drawing

Module ImageExtensions

    ''' <summary>Retrieves the Encoder Information for a given MimeType</summary>
    ''' <param name="mimeType">String: Mimetype</param>
    ''' <returns>ImageCodecInfo: Mime info or null if not found</returns>
    Private Function GetEncoderInfo(mimeType As String) As ImageCodecInfo
        Dim encoders = ImageCodecInfo.GetImageEncoders()
        Return encoders.FirstOrDefault(Function(t) t.MimeType = mimeType)
    End Function

    Private Const JpgMimeType As String = "image/jpeg"

    ''' <summary>Save an Image as a Jpeg with a given compression</summary>
    ''' <param name="image">Image: Image to save</param>
    ''' <param name="destination">The destination.</param>
    ''' <param name="compression">Long: Value between 0 and 100.</param>
    Private Sub SaveAsJpeg(image As Image, destination As Stream, compression As Byte)
        Dim eps = New EncoderParameters(1)
        eps.Param(0) = New EncoderParameter(Encoder.Quality, CLng(compression))
        Dim ici = GetEncoderInfo(JpgMimeType)
        image.Save(destination, ici, eps)
    End Sub

    ''' <summary>Save an Image as a Jpeg with a given compression</summary>
    ''' <param name="image">Image: This image</param>
    ''' <param name="destination">The destination.</param>
    <Extension()>
    Public Sub SaveJpeg(image As Image, destination As Stream)
        SaveAsJpeg(image, destination, 100)
    End Sub

    <Extension()>
    Public Sub SaveExtended(image As Image, destination As Stream, format As ImageFormat)
        If image Is Nothing Then Throw New ArgumentNullException(NameOf(image))
        If destination Is Nothing Then Throw New ArgumentNullException(NameOf(destination))
        If format Is Nothing Then Throw New ArgumentNullException(NameOf(format))

        If format Is ImageFormat.Jpeg Then
            image.SaveJpeg(destination)
        Else
            image.Save(destination, format)
        End If
    End Sub

    <Extension()>
    Public Function CloneGifBug(image As Image, imageFormat As ImageFormat) As Image
        If imageFormat Is ImageFormat.Gif Then
            Return image.CloneDeep()
        Else
            Return CType(image.Clone(), Bitmap)
        End If
    End Function

    <Extension()>
    Public Function GetImageStream(image As Image) As MemoryStream
        Return image.GetImageStream(ImageFormat.Png)
    End Function

    <Extension()>
    Public Function GetImageStream(image As Image, format As ImageFormat) As MemoryStream
        Dim ms As MemoryStream
        If format Is ImageFormat.Gif Then
            Dim bytes = image.GetRawData()
            Debug.Assert(bytes IsNot Nothing)
            Debug.Assert(bytes.Length <> 0)
            ms = New MemoryStream(bytes)
        Else
            ms = New MemoryStream()
            If format Is ImageFormat.Jpeg Then
                image.SaveJpeg(ms)
            Else
                image.Save(ms, format)
            End If
        End If
        ms.Seek(0, SeekOrigin.Begin)
        Return ms
    End Function

    <Extension()>
    Public Function IsLargeImage(image As Image) As Boolean
        Return image.Width * image.Height >= 480000
    End Function
End Module
