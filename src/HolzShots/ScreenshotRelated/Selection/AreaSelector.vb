Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports HolzShots.Input

Namespace ScreenshotRelated.Selection
    Friend NotInheritable Class AreaSelector
        Inherits System.Windows.Forms.Form ' TODO: Enhance?
        Implements IDisposable
        Implements IAreaSelector

        Private _areaTcs As TaskCompletionSource(Of Rectangle)

        Private _lastPosition As Point
        Private _firstInvalidation As Boolean = True

        Private _drawMangifier As Boolean = False
        Private ReadOnly _magnifier As New MagnifierDecoration

        Private ReadOnly _backgroundOverlayBrush As Brush

        Private _wholeScreen As Image
        Private _resizeSelection As Boolean = False
        Private _moveSelection As Boolean = False
        Private _currentSelection As Rectangle
        Private _previousSelection As Rectangle
        Private _firstCoordinates As Point
        Private _secondCoordinates As Point

        Private _previousQuickInfoLocation As Rectangle
        Private Shared ReadOnly SelectionBorderPen As Pen = New Pen(Color.FromArgb(120, 255, 255, 255), 1.0)
        Private Shared ReadOnly MagnifierBorderPen As Pen = New Pen(Color.FromArgb(120, 255, 0, 0), 1.0)

        Private ReadOnly _fpsWatch As New Global.HolzShots.Input.Selection.FpsStopWatch

        Friend Shared Property IsInAreaSelector As Boolean

        Sub New(settingsContext As HSSettings)
            MyBase.New()
            SuspendLayout()

            DoubleBuffered = True
            ShowInTaskbar = False

            Opacity = 1.0
            _backgroundOverlayBrush = New SolidBrush(Color.FromArgb(CInt(settingsContext.AreaSelectorDimmingOpacity * 255), 0, 0, 0))

            StartPosition = FormStartPosition.Manual
            Bounds = SystemInformation.VirtualScreen
            WindowState = FormWindowState.Normal
            FormBorderStyle = FormBorderStyle.None
#If Not DEBUG Then
            TopMost = True
