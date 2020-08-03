Imports HolzShots.ScreenshotRelated.Selection

Namespace Interop
    Friend Module ManagedSettings
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
    End Module
End Namespace
