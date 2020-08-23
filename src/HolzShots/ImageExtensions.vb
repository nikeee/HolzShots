Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.CompilerServices
Imports HolzShots.Drawing

Module ImageExtensions

    <Extension()>
    Public Sub SaveExtended(image As Image, destination As Stream, format As ImageFormat)
        If image Is Nothing Then Throw New ArgumentNullException(NameOf(image))
        If destination Is Nothing Then Throw New ArgumentNullException(NameOf(destination))
        If format Is Nothing Then Throw New ArgumentNullException(NameOf(format))

        If format Is ImageFormat.Jpeg Then
            ImageFormatInformation.SaveAsJpeg(image, destination)
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
    Public Function GetImageStream(image As Image, format As ImageFormat) As MemoryStream

        If format Is ImageFormat.Gif Then
            Dim bytes = image.GetRawData()
            Debug.Assert(bytes IsNot Nothing)
            Debug.Assert(bytes.Length <> 0)
            Dim gifStream = New MemoryStream(bytes)
            gifStream.Seek(0, SeekOrigin.Begin)
            Return gifStream
        End If

        Dim ms = New MemoryStream()
        If format Is ImageFormat.Jpeg Then
            ImageFormatInformation.SaveAsJpeg(image, ms)
        Else
            image.Save(ms, format)
        End If
        ms.Seek(0, SeekOrigin.Begin)
        Return ms
    End Function

    <Extension()>
    Public Function ShouldMaximizeEditorWindowForImage(image As Image) As Boolean
        Return image.Width * image.Height >= 480000
    End Function
End Module