#End If
            Icon = Nothing

            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint, True)

            ResumeLayout(False)
        End Sub

        Private Sub ApproveSelection()
            If _currentSelection.Width < 1 OrElse _currentSelection.Height < 1 Then
                CancelSelection()
                Return
            End If

            CloseInternal()
            _areaTcs.TrySetResult(_currentSelection)
        End Sub
        Private Sub CancelSelection()
            _wholeScreen.Dispose()
            CloseInternal()
            _areaTcs.TrySetCanceled()
        End Sub
        Private Sub CloseInternal()
            Visible = False
            IsInAreaSelector = False
            Close()
        End Sub

        Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
            If e.KeyCode = Keys.Escape Then
                CancelSelection()
            Else
                Invalidate(False)
            End If
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            If e.Button = MouseButtons.Left Then
                _currentSelection = New Rectangle(e.X, e.Y, 0, 0)

                _firstCoordinates = e.Location
                _secondCoordinates = e.Location

                _resizeSelection = True

            ElseIf e.Button = MouseButtons.Right Then
                _moveSelection = True
            End If
        End Sub

        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            If _resizeSelection AndAlso e.Button = MouseButtons.Left Then
                _resizeSelection = False
            ElseIf _moveSelection AndAlso e.Button = MouseButtons.Right Then
                _moveSelection = False
            End If

            If Not _moveSelection AndAlso Not _resizeSelection Then
                ApproveSelection()
            End If
        End Sub

        Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
            If Not _resizeSelection AndAlso Not _moveSelection OrElse _lastPosition = e.Location Then Return
            _lastPosition = e.Location

            If _moveSelection Then

                Dim deltaX As Integer = _secondCoordinates.X - e.X
                Dim deltaY As Integer = _secondCoordinates.Y - e.Y
                _firstCoordinates = New Point(_firstCoordinates.X - deltaX, _firstCoordinates.Y - deltaY)
                _secondCoordinates = New Point(_secondCoordinates.X - deltaX, _secondCoordinates.Y - deltaY)

            ElseIf _resizeSelection Then
                _secondCoordinates = e.Location
            End If

            _currentSelection.X = Math.Max(Math.Min(_secondCoordinates.X, _firstCoordinates.X), 0)
            _currentSelection.Y = Math.Max(Math.Min(_secondCoordinates.Y, _firstCoordinates.Y), 0)

            _currentSelection.Width = Math.Abs(_firstCoordinates.X - _secondCoordinates.X)
            _currentSelection.Height = Math.Abs(_firstCoordinates.Y - _secondCoordinates.Y)

            _currentSelection.Height = If(_currentSelection.Y + _currentSelection.Height > Height, Height - _currentSelection.Y, _currentSelection.Height)
            _currentSelection.Width = If(_currentSelection.X + _currentSelection.Width > Width, Width - _currentSelection.X, _currentSelection.Width)

            If _drawMangifier Then
                _magnifier.Update(_currentSelection, Bounds)
                _previousQuickInfoLocation = Rectangle.Union(_previousQuickInfoLocation, _magnifier.PreviousMagnifierLocation)
            End If

            'Invalidate(False)
            If _firstInvalidation Then
                _firstInvalidation = False
                Invalidate(False)
            Else
                ' Invalidate(Rectangle.Inflate(Rectangle.Union(Rectangle.Union(Rectangle.Union(
                ' _previousSelection, _currentSelection), _decoration.InvalidationRectangle), _previousQuickInfoLocation),
                ' 80, 40), False)
            End If
            _previousSelection = _currentSelection
        End Sub

        Private Sub RenderInGraphics(g As Graphics)
            g.CompositingQuality = CompositingQuality.AssumeLinear
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
            g.SmoothingMode = SmoothingMode.HighSpeed
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed
            g.InterpolationMode = InterpolationMode.NearestNeighbor

            g.DrawImage(_wholeScreen, 0, 0, _wholeScreen.Width, _wholeScreen.Height)

            If _resizeSelection OrElse _moveSelection Then
                g.FillRectangle(_backgroundOverlayBrush, 0, 0, Width, _currentSelection.Y)
                g.FillRectangle(_backgroundOverlayBrush, 0, _currentSelection.Y, _currentSelection.X, Height - _currentSelection.Y)
                g.FillRectangle(_backgroundOverlayBrush, _currentSelection.X, _currentSelection.Y + _currentSelection.Height, Width - _currentSelection.X, Height - _currentSelection.Y)
                g.FillRectangle(_backgroundOverlayBrush, _currentSelection.X + _currentSelection.Width, _currentSelection.Y, Width - _currentSelection.X - _currentSelection.Width, _currentSelection.Height)

            Else
                g.FillRectangle(_backgroundOverlayBrush, 0, 0, Width, Height)
            End If

            If _drawMangifier Then
                _magnifier.Draw(g, _wholeScreen, MagnifierBorderPen, SelectionBorderPen)
            End If

            g.DrawString(_fpsWatch.FramesPerSecond.ToString(), New Font("Consolas", 14.0F), New SolidBrush(Color.White), 10.0F, 10.0F)
        End Sub

        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            ' MyBase.OnPaintBackground(e)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            _fpsWatch.Update()
            RenderInGraphics(e.Graphics)
            Invalidate()
        End Sub

        Private Sub AreaSelectorKeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
            If e.KeyChar = " "c Then
                _drawMangifier = Not _drawMangifier

                _magnifier.Update(_currentSelection, Bounds)
                _previousQuickInfoLocation = Rectangle.Union(_previousQuickInfoLocation, _magnifier.PreviousMagnifierLocation)

                Invalidate()
            End If
        End Sub

        Public Function PromptSelectionAsync(image As Bitmap) As Task(Of Rectangle) Implements IAreaSelector.PromptSelectionAsync
            Debug.Assert(_areaTcs Is Nothing)
            Debug.Assert(_wholeScreen Is Nothing)
            Debug.Assert(image IsNot Nothing)

            _wholeScreen = image
            _areaTcs = New TaskCompletionSource(Of Rectangle)

            IsInAreaSelector = True

            Visible = True
            _fpsWatch.Start()

            Native.User32.SetForegroundWindow(Handle)

            Cursor = New Cursor(My.Resources.crossCursor.Handle)

            Return _areaTcs.Task
        End Function
    End Class
End Namespace
