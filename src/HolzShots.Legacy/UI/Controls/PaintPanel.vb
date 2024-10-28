Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports HolzShots.Drawing.Tools
Imports HolzShots.Drawing.Tools.UI

Namespace UI.Controls
    Friend Class PaintPanel

        Private _currentTool As ShotEditorTool
        Private _screenshot As Screenshot

        Friend Event Initialized()
        Friend Event UpdateMousePosition(e As Point)

        Private _drawCursor As Boolean

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property DrawCursor As Boolean
            Get
                Return _drawCursor
            End Get
            Set(value As Boolean)
                _drawCursor = value
                RawBox.Invalidate()
            End Set
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property Screenshot As Screenshot
            Get
                Return If(DesignMode, Nothing, _screenshot)
            End Get
            Set(value As Screenshot)
                If Not DesignMode Then
                    _screenshot = value
                End If
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public ReadOnly Property CurrentImage As Image
            Get
                Return If(DesignMode, Nothing, If(_undoStack.Count > 0, _undoStack.Peek(), Screenshot.Image))
            End Get
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public ReadOnly Property CombinedImage As Image
            Get
                If Not DesignMode Then
                    Dim bmp As Image = RawBox.Image
                    If _drawCursor Then
                        Using g As Graphics = Graphics.FromImage(bmp)
                            g.SmoothingMode = SmoothingMode.AntiAlias
                            g.CompositingQuality = CompositingQuality.HighQuality
                            g.DrawImage(HolzShots.My.Resources.windowsCursorMedium, Screenshot.CursorPosition.OnImage)
                        End Using
                    End If
                    Return bmp
                End If
                Return Nothing
            End Get
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property CurrentTool As ShotEditorTool
            Get
                Return _currentTool
            End Get
            Set(value As ShotEditorTool)
                _currentTool = value
                Select Case value
                    Case ShotEditorTool.None
                        Cursor = Cursors.Default
                    Case ShotEditorTool.Text
                        CurrentToolObject = Nothing
                        Cursor = New Cursor(My.Resources.textCursor.Handle)
                    Case ShotEditorTool.Crop
                        CurrentToolObject = New Crop()
                    Case ShotEditorTool.Marker
                        CurrentToolObject = New Marker()
                    Case ShotEditorTool.Censor
                        CurrentToolObject = New Censor()
                    Case ShotEditorTool.Eraser
                        CurrentToolObject = New Eraser(Me)
                    Case ShotEditorTool.Blur
                        CurrentToolObject = New Blur
                    Case ShotEditorTool.Ellipse
                        CurrentToolObject = New Ellipse
                    Case ShotEditorTool.Pipette
                        CurrentToolObject = New Pipette
                    Case ShotEditorTool.Brighten
                        CurrentToolObject = New Brighten
                    Case ShotEditorTool.Arrow
                        CurrentToolObject = New Arrow
                    Case ShotEditorTool.Scale
                        CurrentToolObject = New Scale
                        InvokeFinalRender(CurrentToolObject)
                        CurrentToolObject = Nothing
                End Select
            End Set
        End Property


        Private Event CurrentToolChanged(sender As Object, tool As ITool(Of ToolSettingsBase))
        Private _currentToolObject As ITool(Of ToolSettingsBase)

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property CurrentToolObject As ITool(Of ToolSettingsBase)
            Get
                Return _currentToolObject
            End Get
            Set(value As ITool(Of ToolSettingsBase))
                If value IsNot _currentToolObject Then
                    _currentToolObject = value
                    RaiseEvent CurrentToolChanged(Me, _currentToolObject)
                End If
            End Set
        End Property

        Private Sub CurrentToolChanged_Event(sender As Object, tool As ITool(Of ToolSettingsBase)) Handles Me.CurrentToolChanged
            If tool IsNot Nothing Then
                Cursor = tool.Cursor
            End If
        End Sub

        Public Enum ShotEditorTool
            None = 0
            Censor = 1
            Marker = 2
            Text = 4
            Crop = 8
            Arrow = 16
            Eraser = 32
            Blur = 64
            Ellipse = 128
            Pipette = 256
            Brighten = 512
            Scale = 1024
            LegacyNew = 2048
        End Enum

        Public Sub Initialize(shot As Screenshot)

            _screenshot = shot

            RawBox.BringToFront()
            RawBox.SizeMode = PictureBoxSizeMode.AutoSize
            RawBox.Location = New Point(0, 0)

            UpdateRawBox()

            RawBox.Focus()

            RaiseEvent Initialized()
        End Sub

        Private Sub InvokeFinalRender(tool As ITool(Of ToolSettingsBase))
            Dim img = DirectCast(CurrentImage.Clone(), Image)
            Debug.Assert(tool IsNot Nothing)
            tool.RenderFinalImage(img, Me)
            _undoStack.Push(img)
            UpdateRawBox()
        End Sub

        Private Sub UpdateRawBox()
            RawBox.Image = CurrentImage
            RawBox.Invalidate()
        End Sub

        Private Class Localization
            Private Sub New()
            End Sub
            Public Const SizeInfo = "{0}x{1}px, creation date: {2}"
            Public Const SizeInfoText = "The image is {0}-by-{1}px. Left click to copy the image size. Right click to copy creation date. Middle click to copy both."
        End Class

        Public ReadOnly Property SizeInfo As String
            Get
                Return String.Format(Localization.SizeInfo, _screenshot.Size.Width, _screenshot.Size.Height, _screenshot.Timestamp)
            End Get
        End Property

        Public ReadOnly Property SizeInfoText As String
            Get
                Return String.Format(Localization.SizeInfoText, _screenshot.Size.Width, _screenshot.Size.Height)
            End Get
        End Property


