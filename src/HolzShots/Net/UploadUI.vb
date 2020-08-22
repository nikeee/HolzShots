Imports System.Drawing.Imaging
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports HolzShots.Drawing
Imports HolzShots.Interop
Imports HolzShots.UI.Dialogs

Namespace Net
    Friend Class UploadUI
        Implements IDisposable

        Private ReadOnly _reporters As New List(Of IUploadProgressReporter)(2)
        Private ReadOnly _parentWindow As IWin32Window
        Private ReadOnly _uploader As Uploader
        Private ReadOnly _image As Image
        Private ReadOnly _format As ImageFormat

        Private ReadOnly _speedCalculator As New SpeedCalculatorProgress()

        Sub New(uploader As Uploader, image As Image, format As ImageFormat, parentWindow As IWin32Window)
            Debug.Assert(uploader IsNot Nothing)
            Debug.Assert(image IsNot Nothing)
            Debug.Assert(format IsNot Nothing)

            _parentWindow = parentWindow
            _uploader = uploader
            _image = image.CloneGifBug(format) ' TODO: Really clone this image?
            _format = format
            InitReporters(parentWindow.GetHandle())
        End Sub

        Private Sub InitReporters(parentWindowHandle As IntPtr)
            If UserSettings.Current.ShowUploadProgress Then _reporters.Add(New StatusToaster())
            If parentWindowHandle <> IntPtr.Zero Then _reporters.Add(New TaskBarItemProgressReporter(parentWindowHandle))
        End Sub

        Public Async Function InvokeUploadAsync() As Task(Of UploadResult)
            Debug.Assert(Not _speedCalculator.HasStarted)

            Dim stream = _image.GetImageStream(_format)
            Debug.Assert(stream IsNot Nothing)

            Dim metadata = _format.GetExtensionAndMimeType()
            Debug.Assert(Not String.IsNullOrWhiteSpace(metadata.MimeType))
            Debug.Assert(Not String.IsNullOrWhiteSpace(metadata.FileExtension))

            Dim fileName = Path.ChangeExtension(GlobalVariables.DefaultFileName, metadata.FileExtension)

            Dim cts As New CancellationTokenSource()

            Dim speed = _speedCalculator
            AddHandler speed.ProgressChanged, AddressOf ProgressChanged

            speed.Start()
            Try
                Dim res = Await _uploader.InvokeAsync(stream, fileName, metadata.MimeType, speed, cts.Token).ConfigureAwait(False)
                Return res
            Finally
                speed.Stop()
                RemoveHandler speed.ProgressChanged, AddressOf ProgressChanged
            End Try
        End Function

        Private Sub ProgressChanged(sender As Object, report As UploadProgress)
            Debug.Assert(_reporters IsNot Nothing)
            If _reporters Is Nothing Then Return
            For Each r In _reporters
                Debug.Assert(r IsNot Nothing)
                r.UpdateProgress(report, _speedCalculator.CurrentSpeed)
            Next
        End Sub

        Public Sub ShowUi()
            Debug.Assert(_reporters IsNot Nothing)
            For Each r In _reporters
                Debug.Assert(r IsNot Nothing)
                r.ShowProgress()
            Next
        End Sub

        Public Sub HideUi()
            Debug.Assert(_reporters IsNot Nothing)
            For Each r In _reporters
                Debug.Assert(r IsNot Nothing)
                r.CloseProgress()
            Next
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' dispose managed state (managed objects).
                    DisposeUi()
                    Debug.Assert(_image IsNot Nothing)
                    _image.Dispose()
                End If
                ' free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' set large fields to null.
            End If
            disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
        End Sub

        Private Sub DisposeUi()

            ' TODO: Fix objectDisposedException in FlyoutAnimator?
            'For Each r In _reporters
            '    If r IsNot Nothing Then r.Dispose()
            'Next
        End Sub
#End Region
    End Class
End Namespace
