Imports System.Linq
Imports HolzShots.Threading
Imports HolzShots.UI.Transitions
Imports HolzShots.UI.Transitions.TransitionTypes
Imports HolzShots.Interop
Imports HolzShots.Interop.NativeTypes

Namespace UI.Dialogs

    Friend Class FlyoutAnimator

        Private Shared ReadOnly CurrentVisibleFlyouts As New List(Of FlyoutWindow)
        Private _instanceOffsetY As Integer

        Private ReadOnly _target As FlyoutWindow

        Public Sub New(target As FlyoutWindow)
            If target Is Nothing Then Throw New ArgumentNullException(NameOf(target))
            _target = target
            _target.StartPosition = FormStartPosition.Manual
        End Sub

        Private Shared _screenRectangle As Rectangle
        Private _taskBarTopOrBottom As Boolean

        Public Sub AnimateIn(duration As Integer, Optional completedHandler As Action = Nothing)

            _taskBarTopOrBottom = TaskBar.Position = Native.Shell32.TaskbarPosition.Bottom OrElse TaskBar.Position = Native.Shell32.TaskbarPosition.Top
            _screenRectangle = Screen.PrimaryScreen.WorkingArea

            Dim startX As Integer = _screenRectangle.X + _screenRectangle.Width - _target.Width - 10
            Dim startY As Integer = _screenRectangle.Y + _screenRectangle.Height - _target.Height + If(_taskBarTopOrBottom, CInt(TaskBar.Rectangle.Height / 2), 15)

            If CurrentVisibleFlyouts.Count = 0 OrElse CurrentVisibleFlyouts.Count > 3 Then
                _instanceOffsetY = _screenRectangle.Y + _screenRectangle.Height - _target.Height
            Else
                _instanceOffsetY = CurrentVisibleFlyouts.Min(Of Integer)(Function(s) s.Location.Y) - _target.Height
            End If
            CurrentVisibleFlyouts.Add(_target)
            Const offfsetY As Integer = 8

            Dim destY As Integer = _instanceOffsetY - offfsetY

            _target.Location = New Point(startX, startY)
            _target.Opacity = 0.0
            _target.TopMost = True

            Dim t = New Transition(New Deceleration(duration))
            t.Add(Me, "TargetY", destY)
            t.Add(Me, "TargetOpacity", 1.0)

            If completedHandler IsNot Nothing Then
                AddHandler t.TransitionCompletedEvent, Sub() _target.InvokeIfNeeded(completedHandler)
            End If
            t.Run()
        End Sub

        Public Sub AnimateOut(duration As Integer, Optional completedHandler As Action = Nothing)
            CurrentVisibleFlyouts.Remove(_target)

            '_taskBarTopOrBottom = TaskBar.Position = NativeTypes.TaskbarPosition.Bottom OrElse TaskBar.Position = NativeTypes.TaskbarPosition.Top
            '_screenRectangle = Screen.PrimaryScreen.WorkingArea

            'Dim destBaseline = _screenRectangle.Y + _screenRectangle.Height - _target.Height + If(_taskBarTopOrBottom, CInt(TaskBar.Rectangle.Height / 2), 15)

            Dim destY As Integer = _instanceOffsetY + 20 'destBaseline - _instanceOffsetY

            _target.Opacity = 1.0
            _target.TopMost = True

            Dim t = New Transition(New Deceleration(duration))
            t.Add(Me, "TargetY", destY)
            t.Add(Me, "TargetOpacity", 0.0)

            If completedHandler IsNot Nothing Then
                AddHandler t.TransitionCompletedEvent, Sub() _target?.InvokeIfNeeded(completedHandler)
            End If
            t.Run()
        End Sub

        Public Property TargetX As Integer
            Get
                Return _target.Location.X
            End Get
            Set(value As Integer)
                If Not _target.IsDisposed AndAlso _target.Location.X <> value Then
                    _target.InvokeIfNeeded(Sub() _target.Location = New Point(value, _target.Location.Y))
                End If
            End Set
        End Property

        Public Property TargetOpacity As Double
            Get
                Return _target.Opacity
            End Get
            Set(value As Double)
                If Not _target.IsDisposed AndAlso _target.Opacity <> value Then
                    _target.InvokeIfNeeded(Sub() _target.Opacity = value)
                End If
            End Set
        End Property

        Public Property TargetY As Integer
            Get
                Return _target.Location.Y
            End Get
            Set(value As Integer)
                If Not _target.IsDisposed AndAlso _target.Location.Y <> value Then
                    _target.InvokeIfNeeded(Sub() _target.Location = New Point(_target.Location.X, value))
                End If
            End Set
        End Property
    End Class


    Friend Class AnimationSet
        Public Animator As FlyoutAnimator
        Public Duration As Integer
        Public CompletedHandler As Action

        Public Sub New(ByVal animator As FlyoutAnimator, ByVal duration As Integer, ByVal completedHandler As Action)
            Me.Animator = animator
            Me.Duration = duration
            Me.CompletedHandler = completedHandler
        End Sub
    End Class
End Namespace