#Region "DrawLayer"

        Private Sub DrawBoxMouseClick(sender As Object, e As MouseEventArgs) Handles RawBox.MouseClick
            CurrentToolObject?.MouseClicked(CurrentImage, e.Location, Cursor, Me)
        End Sub

        Private Sub MouseLayerMouseDown(sender As Object, e As MouseEventArgs) Handles RawBox.MouseDown
            _mousedown = True
            If _currentTool = ShotEditorTool.None OrElse _currentTool = ShotEditorTool.Text Then Exit Sub

            CurrentToolObject.BeginCoordinates = e.Location
        End Sub

        Private Sub RawBoxMouseEnter(sender As Object, e As EventArgs) Handles RawBox.MouseEnter
            If Not RawBox.Focused Then RawBox.Focus()
        End Sub

        Private Sub MouseLayerMouseMove(sender As Object, e As MouseEventArgs) Handles RawBox.MouseMove
            If e.Button = MouseButtons.Left Then

                If CurrentToolObject IsNot Nothing Then
                    CurrentToolObject.EndCoordinates = e.Location
                    RawBox.Invalidate()
                End If

            End If

            SaveLinealStuff(e)

            If Not RawBox.Focused Then RawBox.Focus()
        End Sub

        Private Sub MouseLayerMouseUp(sender As Object, e As MouseEventArgs) Handles RawBox.MouseUp
            If e.Button = MouseButtons.Left Then
                If CurrentTool = ShotEditorTool.Text Then
                    TextPanel.BringToFront()
                    TextPanel.Location = New Point(e.Location.X + RawBox.Location.X, e.Location.Y + RawBox.Location.Y)
                    TextPanel.Visible = True
                ElseIf CurrentToolObject IsNot Nothing AndAlso CurrentToolObject.GetType IsNot GetType(Scale) Then
                    If TypeOf CurrentToolObject IsNot Pipette Then
                        InvokeFinalRender(CurrentToolObject)
                    Else
                        CurrentToolObject.RenderFinalImage(CurrentImage, Me)
                    End If
                End If
                _mousedown = False
            End If
        End Sub

        Private _mousedown As Boolean = False

        Private Sub RawBoxPaint(sender As Object, e As PaintEventArgs) Handles RawBox.Paint
            If _mousedown = True Then
                If _currentToolObject IsNot Nothing Then
                    CurrentToolObject.RenderPreview(CType(RawBox.Image, Bitmap), e.Graphics)
                    Exit Sub
                End If
            End If
            If _drawCursor AndAlso Screenshot IsNot Nothing Then
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality
                'e.Graphics.DrawImage(Screenshot.Cursor, Screenshot.CursorCoordinates)
            End If
        End Sub

#End Region

        Private ReadOnly _undoStack As New Stack(Of Image)

        Public Sub Undo()
            If _undoStack.Count > 0 Then
                _undoStack.Pop()
                UpdateRawBox()
                GC.Collect()
            End If
        End Sub

        Private Sub PaintPanelDisposed() Handles Me.Disposed
            For Each i In _undoStack
                Try
                    i.Dispose()
                Catch ex As Exception
                    Debugger.Break()
                    Debug.Fail("Failed to dispose undoStack")
                End Try
            Next
        End Sub

        Private Sub PaintPanelLoad() Handles Me.Load
            BackColor = Color.FromArgb(207, 217, 231)
            RawBox.Focus()
        End Sub

        Private Sub TextOkClick() Handles text_ok.Click
            TextPanel.Visible = False

            Using rtb As New RichTextBox With {
                    .Location = RawBox.Location,
                    .Parent = RawBox.Parent,
                    .Font = RawBox.Font,
                    .BorderStyle = BorderStyle.None
                }

                Dim charLocation = rtb.GetPositionFromCharIndex(0)

                Dim location As New Point(TextPanel.Location.X - charLocation.X, TextPanel.Location.Y - charLocation.Y)

                Dim img = DirectCast(CurrentImage.Clone(), Image)

                Using g As Graphics = Graphics.FromImage(img)
                    g.TextRenderingHint = TextRenderingHint.AntiAlias
                    g.DrawString(TextInput.Text, TextInput.Font, New SolidBrush(TextInput.ForeColor), location)
                End Using

                _undoStack.Push(img)
                UpdateRawBox()
                RawBox.Focus()
            End Using
        End Sub

