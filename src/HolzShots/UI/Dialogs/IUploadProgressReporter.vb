Imports HolzShots.Net
Imports HolzShots

Namespace UI.Dialogs
    Interface IUploadProgressReporter
        Inherits IDisposable

        Sub UpdateProgress(report As UploadProgress, speed As Speed(Of MemSize))

        Sub ShowProgress()
        Sub CloseProgress()

    End Interface
End Namespace
