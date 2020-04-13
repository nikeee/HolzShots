Imports System.Drawing.Drawing2D
Imports HolzShots.UI.Controls
Imports HolzShots.UI.Dialogs

Namespace Drawing.Tools
    Friend Class Pipette
        Inherits Tool
        Implements IDisposable
        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Pipette

        Public Overrides ReadOnly Property Cursor As Cursor = Cursors.Cross

        Public Overrides Sub MouseOnlyMoved(ByVal rawImage As Image, ByRef currentCursor As Cursor, ByVal e As MouseEventArgs)
            Debug.Assert(TypeOf rawImage Is Bitmap)
            Dim rawBmp = If(TypeOf rawImage Is Bitmap, DirectCast(rawImage, Bitmap), New Bitmap(rawImage))
            If New Rectangle(0, 0, rawImage.Width, rawImage.Height).Contains(e.Location) Then
                'currentCursor.Dispose()
                currentCursor = New Cursor(DrawCursor(rawBmp.GetPixel(e.X, e.Y)).Handle)
            End If
        End Sub

        Private Shared ReadOnly PipettenCursor As New Bitmap(195, 195)
        Private Shared ReadOnly PenWhite As Pen = New Pen(Color.FromArgb(180, 255, 255, 255))

        Private Shared ReadOnly CursorRectangle As New Rectangle(10, 10, PipettenCursor.Width - 20, PipettenCursor.Height - 20)

        Private _cursorImage As New Bitmap(28, 28)
        Private ReadOnly _cursorPen As New Pen(Brushes.Black)

        Private Function DrawCursor(ByVal c As Color) As Icon
            _cursorImage = New Bitmap(195, 195)
            _cursorImage.MakeTransparent()
            Dim cursorGraphics As Graphics = Graphics.FromImage(_cursorImage)

            cursorGraphics.SmoothingMode = SmoothingMode.AntiAlias
            _cursorPen.Width = 1
            _cursorPen.Color = c

            cursorGraphics.DrawLine(PenWhite, 10, CInt(195 / 2) - 1, CInt(195 / 2) - 1, CInt(195 / 2) - 1)
            cursorGraphics.DrawLine(PenWhite, CInt(195 / 2), CInt(195 / 2) - 1, 190, CInt(195 / 2) - 1)
            cursorGraphics.DrawLine(PenWhite, CInt(195 / 2) - 1, 10, CInt(195 / 2) - 1, CInt(195 / 2) - 2)

            _cursorPen.Width = 18

            cursorGraphics.DrawEllipse(_cursorPen, CursorRectangle)
            cursorGraphics.Dispose()

            Dim hIcon As IntPtr = _cursorImage.GetHicon()
            Dim temp As Icon = Icon.FromHandle(hIcon)
            Dim ico As Icon = DirectCast(temp.Clone(), Icon)
            temp.Dispose()
            Interop.NativeMethods.DestroyIcon(hIcon)
            Return ico
        End Function

        Public Overrides Sub MouseClicked(ByVal rawImage As Image, ByVal e As Point, ByRef currentCursor As Cursor, ByVal trigger As Control)
            Debug.Assert(TypeOf rawImage Is Bitmap)
            Dim rawBmp = If(TypeOf rawImage Is Bitmap, DirectCast(rawImage, Bitmap), New Bitmap(rawImage))
            Dim c As Color = rawBmp.GetPixel(e.X, e.Y)
            Dim viewer As New PipettenColorViewer(c, trigger.PointToScreen(e))
            viewer.Show()
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            _cursorImage.Dispose()
            _cursorPen.Dispose()
        End Sub
    End Class
End Namespace
