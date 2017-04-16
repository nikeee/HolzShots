Imports System.IO
Imports HolzShots.Common
Imports HolzShots.ScreenshotRelated
Imports HolzShots.ScreenshotRelated.Selection

Namespace Interop
    Friend Module ManagedSettings

        ' TODO: ApplicationInformation?
        Private Const PublisherName As String = "nikeee Software"
        Private Const ApplicationName As String = LibraryInformation.Name

        Private Const PluginPathDir As String = "Plugin"
        Private Const CustomUploadersFileName = "custom-uploaders.json"
        Private ReadOnly DefaultPluginPath As String = Path.Combine(Path.Combine(HolzShotsEnvironment.ApplicationDataRoaming, ApplicationName), PluginPathDir)

        Sub New()
            ' Manager = New PolicyManager(PublisherName, ApplicationName, False)

            ' TODO: Entry in settings dialog
            ' TrayIconDoubleClickActionPolicy = New EnumPolicy(Of TrayIconAction)(Manager, "TrayIconDoubleClickAction", PolicyTarget.User, Function() My.Settings.TrayIconDoubleClickAction, Sub(s) My.Settings.TrayIconDoubleClickAction = s)
        End Sub

        Public Property ShotEditorTitleText As String = String.Empty

        Public Property AutoCloseShotEditor As Boolean
            Get
                Return My.Settings.AutoCloseShotEditor
            End Get
            Set
                My.Settings.AutoCloseShotEditor = Value
            End Set
        End Property

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

        Public Property SaveImagesToLocalDisk As Boolean
            Get
                Return My.Settings.SaveImagesToLocalDisk
            End Get
            Set
                My.Settings.SaveImagesToLocalDisk = Value
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

        Public Property AutoCloseLinkViewer As Boolean
            Get
                Return My.Settings.AutoCloseLinkViewer
            End Get
            Set
                My.Settings.AutoCloseLinkViewer = Value
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

        Public Property EnableStatusToaster As Boolean
            Get
                Return My.Settings.EnableStatusToaster
            End Get
            Set
                My.Settings.EnableStatusToaster = Value
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

        Public Property EnableIngameMode As Boolean
            Get
                Return My.Settings.EnableIngameMode
            End Get
            Set
                My.Settings.EnableIngameMode = Value
            End Set
        End Property

        Public Property EnableSmartFormatForUpload As Boolean
            Get
                Return My.Settings.EnableSmartFormatForUpload
            End Get
            Set
                My.Settings.EnableSmartFormatForUpload = Value
            End Set
        End Property

        Public Property EnableSmartFormatForSaving As Boolean
            Get
                Return My.Settings.EnableSmartFormatForSaving
            End Get
            Set
                My.Settings.EnableSmartFormatForSaving = Value
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

        Public Property ShowCopyConfirmation As Boolean
            Get
                Return My.Settings.ShowCopyConfimation
            End Get
            Set
                My.Settings.ShowCopyConfimation = Value
            End Set
        End Property

        Public ReadOnly Property PluginPath As String = DefaultPluginPath
        Public ReadOnly Property CustomUploadersPath As String = Path.Combine(DefaultPluginPath, CustomUploadersFileName)
        Public ReadOnly Property IsAnyPolicyDefined As Boolean = False ' Manager.DefinedPolicyCounter > 0

    End Module

