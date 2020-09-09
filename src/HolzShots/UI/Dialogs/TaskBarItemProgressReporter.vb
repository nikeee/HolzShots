Imports HolzShots.Net
Imports HolzShots.Windows.Forms

Namespace UI.Dialogs
    Friend Class TaskBarItemProgressReporter
        Implements IUploadProgressReporter

        Private ReadOnly _progressManager As TaskbarProgressManager
        Private ReadOnly _hasValidHandle As Boolean
        Private _status As TaskbarProgressBarState

        Private Shared ReadOnly CurrentManagers As New List(Of TaskbarProgressManager)

        Public Sub New(targetWindow As IntPtr)
            _progressManager = Taskbar.CreateProgressManagerForWindow(targetWindow)
            _hasValidHandle = targetWindow <> IntPtr.Zero AndAlso Taskbar.IsPlatformSupported AndAlso Not CurrentManagers.Contains(_progressManager)
            If _hasValidHandle Then CurrentManagers.Add(_progressManager)
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

        Private Sub SetProgress(percentage As UInteger)
            _progressManager.SetProgressValue(percentage, 100)
        End Sub
        Private Sub SetProgressState(state As TaskbarProgressBarState)
            _progressManager.SetProgressState(state)
        End Sub

        Public Sub CloseProgress() Implements IUploadProgressReporter.CloseProgress
            CurrentManagers.Remove(_progressManager)
            SetProgress(0)
            _progressManager.SetProgressState(TaskbarProgressBarState.NoProgress)
        End Sub

        Public Sub ShowProgress() Implements IUploadProgressReporter.ShowProgress
            _progressManager.SetProgressState(TaskbarProgressBarState.Indeterminate)
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
