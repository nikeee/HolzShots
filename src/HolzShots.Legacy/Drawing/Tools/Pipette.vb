Imports System.Drawing.Drawing2D
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls
Imports HolzShots.Windows.Forms

Namespace Drawing.Tools
    Friend NotInheritable Class Pipette
        Implements ITool(Of ToolSettingsBase)
        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Pipette Implements ITool(Of ToolSettingsBase).ToolType

        Public ReadOnly Property Cursor As Cursor = Cursors.Cross Implements ITool(Of ToolSettingsBase).Cursor
        Public ReadOnly Property SettingsControl As ISettingsControl(Of ToolSettingsBase) = Nothing Implements ITool(Of ToolSettingsBase).SettingsControl

        Public Property BeginCoordinates As Point Implements ITool(Of ToolSettingsBase).BeginCoordinates
        Public Property EndCoordinates As Point Implements ITool(Of ToolSettingsBase).EndCoordinates

        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of ToolSettingsBase).MouseOnlyMoved
            Debug.Assert(TypeOf rawImage Is Bitmap)
            Dim rawBmp = If(TypeOf rawImage Is Bitmap, DirectCast(rawImage, Bitmap), New Bitmap(rawImage))
            If New Rectangle(0, 0, rawImage.Width, rawImage.Height).Contains(e.Location) Then
                'currentCursor.Dispose()
                currentCursor = New Cursor(DrawCursor(rawBmp.GetPixel(e.X, e.Y)).Handle)
            End If
        End Sub

        Private Shared ReadOnly PipettenCursor As New Bitmap(195, 195)
        Private Shared ReadOnly PenWhite As New Pen(Color.FromArgb(180, 255, 255, 255))

        Private Shared ReadOnly CursorRectangle As New Rectangle(10, 10, PipettenCursor.Width - 20, PipettenCursor.Height - 20)

        Private _cursorImage As New Bitmap(28, 28)
        Private ReadOnly _cursorPen As New Pen(Brushes.Black)

        Public Sub LoadInitialSettings() Implements ITool(Of ToolSettingsBase).LoadInitialSettings
            ' Nothing to do here
        End Sub

        Public Sub PersistSettings() Implements ITool(Of ToolSettingsBase).PersistSettings
            ' Nothing to do here
        End Sub

        Private Function DrawCursor(c As Color) As Icon
            _cursorImage = New Bitmap(195, 195)
            _cursorImage.MakeTransparent()
            Using cursorGraphics As Graphics = Graphics.FromImage(_cursorImage)

                cursorGraphics.SmoothingMode = SmoothingMode.AntiAlias
                _cursorPen.Width = 1
                _cursorPen.Color = c

                cursorGraphics.DrawLine(PenWhite, 10, CInt(195 / 2) - 1, CInt(195 / 2) - 1, CInt(195 / 2) - 1)
                cursorGraphics.DrawLine(PenWhite, CInt(195 / 2), CInt(195 / 2) - 1, 190, CInt(195 / 2) - 1)
                cursorGraphics.DrawLine(PenWhite, CInt(195 / 2) - 1, 10, CInt(195 / 2) - 1, CInt(195 / 2) - 2)

                _cursorPen.Width = 18

                cursorGraphics.DrawEllipse(_cursorPen, CursorRectangle)

            End Using

            Dim hIcon As IntPtr = _cursorImage.GetHicon()

            Dim ico As Icon
            Using temp As Icon = Icon.FromHandle(hIcon)
                ico = DirectCast(temp.Clone(), Icon)
            End Using

            Native.User32.DestroyIcon(hIcon)
            Return ico
        End Function



        Public Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of ToolSettingsBase).MouseClicked
            Debug.Assert(TypeOf rawImage Is Bitmap)
            Dim rawBmp = If(TypeOf rawImage Is Bitmap, DirectCast(rawImage, Bitmap), New Bitmap(rawImage))
            Dim c As Color = rawBmp.GetPixel(e.X, e.Y)
            Dim viewer As New CopyColorForm(c, trigger.PointToScreen(e))
            viewer.Show()
        End Sub

        Protected Sub Dispose() Implements ITool(Of ToolSettingsBase).Dispose
            _cursorImage.Dispose()
            _cursorPen.Dispose()
        End Sub

        Public Sub RenderFinalImage(ByRef rawImage As Image) Implements ITool(Of ToolSettingsBase).RenderFinalImage
            ' Nothing to do here
        End Sub
        Public Sub RenderPreview(rawImage As Image, g As Graphics) Implements ITool(Of ToolSettingsBase).RenderPreview
            ' Nothing to do here
        End Sub
    End Class
End Namespace