#If False Then
    Friend Module ManagedSettings
        Private Const PublisherName As String = "nikeee Software"
        Private Const ApplicationName As String = "HolzShots"

        Private Const PluginPathDir As String = "Plugin"
        Private ReadOnly DefaultPluginPath As String = Path.Combine(Path.Combine(HolzShotsEnvironment.ApplicationDataRoaming, GlobalVariables.InternalApplicationName), PluginPathDir)

        Private ReadOnly Manager As PolicyManager
        Sub New()
            Manager = New PolicyManager(PublisherName, ApplicationName, False)

            ' TODO: Add to ADMX file
            ShellExtensionUploadPolicy = New BooleanPolicy(Manager, "ShellExtensionUpload", PolicyTarget.User, Function() ShellExtensions.ShellExtensionOpen)
            ShellExtensionOpenPolicy = New BooleanPolicy(Manager, "ShellExtensionOpen", PolicyTarget.User, Function() ShellExtensions.ShellExtensionOpen)
            PluginPathPolicy = New StringPolicy(Manager, "PluginPath", PolicyTarget.User, Function() DefaultPluginPath)

            ' TODO: Entry in settings dialog
            TrayIconDoubleClickActionPolicy = New EnumPolicy(Of TrayIconAction)(Manager, "TrayIconDoubleClickAction", PolicyTarget.User, Function() My.Settings.TrayIconDoubleClickAction, Sub(s) My.Settings.TrayIconDoubleClickAction = s)
            AutoCloseShotEditorPolicy = New BooleanPolicy(Manager, "AutoCloseShotEditor", PolicyTarget.User, Function() My.Settings.AutoCloseShotEditor, Sub(s) My.Settings.AutoCloseShotEditor = s)

            ' DONE:
            AutoCloseLinkViewerPolicy = New BooleanPolicy(Manager, "AutoCloseLinkViewer", PolicyTarget.User, Function() My.Settings.AutoCloseLinkViewer, Sub(s) My.Settings.AutoCloseLinkViewer = s)
            AutoCloseThumbViewerPolicy = New BooleanPolicy(Manager, "AutoCloseThumbViewer", PolicyTarget.User, Function() My.Settings.AutoCloseThumbViewer, Sub(s) My.Settings.AutoCloseThumbViewer = s)
            SaveImagesToLocalDiskPolicy = New BooleanPolicy(Manager, "SaveImagesToLocalDisk", PolicyTarget.User, Function() My.Settings.SaveImagesToLocalDisk, Sub(s) My.Settings.SaveImagesToLocalDisk = s)
            EnableIngameModePolicy = New BooleanPolicy(Manager, "EnableIngameMode", PolicyTarget.User, Function() My.Settings.EnableIngameMode, Sub(s) My.Settings.EnableIngameMode = s)
            EnableLinkViewerPolicy = New BooleanPolicy(Manager, "EnableLinkViewer", PolicyTarget.User, Function() My.Settings.EnableLinkViewer, Sub(s) My.Settings.EnableLinkViewer = s)
            EnableShotEditorPolicy = New BooleanPolicy(Manager, "EnableShotEditor", PolicyTarget.User, Function() My.Settings.EnableShotEditor, Sub(s) My.Settings.EnableShotEditor = s)
            EnableStatusToasterPolicy = New BooleanPolicy(Manager, "EnableStatusToaster", PolicyTarget.User, Function() My.Settings.EnableStatusToaster, Sub(s) My.Settings.EnableStatusToaster = s)
            EnableAreaScreenshotPolicy = New BooleanPolicy(Manager, "EnableAreaScreenshot", PolicyTarget.User, Function() My.Settings.EnableAreaScreenshot, Sub(s) My.Settings.EnableAreaScreenshot = s)
            EnableFullscreenScreenshotPolicy = New BooleanPolicy(Manager, "EnableFullscreenScreenshot", PolicyTarget.User, Function() My.Settings.EnableFullscreenScreenshot, Sub(s) My.Settings.EnableFullscreenScreenshot = s)
            EnableWindowScreenshotPolicy = New BooleanPolicy(Manager, "EnableWindowScreenshot", PolicyTarget.User, Function() My.Settings.EnableWindowScreenshot, Sub(s) My.Settings.EnableWindowScreenshot = s)
            EnableSmartFormatForSavingPolicy = New BooleanPolicy(Manager, "EnableSmartFormatForSaving", PolicyTarget.User, Function() My.Settings.EnableSmartFormatForSaving, Sub(s) My.Settings.EnableSmartFormatForSaving = s)
            EnableSmartFormatForUploadPolicy = New BooleanPolicy(Manager, "EnableSmartFormatForUpload", PolicyTarget.User, Function() My.Settings.EnableSmartFormatForUpload, Sub(s) My.Settings.EnableSmartFormatForUpload = s)

            SelectionDecorationPolicy = New EnumPolicy(Of SelectionDecorations)(Manager, "SelectionDecoration", PolicyTarget.User, Function() My.Settings.SelectionDecoration, Sub(s) My.Settings.SelectionDecoration = s)

            SaveImagesPatternPolicy = New StringPolicy(Manager, "SaveImagesPattern", PolicyTarget.User, Function() My.Settings.SaveImagesPattern, Sub(s) My.Settings.SaveImagesPattern = s)
            ShotEditorTitleTextPolicy = New StringPolicy(Manager, "ShotEditorTitleText", PolicyTarget.User, Function() String.Empty, Sub(s) s = s)
            ScreenshotPathPolicy = New StringPolicy(Manager, "ScreenshotFolderPath", PolicyTarget.User, Function() My.Settings.ScreenshotPath, Sub(s) My.Settings.ScreenshotPath = s)
        End Sub

        Public ReadOnly ShotEditorTitleTextPolicy As StringPolicy
        Public Property ShotEditorTitleText As String
            Get
                Return ShotEditorTitleTextPolicy.Value
            End Get
            Set(value As String)
                ShotEditorTitleTextPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly AutoCloseShotEditorPolicy As BooleanPolicy
        Public Property AutoCloseShotEditor As Boolean
            Get
                Return AutoCloseShotEditorPolicy.Value
            End Get
            Set(value As Boolean)
                AutoCloseShotEditorPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly ScreenshotPathPolicy As StringPolicy
        Public Property ScreenshotPath As String
            Get
                Return ScreenshotPathPolicy.Value
            End Get
            Set(value As String)
                ScreenshotPathPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly SaveImagesPatternPolicy As StringPolicy
        Public Property SaveImagesPattern As String
            Get
                Return SaveImagesPatternPolicy.Value
            End Get
            Set(value As String)
                SaveImagesPatternPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly SaveImagesToLocalDiskPolicy As BooleanPolicy
        Public Property SaveImagesToLocalDisk As Boolean
            Get
                Return SaveImagesToLocalDiskPolicy.Value
            End Get
            Set(value As Boolean)
                SaveImagesToLocalDiskPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly EnableShotEditorPolicy As BooleanPolicy
        Public Property EnableShotEditor As Boolean
            Get
                Return EnableShotEditorPolicy.Value
            End Get
            Set(value As Boolean)
                EnableShotEditorPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly EnableLinkViewerPolicy As BooleanPolicy
        Public Property EnableLinkViewer As Boolean
            Get
                Return EnableLinkViewerPolicy.Value
            End Get
            Set(value As Boolean)
                EnableLinkViewerPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly AutoCloseLinkViewerPolicy As BooleanPolicy
        Public Property AutoCloseLinkViewer As Boolean
            Get
                Return AutoCloseLinkViewerPolicy.Value
            End Get
            Set(value As Boolean)
                AutoCloseLinkViewerPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly AutoCloseThumbViewerPolicy As BooleanPolicy
        Public Property AutoCloseThumbViewer As Boolean
            Get
                Return AutoCloseThumbViewerPolicy.Value
            End Get
            Set(value As Boolean)
                AutoCloseThumbViewerPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly ShellExtensionUploadPolicy As BooleanPolicy
        Public Property ShellExtensionUpload As Boolean
            Get
                Return ShellExtensions.ShellExtensionUpload
            End Get
            Set(value As Boolean)
                If value <> ShellExtensionUploadPolicy.DefaultValue AndAlso InteropHelper.IsAdministrator() Then
                    ShellExtensions.ShellExtensionUpload = value
                End If
            End Set
        End Property

        Public ReadOnly ShellExtensionOpenPolicy As BooleanPolicy
        Public Property ShellExtensionOpen As Boolean
            Get
                Return ShellExtensions.ShellExtensionOpen
            End Get
            Set(value As Boolean)
                If value <> ShellExtensionOpenPolicy.DefaultValue AndAlso InteropHelper.IsAdministrator() Then
                    ShellExtensions.ShellExtensionOpen = value
                End If
            End Set
        End Property

        Public ReadOnly EnableStatusToasterPolicy As BooleanPolicy
        Public Property EnableStatusToaster As Boolean
            Get
                Return EnableStatusToasterPolicy.Value
            End Get
            Set(value As Boolean)
                EnableStatusToasterPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly EnableAreaScreenshotPolicy As BooleanPolicy
        Public Property EnableAreaScreenshot As Boolean
            Get
                Return EnableAreaScreenshotPolicy.Value
            End Get
            Set(value As Boolean)
                EnableAreaScreenshotPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly EnableFullscreenScreenshotPolicy As BooleanPolicy
        Public Property EnableFullscreenScreenshot As Boolean
            Get
                Return EnableFullscreenScreenshotPolicy.Value
            End Get
            Set(value As Boolean)
                EnableFullscreenScreenshotPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly EnableWindowScreenshotPolicy As BooleanPolicy
        Public Property EnableWindowScreenshot As Boolean
            Get
                Return EnableWindowScreenshotPolicy.Value
            End Get
            Set(value As Boolean)
                EnableWindowScreenshotPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly EnableIngameModePolicy As BooleanPolicy
        Public Property EnableIngameMode As Boolean
            Get
                Return EnableIngameModePolicy.Value
            End Get
            Set(value As Boolean)
                EnableIngameModePolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly EnableSmartFormatForUploadPolicy As BooleanPolicy
        Public Property EnableSmartFormatForUpload As Boolean
            Get
                Return EnableSmartFormatForUploadPolicy.Value
            End Get
            Set(value As Boolean)
                EnableSmartFormatForUploadPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly EnableSmartFormatForSavingPolicy As BooleanPolicy
        Public Property EnableSmartFormatForSaving As Boolean
            Get
                Return EnableSmartFormatForSavingPolicy.Value
            End Get
            Set(value As Boolean)
                EnableSmartFormatForSavingPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly TrayIconDoubleClickActionPolicy As EnumPolicy(Of TrayIconAction)
        Public Property TrayIconDoubleClickAction As TrayIconAction
            Get
                Return TrayIconDoubleClickActionPolicy.Value
            End Get
            Set(value As TrayIconAction)
                TrayIconDoubleClickActionPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly SelectionDecorationPolicy As EnumPolicy(Of SelectionDecorations)
        Public Property SelectionDecoration As SelectionDecorations
            Get
                Return SelectionDecorationPolicy.Value
            End Get
            Set(value As SelectionDecorations)
                SelectionDecorationPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly PluginPathPolicy As StringPolicy
        Public Property PluginPath As String
            Get
                Return PluginPathPolicy.Value
            End Get
            Set(value As String)
                PluginPathPolicy.DefaultValue = value
            End Set
        End Property

        Public ReadOnly Property IsAnyPolicyDefined As Boolean
            Get
                Return Manager.DefinedPolicyCounter > 0
            End Get
        End Property
    End Module
#End If
End Namespace
