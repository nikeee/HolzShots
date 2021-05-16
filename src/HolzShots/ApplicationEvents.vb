Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports HolzShots.Composition
Imports HolzShots.Input.Actions
Imports HolzShots.IO
Imports HolzShots.UI
Imports HolzShots.Windows.Forms
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.WindowsAPICodePack.Shell
Imports Microsoft.WindowsAPICodePack.Taskbar

Namespace My
    Partial Friend Class MyApplication

        Friend ReadOnly TheCulture As New CultureInfo("en-US", False)

        Public Shared ReadOnly Property SmallStockIcons As New StockIcons(StockIconSize.Small, False, False)

        Private _uploaders As UploaderManager
        ReadOnly Property Uploaders As UploaderManager
            Get
                Debug.Assert(_uploaders IsNot Nothing)
                Debug.Assert(_uploaders.Loaded)
                Return _uploaders
            End Get
        End Property

        Private Async Function LoadPlugins() As Task
            Debug.Assert(_uploaders Is Nothing)
            Dim plugins = New PluginUploaderSource(HolzShotsPaths.PluginDirectory)
            Dim customs = New CustomUploaderSource(HolzShotsPaths.CustomUploadersDirectory)
            _uploaders = New UploaderManager(plugins, customs)

            Try
                Await _uploaders.Load().ConfigureAwait(False)
            Catch ex As PluginLoadingFailedException
                NotificationManager.PluginLoadingFailed(ex)
                Debugger.Break()
            End Try
        End Function

        Private Async Sub MyApplicationStartup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            My.Settings.Upgrade()
            Await LoadPlugins().ConfigureAwait(True)
            If TaskbarManager.IsPlatformSupported Then AddTasks()
        End Sub

        Private Sub MyApplicationStartupNextInstance(ByVal sender As Object, ByVal e As StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            If e.CommandLine.Count > 0 Then
                ProcessCommandLineArguments(e.CommandLine)
            End If
        End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Trace.WriteLine("Got exception: " + e.Exception.Message)

            e.ExitApplication = True
        End Sub

        Friend Shared Async Function ProcessCommandLineArguments(args As String()) As Task
            ' TODO: Proper command line parsing?
            For i As Integer = 0 To args.Length - 1
                Select Case args(i)
                    Case CommandLine.FullscreenScreenshotCliCommand
                        Await MainWindow.CommandManager.Dispatch(Of FullscreenCommand)(UserSettings.Current).ConfigureAwait(True)
                    Case CommandLine.AreaSelectorCliCommand
                        Await MainWindow.CommandManager.Dispatch(Of SelectAreaCommand)(UserSettings.Current).ConfigureAwait(True)
                    Case CommandLine.UploadImageCliCommand

                        ' TODO: Maybe we can support overriding settings from the command line, too
                        Dim params = New Dictionary(Of String, String)()
                        If i < args.Length - 1 Then
                            params(FileDependentCommand.FileNameParameter) = args(i + 1)
                        End If

                        Await MainWindow.CommandManager.Dispatch(Of UploadImageCommand)(UserSettings.Current, params).ConfigureAwait(True)
                    Case CommandLine.OpenImageCliCommand

                        ' TODO: Maybe we can support overriding settings from the command line, too
                        Dim params = New Dictionary(Of String, String)()
                        If i < args.Length - 1 Then
                            params(FileDependentCommand.FileNameParameter) = args(i + 1)
                        End If

                        Await MainWindow.CommandManager.Dispatch(Of EditImageCommand)(UserSettings.Current, params).ConfigureAwait(True)
                End Select
            Next
        End Function

        Friend Shared Sub AddTasks()
            Debug.Assert(TaskbarManager.IsPlatformSupported)

            If My.Settings.UserTasksInitialized Then Exit Sub
            My.Settings.UserTasksInitialized = True
            My.Settings.Save()

            Dim jumpList = Microsoft.WindowsAPICodePack.Taskbar.JumpList.CreateJumpList()
            jumpList.ClearAllUserTasks()

            Static imgres As String = Path.Combine(HolzShotsPaths.SystemPath, "imageres.dll")

            If File.Exists(imgres) Then
                Dim fullscreen As New JumpListLink(System.Windows.Forms.Application.ExecutablePath, "Capture entire screen") With {
                    .Arguments = CommandLine.FullscreenScreenshotCliCommand,
                    .IconReference = New IconReference(imgres, 105)
                }
                jumpList.AddUserTasks(fullscreen)
            End If

            Dim selector As New JumpListLink(System.Windows.Forms.Application.ExecutablePath, "Capture Region") With {
                .Arguments = CommandLine.AreaSelectorCliCommand,
                .IconReference = New IconReference(System.Windows.Forms.Application.ExecutablePath, 0)
            }
            jumpList.AddUserTasks(selector)

            Try
                jumpList.Refresh()
            Catch ex As UnauthorizedAccessException
            End Try
        End Sub

        Friend Shared Function ProcessCommandLineArguments(args As ReadOnlyCollection(Of String)) As Task
            Return ProcessCommandLineArguments(args.ToArray())
        End Function
    End Class
End Namespace
