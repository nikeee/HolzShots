Imports System.Drawing.Drawing2D
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Blur
        Implements ITool(Of BlurSettings)

        ReadOnly _thePen As New Pen(Color.Red) With {.DashStyle = DashStyle.Dash}
        Private Shared ReadOnly CursorInstance As New Cursor(My.Resources.crossMedium.GetHicon())
        Public ReadOnly Property Cursor As Cursor = CursorInstance Implements ITool(Of BlurSettings).Cursor
        Public ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Blur Implements ITool(Of BlurSettings).ToolType

        Private ReadOnly _settingsControl As ISettingsControl(Of BlurSettings)
        Public ReadOnly Property SettingsControl As ISettingsControl(Of BlurSettings) Implements ITool(Of BlurSettings).SettingsControl
            Get
                Return _settingsControl
            End Get
        End Property
        Public Property BeginCoordinates As Point Implements ITool(Of BlurSettings).BeginCoordinates
        Public Property EndCoordinates As Point Implements ITool(Of BlurSettings).EndCoordinates

        Sub New()
            _settingsControl = New BlurSettingsControl(BlurSettings.Default)
        End Sub

        Public Sub LoadInitialSettings() Implements ITool(Of BlurSettings).LoadInitialSettings
            If My.Settings.BlurFactor > BlurSettings.MaximumDiameter OrElse My.Settings.BlurFactor <= BlurSettings.MinimumDiameter Then
                My.Settings.BlurFactor = BlurSettings.Default.Diameter
                My.Settings.Save()
            End If

            With _settingsControl.Settings
                .Diameter = My.Settings.BlurFactor
            End With
        End Sub

        Public Sub PersistSettings() Implements ITool(Of BlurSettings).PersistSettings
            With _settingsControl.Settings
                My.Settings.BlurFactor = .Diameter
            End With
        End Sub

        Public Sub RenderFinalImage(ByRef rawImage As Image, sender As PaintPanel) Implements ITool(Of BlurSettings).RenderFinalImage
            If rawImage Is Nothing Then
                Return
            End If

            Dim rawSrcRect As New Rectangle(
                Math.Min(BeginCoordinates.X, EndCoordinates.X),
                Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
                Math.Abs(BeginCoordinates.X - EndCoordinates.X),
                Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
            )

            If rawSrcRect.X < 0 Then
                rawSrcRect.Width += rawSrcRect.X
                rawSrcRect.X = 0
            End If
            If rawSrcRect.Y < 0 Then
                rawSrcRect.Height += rawSrcRect.Y
                rawSrcRect.Y = 0
            End If

            If rawSrcRect.Width = 0 OrElse rawSrcRect.Height = 0 Then Exit Sub

            Using g As Graphics = Graphics.FromImage(rawImage)
                Dim settings = _settingsControl.Settings

                Using img As Bitmap = BlurImage(rawImage, settings.Diameter, rawSrcRect)
                    If img Is Nothing Then Exit Sub

                    g.FillRectangle(Brushes.White, rawSrcRect)
                    g.CompositingMode = CompositingMode.SourceOver
                    g.DrawImage(img, rawSrcRect)
                End Using
            End Using
        End Sub

        Public Sub RenderPreview(rawImage As Image, g As Graphics) Implements ITool(Of BlurSettings).RenderPreview
            If rawImage Is Nothing Then
                Return
            End If

            Dim rawSrcRectangle As New Rectangle(
                Math.Min(BeginCoordinates.X, EndCoordinates.X),
                Math.Min(BeginCoordinates.Y, EndCoordinates.Y),
                Math.Abs(BeginCoordinates.X - EndCoordinates.X),
                Math.Abs(BeginCoordinates.Y - EndCoordinates.Y)
            )

            If rawSrcRectangle.X < 0 Then
                rawSrcRectangle.Width += rawSrcRectangle.X
                rawSrcRectangle.X = 0
            End If

            If rawSrcRectangle.Y < 0 Then
                rawSrcRectangle.Height += rawSrcRectangle.Y
                rawSrcRectangle.Y = 0
            End If

            If rawSrcRectangle.Width = 0 OrElse rawSrcRectangle.Height = 0 Then Exit Sub

            Dim settings = _settingsControl.Settings

            Using img As Bitmap = BlurImage(rawImage, settings.Diameter, rawSrcRectangle)
                If img Is Nothing Then Exit Sub

                g.CompositingMode = CompositingMode.SourceOver
                g.FillRectangle(Brushes.White, rawSrcRectangle)
                g.DrawImage(img, rawSrcRectangle)
            End Using
            g.DrawRectangle(_thePen, rawSrcRectangle)
        End Sub

        Private Shared Function BlurImage(img As Image, factor As Integer, rawSrcRect As Rectangle) As Bitmap
            Dim width As Integer = CInt(Math.Round(Math.Ceiling(rawSrcRect.Width / factor)))
            Dim height As Integer = CInt(Math.Round(Math.Ceiling(rawSrcRect.Height / factor)))

            If width <= 0 OrElse height <= 0 Then Return Nothing

            Using smallBitmap As New Bitmap(width, height)
                Using g As Graphics = Graphics.FromImage(smallBitmap)
                    g.CompositingMode = CompositingMode.SourceOver
                    g.InterpolationMode = InterpolationMode.HighQualityBilinear
                    g.DrawImage(
                        img,
                        New Rectangle(0, 0, smallBitmap.Width, smallBitmap.Height),
                        rawSrcRect,
                        GraphicsUnit.Pixel
                    )
                End Using

                Using secondBitmap As New Bitmap(rawSrcRect.Width, rawSrcRect.Height)
                    Using g As Graphics = Graphics.FromImage(secondBitmap)
                        g.CompositingMode = CompositingMode.SourceOver
                        g.InterpolationMode = InterpolationMode.HighQualityBilinear
                        g.DrawImage(
                            smallBitmap,
                            New Rectangle(0, 0, rawSrcRect.Width + factor, rawSrcRect.Height + factor),
                            New Rectangle(0, 0, smallBitmap.Width, smallBitmap.Height),
                            GraphicsUnit.Pixel
                        )
                    End Using
                    Return DirectCast(secondBitmap.Clone(), Bitmap)
                End Using
            End Using
        End Function
        Public Sub MouseOnlyMoved(rawImage As Image, ByRef currentCursor As Cursor, e As MouseEventArgs) Implements ITool(Of BlurSettings).MouseOnlyMoved
            ' Nothing to do here
        End Sub
        Public Sub MouseClicked(rawImage As Image, e As Point, ByRef currentCursor As Cursor, trigger As Control) Implements ITool(Of BlurSettings).MouseClicked
            ' Nothing to do here
        End Sub
        Public Sub Dispose() Implements ITool(Of BlurSettings).Dispose
        End Sub
    End Class
End Namespace
