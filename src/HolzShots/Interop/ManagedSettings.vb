Imports System.IO
Imports HolzShots
Imports HolzShots.ScreenshotRelated
Imports HolzShots.ScreenshotRelated.Selection

Namespace Interop
    Friend Module ManagedSettings

        Private Const CustomUploadersFileName = "custom-uploaders.json"

        Public Property ScreenshotPath As String
            Get
                Return My.Settings.ScreenshotPath
            End Get
            Set
                My.Settings.ScreenshotPath = Value
            End Set
        End Property

        Public Property SaveImagesPattern As String
            Get
                Return My.Settings.SaveImagesPattern
            End Get
            Set
                My.Settings.SaveImagesPattern = Value
            End Set
        End Property

        Public Property EnableShotEditor As Boolean
            Get
                Return My.Settings.EnableShotEditor
            End Get
            Set
                My.Settings.EnableShotEditor = Value
            End Set
        End Property

        Public Property EnableLinkViewer As Boolean
            Get
                Return My.Settings.EnableLinkViewer
            End Get
            Set
                My.Settings.EnableLinkViewer = Value
            End Set
        End Property

        Public Property ShellExtensionUpload As Boolean
            Get
                Return ShellExtensions.ShellExtensionUpload
            End Get
            Set
                If Value <> ShellExtensions.ShellExtensionUpload AndAlso InteropHelper.IsAdministrator() Then
                    ShellExtensions.ShellExtensionUpload = Value
                End If
            End Set
        End Property

        Public Property ShellExtensionOpen As Boolean
            Get
                Return ShellExtensions.ShellExtensionOpen
            End Get
            Set
                If Value <> ShellExtensions.ShellExtensionOpen AndAlso InteropHelper.IsAdministrator() Then
                    ShellExtensions.ShellExtensionOpen = Value
                End If
            End Set
        End Property

        Public Property EnableAreaScreenshot As Boolean
            Get
                Return My.Settings.EnableAreaScreenshot
            End Get
            Set
                My.Settings.EnableAreaScreenshot = Value
            End Set
        End Property

        Public Property EnableFullscreenScreenshot As Boolean
            Get
                Return My.Settings.EnableFullscreenScreenshot
            End Get
            Set
                My.Settings.EnableFullscreenScreenshot = Value
            End Set
        End Property

        Public Property EnableWindowScreenshot As Boolean
            Get
                Return My.Settings.EnableWindowScreenshot
            End Get
            Set
                My.Settings.EnableWindowScreenshot = Value
            End Set
        End Property

        Public Property TrayIconDoubleClickAction As TrayIconAction
            Get
                Return My.Settings.TrayIconDoubleClickAction
            End Get
            Set
                My.Settings.TrayIconDoubleClickAction = Value
            End Set
        End Property

        Public Property SelectionDecoration As SelectionDecorations
            Get
                Return My.Settings.SelectionDecoration
            End Get
            Set
                My.Settings.SelectionDecoration = Value
            End Set
        End Property

        Public ReadOnly Property PluginPath As String = HolzShots.IO.HolzShotsPaths.PluginDirectory
        Public ReadOnly Property CustomUploadersPath As String = Path.Combine(HolzShots.IO.HolzShotsPaths.PluginDirectory, CustomUploadersFileName)
        Public ReadOnly Property IsAnyPolicyDefined As Boolean = False ' Manager.DefinedPolicyCounter > 0

    End Module

End Namespace
