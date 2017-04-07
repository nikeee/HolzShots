Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports HolzShots.Composition
Imports HolzShots.Interop
Imports HolzShots.ScreenshotRelated
Imports HolzShots.UI.Specialized
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.WindowsAPICodePack.Shell
Imports Microsoft.WindowsAPICodePack.Taskbar

Namespace My
    Partial Friend Class MyApplication

        Private Const CultureString As String = "de-DE"
        Friend ReadOnly TheCulture As New CultureInfo(CultureString, False)

        Public Shared SmallStockIcons As New StockIcons(StockIconSize.Small, False, False)
        Public Shared ShellSizeStockIcons As New StockIcons(StockIconSize.ShellSize, False, False)
        Public Shared LargeStockIcons As New StockIcons(StockIconSize.Large, False, False)

        Private _uploaderPlugins As UploaderPluginManager
        ReadOnly Property CurrentUploaderPluginManager As UploaderPluginManager
            Get
                Debug.Assert(_uploaderPlugins IsNot Nothing)
                Debug.Assert(_uploaderPlugins.Loaded)
                Return _uploaderPlugins
            End Get
        End Property

        Private Async Function LoadPlugins() As Task
            Debug.Assert(_uploaderPlugins Is Nothing)
            _uploaderPlugins = New UploaderPluginManager(ManagedSettings.PluginPath)

            Try
                Await _uploaderPlugins.Load().ConfigureAwait(False)
            Catch ex As PluginLoadingFailedException
                HumanInterop.PluginLoadingFailed(ex)
                Debugger.Break()
            End Try
        End Function

        Private Async Sub MyApplicationStartup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            UpgradeSettings()
            Await LoadPlugins()
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
                        Await ScreenshotInvoker.DoFullscreen()
                    Case AreaSelectorParameter
                        Await ScreenshotInvoker.DoSelector()
                    Case UploadParameter
                        ScreenshotInvoker.UploadSelectedImage()
                    Case OpenParameter
                        ScreenshotInvoker.OpenSelectedImage()
                    Case TaskbarScreenshotParameter
                        Await ScreenshotInvoker.DoTaskbar()
                    Case OpenFromShellParameter
                        If i < args.Length - 1 Then
                            ScreenshotInvoker.TryOpenSpecificImage(args(i + 1))
                        End If
                    Case UploadFromShellParameter
                        If i < args.Length - 1 Then
                            ScreenshotInvoker.TryUploadSpecificImage(args(i + 1))
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

            Static imgres As String = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.System), "imageres.dll")

            If File.Exists(imgres) Then
                Dim fullscreen As New JumpListLink(System.Windows.Forms.Application.ExecutablePath, "Vollbildschirm-Screenshot") With {
                    .Arguments = FullscreenScreenshotParameter,
                    .IconReference = New IconReference(imgres, 105)
                }

                jlist.AddUserTasks(fullscreen)

                Dim taskbar As New JumpListLink(System.Windows.Forms.Application.ExecutablePath, "Taskleiste fotografieren") With {
                    .Arguments = TaskbarScreenshotParameter,
                    .IconReference = New IconReference(imgres, 75)
                }
                jlist.AddUserTasks(taskbar)
            End If

            Dim selector As New JumpListLink(System.Windows.Forms.Application.ExecutablePath, "Einen Bereich auswählen") With {
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