#Region "TextPanel"

        Dim _moverMouseDown As Boolean = False
        Dim _dragPointMover As Point
        Dim _startPOsitionMover As Point

        Private Sub PictureBox2MouseDown(sender As Object, e As MouseEventArgs) Handles MoverBox.MouseDown
            If e.Button = MouseButtons.Left Then
                _moverMouseDown = True
                _startPOsitionMover = New Point(TextPanel.Location.X + EckenTeil.Location.X, TextPanel.Location.Y + EckenTeil.Location.Y)
                _dragPointMover = EckenTeil.PointToScreen(New Point(TextPanel.Location.X + e.X, TextPanel.Location.Y + e.Y))
            End If
        End Sub

        Private Sub PictureBox2MouseMove(sender As Object, e As MouseEventArgs) Handles MoverBox.MouseMove
            If _moverMouseDown = True Then
                Dim nCurPos As Point = EckenTeil.PointToScreen(New Point(TextPanel.Location.X + e.X, TextPanel.Location.Y + e.Y))
                TextPanel.Location = New Point(_startPOsitionMover.X + nCurPos.X - _dragPointMover.X, _startPOsitionMover.Y + nCurPos.Y - _dragPointMover.Y)
            End If
        End Sub

        Private Sub PictureBox2MouseUp(sender As Object, e As MouseEventArgs) Handles MoverBox.MouseUp
            _moverMouseDown = False
        End Sub

        Private Sub ChangeFontClick(sender As Object, e As EventArgs) Handles ChangeFont.Click
            If TheFontDialog.ShowDialog() = DialogResult.OK Then
                TextInput.Font = TheFontDialog.Font
                TextInput.ForeColor = TheFontDialog.Color
            End If
        End Sub

        Private Sub TextPanelVisibleChanged(sender As Object, e As EventArgs) Handles TextPanel.VisibleChanged
            ChangeFont.Parent = tools_bg
            ChangeFont.Location = New Point(3, 2)

            MoverBox.Parent = tools_bg
            MoverBox.Location = New Point(178, 2)

            SelectAll.Parent = tools_bg
            SelectAll.Location = New Point(41, 2)

            InsertDate.Parent = tools_bg
            InsertDate.Location = New Point(75, 2)

            CancelButton.Parent = tools_bg
            CancelButton.Location = New Point(117, 2)
        End Sub

        Private Sub SelectAllClick(sender As Object, e As EventArgs) Handles SelectAll.Click
            TextInput.Focus()
            TextInput.SelectAll()
        End Sub

        Private Sub CancelButtonClick(sender As Object, e As EventArgs) Handles CancelButton.Click
            TextPanel.Visible = False
        End Sub

        Private Sub InsertDateClick(sender As Object, e As EventArgs) Handles InsertDate.Click
            TextInput.Paste(_screenshot.Timestamp.ToString())
        End Sub

#End Region

