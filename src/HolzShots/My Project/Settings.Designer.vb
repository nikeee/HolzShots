'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My

    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.6.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase

        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)

#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Global.System.Object, e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region

        Public Shared ReadOnly Property [Default]() As MySettings
            Get

#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property HighlightMouse() As Boolean
            Get
                Return CType(Me("HighlightMouse"),Boolean)
            End Get
            Set
                Me("HighlightMouse") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ShowCursor() As Boolean
            Get
                Return CType(Me("ShowCursor"),Boolean)
            End Get
            Set
                Me("ShowCursor") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Transparent")>  _
        Public Property HighlightColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("HighlightColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("HighlightColor") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("128")>  _
        Public Property ColorAlpha() As Integer
            Get
                Return CType(Me("ColorAlpha"),Integer)
            End Get
            Set
                Me("ColorAlpha") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("20")>  _
        Public Property ZensursulaWidth() As Integer
            Get
                Return CType(Me("ZensursulaWidth"),Integer)
            End Get
            Set
                Me("ZensursulaWidth") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("15")>  _
        Public Property MarkerWidth() As Integer
            Get
                Return CType(Me("MarkerWidth"),Integer)
            End Get
            Set
                Me("MarkerWidth") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Black")>  _
        Public Property ZensursulaColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("ZensursulaColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("ZensursulaColor") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Yellow")>  _
        Public Property MarkerColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("MarkerColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("MarkerColor") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("20")>  _
        Public Property EraserDiameter() As Integer
            Get
                Return CType(Me("EraserDiameter"),Integer)
            End Get
            Set
                Me("EraserDiameter") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property EnableStatusToaster() As Boolean
            Get
                Return CType(Me("EnableStatusToaster"),Boolean)
            End Get
            Set
                Me("EnableStatusToaster") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Red")>  _
        Public Property EllipseColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("EllipseColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("EllipseColor") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("5")>  _
        Public Property EllipseWidth() As Integer
            Get
                Return CType(Me("EllipseWidth"),Integer)
            End Get
            Set
                Me("EllipseWidth") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Black")>  _
        Public Property BrightenColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("BrightenColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("BrightenColor") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ShowFileNameDialog() As Boolean
            Get
                Return CType(Me("ShowFileNameDialog"),Boolean)
            End Get
            Set
                Me("ShowFileNameDialog") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Black")>  _
        Public Property ArrowColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("ArrowColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("ArrowColor") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property ArrowWidth() As Integer
            Get
                Return CType(Me("ArrowWidth"),Integer)
            End Get
            Set
                Me("ArrowWidth") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AutoCloseLinkViewer() As Boolean
            Get
                Return CType(Me("AutoCloseLinkViewer"),Boolean)
            End Get
            Set
                Me("AutoCloseLinkViewer") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property ChangeAeroMode() As Integer
            Get
                Return CType(Me("ChangeAeroMode"),Integer)
            End Get
            Set
                Me("ChangeAeroMode") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Upgraded() As Boolean
            Get
                Return CType(Me("Upgraded"),Boolean)
            End Get
            Set
                Me("Upgraded") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property UseBoxInsteadOfCirlce() As Boolean
            Get
                Return CType(Me("UseBoxInsteadOfCirlce"),Boolean)
            End Get
            Set
                Me("UseBoxInsteadOfCirlce") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("DirectUpload.net")>  _
        Public Property DefaultImageHoster() As String
            Get
                Return CType(Me("DefaultImageHoster"),String)
            End Get
            Set
                Me("DefaultImageHoster") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("7")>  _
        Public Property BlurFactor() As Integer
            Get
                Return CType(Me("BlurFactor"),Integer)
            End Get
            Set
                Me("BlurFactor") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property EnableLinkViewer() As Boolean
            Get
                Return CType(Me("EnableLinkViewer"),Boolean)
            End Get
            Set
                Me("EnableLinkViewer") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property UserTasksInitialized() As Boolean
            Get
                Return CType(Me("UserTasksInitialized"),Boolean)
            End Get
            Set
                Me("UserTasksInitialized") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property EnableShotEditor() As Boolean
            Get
                Return CType(Me("EnableShotEditor"),Boolean)
            End Get
            Set
                Me("EnableShotEditor") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Nomination1")>  _
        Public Property SelectionDecoration() As Global.HolzShots.ScreenshotRelated.Selection.SelectionDecoration
            Get
                Return CType(Me("SelectionDecoration"),Global.HolzShots.ScreenshotRelated.Selection.SelectionDecoration)
            End Get
            Set
                Me("SelectionDecoration") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property EnableDebugMode() As Boolean
            Get
                Return CType(Me("EnableDebugMode"),Boolean)
            End Get
            Set
                Me("EnableDebugMode") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property IsFirstRun() As Boolean
            Get
                Return CType(Me("IsFirstRun"),Boolean)
            End Get
            Set
                Me("IsFirstRun") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My

    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty

        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.HolzShots.My.MySettings
            Get
                Return Global.HolzShots.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
