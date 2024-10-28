Imports HolzShots.Drawing.Tools.UI

Namespace Drawing.Tools
    Friend Class Scale
        Implements IDialogTool

        Public Sub ShowToolDialog(ByRef rawImage As Image, screenshot As Screenshot, parent As IWin32Window) Implements IDialogTool.ShowToolDialog

            Using s As New ScaleWindow(rawImage)

                If s.ShowDialog(parent) <> DialogResult.OK Then
                    Return
                End If

                Dim width = s.WidthBoxV
                Dim height = s.HeightBoxV

                Dim unit As ScaleWindow.ScaleUnit = s.CurrentScaleUnit

                Dim newWidth As Integer = CInt(width)
                Dim newHeight As Integer = CInt(height)

                ' This is wrong and may fail if there was no cursor position
                ' We ignore this for now, since we're not caring about legacy stuff
                Dim newCursorCoordinates As Point = screenshot.CursorPosition.OnImage

                Dim newCursorWidth As Integer = My.Resources.windowsCursorMedium.Width
                Dim newCursorHeight As Integer = My.Resources.windowsCursorMedium.Height

                If unit = ScaleWindow.ScaleUnit.Percent Then
                    newWidth = CInt(rawImage.Width * (width / 100))
                    newHeight = CInt(rawImage.Height * (height / 100))
                    newCursorCoordinates.X = CInt(newCursorCoordinates.X * (height / 100))
                    newCursorCoordinates.Y = CInt(newCursorCoordinates.Y * (width / 100))
                    newCursorWidth = CInt(newCursorWidth * (width / 100))
                    newCursorHeight = CInt(newCursorHeight * (height / 100))
                End If

                Dim newRawImage As New Bitmap(newWidth, newHeight)
                Using g As Graphics = Graphics.FromImage(newRawImage)
                    g.DrawImage(rawImage, 0, 0, newWidth, newHeight)
                End Using

                rawImage = newRawImage
            End Using
        End Sub
        Public Sub LoadInitialSettings() Implements IDialogTool.LoadInitialSettings
            ' Nothing to do here
        End Sub
        Public Sub PersistSettings() Implements IDialogTool.PersistSettings
            ' Nothing to do here
        End Sub

        Public Sub Dispose() Implements IDialogTool.Dispose
        End Sub
    End Class
End Namespace
