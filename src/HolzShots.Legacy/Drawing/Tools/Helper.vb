Namespace Drawing.Tools
    Friend Module Helpers

        Public Function CreateThumbnail(bmp As Bitmap) As Bitmap
            'r = w/h
            'h = w/r
            'w = h*r

            If bmp.Width <= 100 AndAlso bmp.Height <= 100 Then
                Return bmp
            Else
                Dim ratio As Double = bmp.Width / bmp.Height

                Dim newWidth As Integer
                Dim newHeight As Integer

                If bmp.Width >= bmp.Height Then
                    newWidth = 100
                    newHeight = CInt(newWidth / ratio)
                Else
                    newHeight = 100
                    newWidth = CInt(newHeight * ratio)
                End If

                Dim thumb As New Bitmap(newWidth, newHeight)
                thumb.MakeTransparent()
                Using g As Graphics = Graphics.FromImage(thumb)
                    g.DrawImage(bmp, 0, 0, newWidth, newHeight)
                End Using
                Return thumb
            End If
        End Function
    End Module
End Namespace