#Region "Ruler"

        Private Sub SaveLinealStuff(e As MouseEventArgs)

            Dim hRect = New Rectangle(If(_currentMousePosition.X < e.X, _currentMousePosition.X, e.X) - 1, 0, Math.Abs(_currentMousePosition.X - e.X) + 2, HorizontalLinealBox.DisplayRectangle.Height)
            Dim vRect = New Rectangle(0, If(_currentMousePosition.Y < e.Y, _currentMousePosition.Y, e.Y) - 1, VerticalLinealBox.DisplayRectangle.Width, Math.Abs(_currentMousePosition.Y - e.Y) + 2)

            _currentMousePosition = e.Location

            HorizontalLinealBox.Invalidate(hRect, False)
            VerticalLinealBox.Invalidate(vRect, False)

            RaiseEvent UpdateMousePosition(e.Location)

            If CurrentTool = ShotEditorTool.Pipette Then
                CurrentToolObject.MouseOnlyMoved(CurrentImage, Cursor, e)
            End If
        End Sub

        Dim _currentMousePosition As Point
        Private Shared ReadOnly RulerFont As New Font("Verdana", 8, FontStyle.Regular)
        Private Shared ReadOnly FontBrush As New SolidBrush(Color.FromArgb(255, 51, 75, 106)) '(255, 51, 75, 106))
        Private Shared ReadOnly LinearBackgroundBrush As New SolidBrush(Color.FromArgb(255, 241, 243, 248)) '(255, 240, 241, 249))
        Private Shared ReadOnly LinePen As New Pen(Color.FromArgb(255, 142, 156, 175)) '(255, 137, 146, 179))

        Private Sub HorizontalLinealBoxPaint(sender As Object, e As PaintEventArgs) Handles HorizontalLinealBox.Paint
            With e.Graphics
                .TextRenderingHint = TextRenderingHint.AntiAliasGridFit
                .SmoothingMode = SmoothingMode.HighSpeed
                .PixelOffsetMode = PixelOffsetMode.HighSpeed
                .CompositingQuality = CompositingQuality.HighSpeed

                .FillRectangle(LinearBackgroundBrush, HorizontalLinealBox.DisplayRectangle)
                .DrawLine(LinePen, 0, HorizontalLinealBox.DisplayRectangle.Height - 2, HorizontalLinealBox.DisplayRectangle.Width - 1, HorizontalLinealBox.DisplayRectangle.Height - 2)

                Dim xPos As Integer
                Dim offset = WholePanel.HorizontalScroll.Value

                For i As Integer = 0 To WholePanel.Width + offset Step 10
                    xPos = i - offset
                    If xPos < 0 OrElse xPos > WholePanel.Width + offset Then Continue For

                    If i Mod 100 = 0 Then
                        .DrawLine(LinePen, xPos, 0, xPos, HorizontalLinealBox.Height - 2)
                        .DrawString(i.ToString, RulerFont, FontBrush, xPos, 0)
                    Else
                        .DrawLine(LinePen, xPos, HorizontalLinealBox.Height - 6, xPos, HorizontalLinealBox.Height - 2)
                    End If
                Next

                .DrawLine(Pens.Red, _currentMousePosition.X - offset, 0, _currentMousePosition.X - offset, 20)
                .DrawLine(BorderLinePen, 0, 0, Width, 0)
            End With
        End Sub
        Private Sub VerticalLinealBoxPaint(sender As Object, e As PaintEventArgs) Handles VerticalLinealBox.Paint
            With e.Graphics
                .TextRenderingHint = TextRenderingHint.AntiAliasGridFit
                .SmoothingMode = SmoothingMode.HighSpeed
                .PixelOffsetMode = PixelOffsetMode.HighSpeed
                .CompositingQuality = CompositingQuality.HighSpeed

                .FillRectangle(LinearBackgroundBrush, VerticalLinealBox.DisplayRectangle)
                .DrawLine(LinePen, VerticalLinealBox.DisplayRectangle.Width - 2, 0, VerticalLinealBox.DisplayRectangle.Width - 2, VerticalLinealBox.DisplayRectangle.Height - 1)

                Dim yPos As Integer
                Dim offset = WholePanel.VerticalScroll.Value

                For i As Integer = 0 To WholePanel.Height + offset Step 10
                    yPos = i - offset
                    If yPos < 0 OrElse yPos > WholePanel.Height + offset Then Continue For

                    If i Mod 100 = 0 Then
                        .DrawLine(LinePen, 0, yPos, VerticalLinealBox.Width - 2, yPos)
                        .TranslateTransform(0, yPos)
                        .RotateTransform(-90)
                        .DrawString(i.ToString(CurrentCulture), RulerFont, FontBrush, - .MeasureString(i.ToString(CurrentCulture), RulerFont, 100).Width, 0)
                        .ResetTransform()
                    Else
                        .DrawLine(LinePen, VerticalLinealBox.Width - 6, yPos, VerticalLinealBox.Width - 2, yPos)
                    End If
                Next

                .DrawLine(Pens.Red, 0, _currentMousePosition.Y - offset, 20, _currentMousePosition.Y - offset)
            End With
        End Sub

#End Region

        Private Sub WholePanelScroll(sender As Object, e As ScrollEventArgs) Handles WholePanel.Scroll
            If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
                HorizontalLinealBox.Invalidate()
            Else
                VerticalLinealBox.Invalidate()
            End If
        End Sub

        Private Sub EckenTeilPaint(sender As Object, e As PaintEventArgs) Handles EckenTeil.Paint
            e.Graphics.DrawRectangle(LinePen, EckenTeil.Width - 2, EckenTeil.Height - 2, 2, 2)
            e.Graphics.DrawLine(BorderLinePen, 0, 0, Width, 0)
        End Sub

        Private Shared ReadOnly BorderLinePen As New Pen(Color.FromArgb(218, 219, 220))

    End Class
End Namespace
