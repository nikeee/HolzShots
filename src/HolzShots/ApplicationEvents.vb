Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports HolzShots.Composition
Imports HolzShots.Input.Actions
Imports HolzShots.Interop
Imports HolzShots.IO
Imports HolzShots.ScreenshotRelated
Imports HolzShots.UI.Specialized
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.WindowsAPICodePack.Shell
Imports Microsoft.WindowsAPICodePack.Taskbar

Namespace My
    Partial Friend Class MyApplication

        Friend ReadOnly TheCulture As New CultureInfo("en-US", False)

        Public Shared ReadOnly Property SmallStockIcons As New StockIcons(StockIconSize.Small, False, False)
        Public Shared ReadOnly Property ShellSizeStockIcons As New StockIcons(StockIconSize.ShellSize, False, False)
        Public Shared ReadOnly Property LargeStockIcons As New StockIcons(StockIconSize.Large, False, False)

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
                HumanInterop.PluginLoadingFailed(ex)
                Debugger.Break()
            End Try
        End Function

        Private Async Sub MyApplicationStartup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            UpgradeSettings()
            Await LoadPlugins().ConfigureAwait(True)
            If TaskbarManager.IsPlatformSupported Then AddTasks()
        End Sub

        Private Sub MyApplicationStartupNextInstance(ByVal sender As Object, ByVal e As StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            If e.CommandLine.Count > 0 Then
                ProcessCommandLineArguments(e.CommandLine)
            End If
        End Sub

        Private Shared Sub UpgradeSettings()
            If Not Global.HolzShots.My.Settings.Upgraded Then
                Global.HolzShots.My.Settings.Upgrade()
                Global.HolzShots.My.Settings.Upgraded = True
                Global.HolzShots.My.Settings.Save()
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
                    Case FullscreenScreenshotParameter
                        Await MainWindow.CommandManager.Dispatch(Of FullscreenCommand)().ConfigureAwait(True)
                    Case AreaSelectorParameter
                        Await MainWindow.CommandManager.Dispatch(Of SelectAreaCommand)().ConfigureAwait(True)
                    Case UploadParameter
                        Await MainWindow.CommandManager.Dispatch(Of UploadImageCommand)().ConfigureAwait(True)
                    Case OpenParameter
                        Await MainWindow.CommandManager.Dispatch(Of EditImageCommand)().ConfigureAwait(True)
                    Case OpenFromShellParameter
                        If i < args.Length - 1 Then
                            Dim fileName = args(i + 1)
                            Await MainWindow.CommandManager.Dispatch(Of EditImageCommand)(fileName).ConfigureAwait(True)
                        End If
                    Case UploadFromShellParameter
                        If i < args.Length - 1 Then
                            Dim fileName = args(i + 1)
                            Await MainWindow.CommandManager.Dispatch(Of UploadImageCommand)(fileName).ConfigureAwait(True)
                        End If
                End Select
            Next
        End Function

        Friend Shared Sub AddTasks()
            If Global.HolzShots.My.Settings.UserTasksInitialized Then Exit Sub
            Global.HolzShots.My.Settings.UserTasksInitialized = True
            Global.HolzShots.My.Settings.Save()

            Dim jlist = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, SettingsWindow.Instance.Handle)

            jlist.ClearAllUserTasks()

            Static imgres As String = Path.Combine(HolzShotsPaths.SystemPath, "imageres.dll")

            If File.Exists(imgres) Then
                Dim fullscreen As New JumpListLink(System.Windows.Forms.Application.ExecutablePath, "Capture entire screen") With {
                    .Arguments = FullscreenScreenshotParameter,
                    .IconReference = New IconReference(imgres, 105)
                }
                jlist.AddUserTasks(fullscreen)
            End If

            Dim selector As New JumpListLink(System.Windows.Forms.Application.ExecutablePath, "Select a region") With {
                .Arguments = AreaSelectorParameter,
                .IconReference = New IconReference(System.Windows.Forms.Application.ExecutablePath, 0)
            }
            jlist.AddUserTasks(selector)

            Try
                jlist.Refresh()
            Catch ex As UnauthorizedAccessException
            End Try
        End Sub

        Friend Shared Function ProcessCommandLineArguments(args As ReadOnlyCollection(Of String)) As Task
            Return ProcessCommandLineArguments(args.ToArray())
        End Function
    End Class
End Namespace
