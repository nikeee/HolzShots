Imports HolzShots.Net
Imports HolzShots
Imports Microsoft.WindowsAPICodePack.Taskbar

Namespace UI.Dialogs
    Friend Class TaskBarItemProgressReporter
        Implements IUploadProgressReporter

        Private ReadOnly _targetWindowHandle As IntPtr
        Private ReadOnly _hasValidHandle As Boolean
        Private _status As TaskbarProgressBarState

        Private Shared ReadOnly CurrentHandles As New List(Of IntPtr)

        ' Public Sub New(targetWindow As IWin32Window)
        ' Me.New(If(targetWindow Is Nothing, IntPtr.Zero, targetWindow.Handle))
        ' ' Debug.Assert(targetWindow IsNot Nothing)
        ' End Sub
        Public Sub New(targetWindow As IntPtr)
            _targetWindowHandle = targetWindow
            _hasValidHandle = targetWindow <> IntPtr.Zero AndAlso TaskbarManager.IsPlatformSupported AndAlso Not CurrentHandles.Contains(targetWindow)

            If _hasValidHandle Then CurrentHandles.Add(targetWindow)
        End Sub

        Public Sub UpdateProgress(report As UploadProgress, speed As Speed(Of MemSize)) Implements IUploadProgressReporter.UpdateProgress
            If Not _hasValidHandle Then Return

            Select Case report.State
                Case UploadState.NotStarted
                    _status = TaskbarProgressBarState.Indeterminate
                    SetProgressState(_status)
                Case UploadState.Finished
                    _status = TaskbarProgressBarState.Indeterminate
                    SetProgressState(_status)
                Case UploadState.Processing
                    If report.ProgressPercentage >= 0 Then
                        If _status <> TaskbarProgressBarState.Normal Then
                            _status = TaskbarProgressBarState.Normal
                            SetProgressState(_status)
                        End If
                        SetProgress(report.ProgressPercentage)
                    End If
            End Select
        End Sub

        Private Sub SetProgress(percentage As Integer)
            TaskbarManager.Instance.SetProgressValue(percentage, 100, _targetWindowHandle)
        End Sub
        Private Shared Sub SetProgressState(state As TaskbarProgressBarState)
            TaskbarManager.Instance.SetProgressState(state)
        End Sub

        Public Sub CloseProgress() Implements IUploadProgressReporter.CloseProgress
            CurrentHandles.Remove(_targetWindowHandle)
            SetProgress(0)
            SetProgressState(TaskbarProgressBarState.NoProgress)
        End Sub

        Public Sub ShowProgress() Implements IUploadProgressReporter.ShowProgress
            SetProgressState(TaskbarProgressBarState.Indeterminate)
        End Sub

#Region "IDisposable Support"
        Protected Overridable Sub Dispose(disposing As Boolean)
            ' Nothing to dispose here
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
