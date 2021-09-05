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

                Dim newwidth As Integer
                Dim newheight As Integer

                If bmp.Width >= bmp.Height Then
                    newwidth = 100
                    newheight = CInt(newwidth / ratio)
                Else
                    newheight = 100
                    newwidth = CInt(newheight * ratio)
                End If

                Dim thumb As New Bitmap(newwidth, newheight)
                thumb.MakeTransparent()
                Dim g As Graphics = Graphics.FromImage(thumb)
                g.DrawImage(bmp, 0, 0, newwidth, newheight)
                g.Dispose()
                Return thumb
            End If
        End Function
    End Module
End Namespace
